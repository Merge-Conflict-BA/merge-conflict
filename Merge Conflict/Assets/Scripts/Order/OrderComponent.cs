/**********************************************************************************************************************
Name:           OrderComponent
Description:    Contains all calculations for a Component in an Order
Author(s):      Markus Haubold, Hanno Witzleb
Date:           2024-03-27
Version:        V2.0
TODO:           - 
**********************************************************************************************************************/

using System;
using System.Linq;

public class OrderComponent
{
    public string Name;
    public int[] TierUnlockedAtLevel;
    public int[] TierWeights;

    public OrderComponent(string name, int[] tierUnlockedAtLevel, int[] tierWeights)
    {
        Name = name;
        TierUnlockedAtLevel = tierUnlockedAtLevel;
        TierWeights = tierWeights;
    }

    public override string ToString()
    {
        string name = $"---[Component {Name}]---\n";

        string tierUnlockedAtLevel = "tierUnlockedAtLevel: [";
        for (int i = 0; i < TierUnlockedAtLevel.Length; i++)
        {
            tierUnlockedAtLevel += $"{TierUnlockedAtLevel[i]}, ";
        }
        tierUnlockedAtLevel += "]\n";

        string tierWeights = "tierWeights: [";
        for (int i = 0; i < TierWeights.Length; i++)
        {
            tierWeights += $"{TierWeights[i]}, ";
        }
        tierWeights += "]\n";

        return name + tierUnlockedAtLevel + tierWeights;
    }

    public string GetDebugProbabilities(int level, bool verbose = false)
    {
        int[] levelCountSinceTierUnlock = GetLevelCountSinceTierUnlock(level);
        int[] multipliedLevelCountSinceTierUnlock = MultiplyLevelCountSinceTierUnlock(level);
        int sumOfMultipliedLevels = multipliedLevelCountSinceTierUnlock.Sum();
        float[] probabilities = CalculateProbabilities(level);

        string caption = $"|-Level {level}\n"
            + $" |-sumOfMultipliedLevels: {sumOfMultipliedLevels}\n";

        string verboseInfo = "";
        string sLevelCountSinceTierUnlock = " |-LevelCountSinceTierUnlock: [";
        string sMultipliedLevelCountSinceTierUnlock = " |-MultipliedLevelCountSinceTierUnlock: [";
        string sProbabilities = " |-Probabilities: [";

        for (int i = 0; i < levelCountSinceTierUnlock.Length; i++)
        {
            verboseInfo += $" |-levelCountSinceTierUnlock: {levelCountSinceTierUnlock[i]}\n";
            verboseInfo += $" |-multipliedLevelCountSinceTierUnlock: {multipliedLevelCountSinceTierUnlock[i]}\n";
            verboseInfo += $" |-probability: {probabilities[i]}\n";
            verboseInfo += $" |\n";

            sLevelCountSinceTierUnlock += $"{levelCountSinceTierUnlock[i]}, ";
            sMultipliedLevelCountSinceTierUnlock += $"{multipliedLevelCountSinceTierUnlock[i]}, ";
            sProbabilities += $"{probabilities[i]}, ";
        }

        sLevelCountSinceTierUnlock += "]\n";
        sMultipliedLevelCountSinceTierUnlock += "]\n";
        sProbabilities += "]\n";

        if(verbose)
        {
            return caption + verboseInfo;
        }

        return caption + sLevelCountSinceTierUnlock + sMultipliedLevelCountSinceTierUnlock + sProbabilities;        
    }

    /*
     * calculate for depending on the given level the probabilities for the Tier1...Tier4 that this Tier is in the new generated order
     * 1) parse the index for the current component from the given string 
     * 2) count the levels from the current level to the level with which the Tier was unlocked
     * 3) multiply this counted levels with a parameter, which is stored in the ComponentTierParameters object from the component (with changing this 
     *    it's possible to controle/tune the probabilities and the distribution among each other)
     * 4) calculate the probabilities of the Tiers1...Tier4 with which they will be in the new generated order
    
     * Calculate the probability for every Tier of an level by using the therefore calculatet parameters
     * probability = multipliedCountedLevels[Tier1...4] / sumOfMultipliedLevels
    */
    public float[] CalculateProbabilities(int level)
    {
        //return null if current level is not valid 
        if (level != Math.Clamp(level, 0, 10))
        {
            Debugger.LogError("The given level in CalculateProbabilities() is not valid! Check if the value is within the borders OrderConstantsAtlas.FirstLevel and OrderConstantsAtlas.EndLevel.");
            return null;
        }

        int[] multipliedCountedLevels = MultiplyLevelCountSinceTierUnlock(level);
        int sumOfMultipliedLevels = multipliedCountedLevels.Sum();

        float[] probabilityTierIsInOrder = new float[TierUnlockedAtLevel.Length];

        //edgecase: current level==1 => probability Tier1=100% because all other Tiers are locked
        if (sumOfMultipliedLevels == 0)
        {
            probabilityTierIsInOrder[(int)Tiers.T1] = 1;
            probabilityTierIsInOrder[(int)Tiers.T2] = 0;
            probabilityTierIsInOrder[(int)Tiers.T3] = 0;
            probabilityTierIsInOrder[(int)Tiers.T4] = 0;

            return probabilityTierIsInOrder;
        }

        for (int tierIndex = 0; tierIndex < multipliedCountedLevels.Length; tierIndex++)
        {
            if (multipliedCountedLevels[tierIndex] == 0)    //Tier is locked => probability=0 to get in order
            {
                probabilityTierIsInOrder[tierIndex] = 0;
                continue;
            }

            float ratio = (float)multipliedCountedLevels[tierIndex] / sumOfMultipliedLevels;
            probabilityTierIsInOrder[tierIndex] = (float)Math.Round(ratio, 2);
        }

        return probabilityTierIsInOrder;
    }

    /*
     * Multiply the counted levels from all Tiers (corresponding to the level for which they are counted) with a 
     * customizable factor (=multiplicationFactors). This makes it possibility to set a "probability weight" for every Tier.
     * The multiplicationFactors are stored in the OrderConstantsAtlas class with the names MultiplicationFactoreForLevelToUnlockTier1...4 
    */
    private int[] MultiplyLevelCountSinceTierUnlock(int level)
    {
        int[] countedLevelsSinceTiersAreUnlocked = GetLevelCountSinceTierUnlock(level);
        int[] multipliedCountedLevels = new int[countedLevelsSinceTiersAreUnlocked.Length];

        for (int tierIndex = 0; tierIndex < TierUnlockedAtLevel.Length; tierIndex++)
        {
            multipliedCountedLevels[tierIndex]
                = countedLevelsSinceTiersAreUnlocked[tierIndex] * TierWeights[tierIndex];
        }

        return multipliedCountedLevels;
    }

    /*
     * count how long the Tiers are still unlocked (in levels)
     * example: 
     *  current level=5 and count for case
     *  unlock Tier 1 in level 1: TierUnlockedAtLevel[0] = 1
     *  unlock Tier 2 in level 2: TierUnlockedAtLevel[1] = 2
     *  unlock Tier 3 in level 4: TierUnlockedAtLevel[2] = 4
     *  unlock Tier 4 in level 6: TierUnlockedAtLevel[3] = 6
     *
     *  counted levels Tier 1: countedLevelsSinceTiersAreUnlocked[0] = (current level - unlocklevel Tier 1) = 4
     *  counted levels Tier 2: countedLevelsSinceTiersAreUnlocked[1] = (current level - unlocklevel Tier 2) = 2
     *  counted levels Tier 3: countedLevelsSinceTiersAreUnlocked[2] = (current level - unlocklevel Tier 3) = 1 
     *  counted levels Tier 4: countedLevelsSinceTiersAreUnlocked[3] = (current level - unlocklevel Tier 4) = 0 skip because its locked
    */
    private int[] GetLevelCountSinceTierUnlock(int level)
    {
        int[] countedLevelsSinceTiersAreUnlocked = new int[TierUnlockedAtLevel.Length];

        for (int tierIndex = 0; tierIndex < TierUnlockedAtLevel.Length; tierIndex++)
        {
            if (level < TierUnlockedAtLevel[tierIndex]) //if the evolutionTier is locked, skip it
            {
                countedLevelsSinceTiersAreUnlocked[tierIndex] = 0;
                continue;
            }

            countedLevelsSinceTiersAreUnlocked[tierIndex] = level - TierUnlockedAtLevel[tierIndex];
        }

        return countedLevelsSinceTiersAreUnlocked;
    }
}

