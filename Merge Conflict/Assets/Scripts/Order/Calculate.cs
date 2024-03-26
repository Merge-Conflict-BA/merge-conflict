/**********************************************************************************************************************
Name:           Calculate
Description:    Contains all sub-calculations to calculate the probability for the stages from an given component. 
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
     * count how long the stages are still unlocked (in levels)
     * example: 
     *  current level=5 and count for case
     *  unlock stage 1 in level 1: levelsWhenStagesWillUnlocked[0] = 1
     *  unlock stage 2 in level 2: levelsWhenStagesWillUnlocked[1] = 2
     *  unlock stage 3 in level 4: levelsWhenStagesWillUnlocked[2] = 4
     *  unlock stage 4 in level 6: levelsWhenStagesWillUnlocked[3] = 6
     *
     *  counted levels stage 1: countedLevelsSinceStagesAreUnlocked[0] = (current level - unlocklevel stage 1) = 4
     *  counted levels stage 2: countedLevelsSinceStagesAreUnlocked[1] = (current level - unlocklevel stage 2) = 2
     *  counted levels stage 3: countedLevelsSinceStagesAreUnlocked[2] = (current level - unlocklevel stage 3) = 1 
     *  counted levels stage 4: countedLevelsSinceStagesAreUnlocked[3] = (current level - unlocklevel stage 4) = 0 skip because its locked
    */
    public static int[] CountLevelsSinceStagesAreUnlocked(int currentLevel, int[] levelsWhenStagesWillUnlocked)
    {
        //return null if current level is not valid 
        if (currentLevel != Math.Clamp(currentLevel, MinLevel, MaxLevel))
        {
            Debugger.LogError("The given level in CountLevelsSinceStageIsUnlocked() is not valid! Check if the value is within the borders OrderConstantsAtlas.FirstLevel and OrderConstantsAtlas.EndLevel.");
            return null;
        };

        int[] countedLevelsSinceStagesAreUnlocked = new int[StagesCount];

        for (int stage = 0; stage < StagesCount; stage++)
        {
            if (currentLevel < levelsWhenStagesWillUnlocked[stage]) //if the evolutionStage is locked, skip it
            {
                countedLevelsSinceStagesAreUnlocked[stage] = 0;
                continue;
            }
            countedLevelsSinceStagesAreUnlocked[stage] = currentLevel - levelsWhenStagesWillUnlocked[stage];
        }

        return countedLevelsSinceStagesAreUnlocked;
    }

    /*
     * Multiply the counted levels from all stages (corresponding to the level for which they are counted) with a 
     * customizable factor (=multiplicationFactors). This makes it possibility to set a "probability weight" for every stage.
     * The multiplicationFactors are stored in the OrderConstantsAtlas class with the names MultiplicationFactoreForLevelToUnlockStage1...4 
    */
    public static int[] MultiplyCountedLevels(int[] countedLevelsSinceStagesAreUnlocked, int[] multiplicationFactors)
    {
        int[] multipliedCountedLevels = new int[StagesCount];

        for (int stage = 0; stage < StagesCount; stage++)
        {
            multipliedCountedLevels[stage] = countedLevelsSinceStagesAreUnlocked[stage] * multiplicationFactors[stage];
        }

        return multipliedCountedLevels;
    }

    //Sum up all multiplied levels (=return from MultiplyCountedLevels()) - it will be the divisor to calculate the probabilities
    public static int SumOfMultipliedCountedLevels(int[] multipliedCountedLevels)
    {
        return multipliedCountedLevels.Sum();
    }

    /*
     * Calculate the probability for every stage of an level by using the therefore calculatet parameters
     * probability = multipliedCountedLevels[stage1...4] / sumOfMultipliedLevels
    */
    public static float[] ProbabilityStageIsInOrder(int[] multipliedCountedLevels, int sumOfMultipliedLevels)
    {
        float[] probabilityStageIsInOrder = new float[StagesCount];

        //edgecase: current level==1 => probability stage1=100% because all other stages are locked
        if (sumOfMultipliedLevels == 0)
        {
            probabilityStageIsInOrder[Stage1] = 1;
            probabilityStageIsInOrder[Stage2] = 0;
            probabilityStageIsInOrder[Stage3] = 0;
            probabilityStageIsInOrder[Stage4] = 0;

            return probabilityStageIsInOrder;
        }

        for (int stage = 0; stage < StagesCount; stage++)
        {
            if (multipliedCountedLevels[stage] == 0)    //stage is locked => probability=0 to get in order
            {
                probabilityStageIsInOrder[stage] = 0;
                continue;
            }
            float ratio = (float)multipliedCountedLevels[stage] / sumOfMultipliedLevels;
            probabilityStageIsInOrder[stage] = (float)Math.Round(ratio, 2);
        }

        return probabilityStageIsInOrder;
    }
}