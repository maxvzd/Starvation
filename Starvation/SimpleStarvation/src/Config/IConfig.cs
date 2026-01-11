using System.Collections.Generic;

namespace SimpleStarvation.Config;

public interface IConfig
{
    public float HealthyWeight { get; }
    public float CriticalWeight { get; }
    public float MaxWeight { get; }
    public float ExpectedSaturationPerDay { get; }
    public float NumberOfMonthsToStarve { get; }
    public float ThrowUpThreshold { get; }
    public bool ApplyWeightBonuses { get; }
    public float WeightLossOnDeath { get; }
    public float LowestPossibleWeightOnRespawn { get; }
    public float PlayerStartingWeight { get; }
    public IReadOnlyList<Bonus>? WeightBonuses { get; }
    public float StoodStillModifier { get; }
    public float SleepModifier { get; }
    public float SprintModifier { get; }
    public bool AlwaysConsumeFullMeal { get; }
}