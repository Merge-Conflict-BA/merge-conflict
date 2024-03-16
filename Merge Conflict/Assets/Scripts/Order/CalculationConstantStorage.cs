using System.Collections.Generic;
using UnityEngine;

public class CalculationConstantStorage
{
    Dictionary<string, int> componentList = new Dictionary<string, int>();
    private int[,] _evolutionUnlockLevel = new int[7, 5];    //[1...7components, 0=unlocklevel for stage1, 1=unlocklevel for stage2,...]
    private int[,] _distanceMultiplier = new int[7, 5];      //[1...7components, 0=distance multiplier for stage1, 1=distance multiplier for stage2,...]

    public CalculationConstantStorage()
    {
        componentList.Add("RAM", 0);
        componentList.Add("HDD", 1);
        componentList.Add("GPU", 2);
        componentList.Add("CPU", 3);
        componentList.Add("Case", 4);
        componentList.Add("Motherboard", 5);
        componentList.Add("Powersupply", 6);
    }

    private bool ComponentNotListed(string componentName)
    {
        if (!componentList.ContainsKey(componentName))
        {
            Debugger.LogError($"The component with the name: {componentName} doesn't exists! Please use one of this: RAM, CPU, GPU, HDD, Motherboard, Powersupply, Case.");
            return true;
        }

        return false;
    }

    private int ComponentIndexFromList(string componentName)
    {
        return componentList[componentName];
    }

    public void SetEvolutionUnlocklevelForComponant(string componentName, int unlockLevel1, int unlockLevel2, int unlockLevel3, int unlockLevel4)
    {
        if (ComponentNotListed(componentName)) { return; };

        //int[] unlockLevels = { unlockLevel1, unlockLevel2, unlockLevel3, unlockLevel4 };

        int componentIndexFromList = ComponentIndexFromList(componentName);
        
        _evolutionUnlockLevel[componentIndexFromList, 1] = unlockLevel1;
        _evolutionUnlockLevel[componentIndexFromList, 2] = unlockLevel2;
        _evolutionUnlockLevel[componentIndexFromList, 3] = unlockLevel3;
        _evolutionUnlockLevel[componentIndexFromList, 4] = unlockLevel4;
        
        // for (int currentUnlockLevel = 1; currentUnlockLevel <= unlockLevels.Length; currentUnlockLevel++)
        // {
        //     _evolutionUnlockLevel[componentIndexFromList, currentUnlockLevel] = unlockLevels[currentUnlockLevel];
        // }
    }

    public int GetEvolutionUnlocklevelFromComponant(string componentName, int stage)
    {
        if (ComponentNotListed(componentName)) { return 0; };

        int componentIndexFromList = ComponentIndexFromList(componentName);

        return _evolutionUnlockLevel[componentIndexFromList, stage];
    }

    public void SetDistanceMultiplierForComponant(string componentName, int multiplierStage1, int multiplierStage2, int multiplierStage3, int multiplierStage4)
    {
        if (ComponentNotListed(componentName)) { return; };

        //int[] multipliers = { multiplierStage1, multiplierStage2, multiplierStage3, multiplierStage4 };

        int componentIndexFromList = ComponentIndexFromList(componentName);
        
        _distanceMultiplier[componentIndexFromList, 1] = multiplierStage1;
        _distanceMultiplier[componentIndexFromList, 2] = multiplierStage2;
        _distanceMultiplier[componentIndexFromList, 3] = multiplierStage3;
        _distanceMultiplier[componentIndexFromList, 4] = multiplierStage4;
        
        // for (int currentMultiplier = 1; currentMultiplier <= multipliers.Length; currentMultiplier++)
        // {
        //     _distanceMultiplier[componentIndexFromList, currentMultiplier] = multipliers[currentMultiplier];
        // }
    }

    public int GetDistanceMultiplierForComponant(string componentName, int stage)
    {
        if (ComponentNotListed(componentName)) { return 0; };

        int componentIndexFromList = ComponentIndexFromList(componentName);

        return _distanceMultiplier[componentIndexFromList, stage];
    }

    public int[] GetAllDistanceMultiplier(string componentName)
    {
        if (ComponentNotListed(componentName)) { return null; };

        int[] multiplier = new int[4];
        multiplier[0] = GetDistanceMultiplierForComponant(componentName, 1);
        multiplier[1] = GetDistanceMultiplierForComponant(componentName, 2);
        multiplier[2] = GetDistanceMultiplierForComponant(componentName, 3);
        multiplier[3] = GetDistanceMultiplierForComponant(componentName, 4);

        return multiplier;
    }

    public int[] GetAllEvolutionUnlockLevel(string componentName)
    {
        if (ComponentNotListed(componentName)) { return null; };

        int[] multiplier = new int[4];
        multiplier[0] = GetEvolutionUnlocklevelFromComponant(componentName, 1);
        multiplier[1] = GetEvolutionUnlocklevelFromComponant(componentName, 2);
        multiplier[2] = GetEvolutionUnlocklevelFromComponant(componentName, 3);
        multiplier[3] = GetEvolutionUnlocklevelFromComponant(componentName, 4);

        return multiplier;
    }





}