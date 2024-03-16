public static class Calculate
{

    public static int[] DistanceCurrentLevelToEvolutionUnlockLevels(int currentLevel, int[] evolutionUnlockLevels)
    {
        int[] distances = new int[4];

        for (int stage = 0; stage < 4; stage++)
        {
            if (currentLevel < evolutionUnlockLevels[stage]) //if the evolutionStage is locked, skip it
            {
                distances[stage] = 0;
                continue;
            }
            distances[stage] = currentLevel - evolutionUnlockLevels[stage];
        }

        return distances;
    }

}