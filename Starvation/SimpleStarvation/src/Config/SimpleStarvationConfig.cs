using System.Text.Json.Serialization;

namespace Starvation.Config;

public class SimpleStarvationConfig
{
    /// <summary>
    /// A regular weight for a regular man
    /// </summary>
    [JsonInclude] public float HealthyWeight { get; init; } = 75f;
    
    /// <summary>
    ///Roughly how low you'd have to be have organ failure (and die)
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
    /// Doesn't necessarily effect the amount of time it takes to starve but does effect how much saturation = 1kg
    /// </summary>
    [JsonInclude] public float NumberOfMonthsToStarve { get;  init; } = 1.5f;
    
    /// <summary>
    /// How many calories you can eat while full before you throw up
    /// </summary>
    [JsonInclude] public float ThrowUpThreshold { get; init; } = 250;
    
    /// <summary>
    /// Whether or not to apply the bonuses to weight
    /// </summary>
    [JsonInclude] public bool ApplyWeightBonuses { get; init; } = true;
    
    
}