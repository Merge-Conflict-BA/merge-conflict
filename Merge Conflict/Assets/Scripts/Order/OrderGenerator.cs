/**********************************************************************************************************************
Name:           OrderGenerator
Description:    Create a new order with 7 components and the corresponding level. The level is selected with a 
                probability which is calculated based on how long ago the respective level was unlocked. This means that
                a level that has only recently been unlocked will be more likely to be filled than a component that has 
                been unlocked for several levels.  
Author(s):      Markus Haubold
Date:           2024-03-26
Version:        V1.1
TODO:           - 
**********************************************************************************************************************/

using System;
using System.IO;
using UnityEngine;
using static OrderParametersAtlas;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }

    //application interface to get the number from the stage which is in the current order
    public int orderedCase;
    public int orderedHdd;
    public int orderedPowersupply;
    public int orderedMotherboard;
    public int orderedGpu;
    public int orderedCpu;
    public int orderedRam;

    private readonly ComponentStageParameters[] _allComponentStageParameters = new ComponentStageParameters[ComponentsCount];

    public int currentLevel = 1;    //TODO: only for debugging => delete after test
    private string[] _componentNames;

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            //store all component names from the OrderConstantsAtlas.ComponentNamesWithListIndex within an array of string
            _componentNames = ComponentNamesWithListIndex.GetNames(typeof(ComponentNamesWithListIndex));

            /*
             * initialize objects to store the needed parameters for calculate the probabilitiy for every stage to be within the new order
             * stage-parameters for every component: levelsToUnlockStage {stage1, stage2, stage3, stage4}, countedLevelsMultiplicationFactor {stage1, stage2, stage3, stage4}
            */
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.Case] = new ComponentStageParameters(new int[] { 1, 2, 4, 6 }, new int[] { 1, 2, 3, 4 });
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.HDD] = new ComponentStageParameters(new int[] { 1, 3, 5, 7 }, new int[] { 1, 2, 3, 4 });
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.Powersupply] = new ComponentStageParameters(new int[] { 1, 4, 6, 8 }, new int[] { 1, 2, 3, 4 });
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.Motherboard] = new ComponentStageParameters(new int[] { 1, 5, 7, 9 }, new int[] { 1, 2, 3, 4 });
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.GPU] = new ComponentStageParameters(new int[] { 1, 6, 8, 10 }, new int[] { 1, 2, 3, 4 });
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.CPU] = new ComponentStageParameters(new int[] { 1, 6, 8, 10 }, new int[] { 1, 2, 3, 4 });
            _allComponentStageParameters[(int)ComponentNamesWithListIndex.RAM] = new ComponentStageParameters(new int[] { 1, 7, 9, 10 }, new int[] { 1, 2, 3, 4 });

#if UNITY_EDITOR
            WriteDataLogFile();
#endif

            _instance = this;
        }
    }

    /*
     * calculate for depending on the given level the probabilities for the stage1...stage4 that this stage is in the new generated order
     * 1) parse the index for the current component from the given string 
     * 2) count the levels from the current level to the level with which the stage was unlocked
     * 3) multiply this counted levels with a parameter, which is stored in the ComponentStageParameters object from the component (with changing this 
     *    it's possible to controle/tune the probabilities and the distribution among each other)
     * 4) calculate the probabilities of the stages1...stage4 with which they will be in the new generated order
    */
    private float[] CalculateComponentstageProbabilities(int currentLevel, string componentName)
    {
        ComponentNamesWithListIndex componentNameParsedEnumValue = (ComponentNamesWithListIndex)Enum.Parse(typeof(ComponentNamesWithListIndex), componentName);
        int componentIndex = (int)componentNameParsedEnumValue;

        int[] countedLevels = Calculate.CountLevelsSinceStagesAreUnlocked(currentLevel, _allComponentStageParameters[componentIndex].GetLevelsToUnlockStages()); ;

        int[] multipliedCountedLevels = Calculate.MultiplyCountedLevels(countedLevels, _allComponentStageParameters[componentIndex].GetCountedLevelsMultiplicationFactor());

        int totalMultipliedCountedLevels = Calculate.SumOfMultipliedCountedLevels(multipliedCountedLevels);

        return Calculate.ProbabilityStageIsInOrder(multipliedCountedLevels, totalMultipliedCountedLevels);
    }

    /*
     * calculate the probabilities for stage1...stage4
     * generate an random number between 0 and 1
     * choose the stage into which the randomly selected number fits
    */
    public int SelectStageForSingleComponent(string componentName, int currentLevel)
    {
        float[] probabilityStageIsInOrder = CalculateComponentstageProbabilities(currentLevel, componentName);

        float p1, p2, p3, p4, scaledP1, scaledP2, scaledP3, scaledP4, randomNumber;

        p1 = probabilityStageIsInOrder[Stage1];
        p2 = probabilityStageIsInOrder[Stage2];
        p3 = probabilityStageIsInOrder[Stage3];
        p4 = probabilityStageIsInOrder[Stage4];

        scaledP1 = p1;
        scaledP2 = scaledP1 + p2;
        scaledP3 = scaledP2 + p3;
        scaledP4 = scaledP3 + p4;

        randomNumber = UnityEngine.Random.value;
        return randomNumber switch
        {
            float n when n < scaledP1 => 1,
            float n when n < scaledP2 => 2,
            float n when n < scaledP3 => 3,
            float n when n < scaledP4 => 4,
            _ => 0 //if the randomNumber fits with nothing, 0 will be return and the Component class should run into an error
        };
    }

    public void GenerateNewOrder(int currentLevel)
    {
        int[] selectedStageForComponents = new int[ComponentsCount];
        for (int component = 0; component < ComponentsCount; component++)
        {
            selectedStageForComponents[component] = SelectStageForSingleComponent(_componentNames[component], currentLevel);
        }

        orderedCase = selectedStageForComponents[(int)ComponentNamesWithListIndex.Case];
        orderedHdd = selectedStageForComponents[(int)ComponentNamesWithListIndex.HDD];
        orderedPowersupply = selectedStageForComponents[(int)ComponentNamesWithListIndex.Powersupply];
        orderedMotherboard = selectedStageForComponents[(int)ComponentNamesWithListIndex.Motherboard];
        orderedGpu = selectedStageForComponents[(int)ComponentNamesWithListIndex.GPU];
        orderedCpu = selectedStageForComponents[(int)ComponentNamesWithListIndex.CPU];
        orderedRam = selectedStageForComponents[(int)ComponentNamesWithListIndex.RAM];
    }

    public void DeleteCurrentOrder()
    {
        orderedCase = 0;
        orderedHdd = 0;
        orderedPowersupply = 0;
        orderedMotherboard = 0;
        orderedGpu = 0;
        orderedCpu = 0;
        orderedRam = 0;
    }

    /*
     * every time this class will be initialized, all parameters and all calculated values will be logged for all components for all levels
     * this can be usefull for the process of tune the probabilities because it gives an complete overview 
    */
    private void WriteDataLogFile()
    {
        const string FilePath = "Assets/Scripts/Order/dataLog.txt";
        const byte AmountLevels = 10;
        DateTime currentDateAndTime = DateTime.Now;

        using StreamWriter writer = new StreamWriter(FilePath);
        writer.WriteLine("All data which are needed to generate the Order.");
        writer.WriteLine("To understand what they data are used for, please checkout the Order/README.md!");
        writer.WriteLine($"Generated: {currentDateAndTime.Day}.{currentDateAndTime.Month}.{currentDateAndTime.Year} at {currentDateAndTime.Hour}:{currentDateAndTime.Minute}:{currentDateAndTime.Second}");
        writer.WriteLine(" ");
        foreach (var component in _componentNames)
        {
            ComponentNamesWithListIndex componentNameParsedEnumValue = (ComponentNamesWithListIndex)Enum.Parse(typeof(ComponentNamesWithListIndex), component);
            int componentIndex = (int)componentNameParsedEnumValue;

            writer.WriteLine($"---[Component {component}]---");
            writer.WriteLine("countedLevelsMultiplicationFactor:");
            int[] dsf = _allComponentStageParameters[componentIndex].GetCountedLevelsMultiplicationFactor();
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Stage1]);
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Stage2]);
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Stage3]);
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Stage4]);
            writer.WriteLine(" ");
            writer.WriteLine("levelsToUnlockStages:");
            int[] eul = _allComponentStageParameters[componentIndex].GetLevelsToUnlockStages();
            writer.WriteLine("levelsToUnlockStages: " + eul[Stage1]);
            writer.WriteLine("levelsToUnlockStages: " + eul[Stage2]);
            writer.WriteLine("levelsToUnlockStages: " + eul[Stage3]);
            writer.WriteLine("levelsToUnlockStages: " + eul[Stage4]);
            writer.WriteLine(" ");
            writer.WriteLine("Data that depends on current level:");
            for (int level = 1; level <= AmountLevels; level++)
            {
                int[] dist = Calculate.CountLevelsSinceStagesAreUnlocked(level, _allComponentStageParameters[componentIndex].GetLevelsToUnlockStages()); ;

                int[] scaledDist = Calculate.MultiplyCountedLevels(dist, _allComponentStageParameters[componentIndex].GetCountedLevelsMultiplicationFactor());

                int totalScaledDist = Calculate.SumOfMultipliedCountedLevels(scaledDist);

                float[] prob = Calculate.ProbabilityStageIsInOrder(scaledDist, totalScaledDist);

                writer.WriteLine($"|-Level {level}");
                writer.WriteLine($" |-totalScaledDistance: " + totalScaledDist);
                writer.WriteLine($" |-distanceToUnlockLevel1: " + dist[Stage1]);
                writer.WriteLine($" |-scaledDistance1: " + scaledDist[Stage1]);
                writer.WriteLine($" |-probability1: " + prob[Stage1]);
                writer.WriteLine(" |");
                writer.WriteLine($" |-distanceToUnlockLevel2: " + dist[Stage2]);
                writer.WriteLine($" |-scaledDistance2: " + scaledDist[Stage2]);
                writer.WriteLine($" |-probability2: " + prob[Stage2]);
                writer.WriteLine(" |");
                writer.WriteLine($" |-distanceToUnlockLevel3: " + dist[Stage3]);
                writer.WriteLine($" |-scaledDistance3: " + scaledDist[Stage3]);
                writer.WriteLine($" |-probability3: " + prob[Stage3]);
                writer.WriteLine(" |");
                writer.WriteLine($" |-distanceToUnlockLevel4: " + dist[Stage4]);
                writer.WriteLine($" |-scaledDistance4: " + scaledDist[Stage4]);
                writer.WriteLine($" |-probability4: " + prob[Stage4]);
                writer.WriteLine(" ");
            }
            writer.WriteLine("");
            writer.WriteLine("");
        }
    }
}