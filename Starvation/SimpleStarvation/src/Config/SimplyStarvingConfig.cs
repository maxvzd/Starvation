using System.Collections.Generic;

namespace SimpleStarvation.Config;

public class SimplyStarvingConfig : IConfig
{
    private readonly MutableConfig _config; 

    public SimplyStarvingConfig(MutableConfig config)
    {
        _config = config;
        config.WeightBonuses ??= new List<Bonus>
        {
            new(BonusType.WalkSpeed, -0.3f, 40),
            new(BonusType.WalkSpeed, 0f, 60),
            new(BonusType.WalkSpeed, 0.2f, 70),
            new(BonusType.WalkSpeed, -0.1f, 100),
            
            new(BonusType.MiningSpeed, -0.25f, 40),
            new(BonusType.MiningSpeed, 0f, 60),
            new(BonusType.MiningSpeed, .3f, 100),
            
            new(BonusType.MaxHealth, -3f, 40),
            new(BonusType.MaxHealth, 0f, 60),
            new(BonusType.MaxHealth, 5f, 70),
            new(BonusType.MaxHealth, 3f, 100),
            
            new(BonusType.MeleeDamage, -0.5f, 40),
            new(BonusType.MeleeDamage, 0f, 60),
            new(BonusType.MeleeDamage, 0.3f, 100),
            
            new(BonusType.RangedDamage, -0.3f, 40),
            new(BonusType.RangedDamage, 0f, 60),
            new(BonusType.RangedDamage, 0.3f, 80),
            new(BonusType.RangedDamage, 0.1f, 100),
            
            new(BonusType.RangeWeaponAccuracy, -0.6f, 40),
            new(BonusType.RangeWeaponAccuracy, 0.1f, 60),
            new(BonusType.RangeWeaponAccuracy, 0.3f, 80),
            new(BonusType.RangeWeaponAccuracy, 0.1f, 100),
            
            new(BonusType.RustyGearDropRate, .3f, 40),
            new(BonusType.RustyGearDropRate, 0, 60),
            
            new(BonusType.AnimalSeekingRange, -0.3f, 40),
            new(BonusType.AnimalSeekingRange, 0f, 80),
            new(BonusType.AnimalSeekingRange, 0.3f, 100),
            
            new(BonusType.BowDrawStrength, -0.3f, 40),
            new(BonusType.BowDrawStrength, -0.2f, 60),
            new(BonusType.BowDrawStrength, 0.2f, 100),
            
            new(BonusType.GliderLiftMax, 0.3f, 40),
            new(BonusType.GliderLiftMax, 0f, 70),
            new(BonusType.GliderLiftMax, -0.5f, 100),
            
            new(BonusType.GliderSpeedMax, 0.3f, 40),
            new(BonusType.GliderSpeedMax, 0f, 100)
        };
    }

    public float HealthyWeight => float.Clamp(_config.HealthyWeight, CriticalWeight, MaxWeight);
    public float CriticalWeight => float.Max(_config.CriticalWeight, 0);
    public float MaxWeight => _config.MaxWeight;
    public float ExpectedSaturationPerDay => _config.ExpectedSaturationPerDay;
    public float NumberOfMonthsToStarve => _config.NumberOfMonthsToStarve;
    public float ThrowUpThreshold => float.Max(_config.ThrowUpThreshold, 0);
    public bool ApplyWeightBonuses => _config.ApplyWeightBonuses;
    public float WeightLossOnDeath => float.Clamp(_config.WeightLossOnDeath, 0, 100);
    public float LowestPossibleWeightOnRespawn => float.Clamp(_config.LowestPossibleWeightOnRespawn, CriticalWeight, MaxWeight);
    public float PlayerStartingWeight => float.Clamp(_config.PlayerStartingWeight, CriticalWeight, MaxWeight);
    public IReadOnlyList<Bonus>? WeightBonuses => _config.WeightBonuses;
    public float StoodStillModifier => float.Clamp(_config.StoodStillModifier, 0, 1);
    public float SleepModifier => float.Clamp(_config.SleepModifier, 0 ,1);
    public float SprintModifier => float.Clamp(_config.SprintModifier, 0 ,1);
    public bool AlwaysConsumeFullMeal => _config.AlwaysConsumeFullMeal;
    public bool ApplyFatalDamageOnCriticalWeight => _config.ApplyFatalDamageOnCriticalWeight;
    public float AverageGainCheckWindowInHours => float.Clamp(_config.AverageGainCheckWindowInHours, 0, 24);
    public float AverageGainCheckFrequencyInHours => float.Clamp(_config.AverageGainCheckFrequencyInHours, .5f, AverageGainCheckWindowInHours);
}