using System;
using System.Linq;

public static class Calculate
{
    const byte AmountStages = 4;
    const byte FirstLevel = 1;
    const byte EndLevel = 10;
    const byte Stage1 = 0;
    const byte Stage2 = 1;
    const byte Stage3 = 2;
    const byte Stage4 = 3;

    public static int[] DistanceCurrentLevelToEvolutionUnlockLevels(int currentLevel, int[] evolutionUnlockLevelParameters)
    {
        //return null if current level is not valid 
        if (currentLevel != Math.Clamp(currentLevel, FirstLevel, EndLevel))
        {
            Debugger.LogError("The given level in DistanceCurrentLevelToEvolutionUnlockLevels() is not valid! Check if the value is within the borders FirstLevel and EndLevel.");
            return null;
        };

        int[] distances = new int[AmountStages];

        for (int stage = 0; stage < AmountStages; stage++)
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
        int[] scaledDistances = new int[AmountStages];

        for (int stage = 0; stage < AmountStages; stage++)
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
        float[] probabilities = new float[AmountStages];

        //edgecase: current level==1 => probability stage1=100% because all other stages are locked
        if (totalScaledDistance == 0)
        {
            probabilities[Stage1] = 1;
            probabilities[Stage2] = 0;
            probabilities[Stage3] = 0;
            probabilities[Stage4] = 0;

            return probabilities;
        }

        for (int stage = 0; stage < AmountStages; stage++)
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