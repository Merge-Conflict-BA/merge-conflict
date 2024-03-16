using System.Collections.Generic;

public class CalculationConstantStorage
{
    Dictionary<string, int> componentList = new Dictionary<string, int>();
    private int[,] _evolutionUnlockLevel = new int[7, 5];    //[1...7components, 1=unlocklevel for stage1, 2=unlocklevel for stage2,...]
    private int[,] _distanceMultiplier = new int[7, 5];      //[1...7components, 1=distance multiplier for stage1, 2=distance multiplier for stage2,...]

    const byte offsetArrayindexToStage = 1;

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

    public void SetEvolutionUnlocklevelForComponent(string componentName, params int[] unlockLevels)
    {
        if (ComponentNotListed(componentName)) { return; }

        int componentIndexFromList = ComponentIndexFromList(componentName);

        for (int evolutionStage = 0; evolutionStage < unlockLevels.Length; evolutionStage++)
        {
            _evolutionUnlockLevel[componentIndexFromList, evolutionStage] = unlockLevels[evolutionStage];
        }
    }

    public int GetEvolutionUnlocklevelFromComponant(string componentName, int evolutionStage)
    {
        if (ComponentNotListed(componentName)) { return 0; };

        int componentIndexFromList = ComponentIndexFromList(componentName);

        return _evolutionUnlockLevel[componentIndexFromList, evolutionStage - offsetArrayindexToStage];
    }

    public void SetDistanceMultiplierForComponent(string componentName, params int[] distanceMultipliers)
    {
        if (ComponentNotListed(componentName)) { return; }

        int componentIndexFromList = ComponentIndexFromList(componentName);

        for (int stage = 0; stage < distanceMultipliers.Length; stage++)
        {
            _distanceMultiplier[componentIndexFromList, stage] = distanceMultipliers[stage];
        }
    }

    public int GetDistanceMultiplierForComponant(string componentName, int evolutionStage)
    {
        if (ComponentNotListed(componentName)) { return 0; };

        int componentIndexFromList = ComponentIndexFromList(componentName);

        return _distanceMultiplier[componentIndexFromList, evolutionStage - offsetArrayindexToStage];
    }

    public int[] GetAllDistanceMultipliers(string componentName)
    {
        if (ComponentNotListed(componentName)) { return null; };

        int[] multiplier = new int[4];
        multiplier[0] = GetDistanceMultiplierForComponant(componentName, 1);
        multiplier[1] = GetDistanceMultiplierForComponant(componentName, 2);
        multiplier[2] = GetDistanceMultiplierForComponant(componentName, 3);
        multiplier[3] = GetDistanceMultiplierForComponant(componentName, 4);

        return multiplier;
    }

    public int[] GetAllEvolutionUnlockLevels(string componentName)
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