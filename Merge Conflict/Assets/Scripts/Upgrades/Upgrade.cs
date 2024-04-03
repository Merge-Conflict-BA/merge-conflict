/**********************************************************************************************************************
Name:          Upgrade
Description:   Contains all state info for upgrades. !!! Doesn't contain Implementation of Upgrades !!!
Author(s):     Hanno Witzleb
Date:          2024-03-29
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public string Name;
    public int Level { get; private set; } = 0;
    public int[] CostForNextLevel;

    public Upgrade(string name, int[] costForNextLevel)
    {
        Name = name;
        CostForNextLevel = costForNextLevel;

        ApplyPlayerPrefs();
    }

    public virtual string GetCurrentLevelDescription()
    {
        return "";
    }

    public virtual string GetNextLevelDescription()
    {
        return "";
    }

    public int BuyNextLevel(int currentMoney)
    {
        if(CostForNextLevel[Level] > currentMoney || isAtMaxLevel())
        {
            return currentMoney;
        }

        Level++;
        SaveToPlayerPrefs();

        return currentMoney - CostForNextLevel[Level];
    }

    public bool isAtMaxLevel()
    {
        return Level >= CostForNextLevel.Length;
    }

    public void ApplyPlayerPrefs()
    {
        Level = PlayerPrefs.GetInt(GetPlayerPrefsKey());
    }

    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetInt(GetPlayerPrefsKey(), Level);
    }

    private string GetPlayerPrefsKey()
    {
        return $"Upgrade: {Name}";
    }
}
