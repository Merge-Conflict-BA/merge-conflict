public static class Calculate
{

    public static int[] DistanceCurrentLevelToEvolutionUnlockLevels(int currentLevel, int[] evolutionUnlockLevelParameters)
    {
        int[] distances = new int[4];

        for (int stage = 0; stage < 4; stage++)
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
        int[] scaledDistances = new int[4];

        for (int stage = 0; stage < 4; stage++)
        {
            scaledDistances[stage] = calculatedDistances[stage] * distanceScalingFactors[stage];
        }

        return scaledDistances;
    }

}