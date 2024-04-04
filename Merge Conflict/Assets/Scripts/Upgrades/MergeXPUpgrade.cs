using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeXPUpgrade : Upgrade
{
    public int[] PercentageOfSalesXP; // for current level

    public MergeXPUpgrade(int[] percentageOfSalesXP, int[] costForNextLevel) : base("MergeXP", costForNextLevel)
    {
        PercentageOfSalesXP = percentageOfSalesXP;

        Debugger.LogErrorIf(percentageOfSalesXP.Length - 1 != costForNextLevel.Length,
            $"Values for {Name} do not match!");
    }

    public int GetCurrentPercentageOfSalesXP()
    {
        return PercentageOfSalesXP[Level];
    }

    public override string GetCurrentLevelDescription()
    {
        return $"{GetCurrentLevelDescription()} %";
    }

    public override string GetNextLevelDescription()
    {
        if (isAtMaxLevel())
        {
            return "-";
        }

        return $"{PercentageOfSalesXP[Level]} %";
    }
}
