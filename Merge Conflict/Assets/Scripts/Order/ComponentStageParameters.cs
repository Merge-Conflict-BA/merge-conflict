/**********************************************************************************************************************
Name:           ComponentTierParameters
Description:    Store all component-specific Tier parameters. 
Author(s):      Markus Haubold
Date:           2024-03-26
Version:        V1.1
TODO:           - 
**********************************************************************************************************************/

public class ComponentTierParameters
{
    public int[] LevelsToUnlockTiers { get; private set; } //contains the levels that unlocks the Tiers1...Tier4 (checkout the Readme)

    /*
     * During the probability-calcultion the levels from the current level to the level with which a Tier was unlocked
     * will be count. To get the possibility to tune/controle the probabilities of the 4 Tiers, the counted levels will
     * multiply with an factor (it's a kind of probability-weigh).
    */
    public int[] CountedLevelsMultiplicationFactor { get; private set; }

    public ComponentTierParameters(int[] levelsToUnlockTiers, int[] countedLevelsMultiplicationFactor)
    {
        SetLevelsToUnlockTiers(levelsToUnlockTiers);
        SetCountedLevelsMultiplicationFactor(countedLevelsMultiplicationFactor);
    }

    public void SetLevelsToUnlockTiers(int[] LevelToUnlockTiers)
    {
        LevelsToUnlockTiers = LevelToUnlockTiers;
    }

    public void SetCountedLevelsMultiplicationFactor(int[] multiplicationFactors)
    {
        CountedLevelsMultiplicationFactor = multiplicationFactors;
    }
}