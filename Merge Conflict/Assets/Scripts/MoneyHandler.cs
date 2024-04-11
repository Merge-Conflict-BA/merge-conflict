/**********************************************************************************************************************
Name:          MoneyHandler
Description:   Governs the money of the player
Author(s):     Hanno Witzleb
Date:          2024-04-03
Version:       V1.0
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    private static MoneyHandler _instance;
    public static MoneyHandler Instance { get { return _instance; } }

    public int Money { get; private set; } = 0;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        ApplyPlayerPrefs();

        // TODO: delete this function later --> only for generating testing money
#if UNITY_EDITOR
        AddMoney(100000);
#endif

    }

    public void AddMoney(int moneyToAdd)
    {
        Money += moneyToAdd;
        SafeToPlayerPrefs();
    }

    public bool SpendMoney(int moneyToSpend)
    {
        if (moneyToSpend > Money)
        {
            return false;
        }

        Money -= moneyToSpend;
        SafeToPlayerPrefs();
        return true;
    }

    private void SafeToPlayerPrefs()
    {
        PlayerPrefs.SetInt(GetPlayerPrefsKey(), Money);
    }

    private void ApplyPlayerPrefs()
    {
        Money = PlayerPrefs.GetInt(GetPlayerPrefsKey());
    }

    private string GetPlayerPrefsKey()
    {
        return "Money";
    }
}
