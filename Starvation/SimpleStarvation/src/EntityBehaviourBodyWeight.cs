using Starvation.Config;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Starvation;

public class EntityBehaviourBodyWeight(Entity entity) : EntityBehavior(entity)
{
    private float _saturationLastTick = 0f;
    private float _hourAtLastTick = 0f;
    private float _hungerTick = 0f;
    private ITreeAttribute? _bodyWeightTree;
    private static SimpleStarvationConfig Config => SimpleStarvationModSystem.Config ?? new SimpleStarvationConfig();
    private float WeightToSaturationScale =>  AmountOfSatToStarve / (Config.HealthyWeight - Config.CriticalWeight);
    private float AmountOfSatToStarve => Config.ExpectedSaturationPerDay * entity.World.Calendar.DaysPerMonth * Config.NumberOfMonthsToStarve;
    
    public override string PropertyName() => ENTITY_KEY;
    public const string ENTITY_KEY = "body-weight";

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
        base.Initialize(properties, attributes);

        var hungerBehaviour = entity.GetBehavior<EntityBehaviorHunger>();
        if (hungerBehaviour is not null)
        {
            _saturationLastTick = hungerBehaviour.Saturation;
        }
        _hourAtLastTick = entity.World.Calendar.HourOfDay;

        _bodyWeightTree = entity.WatchedAttributes.GetTreeAttribute(PropertyName());
        if (_bodyWeightTree is null)
        {
            entity.WatchedAttributes.SetAttribute(PropertyName(), _bodyWeightTree = new TreeAttribute());
            const float fullStomachSatOnSpawnIn = 1500f;
            StoredSaturation = GetSatForWeight(60) - fullStomachSatOnSpawnIn;
        }
    }

    public override void OnGameTick(float deltaTime)
    {
        if (entity.World.Side != EnumAppSide.Server) return;
        
        _hungerTick += deltaTime;
        if (_hungerTick < 10) return;

        if (entity is EntityPlayer player 
            && player.World.PlayerByUid(player.PlayerUID).WorldData.CurrentGameMode is not EnumGameMode.Survival) return;
        
        var hungerBehaviour = entity.GetBehavior<EntityBehaviorHunger>();
        if (hungerBehaviour is null) return;

        DigestFood(hungerBehaviour);
        MetaboliseFoodStores();
            
        _hourAtLastTick = entity.World.Calendar.HourOfDay;
        _saturationLastTick = hungerBehaviour.Saturation;
        _hungerTick = 0f;

        if (BodyWeight < Config.CriticalWeight)
        {
            entity.ReceiveDamage(new DamageSource
            {
                Source = EnumDamageSource.Internal,
                Type = EnumDamageType.Hunger
            }, 2.5f); //A quick painful death 
        }
    }

    private void DigestFood(EntityBehaviorHunger hungerBehaviour)
    {
        var satDiff = _saturationLastTick - hungerBehaviour.Saturation;
        if (satDiff > 0)
        {
            StoredSaturation += satDiff;
        }
        //entity.World.Logger.Debug($"Digesting: _saturationLastTick: {_saturationLastTick}, currSat: {hungerBehaviour.Saturation}, satDiff: {satDiff}, StoredSaturation: {StoredSaturation} BodyWeight: {BodyWeight}");
    }

    private void MetaboliseFoodStores()
    {
        var hungerRate = entity.Stats.GetBlended("hungerrate");
            
        var hoursPerDay = entity.World.Calendar.HoursPerDay;
        var lossPerHour = Config.ExpectedSaturationPerDay / hoursPerDay * hungerRate * GlobalConstants.HungerSpeedModifier;

        var hourDiff = CalculateTimeSinceHungerLastChecked();

        StoredSaturation -= lossPerHour * hourDiff;
            
        //var currentHour = entity.World.Calendar.HourOfDay;
        //entity.World.Logger.Debug($"Metabolising: currentHour: {currentHour}, hourLastTick: {_hourAtLastTick}, lossPerHour:{lossPerHour}, hourDIff {hourDiff}, loss: {lossPerHour * hourDiff}");
    }

    private float CalculateTimeSinceHungerLastChecked()
    {
        var hoursPerDay = entity.World.Calendar.HoursPerDay;
        var currentHour = entity.World.Calendar.HourOfDay;
        if (currentHour < _hourAtLastTick)
        {
            currentHour += hoursPerDay;
        }
        return currentHour - _hourAtLastTick;
    }
    
    //Half current body-weight reserves when revived.
    public override void OnEntityReceiveDamage(DamageSource damageSource, ref float damage)
    {
        if (damageSource is not { Type: EnumDamageType.Heal, Source: EnumDamageSource.Revive }) return;
        
        var newSaturation = StoredSaturation / 2;
        var satAt50Kg = GetSatForWeight(50);
        if (newSaturation < satAt50Kg)
        {
            newSaturation = satAt50Kg;
        }
            
        StoredSaturation = newSaturation;
    }
    
    public void CheckForThrowUp()
    {
        var hunger = entity.GetBehavior<EntityBehaviorHunger>();
        if (hunger is null) return;

        if (hunger.Saturation < hunger.MaxSaturation + Config.ThrowUpThreshold) return;
        
        CreateThrowUpParticles();
        
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
    }
    
    private float GetSatForWeight(float weightInKg)
    {
        var weightDiff = weightInKg - Config.CriticalWeight;

        return weightDiff * WeightToSaturationScale;
    }

    private void CreateThrowUpParticles()
    {
        var pos = entity.Pos.AheadCopy(0.4f).XYZ.Add(entity.LocalEyePos);
        pos.Y -= 0.4f;
            
        var particleProps = new SimpleParticleProperties
        {
            MinQuantity = 20,
            AddQuantity = 5,

            MinPos = pos,
            AddPos = new Vec3d(0.1f, 0.1f, 0.1f),

            MinVelocity = new Vec3f(-0.2f, 0.7f, -0.2f),
            AddVelocity = new Vec3f(-.5f, 1f, 0.5f),

            LifeLength = 5f,
            GravityEffect = .5f,

            MinSize = 1f,
            MaxSize = 1.5f,

            ParticleModel = EnumParticleModel.Cube,
            SelfPropelled = false,

            Color = ColorUtil.ToRgba(255, 181, 230, 29)
        };
        entity.World.SpawnParticles(particleProps);
    }
}