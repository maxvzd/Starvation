using System;
using System.Collections.Generic;
using Starvation.Config;
using Vintagestory.API.Common.Entities;

namespace Starvation;

public class EntityBehaviourWeightBonuses(Entity entity) : EntityBehavior(entity)
{
    public override string PropertyName() => BEHAVIOUR_KEY;
    public const string BEHAVIOUR_KEY = "weight-bonuses";
    private int StepCountForWeights => _weightBonuses.Count - 1;
    
    private static SimpleStarvationConfig Config => SimpleStarvationModSystem.Config ?? new SimpleStarvationConfig();
    private readonly IReadOnlyList<Tuple<int, WeightBonus>> _weightBonuses = new List<Tuple<int, WeightBonus>>
    {
        new(0, new WeightBonus(-0.5f, -0.2f, -5f, -0.3f, -0.3f)), //40kg for default values
        new(1, new WeightBonus(-0.3f, -0.15f, -3f, -0.2f, -0.2f)), //50kg for default values
        new(2, new WeightBonus(-0.1f, -0.05f, -2f, -0.1f, -0.1f)), //..etc, etc
        new(3, new WeightBonus(.1f, .05f, 0f, 0.05f, 0.05f)),
        new(4, new WeightBonus(.25f, .1f, 3f, 0.1f, 0.1f)),
        new(5, new WeightBonus(-0.1f, 0f, 2f, 0.05f, 0f)),
        new(6, new WeightBonus(-0.15f, 0f, 1f, 0.05f, 0f)) //100kg for default values (Oh boi he thicc)
    };
    
    public void SetWeightBonuses()
    {
        if (!PlayerHelper.IsPlayerInSurvival(entity) || !Config.ApplyWeightBonuses)
        {
            SetBonuses(new WeightBonus(0, 0, 0, 0, 0));
            return;
        }

        var bodyWeight = entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.BEHAVIOUR_KEY).GetFloat("weight");
        
        var difference = float.MaxValue;
        var closestIndex = 0;
        
        for (var i = 0; i < _weightBonuses.Count; i++)
        {
            var bonusWeight = GetWeightForBonus(_weightBonuses[i].Item1);
            
            var currentDifference = Math.Abs(bodyWeight - bonusWeight);
            if (currentDifference > difference) continue;
            
            closestIndex = i;
            difference = currentDifference;
        }

        var bonus = _weightBonuses[closestIndex].Item2; 
        SetBonuses(bonus);
    }
    
    private float GetWeightForBonus(int index)
    {
        var criticalToMaxDifference = Config.MaxWeight - Config.CriticalWeight;
        var stepInKg = criticalToMaxDifference / StepCountForWeights;

        return Config.CriticalWeight + stepInKg * index;
    }

    private void SetBonuses(WeightBonus bonus)
    {
        entity.Stats.Set("walkspeed", "SimpleStarvation", bonus.WalkSpeed);
        entity.Stats.Set("maxhealthExtraPoints", "SimpleStarvation", bonus.MaxHealth);
        entity.Stats.Set("meleeWeaponsDamage", "SimpleStarvation", bonus.MeleeDamage);
        entity.Stats.Set("rangedWeaponsDamage", "SimpleStarvation", bonus.RangedDamage);
        entity.Stats.Set("miningSpeedMul", "SimpleStarvation", bonus.MiningSpeed);
    }
    
    private readonly struct WeightBonus(float walkSpeed, float miningSpeed, float maxHealth, float meleeDamage, float rangedDamage)
    {
        public float WalkSpeed { get; } = walkSpeed;
        public float MiningSpeed { get; } = miningSpeed;
        public float MaxHealth { get; } = maxHealth;
        public float MeleeDamage { get; } = meleeDamage;
        public float RangedDamage { get; } = rangedDamage;
    }
}