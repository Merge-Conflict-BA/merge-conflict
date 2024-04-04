/**********************************************************************************************************************
Name:          Upgrades
Description:   Contains all upgrades
Author(s):     Hanno Witzleb
Date:          2024-03-29
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Upgrades
{
    public static MergeXPUpgrade MergeXPUpgrade = new(
        new int[] { 0, 5, 10, 15, 20, 25 },
        new int[] { 300, 400, 500, 600, 700 });

    public static MoneyWhenTrashedUpgrade MoneyWhenTrashedUpgrade = new(
        new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 },
        new int[] { 150, 225, 300, 375, 450, 525, 600, 675, 750 });

    public static SpawnIntervalUpgrade SpawnIntervalUpgrade = new(
        new int[] { 10, 9, 8, 7, 6, 5, 4, 3 },
        new int[] { 400, 525, 650, 775, 900, 1025, 1150 });

    public static SpawnChanceWhenTrashDiscardedUpgrade SpawnChanceWhenTrashDiscardedUpgrade = new(
        new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 },
        new int[] { 75, 125, 175, 225, 275, 325, 375, 425, 475, 525 });
}
