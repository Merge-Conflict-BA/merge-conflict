/**********************************************************************************************************************
Name:          UpgradeMenu
Description:   Display upgrades on menu.
Author(s):     Le Xuan Tran, Daniel Rittrich
Date:          2024-04-05
Version:       V1.1
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



    void Start()
    {
        mergeXPButton.onClick.AddListener(() => UpgradeMergeXP(Upgrades.MergeXPUpgrade));
        moneyWhenTrashedButton.onClick.AddListener(() => UpgradeMoneyWhenTrashed(Upgrades.MoneyWhenTrashedUpgrade));
        spawnChanceWhenTrashDiscardedButton.onClick.AddListener(() => UpgradeSpawnChanceWhenTrashDiscarded(Upgrades.SpawnChanceWhenTrashDiscardedUpgrade));
        spawnIntervalButton.onClick.AddListener(() => UpgradeSpawnInterval(Upgrades.SpawnIntervalUpgrade));

        // TODO: delete this test money function later
#if UNITY_EDITOR
        MoneyHandler.Instance.AddMoney(100000);
#endif

        UpdateUI();
    }

    void Update()
    {
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
        mergeXPButtonText.text = Upgrades.MergeXPUpgrade.isAtMaxLevel() ? "Max" : $"{Upgrades.MergeXPUpgrade.CostForNextLevel[Upgrades.MergeXPUpgrade.Level]} $";
        moneyWhenTrashedButtonText.text = Upgrades.MoneyWhenTrashedUpgrade.isAtMaxLevel() ? "Max" : $"{Upgrades.MoneyWhenTrashedUpgrade.CostForNextLevel[Upgrades.MoneyWhenTrashedUpgrade.Level]} $";
        spawnChanceWhenTrashDiscardedButtonText.text = Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.isAtMaxLevel() ? "Max" : $" {Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.CostForNextLevel[Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.Level]} $";
        spawnIntervalButtonText.text = Upgrades.SpawnIntervalUpgrade.isAtMaxLevel() ? "Max" : $"{Upgrades.SpawnIntervalUpgrade.CostForNextLevel[Upgrades.SpawnIntervalUpgrade.Level]} $";
    }

    void UpdateDescriptionsTexts()
    {
        mergeXPDescriptionText.text = $"{Upgrades.MergeXPUpgrade.GetCurrentLevelDescription()}" + (!Upgrades.MergeXPUpgrade.isAtMaxLevel() ? $"\n -> Next Level ({Upgrades.MergeXPUpgrade.GetNextLevelDescription()})" : "");
        moneyWhenTrashedDescriptionText.text = $"{Upgrades.MoneyWhenTrashedUpgrade.GetCurrentLevelDescription()}" + (!Upgrades.MoneyWhenTrashedUpgrade.isAtMaxLevel() ? $"\n -> Next Level ({Upgrades.MoneyWhenTrashedUpgrade.GetNextLevelDescription()})" : "");
        spawnChanceWhenTrashDiscardedDescriptionText.text = $"{Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.GetCurrentLevelDescription()}" + (!Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.isAtMaxLevel() ? $"\n -> Next Level ({Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.GetNextLevelDescription()})" : "");
        spawnIntervalDescriptionText.text = $"{Upgrades.SpawnIntervalUpgrade.GetCurrentLevelDescription()}" + (!Upgrades.SpawnIntervalUpgrade.isAtMaxLevel() ? $"\n -> Next Level ({Upgrades.SpawnIntervalUpgrade.GetNextLevelDescription()})" : "");
    }

    void UpgradeMergeXP(MergeXPUpgrade upgrade)
    {
        upgrade.BuyNextLevel();
        UpdateUI();
        Debugger.LogMessage($"MergeXP Upgrade: Current XP: +{upgrade.GetCurrentPercentageOfSalesXP()}%");
    }

    void UpgradeMoneyWhenTrashed(MoneyWhenTrashedUpgrade upgrade)
    {
        upgrade.BuyNextLevel();
        UpdateUI();
        Debugger.LogMessage($"MoneyWhenTrashed Upgrade: Current money: +{upgrade.GetCurrentPercentageOfTrashMoney()}%");
    }

    void UpgradeSpawnChanceWhenTrashDiscarded(SpawnChanceWhenTrashDiscardedUpgrade upgrade)
    {
        upgrade.BuyNextLevel();
        UpdateUI();
        Debugger.LogMessage($"UpgradeSpawnChanceWhenTrashDiscarded Upgrade: Current chance: +{upgrade.GetCurrentSpawnChancePercentWhenTrashDiscarded()}%");
    }

    void UpgradeSpawnInterval(SpawnIntervalUpgrade upgrade)
    {
        upgrade.BuyNextLevel();
        UpdateUI();
        Debugger.LogMessage($"SpawnInterval Upgrade: Current interval: {upgrade.GetCurrentSecondsInterval()}sec");
    }
}
