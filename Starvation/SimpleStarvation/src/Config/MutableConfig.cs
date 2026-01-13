using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SimpleStarvation.Config;

public class MutableConfig : IConfig
{
    /// <summary>
    /// A regular weight for a regular man
    /// </summary>
    [JsonInclude] public float HealthyWeight { get; init; } = 75f;
    
    /// <summary>
    ///Roughly how low you'd have to be to have organ failure (and die)
    /// </summary>
    [JsonInclude] public float CriticalWeight { get;  init; } = 40f;
    
    /// <summary>
    /// Let's not get too large and in charge now
    /// </summary>
    [JsonInclude] public float MaxWeight { get;  init; } = 100f;
    
    /// <summary>
    /// Roughly how much saturation you'd expect a player to eat in a day to maintain weight
    /// </summary>
    [JsonInclude] public float ExpectedSaturationPerDay { get;  init; } = 4000f;
    
    /// <summary>
    /// How long you would starve from a healthy weight of 75kg.
    /// Doesn't necessarily affect the amount of time it takes to starve but does affect how much saturation = 1kg
    /// </summary>
    [JsonInclude] public float NumberOfMonthsToStarve { get;  init; } = 1.5f;
    
    /// <summary>
    /// How many calories you can eat while full before you throw up
    /// </summary>
    [JsonInclude] public float ThrowUpThreshold { get; init; } = 250f;
    
    /// <summary>
    /// Whether or not to apply the bonuses to weight
    /// </summary>
    [JsonInclude] public bool ApplyWeightBonuses { get; init; } = true;

    /// <summary>
    /// The percentage of weight to lose on death 
    /// </summary>
    [JsonInclude] public float WeightLossOnDeath { get; init; } = 30f;

    /// <summary>
    /// The lowest weight a player can be when they respawn
    /// </summary>
    [JsonInclude] public float LowestPossibleWeightOnRespawn { get; init; } = 45f;
    
    /// <summary>
    /// The weight a player when they first spawn in
    /// </summary>
    [JsonInclude] public float PlayerStartingWeight { get; init; } = 60f;

    /// <summary>
    /// Bonuses to apply at the given weight values
    /// </summary>
    [JsonInclude] public IReadOnlyList<Bonus>? WeightBonuses { get; set; }

    /// <summary>
    /// How much less saturation you consume when standing still
    /// </summary>
    [JsonInclude] public float StoodStillModifier { get; init; } = 0.25f;

    /// <summary>
    /// How much less saturation you consume when sleeping
    /// </summary>
    [JsonInclude] public float SleepModifier { get; init; } = 0.25f;
    
    /// <summary>
    /// How much extra saturation you burn when sprinting
    /// </summary>
    [JsonInclude] public float SprintModifier { get; init; } = 0.1f;

    /// <summary>
    /// Whether the patch that allows for consumption of a whole meal (no portions) is applied
    /// </summary>
    [JsonInclude] public bool AlwaysConsumeFullMeal { get; init; } = true;

    /// <summary>
    /// Whether damage is applied when the player reaches the critical weight
    /// </summary>
    [JsonInclude] public bool ApplyFatalDamageOnCriticalWeight { get; init; } = true;

    public SimplyStarvingConfig Freeze()
    {
        return new SimplyStarvingConfig(this);
    }

}