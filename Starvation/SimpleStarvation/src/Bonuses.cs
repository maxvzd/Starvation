using System.Collections.Generic;

namespace Starvation;

public readonly struct Bonus(BonusType type, float value, float weight)
{
    public float Weight { get; } = weight;
    public BonusType Type { get; } = type;
    public float Value { get; } = value;
}

public enum BonusType
{
    WalkSpeed,
    MiningSpeed,
    MaxHealth,
    MeleeDamage,
    RangedDamage,
    HealingEffectiveness,
    HungerRate,
    RangeWeaponAccuracy,
    RangedWeaponsSpeed,
    MechanicalsDamage,
    AnimalLootDropRate,
    ForageDropRate,
    WildCropDropRate,
    VesselContentsDropRate,
    OreDropRate,
    RustyGearDropRate,
    AnimalSeekingRange,
    ArmorDurabilityLoss,
    ArmorWalkSpeed,
    BowDrawStrength,
    WholeVesselLootChance,
    TemporalGearRepairCost,
    AnimalHarvestTime,
    GliderLiftMax,
    GliderSpeedMax,
    JumpHeightMul
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
