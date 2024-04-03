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
}
