using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    private static OrderGenerator _instance;
    public static OrderGenerator Instance { get { return _instance; } }
    private CalculationConstantStorage _constantsStorage;
    public bool setData = false;


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
            _constantsStorage = new CalculationConstantStorage();

            //case
            _constantsStorage.SetDistanceMultiplierForComponant("Case", (int)Case.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("Case", (int)Case.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //hdd
            _constantsStorage.SetDistanceMultiplierForComponant("HDD", (int)HDD.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("HDD", (int)HDD.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //powersupply
            _constantsStorage.SetDistanceMultiplierForComponant("Powersupply", (int)Powersupply.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("Powersupply", (int)Powersupply.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //motherboard
            _constantsStorage.SetDistanceMultiplierForComponant("Motherboard", (int)Motherboard.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("Motherboard", (int)Motherboard.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //gpu
            _constantsStorage.SetDistanceMultiplierForComponant("GPU", (int)GPU.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("GPU", (int)GPU.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //cpu
            _constantsStorage.SetDistanceMultiplierForComponant("CPU", (int)CPU.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("CPU", (int)CPU.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
            //ram
            _constantsStorage.SetDistanceMultiplierForComponant("RAM", (int)RAM.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("RAM", (int)RAM.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);
        }
    }

    void Update()
    {
        Debugger.LogMessage("case multiplier should be 2: " + _constantsStorage.GetDistanceMultiplierForComponant("Case", 2));
        Debugger.LogMessage("case unlocklevel should be 4: " + _constantsStorage.GetEvolutionUnlocklevelFromComponant("Case", 3));

        if (setData)
        {
            _constantsStorage.SetDistanceMultiplierForComponant("Case", (int)Case.DistanceMultiplierStage1, (int)Case.DistanceMultiplierStage2, (int)Case.DistanceMultiplierStage3, (int)Case.DistanceMultiplierStage4);
            _constantsStorage.SetEvolutionUnlocklevelForComponant("Case", (int)Case.UnlocklevelStage1, (int)Case.UnlocklevelStage2, (int)Case.UnlocklevelStage3, (int)Case.UnlocklevelStage4);

            setData = false;
        }

    }


}
