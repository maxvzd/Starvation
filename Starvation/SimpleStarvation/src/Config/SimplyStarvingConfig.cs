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
            // new(40, new Bonus(-0.5f, -0.2f, -5f, -0.3f, -0.3f)), 
            // new(50, new Bonus(-0.3f, -0.15f, -3f, -0.2f, -0.2f)), 
            // new(60, new Bonus(-0.1f, -0.05f, -2f, -0.1f, -0.1f)), 
            // new(80, new Bonus(.25f, .1f, 3f, 0.1f, 0.1f)),
            // new(90, new Bonus(-0.1f, 0f, 2f, 0.05f, 0f)),
            // new(100, new Bonus(-0.15f, 0f, 1f, 0.05f, 0f)) // Damn boi he thicc
            
            new(BonusType.WalkSpeed, 1, 70),
            new(BonusType.WalkSpeed, 0, 90),
            new(BonusType.MiningSpeed, 1, 80),
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