using System;
using System.Collections.Generic;
using Starvation.Config;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace Starvation;

public class EntityBehaviourWeightBonuses(Entity entity) : EntityBehavior(entity)
{
    public override string PropertyName() => BEHAVIOUR_KEY;
    public const string BEHAVIOUR_KEY = "weight-bonuses";
    
    private static SimpleStarvationConfig Config => SimpleStarvationModSystem.Config ?? new SimpleStarvationConfig();
    private IReadOnlyList<WeightBonus> WeightBonuses => Config.WeightBonuses;
    
    public void SetWeightBonuses()
    {
        if (!PlayerHelper.IsPlayerInSurvival(entity) || !Config.ApplyWeightBonuses)
        {
            SetBonuses(new Bonus(0, 0, 0, 0, 0));
            return;
        }

        var bodyWeight = entity.WatchedAttributes.GetTreeAttribute(EntityBehaviourBodyWeight.BEHAVIOUR_KEY).GetFloat("weight");
        
        var difference = float.MaxValue;
        var closestIndex = 0;
        
        for (var i = 0; i < WeightBonuses.Count; i++)
        {
            var bonusWeight = WeightBonuses[i].Weight;
            
            var currentDifference = Math.Abs(bodyWeight - bonusWeight);
            if (currentDifference > difference) continue;
            
            closestIndex = i;
            difference = currentDifference;
        }
        
        var closestWeightBonus = WeightBonuses[closestIndex];
        var closestBonus = closestWeightBonus.Bonus;
        if (Math.Abs(closestWeightBonus.Weight - bodyWeight) < 0.01f || closestIndex == 0 || closestIndex == WeightBonuses.Count - 1) //If bonusweight is exactly bodyweight then just set bonus
        {
        }
        else if (closestWeightBonus.Weight > bodyWeight)
        {
            closestBonus = LerpBetweenWeightBonus( WeightBonuses[closestIndex - 1], WeightBonuses[closestIndex], bodyWeight);
        }
        else if (closestWeightBonus.Weight < bodyWeight)
        {
            closestBonus = LerpBetweenWeightBonus( WeightBonuses[closestIndex], WeightBonuses[closestIndex + 1], bodyWeight);
        }

        SetBonuses(closestBonus);
    }

    private Bonus LerpBetweenWeightBonus(WeightBonus below, WeightBonus above, float bodyWeight)
    {
        var lerpFactor = (bodyWeight - below.Weight) / (above.Weight - below.Weight);

        var walkSpeed = GameMath.Lerp(below.Bonus.WalkSpeed, above.Bonus.WalkSpeed, lerpFactor);
        var miningSpeed = GameMath.Lerp(below.Bonus.MiningSpeed, above.Bonus.MiningSpeed, lerpFactor);
        var meleeDamage = GameMath.Lerp(below.Bonus.MeleeDamage, above.Bonus.MeleeDamage, lerpFactor);
        var rangeDamage = GameMath.Lerp(below.Bonus.RangedDamage, above.Bonus.RangedDamage, lerpFactor);
        var maxHealth = GameMath.Lerp(below.Bonus.MaxHealth, above.Bonus.MaxHealth, lerpFactor);

        return new Bonus(walkSpeed, miningSpeed, maxHealth, meleeDamage, rangeDamage);
    }

    private void SetBonuses(Bonus bonus)
    {
        entity.Stats.Set("walkspeed", "SimpleStarvation", bonus.WalkSpeed);
        entity.Stats.Set("maxhealthExtraPoints", "SimpleStarvation", bonus.MaxHealth);
        entity.Stats.Set("meleeWeaponsDamage", "SimpleStarvation", bonus.MeleeDamage);
        entity.Stats.Set("rangedWeaponsDamage", "SimpleStarvation", bonus.RangedDamage);
        entity.Stats.Set("miningSpeedMul", "SimpleStarvation", bonus.MiningSpeed);
    }
}