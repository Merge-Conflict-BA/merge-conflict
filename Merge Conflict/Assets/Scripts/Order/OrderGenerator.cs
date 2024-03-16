using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    private ParameterStorage _parameterStorage;
    public int currentLevel = 1;


    //define calculation parameters for component
    //case
    public enum Case
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 2,
        UnlocklevelStage3 = 4,
        UnlocklevelStage4 = 6,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
    }
    public enum HDD
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 3,
        UnlocklevelStage3 = 5,
        UnlocklevelStage4 = 7,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
    }
    public enum Powersupply
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 4,
        UnlocklevelStage3 = 6,
        UnlocklevelStage4 = 8,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
    }
    public enum Motherboard
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 5,
        UnlocklevelStage3 = 7,
        UnlocklevelStage4 = 9,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
    }
    public enum GPU
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 6,
        UnlocklevelStage3 = 8,
        UnlocklevelStage4 = 10,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
    }
    public enum CPU
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 6,
        UnlocklevelStage3 = 8,
        UnlocklevelStage4 = 10,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
    }
    public enum RAM
    {
        UnlocklevelStage1 = 1,
        UnlocklevelStage2 = 7,
        UnlocklevelStage3 = 9,
        UnlocklevelStage4 = 10,
        DistanceMultiplierStage1 = 1,
        DistanceMultiplierStage2 = 2,
        DistanceMultiplierStage3 = 3,
        DistanceMultiplierStage4 = 4,
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
            _parameterStorage.SetDistanceMultiplierForComponent("Case", (int)Case.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("Case", (int)Case.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //hdd
            _parameterStorage.SetDistanceMultiplierForComponent("HDD", (int)HDD.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("HDD", (int)HDD.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //powersupply
            _parameterStorage.SetDistanceMultiplierForComponent("Powersupply", (int)Powersupply.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("Powersupply", (int)Powersupply.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //motherboard
            _parameterStorage.SetDistanceMultiplierForComponent("Motherboard", (int)Motherboard.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("Motherboard", (int)Motherboard.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //gpu
            _parameterStorage.SetDistanceMultiplierForComponent("GPU", (int)GPU.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("GPU", (int)GPU.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //cpu
            _parameterStorage.SetDistanceMultiplierForComponent("CPU", (int)CPU.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("CPU", (int)CPU.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //ram
            _parameterStorage.SetDistanceMultiplierForComponent("RAM", (int)RAM.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _parameterStorage.SetEvolutionUnlocklevelForComponent("RAM", (int)RAM.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
        }
    }

    void Update()
    {
        //debugging & test
        // Debugger.LogMessage("case multiplier should be 2: " + _constantsStorage.GetDistanceMultiplierForComponant("Case", 2));
        // Debugger.LogMessage("case unlocklevel should be 4: " + _constantsStorage.GetEvolutionUnlocklevelFromComponant("Case", 3));


        // int[] temp = _parameterStorage.GetAllEvolutionUnlockLevels("Case");
        // int[] temp2 = _parameterStorage.GetAllDistanceMultipliers("Case");
        // Debugger.LogMessage($"All unlockLevels case should be 1,2,4,6: {temp[0]}, {temp[1]}, {temp[2]}, {temp[3]}");
        // Debugger.LogMessage($"All distanceMulti case should be 1,2,3,4: {temp2[0]}, {temp2[1]}, {temp2[2]}, {temp2[3]}");

        
        int[] distances = Calculate.DistanceCurrentLevelToEvolutionUnlockLevels(currentLevel, _parameterStorage.GetAllEvolutionUnlockLevels("Case"));
        Debugger.LogMessage("Distance1: " + distances[0]);
        Debugger.LogMessage("Distance2: " + distances[1]);
        Debugger.LogMessage("Distance3: " + distances[2]);
        Debugger.LogMessage("Distance4: " + distances[3]);




    }







}