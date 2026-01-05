namespace Starvation;

public readonly struct Bonus(float walkSpeed, float miningSpeed, float maxHealth, float meleeDamage, float rangedDamage)
{
    public float WalkSpeed { get; } = walkSpeed;
    public float MiningSpeed { get; } = miningSpeed;
    public float MaxHealth { get; } = maxHealth;
    public float MeleeDamage { get; } = meleeDamage;
    public float RangedDamage { get; } = rangedDamage;
}

public readonly struct WeightBonus(float weight, Bonus bonus)
{
    public float Weight { get; } = weight;
    public Bonus Bonus { get; } = bonus;
}