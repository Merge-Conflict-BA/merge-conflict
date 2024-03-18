using System;
using System.IO;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    private ParameterStorage _parameterStorage;
    public int currentLevel = 1;
    public bool writeLog = false;
    const byte InitialLevel = 1;
    const byte EndLevel = 10;
    const byte stage1 = 0;
    const byte stage2 = 1;
    const byte stage3 = 2;
    const byte stage4 = 3;


    public bool calcProbs = false;


    //define calculation parameters for component
    //case
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

            string[] ComponentNames = { "RAM", "HDD", "GPU", "CPU", "Case", "Motherboard", "Powersupply" };
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
        }
    }

    void Update()
    {
        if (writeLog)
        {
            WriteDataLogFile();
            writeLog = false;
        }

        if (calcProbs)
        {
            int caseProbs = SelectComponentstage("Case", 5);
            Debugger.LogMessage("selected comp: " + caseProbs);

            calcProbs = false;
        }
    }

    private float[] CalculateComponentStageProbabilities(int currentLevel, string componentName)
    {
        //return null if current level is not valid 
        if (currentLevel != Math.Clamp(currentLevel, InitialLevel, EndLevel)) 
        { 
            Debugger.LogError("The given level in CalculateComponentStageProbabilities() is not valid! Check if the value is within the borders InitialLevel and EndLEvel.");
            return null; 
            };

        int[] distances = Calculate.DistanceCurrentLevelToEvolutionUnlockLevels(currentLevel, _parameterStorage.GetAllEvolutionUnlockLevels(componentName)); ;

        int[] scaledDistances = Calculate.ScaledDistance(distances, _parameterStorage.GetAllDistanceScalingFactors(componentName));

        int totalScaledDistance = Calculate.TotalScaledDistances(scaledDistances);

        return Calculate.Probabilities(scaledDistances, totalScaledDistance);
    }

    public int SelectComponentstage(string componentName, int currentLevel)
    {
        float[] stageProbabilities = CalculateComponentStageProbabilities(currentLevel, componentName);

        float p1 = stageProbabilities[stage1];
        float p2 = stageProbabilities[stage2];
        float p3 = stageProbabilities[stage3];
        float p4 = stageProbabilities[stage4];

        float scaledP1 = p1;
        float scaledP2 = scaledP1 + p2;
        float scaledP3 = scaledP2 + p3;
        float scaledP4 = scaledP3 + p4;

        float randomNumber = UnityEngine.Random.value;

        if (randomNumber < scaledP1){return 1;}
        if (randomNumber < scaledP2){return 2;}
        if (randomNumber < scaledP3){return 3;}
        else{return 4;}

    }

    private void WriteDataLogFile()
    {
        const string FilePath = "Assets/Scripts/Order/dataLog.txt";
        const byte AmountLevel = 10;
        const byte stage1 = 0;
        const byte stage2 = 1;
        const byte stage3 = 2;
        const byte stage4 = 3;

        using (StreamWriter writer = new StreamWriter(FilePath))
        {

            string[] components = { "Case", "HDD", "Powersupply", "Motherboard", "GPU", "CPU", "RAM" };


            foreach (var component in components)
            {
                writer.WriteLine($"---[Component {component}]---");
                writer.WriteLine("distanceScalingFactors:");
                int[] dsf = _parameterStorage.GetAllDistanceScalingFactors(component);
                writer.WriteLine("DistanceScalingFactor1: " + dsf[stage1]);
                writer.WriteLine("DistanceScalingFactor2: " + dsf[stage2]);
                writer.WriteLine("DistanceScalingFactor3: " + dsf[stage3]);
                writer.WriteLine("DistanceScalingFactor4: " + dsf[stage4]);
                writer.WriteLine(" ");
                writer.WriteLine("unlockEvolutionLevels:");
                int[] eul = _parameterStorage.GetAllEvolutionUnlockLevels(component);
                writer.WriteLine("evolutionUnlockLevel1: " + eul[stage1]);
                writer.WriteLine("evolutionUnlockLevel2: " + eul[stage2]);
                writer.WriteLine("evolutionUnlockLevel3: " + eul[stage3]);
                writer.WriteLine("evolutionUnlockLevel4: " + eul[stage4]);
                writer.WriteLine(" ");
                writer.WriteLine("Data that depends on current level:");
                for (int level = 1; level <= AmountLevel; level++)
                {
                    int[] dist = Calculate.DistanceCurrentLevelToEvolutionUnlockLevels(level, _parameterStorage.GetAllEvolutionUnlockLevels(component));
                    int[] scaledDist = Calculate.ScaledDistance(dist, _parameterStorage.GetAllDistanceScalingFactors(component));
                    int totalScaledDist = Calculate.TotalScaledDistances(scaledDist);
                    float[] prob = Calculate.Probabilities(scaledDist, totalScaledDist);

                    writer.WriteLine($" Level {level}");
                    writer.WriteLine($"  totalScaledDistance: " + totalScaledDist);
                    writer.WriteLine($"  distanceToUnlockLevel1: " + dist[stage1]);
                    writer.WriteLine($"  scaledDistance1: " + scaledDist[stage1]);
                    writer.WriteLine($"  probability1: " + prob[stage1]);
                    writer.WriteLine(" ");
                    writer.WriteLine($"  distanceToUnlockLevel2: " + dist[stage2]);
                    writer.WriteLine($"  scaledDistance2: " + scaledDist[stage2]);
                    writer.WriteLine($"  probability2: " + prob[stage2]);
                    writer.WriteLine(" ");
                    writer.WriteLine($"  distanceToUnlockLevel3: " + dist[stage3]);
                    writer.WriteLine($"  scaledDistance3: " + scaledDist[stage3]);
                    writer.WriteLine($"  probability3: " + prob[stage3]);
                    writer.WriteLine(" ");
                    writer.WriteLine($"  distanceToUnlockLevel4: " + dist[stage4]);
                    writer.WriteLine($"  scaledDistance4: " + scaledDist[stage4]);
                    writer.WriteLine($"  probability4: " + prob[stage4]);
                    writer.WriteLine(" ");
                }
                writer.WriteLine("");
                writer.WriteLine("");
            }
        }

        Debugger.LogMessage("All data are written to the file: dataLog.txt");
    }
}