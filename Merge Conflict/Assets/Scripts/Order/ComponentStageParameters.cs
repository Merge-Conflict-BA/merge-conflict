public class ComponentStageParameters 
{
    private int[] levelsToUnlockStages;
    private int[] countedLevelsMultiplicationFactor;

    public ComponentStageParameters(int[] levelsToUnlockStages, int[] countedLevelsMultiplicationFactor)
    {
        //TODO: error log if input params empty (or on of them)
        SetLevelsToUnlockStages(levelsToUnlockStages);
        SetCountedLevelsMultiplicationFactor(countedLevelsMultiplicationFactor);
    }

    public int[] GetLevelsToUnlockStages()
    {
        return this.levelsToUnlockStages;
    }

    public void SetLevelsToUnlockStages(int[] LevelToUnlockStages)
    {
        this.levelsToUnlockStages = LevelToUnlockStages;
    }


    public int[] GetCountedLevelsMultiplicationFactor()
    {
        return this.countedLevelsMultiplicationFactor;
    }

    public void SetCountedLevelsMultiplicationFactor(int[] multiplicationFactors)
    {
        this.countedLevelsMultiplicationFactor = multiplicationFactors;
    }

    

}