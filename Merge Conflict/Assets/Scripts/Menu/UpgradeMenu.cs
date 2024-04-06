/**********************************************************************************************************************
Name:          UpgradeMenu
Description:   Display upgrades on menu.
Author(s):     Le Xuan Tran
Date:          2024-04-05
Version:       V1.0
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
    public Button mergeXPButton;
    public Button moneyWhenTrashedButton;
    public Button spawnIntervalButton;
    public Button spawnChanceWhenTrashDiscardedButton;

    public TextMeshProUGUI mergeXPButtonText;
    public TextMeshProUGUI moneyWhenTrashedButtonText;
    public TextMeshProUGUI spawnIntervalButtonText;
    public TextMeshProUGUI spawnChanceWhenTrashDiscardedButtonText;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI experienceText;

    

    void Start()
    {
       mergeXPButton.onClick.AddListener(() => UpgradeMergeXP(Upgrades.MergeXPUpgrade));
       moneyWhenTrashedButton.onClick.AddListener(() => UpgradeMoneyWhenTrashed(Upgrades.MoneyWhenTrashedUpgrade));
       spawnIntervalButton.onClick.AddListener(() => UpgradeSpawnInterval(Upgrades.SpawnIntervalUpgrade));
       spawnChanceWhenTrashDiscardedButton.onClick.AddListener(() => UpgradeSpawnChanceWhenTrashDiscarded(Upgrades.SpawnChanceWhenTrashDiscardedUpgrade));

       UpdateButtonCosts();

       UpdateUI();
    }

    public void UpdateUI()
    {
        levelText.text = "Level " + ExperienceSystem.ExperienceHandler.GetCurrentLevel().ToString();
        experienceText.text = ExperienceSystem.ExperienceHandler.GetExperiencePoints().ToString() + " XP"; 
    }


void UpdateButtonCosts()
    {
        // Check if next level is available
        mergeXPButtonText.text = Upgrades.MergeXPUpgrade.isAtMaxLevel() ? "Max" : $"{Upgrades.MergeXPUpgrade.CostForNextLevel[Upgrades.MergeXPUpgrade.Level]}";
        moneyWhenTrashedButtonText.text = Upgrades.MoneyWhenTrashedUpgrade.isAtMaxLevel() ? "Max" : $"{Upgrades.MoneyWhenTrashedUpgrade.CostForNextLevel[Upgrades.MoneyWhenTrashedUpgrade.Level]}";
        spawnIntervalButtonText.text = Upgrades.SpawnIntervalUpgrade.isAtMaxLevel() ? "Max" : $"{Upgrades.SpawnIntervalUpgrade.CostForNextLevel[Upgrades.SpawnIntervalUpgrade.Level]}";
        spawnChanceWhenTrashDiscardedButtonText.text = Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.isAtMaxLevel() ? "Max" : $" {Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.CostForNextLevel[Upgrades.SpawnChanceWhenTrashDiscardedUpgrade.Level]}";
    }

    void UpgradeMergeXP(MergeXPUpgrade upgrade)
    {
        Debug.Log($"MergeXP Upgrade executed: Current XP: {upgrade.GetCurrentPercentageOfSalesXP()}%");
    }

    void UpgradeMoneyWhenTrashed(MoneyWhenTrashedUpgrade upgrade)
    {
        Debug.Log($"MoneyWhenTrashed Upgrade executed: Current money: {upgrade.GetCurrentPercentageOfTrashMoney()}$");
    }

    void UpgradeSpawnInterval(SpawnIntervalUpgrade upgrade)
    {
        Debug.Log($"SpawnInterval Upgrade executed: {upgrade.GetCurrentSecondsInterval()}");
    }

    void UpgradeSpawnChanceWhenTrashDiscarded(SpawnChanceWhenTrashDiscardedUpgrade upgrade)
    {
       Debug.Log($"UpgradeSpawnChanceWhenTrashDiscarded Upgrade executed: {upgrade.GetCurrentSpawnChancePercentWhenTrashDiscarded()}%");
    }
}
