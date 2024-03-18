using System;
using System.Diagnostics;
using System.Linq;

public static class Calculate
{
    const byte AmountStages = 4;

    public static int[] DistanceCurrentLevelToEvolutionUnlockLevels(int currentLevel, int[] evolutionUnlockLevelParameters)
    {
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
            probabilities[0] = 1;
            probabilities[1] = 0;
            probabilities[2] = 0;
            probabilities[3] = 0;

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