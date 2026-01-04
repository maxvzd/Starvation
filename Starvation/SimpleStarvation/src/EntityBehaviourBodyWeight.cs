using System;
using System.Collections.Generic;
using System.Linq;
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

    private readonly IReadOnlyList<Tuple<float, WeightModifiers>> _weightBonuses = new List<Tuple<float, WeightModifiers>>
    {
        new(50, new WeightModifiers(-0.5f, -0.15f, -3f, -0.2f, -0.2f)),
        new(60, new WeightModifiers(-0.3f, -0.05f, -2f, -0.1f, -0.1f)),
        new(70, new WeightModifiers(.1f, .05f, 0f, 0.05f, 0.05f)),
        new(80, new WeightModifiers(.25f, .1f, 2f, 0.1f, 0.1f)),
        new(90, new WeightModifiers(-0.1f, 0f, 3f, 0.05f, 0f))
    };
    
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
        SetWeightBonuses();
            
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
    
    public float SetBodyWeight(float weight)
    {
        weight = Math.Min(weight, Config.MaxWeight);
        weight = Math.Max(weight, Config.CriticalWeight);

        var satForWeight = GetSatForWeight(weight);
        StoredSaturation = satForWeight;
        return weight;
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

    private void SetWeightBonuses()
    {
        if (BodyWeight is not { } bodyWeight)
            return;
        
        var difference = float.MaxValue;
        var closestIndex = 0;
        
        for (var i = 0; i < _weightBonuses.Count; i++)
        {
            var currentDifference = Math.Abs(bodyWeight - _weightBonuses[i].Item1);
            if (currentDifference < difference)
            {
                closestIndex = i;
                difference = currentDifference;
            }
        }

        var bonus = _weightBonuses[closestIndex].Item2;
        entity.Stats.Set("walkspeed", "SimplyStarving", bonus.WalkSpeed);
        entity.Stats.Set("maxhealthExtraPoints", "SimplyStarving", bonus.MaxHealth);
        entity.Stats.Set("meleeWeaponsDamage", "SimplyStarving", bonus.MeleeDamage);
        entity.Stats.Set("rangedWeaponsDamage", "SimplyStarving", bonus.RangedDamage);
        entity.Stats.Set("miningSpeedMul", "SimplyStarving", bonus.MiningSpeed);
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
        SetWeightBonuses();
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
            GravityEffect = .5f,

            MinSize = 1f,
            MaxSize = 1.5f,

            ParticleModel = EnumParticleModel.Cube,
            SelfPropelled = false,

            Color = ColorUtil.ToRgba(255, 181, 230, 29)
        };
        entity.World.SpawnParticles(particleProps);
    }

    private readonly struct WeightModifiers(float walkSpeed, float miningSpeed, float maxHealth, float meleeDamage, float rangedDamage)
    {
        public float WalkSpeed { get; } = walkSpeed;
        public float MiningSpeed { get; } = miningSpeed;
        public float MaxHealth { get; } = maxHealth;
        public float MeleeDamage { get; } = meleeDamage;
        public float RangedDamage { get; } = rangedDamage;
    }
}