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
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 2,
        UnlocklevelStage3 = 4,
        UnlocklevelStage4 = 6,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
    public enum HDD
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 3,
        UnlocklevelStage3 = 5,
        UnlocklevelStage4 = 7,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
    public enum Powersupply
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 4,
        UnlocklevelStage3 = 6,
        UnlocklevelStage4 = 8,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
    public enum Motherboard
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 5,
        UnlocklevelStage3 = 7,
        UnlocklevelStage4 = 9,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
    public enum GPU
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 6,
        UnlocklevelStage3 = 8,
        UnlocklevelStage4 = 10,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
    public enum CPU
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 6,
        UnlocklevelStage3 = 8,
        UnlocklevelStage4 = 10,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
    public enum RAM
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 7,
        UnlocklevelStage3 = 9,
        UnlocklevelStage4 = 10,
        DistanceScalingFactor1 = 1,
        DistanceScalingFactor2 = 2,
        DistanceScalingFactor3 = 3,
        DistanceScalingFactor4 = 4,
    }
}