using System;
using System.Collections.Generic;
using System.Linq;
using Starvation.Config;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Starvation;

public class EntityBehaviourWeightBonuses(Entity entity) : EntityBehavior(entity)
{
    public override string PropertyName() => BEHAVIOUR_KEY;
    public const string BEHAVIOUR_KEY = "weight-bonuses";
    
    private static SimplyStarvingConfig Config => SimpleStarvationModSystem.Config ?? new MutableConfig().Freeze();
    private static IReadOnlyList<Bonus>? WeightBonuses => Config.WeightBonuses;
    private IReadOnlyList<IGrouping<BonusType, Bonus>>? _distinctBonusTypes;
    private ITreeAttribute? _weightBonusTree;

    public override void OnEntitySpawn()
    {
        base.OnEntitySpawn();
        if (WeightBonuses is null) return;
        _distinctBonusTypes = WeightBonuses.GroupBy(x => x.Type).ToList();
        SetWeightBonuses();
    }

    public override void Initialize(EntityProperties properties, JsonObject attributes)
    {
        base.Initialize(properties, attributes);
        _weightBonusTree = entity.WatchedAttributes.GetTreeAttribute(PropertyName());
        if (_weightBonusTree is null)
        {
            entity.WatchedAttributes.SetAttribute(PropertyName(), _weightBonusTree = new TreeAttribute());
        }
    }

    public void SetWeightBonuses()
    {
        var currentBodyWeightNullable  = entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.BEHAVIOUR_KEY)?.GetFloat("weight");
        
        if (PlayerHelper.IsPlayerInCreative(entity) || !Config.ApplyWeightBonuses || currentBodyWeightNullable is not { } currentBodyWeight)
        {
            ClearBonuses();
            return;
        }

        if (_distinctBonusTypes is null || !_distinctBonusTypes.Any()) return;
        
        var bonusesToSet = new List<Bonus>();
        foreach (var bonusType in _distinctBonusTypes)
        {
            var orderedBonuses = bonusType.OrderBy(x => x.Weight).ToList();
            if (orderedBonuses.Count == 0) continue;
            
            bonusesToSet.Add(GetBonusForWeight(orderedBonuses, currentBodyWeight));
        }
        SetBonuses(bonusesToSet);
    }

    private static Bonus GetBonusForWeight(IReadOnlyList<Bonus> orderedBonuses, float currentBodyWeight)
    {
        if (currentBodyWeight < orderedBonuses[0].Weight)
        {
            var bonusAbove = orderedBonuses[0];
            var bonusBelow = new Bonus(bonusAbove.Type, 0, Config.CriticalWeight);
            return LerpBonus(bonusBelow, bonusAbove, currentBodyWeight);
        }
        
        var bonusAtLastIndex = orderedBonuses[^1];
        if (currentBodyWeight > bonusAtLastIndex.Weight)
        {
            return new Bonus(bonusAtLastIndex.Type, bonusAtLastIndex.Value, currentBodyWeight);
        }

        for (var i = 1; i < orderedBonuses.Count; i++)
        {
            var bonus = orderedBonuses[i];
            if (currentBodyWeight < bonus.Weight)
            {
                return LerpBonus(orderedBonuses[i - 1], bonus, currentBodyWeight);
            }
        }
        
        //Shouldn't ever happen but just incase
        return bonusAtLastIndex;
    }
    
    private static Bonus LerpBonus(Bonus below, Bonus above, float currentBodyWeight)
    {
        if (Math.Abs(below.Weight - above.Weight) < 0.001f) return below;
        
        var lerpFactor = (currentBodyWeight - below.Weight) / (above.Weight - below.Weight);
        lerpFactor = GameMath.Clamp(lerpFactor, 0, 1);
        
        var value = GameMath.Lerp(below.Value, above.Value, lerpFactor);
        return new Bonus(below.Type, value, currentBodyWeight);
    }
    
    private void SetBonuses(IEnumerable<Bonus> bonuses)
    {
        foreach (var bonus in bonuses)
        {
            var key = BonusTypeToKey.GetKey(bonus.Type);
            if (string.IsNullOrEmpty(key)) continue;
            
            entity.Stats.Set(key, "SimpleStarvation", bonus.Value);
            _weightBonusTree?.SetFloat(Enum.GetName(bonus.Type), bonus.Value);
            
            //TODO: Maybe sort this out one day (command pattern)
            if (bonus.Type == BonusType.MaxHealth)
            {
                var healthBehaviour = entity.GetBehavior<EntityBehaviorHealth>();
                healthBehaviour?.UpdateMaxHealth();
            }
        }
        entity.WatchedAttributes.MarkPathDirty(PropertyName());
    }

    private void ClearBonuses()
    {
        foreach (var stat in entity.Stats)
        {
            entity.Stats.Remove(stat.Key, "SimpleStarvation");
        }
        entity.WatchedAttributes.MarkPathDirty(PropertyName());
    }
}