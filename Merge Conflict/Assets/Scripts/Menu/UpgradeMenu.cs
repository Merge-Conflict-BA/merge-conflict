/**********************************************************************************************************************
Name:          UpgradeMenu
Description:   Display upgrades on menu.
Author(s):     Le Xuan Tran, Daniel Rittrich
Date:          2024-04-05
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeMenu : Menu
{
    [Header("Buttons")]
    public Button mergeXPButton;
    public Button moneyWhenTrashedButton;
    public Button spawnChanceWhenTrashDiscardedButton;
    public Button spawnIntervalButton;

    [Header("Button Texts")]
    public TextMeshProUGUI mergeXPButtonText;
    public TextMeshProUGUI moneyWhenTrashedButtonText;
    public TextMeshProUGUI spawnChanceWhenTrashDiscardedButtonText;
    public TextMeshProUGUI spawnIntervalButtonText;

    [Header("Description Texts")]
    public TextMeshProUGUI mergeXPDescriptionText;
    public TextMeshProUGUI moneyWhenTrashedDescriptionText;
    public TextMeshProUGUI spawnChanceWhenTrashDiscardedDescriptionText;
    public TextMeshProUGUI spawnIntervalDescriptionText;

    [Header("UI Texts")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyText;

    #region Singleton
    private static UpgradeMenu _instance;
    public static UpgradeMenu Instance { get { return _instance; } }

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
    #endregion

    void Start()
    {
        mergeXPButton.onClick.AddListener(() => BuyNextUpgradeLevel(UpgradeType.MergeXP));
        moneyWhenTrashedButton.onClick.AddListener(() => BuyNextUpgradeLevel(UpgradeType.MoneyWhenTrashed));
        spawnChanceWhenTrashDiscardedButton.onClick.AddListener(() => BuyNextUpgradeLevel(UpgradeType.SpawnChanceWhenTrashDiscarded));
        spawnIntervalButton.onClick.AddListener(() => BuyNextUpgradeLevel(UpgradeType.SpawnInterval));

        // TODO: delete this test money function later
#if UNITY_EDITOR
        MoneyHandler.Instance.AddMoney(100000);
#endif

        UpdateUI();
    }

    public void OpenMenu()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        levelText.text = "Level " + ExperienceSystem.ExperienceHandler.GetCurrentLevel().ToString();
        moneyText.text = MoneyHandler.Instance.Money.ToString() + " $";

        UpdateDescriptionsTexts();
        UpdateButtons();
    }

    void UpdateButtons()
    {
        // Check if next level is available
        mergeXPButtonText.text = Upgrades.MergeXPUpgrade.GetNextLevelBuyText();
        moneyWhenTrashedButtonText.text = Upgrades.MoneyWhenTrashedUpgrade.GetNextLevelBuyText();
        spawnChanceWhenTrashDiscardedButtonText.text = Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.GetNextLevelBuyText();
        spawnIntervalButtonText.text = Upgrades.SpawnIntervalUpgrade.GetNextLevelBuyText();
    }

    void UpdateDescriptionsTexts()
    {
        mergeXPDescriptionText.text = GetDescriptionText(UpgradeType.MergeXP);
        moneyWhenTrashedDescriptionText.text = GetDescriptionText(UpgradeType.MoneyWhenTrashed);
        spawnChanceWhenTrashDiscardedDescriptionText.text = GetDescriptionText(UpgradeType.SpawnChanceWhenTrashDiscarded);
        spawnIntervalDescriptionText.text = GetDescriptionText(UpgradeType.SpawnInterval);
    }

    private string GetDescriptionText(UpgradeType upgradeType)
    {
        string description = "-";
        string descCurrLevel = "";
        string descNextLevel = "";
        bool isMaxLevel = false;

        switch (upgradeType)
        {
            case UpgradeType.MergeXP:
                descCurrLevel = Upgrades.MergeXPUpgrade.GetCurrentLevelDescription();
                isMaxLevel = Upgrades.MergeXPUpgrade.IsAtMaxLevel();
                if (!isMaxLevel) { descNextLevel = Upgrades.MergeXPUpgrade.GetNextLevelDescription(); };
                break;

            case UpgradeType.MoneyWhenTrashed:
                descCurrLevel = Upgrades.MoneyWhenTrashedUpgrade.GetCurrentLevelDescription();
                isMaxLevel = Upgrades.MoneyWhenTrashedUpgrade.IsAtMaxLevel();
                if (!isMaxLevel) { descNextLevel = Upgrades.MoneyWhenTrashedUpgrade.GetNextLevelDescription(); };
                break;

            case UpgradeType.SpawnChanceWhenTrashDiscarded:
                descCurrLevel = Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.GetCurrentLevelDescription();
                isMaxLevel = Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.IsAtMaxLevel();
                if (!isMaxLevel) { descNextLevel = Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.GetNextLevelDescription(); };
                break;

            case UpgradeType.SpawnInterval:
                descCurrLevel = Upgrades.SpawnIntervalUpgrade.GetCurrentLevelDescription();
                isMaxLevel = Upgrades.SpawnIntervalUpgrade.IsAtMaxLevel();
                if (!isMaxLevel) { descNextLevel = Upgrades.SpawnIntervalUpgrade.GetNextLevelDescription(); };
                break;

            default:
                Debugger.LogWarning($"No matching upgrade. Can't get description text.");
                break;
        }

        description = $"{descCurrLevel}" + (!isMaxLevel ? $"\n -> Next Level ({descNextLevel})" : "");

        return description;
    }

    public void BuyNextUpgradeLevel(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.MergeXP:
                Upgrades.MergeXPUpgrade.BuyNextLevel();
                break;

            case UpgradeType.MoneyWhenTrashed:
                Upgrades.MoneyWhenTrashedUpgrade.BuyNextLevel();
                break;

            case UpgradeType.SpawnChanceWhenTrashDiscarded:
                Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.BuyNextLevel();
                break;

            case UpgradeType.SpawnInterval:
                Upgrades.SpawnIntervalUpgrade.BuyNextLevel();
                break;

            default:
                Debugger.LogWarning($"No matching upgrade. The upgrade to be purchased cannot be activated.");
                break;
        }

        UpdateUI();
    }
}
