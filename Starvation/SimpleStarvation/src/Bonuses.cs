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
    RangedDamage
}

public static class BonusTypeToKey
{
    private static IReadOnlyDictionary<BonusType, string?> _bonusValues = new Dictionary<BonusType, string?>()
    {
        { BonusType.WalkSpeed, "walkspeed"},
        { BonusType.MaxHealth, "maxhealthExtraPoints"},
        { BonusType.MeleeDamage, "meleeWeaponsDamage"},
        { BonusType.RangedDamage, "rangedWeaponsDamage"},
        { BonusType.MiningSpeed, "miningSpeedMul"}
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
