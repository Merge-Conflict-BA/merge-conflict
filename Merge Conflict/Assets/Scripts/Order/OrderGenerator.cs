using System;
using System.IO;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    private ParameterStorage _parameterStorage;
    private readonly string[] ComponentNames = { "RAM", "HDD", "GPU", "CPU", "Case", "Motherboard", "Powersupply" };
    public byte currentLevel = 1;
    public bool writeLog = false;
    const byte Stage1 = 0;
    const byte Stage2 = 1;
    const byte Stage3 = 2;
    const byte Stage4 = 3;


    public bool calcProbs = false;

    //define calculation parameters for component
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


    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _parameterStorage = new ParameterStorage(ComponentNames);

            //case
            _parameterStorage.SetDistanceScalingFactor("Case", (int)Case.DistanceScalingFactor1, (int)Case.DistanceScalingFactor2, (int)Case.DistanceScalingFactor3, (int)Case.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("Case", (int)Case.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //hdd
            _parameterStorage.SetDistanceScalingFactor("HDD", (int)HDD.DistanceScalingFactor1, (int)HDD.DistanceScalingFactor2, (int)HDD.DistanceScalingFactor3, (int)HDD.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("HDD", (int)HDD.UnlocklevelStage1, (int)HDD.UnlocklevelStage2, (int)HDD.UnlocklevelStage3, (int)HDD.UnlocklevelStage4);
            //powersupply
            _parameterStorage.SetDistanceScalingFactor("Powersupply", (int)Powersupply.DistanceScalingFactor1, (int)Powersupply.DistanceScalingFactor2, (int)Powersupply.DistanceScalingFactor3, (int)Powersupply.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("Powersupply", (int)Powersupply.UnlocklevelStage1, (int)Powersupply.UnlocklevelStage2, (int)Powersupply.UnlocklevelStage3, (int)Powersupply.UnlocklevelStage4);
            //motherboard
            _parameterStorage.SetDistanceScalingFactor("Motherboard", (int)Motherboard.DistanceScalingFactor1, (int)Motherboard.DistanceScalingFactor2, (int)Motherboard.DistanceScalingFactor3, (int)Motherboard.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("Motherboard", (int)Motherboard.UnlocklevelStage1, (int)Motherboard.UnlocklevelStage2, (int)Motherboard.UnlocklevelStage3, (int)Motherboard.UnlocklevelStage4);
            //gpu
            _parameterStorage.SetDistanceScalingFactor("GPU", (int)GPU.DistanceScalingFactor1, (int)GPU.DistanceScalingFactor2, (int)GPU.DistanceScalingFactor3, (int)GPU.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("GPU", (int)GPU.UnlocklevelStage1, (int)GPU.UnlocklevelStage2, (int)GPU.UnlocklevelStage3, (int)GPU.UnlocklevelStage4);
            //cpu
            _parameterStorage.SetDistanceScalingFactor("CPU", (int)CPU.DistanceScalingFactor1, (int)CPU.DistanceScalingFactor2, (int)CPU.DistanceScalingFactor3, (int)CPU.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("CPU", (int)CPU.UnlocklevelStage1, (int)CPU.UnlocklevelStage2, (int)CPU.UnlocklevelStage3, (int)CPU.UnlocklevelStage4);
            //ram
            _parameterStorage.SetDistanceScalingFactor("RAM", (int)RAM.DistanceScalingFactor1, (int)RAM.DistanceScalingFactor2, (int)RAM.DistanceScalingFactor3, (int)RAM.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("RAM", (int)RAM.UnlocklevelStage1, (int)RAM.UnlocklevelStage2, (int)RAM.UnlocklevelStage3, (int)RAM.UnlocklevelStage4);

#if UNITY_EDITOR
            // WriteDataLogFile();
#endif

        }
    }

    void Update()
    {
        //test
        if (calcProbs)
        {
            int caseProbs = SelectComponentstage("Case", 5);
            Debugger.LogMessage("selected comp: " + caseProbs);

            calcProbs = false;
        }
    }

    private float[] CalculateComponentstageProbabilities(int currentLevel, string componentName)
    {
        int[] distances = Calculate.DistanceCurrentLevelToEvolutionUnlockLevels(currentLevel, _parameterStorage.GetAllEvolutionUnlockLevels(componentName)); ;

        int[] scaledDistances = Calculate.ScaledDistance(distances, _parameterStorage.GetAllDistanceScalingFactors(componentName));

        int totalScaledDistance = Calculate.TotalScaledDistances(scaledDistances);

        return Calculate.Probabilities(scaledDistances, totalScaledDistance);
    }

    public byte SelectComponentstage(string componentName, int currentLevel)
    {
        byte returnedStage = 0;
        float[] stageProbabilities = CalculateComponentstageProbabilities(currentLevel, componentName);

        float p1 = stageProbabilities[Stage1];
        float p2 = stageProbabilities[Stage2];
        float p3 = stageProbabilities[Stage3];
        float p4 = stageProbabilities[Stage4];

        float scaledP1 = p1;
        float scaledP2 = scaledP1 + p2;
        float scaledP3 = scaledP2 + p3;
        float scaledP4 = scaledP3 + p4;

        float randomNumber = UnityEngine.Random.value;

        if (randomNumber < scaledP1) { return returnedStage = 1; }
        if (randomNumber < scaledP2) { return returnedStage = 2; }
        if (randomNumber < scaledP3) { return returnedStage = 3; }
        if (randomNumber < scaledP4) { return returnedStage = 4; }
        return returnedStage;   //if the randomNumber fits with nothing, 0 will be return and the Component class should run into an error
    }

    private void WriteDataLogFile()
    {
        const string FilePath = "Assets/Scripts/Order/dataLog.txt";
        const byte AmountLevels = 10;
        DateTime currentDateAndTime = DateTime.Now;

        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            writer.WriteLine("All data which are needed to generate the Order.");
            writer.WriteLine("To understand what they data are used for, please checkout the Order/README.md!");
            writer.WriteLine($"Generated: {currentDateAndTime.Day}.{currentDateAndTime.Month}.{currentDateAndTime.Year} at {currentDateAndTime.Hour}:{currentDateAndTime.Minute}:{currentDateAndTime.Second}");
            writer.WriteLine(" ");
            foreach (var component in ComponentNames)
            {
                writer.WriteLine($"---[Component {component}]---");
                writer.WriteLine("distanceScalingFactors:");
                int[] dsf = _parameterStorage.GetAllDistanceScalingFactors(component);
                writer.WriteLine("DistanceScalingFactor1: " + dsf[Stage1]);
                writer.WriteLine("DistanceScalingFactor2: " + dsf[Stage2]);
                writer.WriteLine("DistanceScalingFactor3: " + dsf[Stage3]);
                writer.WriteLine("DistanceScalingFactor4: " + dsf[Stage4]);
                writer.WriteLine(" ");
                writer.WriteLine("unlockEvolutionLevels:");
                int[] eul = _parameterStorage.GetAllEvolutionUnlockLevels(component);
                writer.WriteLine("evolutionUnlockLevel1: " + eul[Stage1]);
                writer.WriteLine("evolutionUnlockLevel2: " + eul[Stage2]);
                writer.WriteLine("evolutionUnlockLevel3: " + eul[Stage3]);
                writer.WriteLine("evolutionUnlockLevel4: " + eul[Stage4]);
                writer.WriteLine(" ");
                writer.WriteLine("Data that depends on current level:");
                for (int level = 1; level <= AmountLevels; level++)
                {
                    int[] dist = Calculate.DistanceCurrentLevelToEvolutionUnlockLevels(level, _parameterStorage.GetAllEvolutionUnlockLevels(component));
                    int[] scaledDist = Calculate.ScaledDistance(dist, _parameterStorage.GetAllDistanceScalingFactors(component));
                    int totalScaledDist = Calculate.TotalScaledDistances(scaledDist);
                    float[] prob = Calculate.Probabilities(scaledDist, totalScaledDist);

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
}