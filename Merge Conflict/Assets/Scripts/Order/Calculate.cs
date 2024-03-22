/**********************************************************************************************************************
Name:           Calculate
Description:    Contains all sub-calculations to calculate the probabilitie for the stages from an given component. 
                It is needed to generate an new order.
Author(s):      Markus Haubold
Date:           2024-03-20
Version:        V1.0
TODO:           - 
**********************************************************************************************************************/

using System;
using System.Linq;

public static class Calculate
{
    //1) count how long the stages are still unlocked (in levels)
    /*example: 
        current level=5 and count for case
        unlock stage 1 in level 1: levelsWhenStagesWillUnlocked[0] = 1
        unlock stage 2 in level 2: levelsWhenStagesWillUnlocked[1] = 2
        unlock stage 3 in level 4: levelsWhenStagesWillUnlocked[2] = 4
        unlock stage 4 in level 6: levelsWhenStagesWillUnlocked[3] = 6

        counted levels stage 1: countedLevelsSinceStagesAreUnlocked[0] = (current level - unlocklevel stage 1) = 4
        counted levels stage 2: countedLevelsSinceStagesAreUnlocked[1] = (current level - unlocklevel stage 2) = 2
        counted levels stage 3: countedLevelsSinceStagesAreUnlocked[2] = (current level - unlocklevel stage 3) = 1 
        counted levels stage 4: countedLevelsSinceStagesAreUnlocked[3] = (current level - unlocklevel stage 4) = 0 skip because its locked
    */
    public static int[] CountLevelsSinceStagesAreUnlocked(int currentLevel, int[] levelsWhenStagesWillUnlocked)
    {
        //return null if current level is not valid 
        if (currentLevel != Math.Clamp(currentLevel, Const.FirstLevel, Const.EndLevel))
        {
            Debugger.LogError("The given level in CountLevelsSinceStageIsUnlocked() is not valid! Check if the value is within the borders Const.FirstLevel and Const.EndLevel.");
            return null;
        };

        int[] countedLevelsSinceStagesAreUnlocked = new int[Const.AmountStages];

        for (int stage = 0; stage < Const.AmountStages; stage++)
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

    //multiply the counted levels, which gives us later the possibility the set a "probability weight" for every stage
    // 
    public static int[] MultiplyCountedLevelsSinceStagesAreUnlocked(int[] countedLevelsSinceStagesAreUnlocked, int[] multiplicationFactors)
    {
        int[] multipliedLevelsSinceStagesAreUnlocked = new int[Const.AmountStages];

        for (int stage = 0; stage < Const.AmountStages; stage++)
        {
            multipliedLevelsSinceStagesAreUnlocked[stage] = countedLevelsSinceStagesAreUnlocked[stage] * multiplicationFactors[stage];
        }

        return multipliedLevelsSinceStagesAreUnlocked;
    }

    public static int AllMultipliedLevelsSinceStagesAreUnlocked(int[] multipliedLevelsSinceStagesAreUnlocked)
    {
        return multipliedLevelsSinceStagesAreUnlocked.Sum();
    }

    public static float[] Probabilities(int[] multipliedLevelsSinceStagesAreUnlocked, int allLevelsSinceStageIsUnlocked)
    {
        float[] probabilities = new float[Const.AmountStages];

        //edgecase: current level==1 => probability stage1=100% because all other stages are locked
        if (allLevelsSinceStageIsUnlocked == 0)
        {
            probabilities[Const.Stage1] = 1;
            probabilities[Const.Stage2] = 0;
            probabilities[Const.Stage3] = 0;
            probabilities[Const.Stage4] = 0;

            return probabilities;
        }

        for (int stage = 0; stage < Const.AmountStages; stage++)
        {
            if (multipliedLevelsSinceStagesAreUnlocked[stage] == 0)    //stage is locked => probabilitie=0 to get in order
            {
                probabilities[stage] = 0;
                continue;
            }
            float ratio = (float)multipliedLevelsSinceStagesAreUnlocked[stage] / allLevelsSinceStageIsUnlocked;
            probabilities[stage] = (float)Math.Round(ratio, 2);
        }

        return probabilities;
    }
}