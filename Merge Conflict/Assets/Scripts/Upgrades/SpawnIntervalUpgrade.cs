using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIntervalUpgrade : Upgrade
{
    public int[] SecondsInterval; // for current level

    public SpawnIntervalUpgrade(int[] secondsInterval, int[] costForNextLevel) : base("SpawnInterval", costForNextLevel)
    {
        SecondsInterval = secondsInterval;

        Debugger.LogErrorIf(secondsInterval.Length - 1 != costForNextLevel.Length,
            $"Values for {Name} do not match!");
    }

    public int GetCurrentSecondsInterval()
    {
        return SecondsInterval[Level];
    }

    public override string GetCurrentLevelDescription()
    {
        return $"Components spawn every {GetCurrentSecondsInterval()}s";
    }

    public override string GetNextLevelDescription()
    {
        if (IsAtMaxLevel())
        {
            return "-";
        }

        return $"{SecondsInterval[Level + 1]}s";
    }
}
