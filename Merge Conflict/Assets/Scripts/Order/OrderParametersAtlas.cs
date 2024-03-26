/**********************************************************************************************************************
Name:           OrderParameterAtlas
Description:    All parameters which are needed to calculate/generate a new order or make the code from this more 
                readable. 
Author(s):      Markus Haubold
Date:           2024-03-26
Version:        V1.1
TODO:           - 
**********************************************************************************************************************/

public static class OrderParametersAtlas
{
    public static int Stage1 = 0;
    public static int Stage2 = 1;
    public static int Stage3 = 2;
    public static int Stage4 = 3;

    public enum ComponentNamesWithListIndex
    {
        Case = 0,
        HDD = 1,
        Powersupply = 2,
        Motherboard = 3,
        GPU = 4,
        CPU = 5,
        RAM = 6
    }

    public static int ComponentsCount = ComponentNamesWithListIndex.GetValues(typeof(ComponentNamesWithListIndex)).Length;
    public static int StagesCount = 4;
    public static int MinLevel = 1;
    public static int MaxLevel = 10;

}