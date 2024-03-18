using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public class ParameterStorage
{
    Dictionary<string, int> componentList = new Dictionary<string, int>();
    const byte offsetArrayindexToStage = 1; //need this because the array counts from 0...3 but we will give the evolutionStages from 1...4 because its more natural for humans
    const byte AmountStages = 4;
    const byte AmountComponents = 7;
    private int[,] _evolutionUnlockLevels = new int[AmountComponents, AmountStages];    //[1...7components, 1=unlocklevel for stage1, 2=unlocklevel for stage2,...]
    private int[,] _distanceScalingfactors = new int[AmountComponents, AmountStages];      //[1...7components, 1=scaling factor for stage1, 2=scalingFactor for stage2,...]

    public ParameterStorage(params string[] componentNames)
    {

        for (int listIndex = 0; listIndex < componentNames.Length; listIndex++)
        {
            componentList.Add(componentNames[listIndex], listIndex);
        }
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

        for (int evolutionStage = 0; evolutionStage < AmountStages; evolutionStage++)
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

    public int[] GetAllEvolutionUnlockLevels(string componentName)
    {
        if (ComponentNotListed(componentName)) { return null; };

        int[] unlockLevels = new int[AmountStages];
        for (int evolutionStage = 0; evolutionStage < AmountStages; evolutionStage++)
        {
            unlockLevels[evolutionStage] = GetEvolutionUnlocklevel(componentName, evolutionStage + offsetArrayindexToStage);
        }

        return unlockLevels;
    }

    public void SetDistanceScalingFactor(string componentName, params int[] distanceScalingFactors)
    {
        if (ComponentNotListed(componentName)) { return; }

        int componentIndexFromList = ComponentIndexFromList(componentName);

        for (int stage = 0; stage < AmountStages; stage++)
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

        int[] scalingFactors = new int[AmountStages];
        for (int evolutionStage = 0; evolutionStage < AmountStages; evolutionStage++)
        {
            scalingFactors[evolutionStage] = GetDistanceScalingFactor(componentName, evolutionStage + offsetArrayindexToStage);
        }

        return scalingFactors;
    }


}