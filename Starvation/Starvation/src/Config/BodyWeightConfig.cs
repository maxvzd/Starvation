namespace Starvation.Config;

public class BodyWeightConfig
{
    //A regular weight for a regular man
    public float HealthyWeight = 75f;
    //Roughly how low you'd have to be to feel organ failure
    public float CriticalWeight = 40f;
    //Roughly how much you'd expect a player to eat in a day to maintain weight
    public float ExpectedSaturationPerDay = 4000f;
    //How long you would starve from a healthy weight of 75kg
    public float NumberOfMonthsToStarve = 1.5f;
}