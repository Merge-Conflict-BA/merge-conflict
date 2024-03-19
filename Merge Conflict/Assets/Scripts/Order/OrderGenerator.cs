using System;
using System.IO;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    private ParameterStorage _parameterStorage;
    public byte currentLevel = 1;
    public bool writeLog = false;

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _parameterStorage = new ParameterStorage(Const.ComponentNames);

            //case
            _parameterStorage.SetDistanceScalingFactor("Case", (int)Const.Case.DistanceScalingFactor1, (int)Const.Case.DistanceScalingFactor2, (int)Const.Case.DistanceScalingFactor3, (int)Const.Case.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("Case", (int)Const.Case.UnlocklevelStage1, (int)Const.Case.UnlocklevelStage2, (int)Const.Case.UnlocklevelStage3, (int)Const.Case.UnlocklevelStage4);
            //hdd
            _parameterStorage.SetDistanceScalingFactor("HDD", (int)Const.HDD.DistanceScalingFactor1, (int)Const.HDD.DistanceScalingFactor2, (int)Const.HDD.DistanceScalingFactor3, (int)Const.HDD.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("HDD", (int)Const.HDD.UnlocklevelStage1, (int)Const.HDD.UnlocklevelStage2, (int)Const.HDD.UnlocklevelStage3, (int)Const.HDD.UnlocklevelStage4);
            //powersupply
            _parameterStorage.SetDistanceScalingFactor("Powersupply", (int)Const.Powersupply.DistanceScalingFactor1, (int)Const.Powersupply.DistanceScalingFactor2, (int)Const.Powersupply.DistanceScalingFactor3, (int)Const.Powersupply.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("Powersupply", (int)Const.Powersupply.UnlocklevelStage1, (int)Const.Powersupply.UnlocklevelStage2, (int)Const.Powersupply.UnlocklevelStage3, (int)Const.Powersupply.UnlocklevelStage4);
            //motherboard
            _parameterStorage.SetDistanceScalingFactor("Motherboard", (int)Const.Motherboard.DistanceScalingFactor1, (int)Const.Motherboard.DistanceScalingFactor2, (int)Const.Motherboard.DistanceScalingFactor3, (int)Const.Motherboard.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("Motherboard", (int)Const.Motherboard.UnlocklevelStage1, (int)Const.Motherboard.UnlocklevelStage2, (int)Const.Motherboard.UnlocklevelStage3, (int)Const.Motherboard.UnlocklevelStage4);
            //gpu
            _parameterStorage.SetDistanceScalingFactor("GPU", (int)Const.GPU.DistanceScalingFactor1, (int)Const.GPU.DistanceScalingFactor2, (int)Const.GPU.DistanceScalingFactor3, (int)Const.GPU.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("GPU", (int)Const.GPU.UnlocklevelStage1, (int)Const.GPU.UnlocklevelStage2, (int)Const.GPU.UnlocklevelStage3, (int)Const.GPU.UnlocklevelStage4);
            //cpu
            _parameterStorage.SetDistanceScalingFactor("CPU", (int)Const.CPU.DistanceScalingFactor1, (int)Const.CPU.DistanceScalingFactor2, (int)Const.CPU.DistanceScalingFactor3, (int)Const.CPU.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("CPU", (int)Const.CPU.UnlocklevelStage1, (int)Const.CPU.UnlocklevelStage2, (int)Const.CPU.UnlocklevelStage3, (int)Const.CPU.UnlocklevelStage4);
            //ram
            _parameterStorage.SetDistanceScalingFactor("RAM", (int)Const.RAM.DistanceScalingFactor1, (int)Const.RAM.DistanceScalingFactor2, (int)Const.RAM.DistanceScalingFactor3, (int)Const.RAM.DistanceScalingFactor4);
            _parameterStorage.SetEvolutionUnlocklevel("RAM", (int)Const.RAM.UnlocklevelStage1, (int)Const.RAM.UnlocklevelStage2, (int)Const.RAM.UnlocklevelStage3, (int)Const.RAM.UnlocklevelStage4);

#if UNITY_EDITOR
    WriteDataLogFile();
#endif

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

        float p1 = stageProbabilities[Const.Stage1];
        float p2 = stageProbabilities[Const.Stage2];
        float p3 = stageProbabilities[Const.Stage3];
        float p4 = stageProbabilities[Const.Stage4];

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
            foreach (var component in Const.ComponentNames)
            {
                writer.WriteLine($"---[Component {component}]---");
                writer.WriteLine("distanceScalingFactors:");
                int[] dsf = _parameterStorage.GetAllDistanceScalingFactors(component);
                writer.WriteLine("DistanceScalingFactor1: " + dsf[Const.Stage1]);
                writer.WriteLine("DistanceScalingFactor2: " + dsf[Const.Stage2]);
                writer.WriteLine("DistanceScalingFactor3: " + dsf[Const.Stage3]);
                writer.WriteLine("DistanceScalingFactor4: " + dsf[Const.Stage4]);
                writer.WriteLine(" ");
                writer.WriteLine("unlockEvolutionLevels:");
                int[] eul = _parameterStorage.GetAllEvolutionUnlockLevels(component);
                writer.WriteLine("evolutionUnlockLevel1: " + eul[Const.Stage1]);
                writer.WriteLine("evolutionUnlockLevel2: " + eul[Const.Stage2]);
                writer.WriteLine("evolutionUnlockLevel3: " + eul[Const.Stage3]);
                writer.WriteLine("evolutionUnlockLevel4: " + eul[Const.Stage4]);
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
                    writer.WriteLine($" |-distanceToUnlockLevel1: " + dist[Const.Stage1]);
                    writer.WriteLine($" |-scaledDistance1: " + scaledDist[Const.Stage1]);
                    writer.WriteLine($" |-probability1: " + prob[Const.Stage1]);
                    writer.WriteLine(" |");
                    writer.WriteLine($" |-distanceToUnlockLevel2: " + dist[Const.Stage2]);
                    writer.WriteLine($" |-scaledDistance2: " + scaledDist[Const.Stage2]);
                    writer.WriteLine($" |-probability2: " + prob[Const.Stage2]);
                    writer.WriteLine(" |");
                    writer.WriteLine($" |-distanceToUnlockLevel3: " + dist[Const.Stage3]);
                    writer.WriteLine($" |-scaledDistance3: " + scaledDist[Const.Stage3]);
                    writer.WriteLine($" |-probability3: " + prob[Const.Stage3]);
                    writer.WriteLine(" |");
                    writer.WriteLine($" |-distanceToUnlockLevel4: " + dist[Const.Stage4]);
                    writer.WriteLine($" |-scaledDistance4: " + scaledDist[Const.Stage4]);
                    writer.WriteLine($" |-probability4: " + prob[Const.Stage4]);
                    writer.WriteLine(" ");
                }
                writer.WriteLine("");
                writer.WriteLine("");
            }
        }
    }
}