using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyWhenTrashedUpgrade : Upgrade
{
    public int[] PercentageOfTrashMoney; // for current level

    public MoneyWhenTrashedUpgrade(int[] percentageOfTrashMoney, int[] costForNextLevel) : base("MoneyWhenTrashed", costForNextLevel)
    {
        PercentageOfTrashMoney = percentageOfTrashMoney;

        Debugger.LogErrorIf(percentageOfTrashMoney.Length - 1 != costForNextLevel.Length,
            $"Values for {Name} do not match!");
    }

    public int GetCurrentPercentageOfTrashMoney()
    {
        return PercentageOfTrashMoney[Level];
    }

    public override string GetCurrentLevelDescription()
    {
        return $"Granding {GetCurrentPercentageOfTrashMoney()}% money for discarding trash";
    }

    public override string GetNextLevelDescription()
    {
        if (isAtMaxLevel())
        {
            return "-";
        }

        return $"{PercentageOfTrashMoney[Level + 1]}%";
    }
}
