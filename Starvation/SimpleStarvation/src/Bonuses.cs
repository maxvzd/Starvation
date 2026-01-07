using System.Collections.Generic;

namespace Starvation;

public readonly struct Bonus(BonusType type, float value, float weight)
{
    public float Weight { get; } = weight;
    public BonusType Type { get; } = type;
    public float Value { get; } = value;
}

//Explicitly set values so that they don't change if anyone re-orders them
public enum BonusType
{
    //Likely to use
    WalkSpeed = 0,
    MiningSpeed = 1,
    MaxHealth = 2,
    MeleeDamage = 3,
    RangedDamage = 4,
    HealingEffectiveness = 5,
    HungerRate = 6,
    RangeWeaponAccuracy = 7,
    RangedWeaponsSpeed = 8,
    RustyGearDropRate = 9,
    AnimalSeekingRange = 10,
    BowDrawStrength = 11,
    GliderLiftMax = 12,
    GliderSpeedMax = 13,
    JumpHeightMul = 14,
    
    //Not Likely to use
    WholeVesselLootChance = 15,
    TemporalGearRepairCost = 16,
    AnimalHarvestTime = 17,
    MechanicalsDamage = 18,
    AnimalLootDropRate = 19,
    ForageDropRate = 20,
    WildCropDropRate = 21,
    VesselContentsDropRate = 22,
    OreDropRate = 23,
    ArmorWalkSpeed = 24,
    ArmorDurabilityLoss = 25,
}

public static class BonusTypeToKey
{
    private static IReadOnlyDictionary<BonusType, string?> _bonusValues = new Dictionary<BonusType, string?>()
    {
         { BonusType.WalkSpeed, "walkspeed"},
         { BonusType.MaxHealth, "maxhealthExtraPoints"},
         { BonusType.MeleeDamage, "meleeWeaponsDamage"},
         { BonusType.RangedDamage, "rangedWeaponsDamage"},
         { BonusType.MiningSpeed, "miningSpeedMul"},
         { BonusType.HealingEffectiveness, "healingeffectivness" },
         { BonusType.HungerRate, "hungerrate" },
         { BonusType.RangeWeaponAccuracy, "rangedWeaponsAcc" },
         { BonusType.RangedWeaponsSpeed, "rangedWeaponsSpeed" },
         { BonusType.MechanicalsDamage, "mechanicalsDamage" },
         { BonusType.AnimalLootDropRate, "animalLootDropRate" },
         { BonusType.ForageDropRate, "forageDropRate" },
         { BonusType.WildCropDropRate, "wildCropDropRate" },
         { BonusType.VesselContentsDropRate, "vesselContentsDropRate" },
         { BonusType.OreDropRate, "oreDropRate" },
         { BonusType.RustyGearDropRate, "rustyGearDropRate" },
         { BonusType.AnimalSeekingRange, "animalSeekingRange" },
         { BonusType.ArmorDurabilityLoss, "armorDurabilityLoss" },
         { BonusType.ArmorWalkSpeed, "armorWalkSpeedAffectedness" },
         { BonusType.BowDrawStrength, "bowDrawingStrength" },
         { BonusType.WholeVesselLootChance, "wholeVesselLootChance" },
         { BonusType.TemporalGearRepairCost, "temporalGearTLRepairCost" },
         { BonusType.AnimalHarvestTime, "animalHarvestTime" },
         { BonusType.GliderLiftMax, "gliderLiftMax" },
         { BonusType.GliderSpeedMax, "gliderSpeedMax" },
         { BonusType.JumpHeightMul, "jumpHeightMul" },
     };
    
     public static string GetKey(BonusType bonusType)
     {
         if (_bonusValues.TryGetValue(bonusType, out var key))
         {
             return key ?? string.Empty;
         }
         return string.Empty;
     }    
}
