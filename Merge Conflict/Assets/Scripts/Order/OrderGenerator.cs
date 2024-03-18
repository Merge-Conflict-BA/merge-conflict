using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    private ParameterStorage _parameterStorage;
    public int currentLevel = 1;
    public bool writeLog = false;


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
            _instance = this;
            _parameterStorage = new ParameterStorage();

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
        //debugging & test
        // Debugger.LogMessage("case multiplier should be 2: " + _parameterStorage.GetDistanceScalingFactor("HDD", 2));
        // Debugger.LogMessage("case unlocklevel should be 4: " + _parameterStorage.GetEvolutionUnlocklevel("HDD", 3));


        // int[] temp = _parameterStorage.GetAllEvolutionUnlockLevels("HDD");
        // int[] temp2 = _parameterStorage.GetAllDistanceMultipliers("Case");
        //Debugger.LogMessage($"All unlockLevels case should be 1,2,4,6: {temp[0]}, {temp[1]}, {temp[2]}, {temp[3]}");
        // Debugger.LogMessage($"All distanceMulti case should be 1,2,3,4: {temp2[0]}, {temp2[1]}, {temp2[2]}, {temp2[3]}");

        //test DistanceCurrentLevelToEvolutionUnlockLevels()
        int[] distances = Calculate.DistanceCurrentLevelToEvolutionUnlockLevels(currentLevel, _parameterStorage.GetAllEvolutionUnlockLevels("Case"));
        // Debugger.LogMessage("Distance1: " + distances[0]);
        // Debugger.LogMessage("Distance2: " + distances[1]);
        // Debugger.LogMessage("Distance3: " + distances[2]);
        // Debugger.LogMessage("Distance4: " + distances[3]);

        //test DistanceCurrentLevelToEvolutionUnlockLevels()
        int[] scaledDistances = Calculate.ScaledDistance(distances, _parameterStorage.GetAllDistanceScalingFactors("Case"));
        // Debugger.LogMessage("Distance1: " + scaledDistances[0]);
        // Debugger.LogMessage("Distance2: " + scaledDistances[1]);
        // Debugger.LogMessage("Distance3: " + scaledDistances[2]);
        // Debugger.LogMessage("Distance4: " + scaledDistances[3]);

        //test TotalScaledDistances()
        int totalScaledDistance = Calculate.TotalScaledDistances(scaledDistances);
        // Debugger.LogMessage("total scaledDistances: " + totalScaledDistance);

        //test Probabilities()
        int[] probabilities = Calculate.Probabilities(scaledDistances, totalScaledDistance);
        // Debugger.LogMessage("Probability1: " + probabilities[0]);
        // Debugger.LogMessage("Probability2: " + probabilities[1]);
        // Debugger.LogMessage("Probability3: " + probabilities[2]);
        // Debugger.LogMessage("Probability4: " + probabilities[3]);


        if (writeLog)
        {
            WriteDataLogFile();
            writeLog = false;
        }


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
                    int[] prob = Calculate.Probabilities(scaledDist, totalScaledDist);

                    writer.WriteLine($" Level {level}");
                    writer.WriteLine($"  totalScaledDistance: " + totalScaledDist);
                    writer.WriteLine($"  distance1: " + dist[stage1]);
                    writer.WriteLine($"  scaledDistance1: " + scaledDist[stage1]);
                    writer.WriteLine($"  probability1: " + prob[stage1]);
                    writer.WriteLine(" ");
                    writer.WriteLine($"  distance2: " + dist[stage2]);
                    writer.WriteLine($"  scaledDistance2: " + scaledDist[stage2]);
                    writer.WriteLine($"  probability2: " + prob[stage2]);
                    writer.WriteLine(" ");
                    writer.WriteLine($"  distance3: " + dist[stage3]);
                    writer.WriteLine($"  scaledDistance3: " + scaledDist[stage3]);
                    writer.WriteLine($"  probability3: " + prob[stage3]);
                    writer.WriteLine(" ");
                    writer.WriteLine($"  distance4: " + dist[stage4]);
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