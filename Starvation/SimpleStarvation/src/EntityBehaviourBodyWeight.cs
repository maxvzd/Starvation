using SimpleStarvation.Config;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace SimpleStarvation;

public class EntityBehaviourBodyWeight(Entity entity) : EntityBehavior(entity)
{
    private float _saturationLastTick = 0f;
    private double _hourAtLastHungerTick = 0f;
    private double _hourAtLastTick = 0f;
    private float _hungerTick = 0f;
    private ITreeAttribute? _bodyWeightTree;
    
    private double _timePlayerSpentSleeping;
    private long _timePlayerLastMoved;
    private double _timePlayerStoodStandingStill;
    private double _timePlayerSpentSprinting;

    private static SimplyStarvingConfig Config => SimpleStarvationModSystem.Config ?? new MutableConfig().Freeze();
    private float WeightToSaturationScale =>  AmountOfSatToStarve / (Config.HealthyWeight - Config.CriticalWeight);
    private float AmountOfSatToStarve => Config.ExpectedSaturationPerDay * entity.World.Calendar.DaysPerMonth * Config.NumberOfMonthsToStarve;
    
    public override string PropertyName() => BEHAVIOUR_KEY;
    public const string BEHAVIOUR_KEY = "body-weight";

    public float? BodyWeight
    {
        get => _bodyWeightTree?.GetFloat("weight");
        private set
        {
            _bodyWeightTree?.SetFloat("weight", value ?? 0);
            entity.WatchedAttributes.MarkPathDirty(PropertyName());
        }
    }

    public float? StoredSaturation
    {
        get => _bodyWeightTree?.GetFloat("stored-sat");
        private set
        {
            _bodyWeightTree?.SetFloat("stored-sat", value ?? 0);
            UpdateBodyWeight();
            entity.WatchedAttributes.MarkPathDirty(PropertyName());
        }
    }

    public override void Initialize(EntityProperties properties, JsonObject attributes)
    {
        if (entity.World.Side != EnumAppSide.Server)
        {
            entity.RemoveBehavior(this);
            return;
        }
        
        base.Initialize(properties, attributes);
        var hungerBehaviour = entity.GetBehavior<EntityBehaviorHunger>();
        if (hungerBehaviour is not null)
        {
            _saturationLastTick = hungerBehaviour.Saturation;
        }
        
        _bodyWeightTree = entity.WatchedAttributes.GetTreeAttribute(PropertyName());
        if (_bodyWeightTree is null)
        {
            entity.WatchedAttributes.SetAttribute(PropertyName(), _bodyWeightTree = new TreeAttribute());
            StoredSaturation = GetSatForWeight(Config.PlayerStartingWeight);
        }
    }

    public override void OnGameTick(float deltaTime)
    {
        if (entity.World.Side != EnumAppSide.Server || PlayerHelper.IsPlayerInCreative(entity)) return;

        TrackPlayerCurrentActions();
        
        _hungerTick += deltaTime;
        if (_hungerTick < 10) return;
       
        var hungerBehaviour = entity.GetBehavior<EntityBehaviorHunger>();
        if (hungerBehaviour is null) return;

        DigestFood(hungerBehaviour);
        MetaboliseFoodStores();
            
        _saturationLastTick = hungerBehaviour.Saturation;
        ResetTicks();

        if (BodyWeight < Config.CriticalWeight && Config.ApplyFatalDamageOnCriticalWeight)
        {
            entity.ReceiveDamage(new DamageSource
            {
                Source = EnumDamageSource.Internal,
                Type = EnumDamageType.Hunger
            }, 2.5f); //A quick painful death 
        }
    }
    
    public float SetBodyWeight(float weight)
    {
        weight = float.Clamp(weight, Config.CriticalWeight, Config.MaxWeight);
        var satForWeight = GetSatForWeight(weight);
        StoredSaturation = satForWeight;
        return weight;
    }
    
    public void ResetTicks()
    {
        var now = entity.World.Calendar.TotalHours;
        
        _hourAtLastTick = now;
        _hungerTick = 0f;
        _hourAtLastHungerTick = now;
        _timePlayerSpentSleeping = 0;
        _timePlayerStoodStandingStill = 0f;
        _timePlayerSpentSprinting = 0f;
    }

    private void DigestFood(EntityBehaviorHunger hungerBehaviour)
    {
        var satDiff = _saturationLastTick - hungerBehaviour.Saturation; 
        StoredSaturation += float.Max(0, satDiff);
        
        //entity.World.Logger.Debug($"Digesting: _saturationLastTick: {_saturationLastTick}, currSat: {hungerBehaviour.Saturation}, StoredSaturation: {StoredSaturation} BodyWeight: {BodyWeight}, Gain: {satDiff}");
    }

    private void MetaboliseFoodStores()
    {
        if (StoredSaturation is null) return;
        
        var hungerRate = entity.Stats.GetBlended(BonusTypeToKey.GetKey(BonusType.HungerRate));
        
        var hoursPerDay = entity.World.Calendar.HoursPerDay;
        var lossPerHour = Config.ExpectedSaturationPerDay / hoursPerDay * hungerRate * GlobalConstants.HungerSpeedModifier;
        var hourDiff = CalculateTimeSinceHungerLastChecked();

        var timeAsleep = double.Min(_timePlayerSpentSleeping, hourDiff);
        var timeAwake = hourDiff - timeAsleep;
        
        var timeActive = timeAwake - _timePlayerStoodStandingStill;

        var lossDotJpeg = (float)((timeActive * lossPerHour) 
                                  + (timeAsleep * lossPerHour * Config.SleepModifier) 
                                  //Emulate vanilla logic of less digestion when stood still
                                  + (_timePlayerStoodStandingStill * lossPerHour * Config.StoodStillModifier)
                                  //This one just adds extra and isn't a ratio of stood/sleep/awake 
                                  + (_timePlayerSpentSprinting * lossPerHour * Config.SprintModifier));
        
        StoredSaturation = float.Max(0, (float)StoredSaturation - lossDotJpeg);
            
        // entity.World.Logger.Debug($"Metabolising: currentHour: {entity.World.Calendar.TotalHours}, " +
        //                           $"hourLastTick: {_hourAtLastHungerTick}, " +
        //                           $"hourDiff {hourDiff}, " +
        //                           $"lossPerHour:{lossPerHour}, " +
        //                           $"Loss: {lossDotJpeg}");
        // entity.World.Logger.Debug($"hourdiff: {hourDiff}, sleep:{timeAsleep}, active:{timeActive}, stoodStill:{_timePlayerStoodStandingStill}");
    }
    
    private void TrackPlayerCurrentActions()
    {
        if (entity is not EntityPlayer player) return;
        
        var now = entity.World.Calendar.TotalHours;
        var deltaHour = now - _hourAtLastTick;
        var playerIsSleeping = player.MountedOn is BlockEntityBed;
        
        if (playerIsSleeping)
        {
            _timePlayerSpentSleeping += deltaHour;
        }

        var playerIsCurrentlyMoving = player.Controls.TriesToMove || player.Controls.Jump || player.Controls.LeftMouseDown || player.Controls.RightMouseDown;
        if (playerIsCurrentlyMoving)
        {
            _timePlayerLastMoved = entity.World.ElapsedMilliseconds;
        }
        
        // Emulate vanilla logic to slow down weight loss when stood still
        var isStandingStill = !playerIsSleeping && entity.World.ElapsedMilliseconds - _timePlayerLastMoved > 3000;
        if (isStandingStill)
        {
            _timePlayerStoodStandingStill += deltaHour;
        }

        if (player.Controls.Sprint)
        {
            _timePlayerSpentSprinting += deltaHour;
        }
        _hourAtLastTick = now;
    }
    
    private double CalculateTimeSinceHungerLastChecked()
    {
        var now = entity.World.Calendar.TotalHours;
        var diff = now - _hourAtLastHungerTick;
        return diff;
    }

    public override void OnEntitySpawn()
    {
        base.OnEntitySpawn();
        ResetTicks();
    }

    public override void OnEntityReceiveDamage(DamageSource damageSource, ref float damage)
    {
        if (damageSource is not { Type: EnumDamageType.Heal, Source: EnumDamageSource.Revive }) return;
        if (StoredSaturation is null) return;
        if (BodyWeight is not { } currentBodyWeight) return;

        var percentageLost = Config.WeightLossOnDeath / 100;
        percentageLost = float.Clamp(percentageLost, 0, 1);
        
        var newSaturation = GetSatForWeight(currentBodyWeight - (currentBodyWeight * percentageLost));
        var satAtLowestWeightPossible = GetSatForWeight(Config.LowestPossibleWeightOnRespawn);

        newSaturation = float.Max(satAtLowestWeightPossible, newSaturation);
            
        StoredSaturation = newSaturation;
    }
    
    public void CheckForThrowUp()
    {
        var hunger = entity.GetBehavior<EntityBehaviorHunger>();
        if (hunger is null) return;

        if (hunger.Saturation < hunger.MaxSaturation + Config.ThrowUpThreshold) return;
        
        CreateThrowUpParticles();
        _saturationLastTick = 0;
        
        //Apply effect?
        hunger.Saturation = 0f;
        entity.ReceiveDamage(new DamageSource
        {
            Source = EnumDamageSource.Internal, 
            Type = EnumDamageType.Hunger
        }, 0.5f);
    }
    
    private void UpdateBodyWeight()
    {
        BodyWeight = Config.CriticalWeight + StoredSaturation / WeightToSaturationScale;
        entity.GetBehavior<EntityBehaviourWeightBonuses>()?.SetWeightBonuses();
    }
    
    private float GetSatForWeight(float weightInKg)
    {
        var weightDiff = weightInKg - Config.CriticalWeight;

        return weightDiff * WeightToSaturationScale;
    }

    //AI did this I couldn't work it out so don't ask me to explain it
    private void CreateThrowUpParticles()
    {
        // Use the engine-provided view vector to avoid yaw/pitch sign convention issues.
        // This should point "where the entity looks".
        var forward = entity.SidedPos.GetViewVector().Clone();
        if (forward.Length() < 1e-4f)
        {
            forward = new Vec3f(0, 0, 1);
        }
        forward.Normalize();

        // Eye/mouth-ish position
        var eyePos = entity.Pos.XYZ.Add(0, entity.LocalEyePos.Y, 0);

        // Start point slightly in front of face and slightly down.
        var spawnPos = eyePos.Add(forward.X * 0.35f, forward.Y * 0.35f - 0.10f, forward.Z * 0.35f);

        // Build a local orthonormal basis: right/up around forward.
        // (Handle looking almost straight up/down.)
        var worldUp = new Vec3f(0, 1, 0);
        var right = forward.Cross(worldUp);
        if (right.Length() < 1e-4f)
        {
            // forward ~ parallel to worldUp; choose an arbitrary alternative axis
            right = forward.Cross(new Vec3f(1, 0, 0));
        }
        right.Normalize();

        var up = right.Cross(forward);
        up.Normalize();

        // Cone parameters
        const float speed = 3f;
        const float spreadSide = 1.5f; // lateral spread magnitude
        const float spreadUp = 1.0f; // vertical spread magnitude

        // We can't sample per-particle randomness directly here, but we can still define a symmetric range.
        // Velocity is chosen within [MinVelocity, MinVelocity + AddVelocity] per particle.
        var baseVel = forward * speed;

        // Symmetric ranges around baseVel in the right/up directions.
        var minVel = baseVel - right * spreadSide - up * spreadUp;
        var addVel = right * (spreadSide * 2f) + up * (spreadUp * 2f);
        
        var particleProps = new SimpleParticleProperties
        {
            MinQuantity = 25,
            AddQuantity = 10,

            MinPos = spawnPos,
            AddPos = new Vec3d(0.05f, 0.05f, 0.05f),

            // "Spray" spread around the forward direction
            MinVelocity = minVel,
            AddVelocity = addVel,

            LifeLength = 5f,
            GravityEffect = .8f,

            MinSize = 1f,
            MaxSize = 1.5f,

            ParticleModel = EnumParticleModel.Cube,
            SelfPropelled = false,

            Color = ColorUtil.ToRgba(255, 181, 230, 29)
        };
        entity.World.SpawnParticles(particleProps);
    }
}