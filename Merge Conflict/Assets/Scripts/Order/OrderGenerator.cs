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

    public Order Order = Order.EmptyOrder();

    private readonly ComponentTierParameters[] _allComponentTierParameters = new ComponentTierParameters[ComponentsCount];

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
             * initialize objects to store the needed parameters for calculate the probabilitiy for every Tier to be within the new order
             * Tier-parameters for every component: levelsToUnlockTier {Tier1, Tier2, Tier3, Tier4}, countedLevelsMultiplicationFactor {Tier1, Tier2, Tier3, Tier4}
            */
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.Case] = new ComponentTierParameters(new int[] { 1, 2, 4, 6 }, new int[] { 1, 2, 3, 4 });
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.HDD] = new ComponentTierParameters(new int[] { 1, 3, 5, 7 }, new int[] { 1, 2, 3, 4 });
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.Powersupply] = new ComponentTierParameters(new int[] { 1, 4, 6, 8 }, new int[] { 1, 2, 3, 4 });
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.Motherboard] = new ComponentTierParameters(new int[] { 1, 5, 7, 9 }, new int[] { 1, 2, 3, 4 });
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.GPU] = new ComponentTierParameters(new int[] { 1, 6, 8, 10 }, new int[] { 1, 2, 3, 4 });
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.CPU] = new ComponentTierParameters(new int[] { 1, 6, 8, 10 }, new int[] { 1, 2, 3, 4 });
            _allComponentTierParameters[(int)ComponentNamesWithListIndex.RAM] = new ComponentTierParameters(new int[] { 1, 7, 9, 10 }, new int[] { 1, 2, 3, 4 });

#if UNITY_EDITOR
            WriteDataLogFile();
#endif

            _instance = this;
        }
    }

    /*
     * calculate for depending on the given level the probabilities for the Tier1...Tier4 that this Tier is in the new generated order
     * 1) parse the index for the current component from the given string 
     * 2) count the levels from the current level to the level with which the Tier was unlocked
     * 3) multiply this counted levels with a parameter, which is stored in the ComponentTierParameters object from the component (with changing this 
     *    it's possible to controle/tune the probabilities and the distribution among each other)
     * 4) calculate the probabilities of the Tiers1...Tier4 with which they will be in the new generated order
    */
    private float[] CalculateComponentTierProbabilities(int currentLevel, string componentName)
    {
        ComponentNamesWithListIndex componentNameParsedEnumValue = (ComponentNamesWithListIndex)Enum.Parse(typeof(ComponentNamesWithListIndex), componentName);
        int componentIndex = (int)componentNameParsedEnumValue;

        int[] countedLevels = Calculate.GetLevelCountSinceTierUnlock(currentLevel, _allComponentTierParameters[componentIndex].LevelsToUnlockTiers); ;

        int[] multipliedCountedLevels = Calculate.MultiplyLevelCountSinceTierUnlock(countedLevels, _allComponentTierParameters[componentIndex].CountedLevelsMultiplicationFactor);

        int totalMultipliedCountedLevels = Calculate.SumOfMultipliedLevelCountSinceTierUnlock(multipliedCountedLevels);

        return Calculate.ProbabilityTierIsInOrder(multipliedCountedLevels, totalMultipliedCountedLevels);
    }

    /*
     * calculate the probabilities for Tier1...Tier4
     * generate an random number between 0 and 1
     * choose the Tier into which the randomly selected number fits
    */
    public int SelectTierForComponent(string componentName, int currentLevel)
    {
        float[] probabilityTierIsInOrder = CalculateComponentTierProbabilities(currentLevel, componentName);

        float p1 = probabilityTierIsInOrder[Tier1];
        float p2 = probabilityTierIsInOrder[Tier2];
        float p3 = probabilityTierIsInOrder[Tier3];
        float p4 = probabilityTierIsInOrder[Tier4];

        float cumulativeP1 = p1;
        float cumulativeP2 = cumulativeP1 + p2;
        float cumulativeP3 = cumulativeP2 + p3;
        float cumulativeP4 = cumulativeP3 + p4;

        float randomNumber = UnityEngine.Random.value;
        return randomNumber switch
        {
            float n when n < cumulativeP1 => 1,
            float n when n < cumulativeP2 => 2,
            float n when n < cumulativeP3 => 3,
            float n when n < cumulativeP4 => 4,
            _ => 0 //if the randomNumber fits with nothing, 0 will be return and the Component class should run into an error
        };
    }

    public Order GenerateNewOrder(int currentLevel)
    {
        int[] selectedTierForComponents = new int[ComponentsCount];
        for (int component = 0; component < ComponentsCount; component++)
        {
            selectedTierForComponents[component] = SelectTierForComponent(_componentNames[component], currentLevel);
        }

        int caseTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.Case];
        int hddTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.HDD];
        int powersupplyTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.Powersupply];
        int motherboardTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.Motherboard];
        int gpuTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.GPU];
        int cpuTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.CPU];
        int ramTier = selectedTierForComponents[(int)ComponentNamesWithListIndex.RAM];

        Order order = new Order(caseTier, hddTier, powersupplyTier, motherboardTier, gpuTier, cpuTier, ramTier);
        Order = order;
       
        return order;
    }

    public void DeleteOrder()
    {
        Order = Order.EmptyOrder();
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
            int[] dsf = _allComponentTierParameters[componentIndex].CountedLevelsMultiplicationFactor;
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Tier1]);
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Tier2]);
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Tier3]);
            writer.WriteLine("countedLevelsMultiplicationFactor: " + dsf[Tier4]);
            writer.WriteLine(" ");
            writer.WriteLine("levelsToUnlockTiers:");
            int[] eul = _allComponentTierParameters[componentIndex].LevelsToUnlockTiers;
            writer.WriteLine("levelsToUnlockTiers: " + eul[Tier1]);
            writer.WriteLine("levelsToUnlockTiers: " + eul[Tier2]);
            writer.WriteLine("levelsToUnlockTiers: " + eul[Tier3]);
            writer.WriteLine("levelsToUnlockTiers: " + eul[Tier4]);
            writer.WriteLine(" ");
            writer.WriteLine("Data that depends on current level:");
            for (int level = 1; level <= AmountLevels; level++)
            {
                int[] dist = Calculate.GetLevelCountSinceTierUnlock(level, _allComponentTierParameters[componentIndex].LevelsToUnlockTiers); ;

                int[] scaledDist = Calculate.MultiplyLevelCountSinceTierUnlock(dist, _allComponentTierParameters[componentIndex].CountedLevelsMultiplicationFactor);

                int totalScaledDist = Calculate.SumOfMultipliedLevelCountSinceTierUnlock(scaledDist);

                float[] prob = Calculate.ProbabilityTierIsInOrder(scaledDist, totalScaledDist);

                writer.WriteLine($"|-Level {level}");
                writer.WriteLine($" |-totalScaledDistance: " + totalScaledDist);
                writer.WriteLine($" |-distanceToUnlockLevel1: " + dist[Tier1]);
                writer.WriteLine($" |-scaledDistance1: " + scaledDist[Tier1]);
                writer.WriteLine($" |-probability1: " + prob[Tier1]);
                writer.WriteLine(" |");
                writer.WriteLine($" |-distanceToUnlockLevel2: " + dist[Tier2]);
                writer.WriteLine($" |-scaledDistance2: " + scaledDist[Tier2]);
                writer.WriteLine($" |-probability2: " + prob[Tier2]);
                writer.WriteLine(" |");
                writer.WriteLine($" |-distanceToUnlockLevel3: " + dist[Tier3]);
                writer.WriteLine($" |-scaledDistance3: " + scaledDist[Tier3]);
                writer.WriteLine($" |-probability3: " + prob[Tier3]);
                writer.WriteLine(" |");
                writer.WriteLine($" |-distanceToUnlockLevel4: " + dist[Tier4]);
                writer.WriteLine($" |-scaledDistance4: " + scaledDist[Tier4]);
                writer.WriteLine($" |-probability4: " + prob[Tier4]);
                writer.WriteLine(" ");
            }
            writer.WriteLine("");
            writer.WriteLine("");
        }
    }
}