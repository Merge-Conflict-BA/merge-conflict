using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChanceWhenTrashDiscardedUpgrade : Upgrade
{
    public int[] SpawnChancePercentWhenTrashDiscarded; // for current level

    public SpawnChanceWhenTrashDiscardedUpgrade(int[] spawnChancePercentWhenTrashDiscarded, int[] costForNextLevel) : base("SpawnChanceWhenTrashDiscarded", costForNextLevel)
    {
        SpawnChancePercentWhenTrashDiscarded = spawnChancePercentWhenTrashDiscarded;

        Debugger.LogErrorIf(spawnChancePercentWhenTrashDiscarded.Length - 1 != costForNextLevel.Length,
            $"Values for {Name} do not match!");
    }

    public int GetCurrentSpawnChancePercentWhenTrashDiscarded()
    {
        return SpawnChancePercentWhenTrashDiscarded[Level];
    }

    public override string GetCurrentLevelDescription()
    {
        return $"{GetCurrentSpawnChancePercentWhenTrashDiscarded()}% chance for component spawn when trash is discarded";
    }

    public override string GetNextLevelDescription()
    {
        if (isAtMaxLevel())
        {
            return "-";
        }

        return $"{SpawnChancePercentWhenTrashDiscarded[Level + 1]}%";
    }
}