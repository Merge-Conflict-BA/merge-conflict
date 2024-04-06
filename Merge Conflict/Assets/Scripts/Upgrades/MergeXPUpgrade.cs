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
        return $"Granting {GetCurrentPercentageOfSalesXP()}% more XP for component merging";
    }

    public override string GetNextLevelDescription()
    {
        if (isAtMaxLevel())
        {
            return "-";
        }

        return $"{PercentageOfSalesXP[Level + 1]}%";
    }
}
