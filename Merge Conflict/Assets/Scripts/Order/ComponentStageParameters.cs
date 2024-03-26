/**********************************************************************************************************************
Name:           ComponentStageParameters
Description:    Store all component-specific stage parameters. 
Author(s):      Markus Haubold
Date:           2024-03-26
Version:        V1.1
TODO:           - 
**********************************************************************************************************************/

public class ComponentStageParameters
{
    private int[] levelsToUnlockStages; //contains the levels that unlocks the stages1...stage4 (checkout the Readme)

    /*
     * During the probability-calcultion the levels from the current level to the level with which a stage was unlocked
     * will be count. To get the possibility to tune/controle the probabilities of the 4 stages, the counted levels will
     * multiply with an factor (it's a kind of probability-weigh).
    */
    private int[] countedLevelsMultiplicationFactor;

    public ComponentStageParameters(int[] levelsToUnlockStages, int[] countedLevelsMultiplicationFactor)
    {
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