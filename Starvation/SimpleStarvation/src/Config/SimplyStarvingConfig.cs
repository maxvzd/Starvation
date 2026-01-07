using System.Collections.Generic;

namespace Starvation.Config;

public class SimplyStarvingConfig
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

    public float HealthyWeight => _config.HealthyWeight;
    public float CriticalWeight => _config.CriticalWeight;
    public float MaxWeight => _config.MaxWeight;
    public float ExpectedSaturationPerDay => _config.ExpectedSaturationPerDay;
    public float NumberOfMonthsToStarve => _config.NumberOfMonthsToStarve;
    public float ThrowUpThreshold => _config.ThrowUpThreshold;
    public bool ApplyWeightBonuses => _config.ApplyWeightBonuses;
    public float WeightLossOnDeath => _config.WeightLossOnDeath;
    public float LowestPossibleWeightOnRespawn => _config.LowestPossibleWeightOnRespawn;
    public float PlayerStartingWeight => _config.PlayerStartingWeight;
    public IReadOnlyList<Bonus>? WeightBonuses => _config.WeightBonuses;
}