using System.Collections.Generic;

public class ParameterStorage
{
    Dictionary<string, int> componentList = new Dictionary<string, int>();
    private int[,] _evolutionUnlockLevels = new int[7, 4];    //[1...7components, 1=unlocklevel for stage1, 2=unlocklevel for stage2,...]
    private int[,] _distanceScalingfactors = new int[7, 4];      //[1...7components, 1=scaling factor for stage1, 2=scalingFactor for stage2,...]

    const byte offsetArrayindexToStage = 1; //need this because the array counts from 0...3 but we will give the evolutionStages from 1...4 because its more natural for humans

    public ParameterStorage()
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

    public void SetEvolutionUnlocklevel(string componentName, params int[] unlockLevels)
    {
        if (ComponentNotListed(componentName)) { return; }

        int componentIndexFromList = ComponentIndexFromList(componentName);

        for (int evolutionStage = 0; evolutionStage < unlockLevels.Length; evolutionStage++)
        {
            _evolutionUnlockLevels[componentIndexFromList, evolutionStage] = unlockLevels[evolutionStage];
        }
    }

    public int GetEvolutionUnlocklevel(string componentName, int evolutionStage)
    {
        if (ComponentNotListed(componentName)) { return 0; };

        int componentIndexFromList = ComponentIndexFromList(componentName);

        return _evolutionUnlockLevels[componentIndexFromList, evolutionStage - offsetArrayindexToStage];
    }

    public void SetDistanceScalingFactor(string componentName, params int[] distanceScalingFactors)
    {
        if (ComponentNotListed(componentName)) { return; }

        int componentIndexFromList = ComponentIndexFromList(componentName);

        for (int stage = 0; stage < distanceScalingFactors.Length; stage++)
        {
            _distanceScalingfactors[componentIndexFromList, stage] = distanceScalingFactors[stage];
        }
    }

    public int GetDistanceScalingFactor(string componentName, int evolutionStage)
    {
        if (ComponentNotListed(componentName)) { return 0; };

        int componentIndexFromList = ComponentIndexFromList(componentName);

        return _distanceScalingfactors[componentIndexFromList, evolutionStage - offsetArrayindexToStage];
    }

    public int[] GetAllDistanceScalingFactors(string componentName)
    {
        if (ComponentNotListed(componentName)) { return null; };

        int[] scalingFactors = new int[4];
        for (int evolutionStage = 0; evolutionStage < 4; evolutionStage++)
        {
            scalingFactors[evolutionStage] = GetDistanceScalingFactor(componentName, evolutionStage + offsetArrayindexToStage);
        }

        return scalingFactors;
    }

    public int[] GetAllEvolutionUnlockLevels(string componentName)
    {
        if (ComponentNotListed(componentName)) { return null; };

        int[] unlockLevels = new int[4];
        for (int evolutionStage = 0; evolutionStage < 4; evolutionStage++)
        {
            unlockLevels[evolutionStage] = GetEvolutionUnlocklevel(componentName, evolutionStage + offsetArrayindexToStage);
        }

        return unlockLevels;
    }
}