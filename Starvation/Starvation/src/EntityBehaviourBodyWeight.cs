using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace Starvation;

public class EntityBehaviourBodyWeight(Entity entity) : EntityBehavior(entity)
{
    private float _saturationLastTick = 0f;
    private float _hourAtLastTick = 0f;
    private float _hungerTick = 0f;
    private ITreeAttribute? _bodyWeightTree;
    
    //A regular weight for a regular man
    private const float HEALTHY_WEIGHT = 75f;
    //Roughly how low you'd have to be to feel organ failure
    private const float CRITICAL_WEIGHT = 40f;
    //Roughly how much you'd expect a player to eat in a day to maintain weight
    private const float EXPECTED_SATURATION_PER_DAY = 4000f;
    //How long you would starve from a healthy weight of 75kg
    private const float NUMBER_OF_MONTHS_TO_STARVE = 1.5f;
    
    private float WeightScaling =>  AmountOfSatToStarve / (HEALTHY_WEIGHT - CRITICAL_WEIGHT);
    private float AmountOfSatToStarve => EXPECTED_SATURATION_PER_DAY * entity.World.Calendar.DaysPerMonth * NUMBER_OF_MONTHS_TO_STARVE;

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
    }

    private void DigestFood(EntityBehaviorHunger hungerBehaviour)
    {
        var satDiff = _saturationLastTick - hungerBehaviour.Saturation;
        if (satDiff > 0)
        {
            StoredSaturation += satDiff;
        }
        Globals.CoreApiInstance?.Logger.Debug($"Digesting: _saturationLastTick: {_saturationLastTick}, currSat: {hungerBehaviour.Saturation}, satDiff: {satDiff}, StoredSaturation: {StoredSaturation} BodyWeight: {BodyWeight}");
    }

    private void MetaboliseFoodStores()
    {
        var calculatedHungerRate = entity.Stats.GetBlended("hungerrate");
            
        var hoursPerDay = entity.World.Calendar.HoursPerDay;
        var lossPerHour = EXPECTED_SATURATION_PER_DAY / hoursPerDay * calculatedHungerRate;

        var hourDiff = CalculateTimeSinceHungerLastChecked();

        StoredSaturation -= lossPerHour * hourDiff;
            
        var currentHour = entity.World.Calendar.HourOfDay;
        Globals.CoreApiInstance?.Logger.Debug($"Metabolising: currentHour: {currentHour}, hourLastTick: {_hourAtLastTick}, lossPerHour:{lossPerHour}, hourDIff {hourDiff}, loss: {lossPerHour * hourDiff}");
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
    
    private void UpdateBodyWeight()
    {
        BodyWeight = CRITICAL_WEIGHT + StoredSaturation / WeightScaling;
    }
    
    private float GetSatForWeight(float weightInKg)
    {
        var weightDiff = weightInKg - CRITICAL_WEIGHT;

        return weightDiff * AmountOfSatToStarve / (HEALTHY_WEIGHT - CRITICAL_WEIGHT);
    }
}