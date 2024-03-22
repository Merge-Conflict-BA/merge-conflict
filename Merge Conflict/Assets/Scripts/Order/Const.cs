/**********************************************************************************************************************
Name:           Const
Description:    All const-values which are needed to calculate/generate a new order or make the code from this more 
                readable. 
Author(s):      Markus Haubold
Date:           2024-03-20
Version:        V1.0
TODO:           - 
**********************************************************************************************************************/

public static class Const {
    public static byte Stage1 = 0;
    public static byte Stage2 = 1;
    public static byte Stage3 = 2;
    public static byte Stage4 = 3;

    public static byte CaseIndex = 0;
    public static byte HddIndex = 1;
    public static byte PowersupplyIndex = 2;
    public static byte MotherboardIndex = 3;
    public static byte GpuIndex = 4;
    public static byte CpuIndex = 5;
    public static byte RamIndex = 6;

    public static string[] ComponentNames = { "Case", "HDD", "Powersupply", "Motherboard", "GPU", "CPU", "RAM" }; //component with higher index == rarer in order (check parameters in the enum's)
    public static int AmountComponents = ComponentNames.Length;
    public static byte AmountStages = 4;
    public static byte FirstLevel = 1;
    public static byte EndLevel = 10;


    public enum Case
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 2,
        LevelToUnlockStage3 = 4,
        LevelToUnlockStage4 = 6,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
    public enum HDD
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 3,
        LevelToUnlockStage3 = 5,
        LevelToUnlockStage4 = 7,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
    public enum Powersupply
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 4,
        LevelToUnlockStage3 = 6,
        LevelToUnlockStage4 = 8,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
    public enum Motherboard
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 5,
        LevelToUnlockStage3 = 7,
        LevelToUnlockStage4 = 9,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
    public enum GPU
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 6,
        LevelToUnlockStage3 = 8,
        LevelToUnlockStage4 = 10,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
    public enum CPU
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 6,
        LevelToUnlockStage3 = 8,
        LevelToUnlockStage4 = 10,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
    public enum RAM
    {
        LevelToUnlockStage1 = 1,
        LevelToUnlockStage2 = 7,
        LevelToUnlockStage3 = 9,
        LevelToUnlockStage4 = 10,
        MultiplicationFactoreForLevelToUnlockStage1 = 1,
        MultiplicationFactoreForLevelToUnlockStage2 = 2,
        MultiplicationFactoreForLevelToUnlockStage3 = 3,
        MultiplicationFactoreForLevelToUnlockStage4 = 4,
    }
}