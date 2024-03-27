/**********************************************************************************************************************
Name:           Calculate
Description:    Contains all sub-calculations to calculate the probability for the Tiers from an given component. 
                It is needed to generate an new order.
Author(s):      Markus Haubold
Date:           2024-03-26
Version:        V1.1
TODO:           - 
**********************************************************************************************************************/

using System;
using System.Linq;
using static OrderParametersAtlas;

public static class Calculate
{
    /*
     * count how long the Tiers are still unlocked (in levels)
     * example: 
     *  current level=5 and count for case
     *  unlock Tier 1 in level 1: levelsWhenTiersWillUnlocked[0] = 1
     *  unlock Tier 2 in level 2: levelsWhenTiersWillUnlocked[1] = 2
     *  unlock Tier 3 in level 4: levelsWhenTiersWillUnlocked[2] = 4
     *  unlock Tier 4 in level 6: levelsWhenTiersWillUnlocked[3] = 6
     *
     *  counted levels Tier 1: countedLevelsSinceTiersAreUnlocked[0] = (current level - unlocklevel Tier 1) = 4
     *  counted levels Tier 2: countedLevelsSinceTiersAreUnlocked[1] = (current level - unlocklevel Tier 2) = 2
     *  counted levels Tier 3: countedLevelsSinceTiersAreUnlocked[2] = (current level - unlocklevel Tier 3) = 1 
     *  counted levels Tier 4: countedLevelsSinceTiersAreUnlocked[3] = (current level - unlocklevel Tier 4) = 0 skip because its locked
    */
    public static int[] GetLevelCountSinceTierUnlock(int currentLevel, int[] levelsWhenTiersWillUnlocked)
    {
        //return null if current level is not valid 
        if (currentLevel != Math.Clamp(currentLevel, MinLevel, MaxLevel))
        {
            Debugger.LogError("The given level in CountLevelsSinceTierIsUnlocked() is not valid! Check if the value is within the borders OrderConstantsAtlas.FirstLevel and OrderConstantsAtlas.EndLevel.");
            return null;
        };

        int[] countedLevelsSinceTiersAreUnlocked = new int[TiersCount];

        for (int Tier = 0; Tier < TiersCount; Tier++)
        {
            if (currentLevel < levelsWhenTiersWillUnlocked[Tier]) //if the evolutionTier is locked, skip it
            {
                countedLevelsSinceTiersAreUnlocked[Tier] = 0;
                continue;
            }
            countedLevelsSinceTiersAreUnlocked[Tier] = currentLevel - levelsWhenTiersWillUnlocked[Tier];
        }

        return countedLevelsSinceTiersAreUnlocked;
    }

    /*
     * Multiply the counted levels from all Tiers (corresponding to the level for which they are counted) with a 
     * customizable factor (=multiplicationFactors). This makes it possibility to set a "probability weight" for every Tier.
     * The multiplicationFactors are stored in the OrderConstantsAtlas class with the names MultiplicationFactoreForLevelToUnlockTier1...4 
    */
    public static int[] MultiplyLevelCountSinceTierUnlock(int[] countedLevelsSinceTiersAreUnlocked, int[] multiplicationFactors)
    {
        int[] multipliedCountedLevels = new int[TiersCount];

        for (int Tier = 0; Tier < TiersCount; Tier++)
        {
            multipliedCountedLevels[Tier] = countedLevelsSinceTiersAreUnlocked[Tier] * multiplicationFactors[Tier];
        }

        return multipliedCountedLevels;
    }

    //Sum up all multiplied levels (=return from MultiplyCountedLevels()) - it will be the divisor to calculate the probabilities
    public static int SumOfMultipliedLevelCountSinceTierUnlock(int[] multipliedCountedLevels)
    {
        return multipliedCountedLevels.Sum();
    }

    /*
     * Calculate the probability for every Tier of an level by using the therefore calculatet parameters
     * probability = multipliedCountedLevels[Tier1...4] / sumOfMultipliedLevels
    */
    public static float[] ProbabilityTierIsInOrder(int[] multipliedCountedLevels, int sumOfMultipliedLevels)
    {
        float[] probabilityTierIsInOrder = new float[TiersCount];

        //edgecase: current level==1 => probability Tier1=100% because all other Tiers are locked
        if (sumOfMultipliedLevels == 0)
        {
            probabilityTierIsInOrder[Tier1] = 1;
            probabilityTierIsInOrder[Tier2] = 0;
            probabilityTierIsInOrder[Tier3] = 0;
            probabilityTierIsInOrder[Tier4] = 0;

            return probabilityTierIsInOrder;
        }

        for (int Tier = 0; Tier < TiersCount; Tier++)
        {
            if (multipliedCountedLevels[Tier] == 0)    //Tier is locked => probability=0 to get in order
            {
                probabilityTierIsInOrder[Tier] = 0;
                continue;
            }
            float ratio = (float)multipliedCountedLevels[Tier] / sumOfMultipliedLevels;
            probabilityTierIsInOrder[Tier] = (float)Math.Round(ratio, 2);
        }

        return probabilityTierIsInOrder;
    }
}