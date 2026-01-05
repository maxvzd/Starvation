using System;
using System.Collections.Generic;
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
    [JsonInclude] public float ThrowUpThreshold { get; init; } = 250f;
    
    /// <summary>
    /// Whether or not to apply the bonuses to weight
    /// </summary>
    [JsonInclude] public bool ApplyWeightBonuses { get; init; } = true;

    /// <summary>
    /// The percentage of weight to lose on death 
    /// </summary>
    [JsonInclude] public float WeightLossOnDeath { get; init; } = 50f;

    /// <summary>
    /// The lowest weight a player can be when they respawn
    /// </summary>
    [JsonInclude] public float LowestPossibleWeightOnRespawn { get; init; } = 50f;
    
    /// <summary>
    /// The weight a player when they first spawn in
    /// </summary>
    [JsonInclude] public float PlayerStartingWeight { get; init; } = 60;
    
    /// <summary>
    /// Bonuses to apply at the given weight values
    /// </summary>
    [JsonInclude] public IReadOnlyList<WeightBonus> WeightBonuses = new List<WeightBonus>
    {
        new(40, new Bonus(-0.5f, -0.2f, -5f, -0.3f, -0.3f)), 
        new(50, new Bonus(-0.3f, -0.15f, -3f, -0.2f, -0.2f)), 
        new(60, new Bonus(-0.1f, -0.05f, -2f, -0.1f, -0.1f)), 
        new(70, new Bonus(.1f, .05f, 0f, 0.05f, 0.05f)),
        new(80, new Bonus(.25f, .1f, 3f, 0.1f, 0.1f)),
        new(90, new Bonus(-0.1f, 0f, 2f, 0.05f, 0f)),
        new(100, new Bonus(-0.15f, 0f, 1f, 0.05f, 0f)) // Damn boi he thicc
    };

}