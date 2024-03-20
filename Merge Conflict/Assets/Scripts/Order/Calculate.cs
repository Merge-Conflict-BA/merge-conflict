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
   public static int[] DistanceCurrentLevelToEvolutionUnlockLevels(int currentLevel, int[] evolutionUnlockLevelParameters)
    {
        //return null if current level is not valid 
        if (currentLevel != Math.Clamp(currentLevel, Const.FirstLevel, Const.EndLevel))
        {
            Debugger.LogError("The given level in DistanceCurrentLevelToEvolutionUnlockLevels() is not valid! Check if the value is within the borders Const.FirstLevel and Const.EndLevel.");
            return null;
        };

        int[] distances = new int[Const.AmountStages];

        for (int stage = 0; stage < Const.AmountStages; stage++)
        {
            if (currentLevel < evolutionUnlockLevelParameters[stage]) //if the evolutionStage is locked, skip it
            {
                distances[stage] = 0;
                continue;
            }
            distances[stage] = currentLevel - evolutionUnlockLevelParameters[stage];
        }

        return distances;
    }

    public static int[] ScaledDistance(int[] calculatedDistances, int[] distanceScalingFactors)
    {
        int[] scaledDistances = new int[Const.AmountStages];

        for (int stage = 0; stage < Const.AmountStages; stage++)
        {
            scaledDistances[stage] = calculatedDistances[stage] * distanceScalingFactors[stage];
        }

        return scaledDistances;
    }

    public static int TotalScaledDistances(int[] scaledDistances)
    {
        return scaledDistances.Sum();
    }

    public static float[] Probabilities(int[] scaledDistances, int totalScaledDistance)
    {
        float[] probabilities = new float[Const.AmountStages];

        //edgecase: current level==1 => probability stage1=100% because all other stages are locked
        if (totalScaledDistance == 0)
        {
            probabilities[Const.Stage1] = 1;
            probabilities[Const.Stage2] = 0;
            probabilities[Const.Stage3] = 0;
            probabilities[Const.Stage4] = 0;

            return probabilities;
        }

        for (int stage = 0; stage < Const.AmountStages; stage++)
        {
            if (scaledDistances[stage] == 0)    //stage is locked => probabilitie=0 to get in order
            {
                probabilities[stage] = 0;
                continue;
            }
            float ratio = (float)scaledDistances[stage] / totalScaledDistance;
            probabilities[stage] = (float)Math.Round(ratio, 2);
        }

        return probabilities;
    }
}