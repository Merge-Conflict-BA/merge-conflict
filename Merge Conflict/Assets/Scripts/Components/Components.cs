/**********************************************************************************************************************
Name:          Components
Description:   Starting Point when wanting to create a Component. Contains all balancing details.

Author(s):     Hanno Witzleb
Date:          2024-03-07
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Components
{    
    public static ComponentData EmptyComponentData = new(
        salePrices: new int[] { 0, 0, 0, 0 },
        saleXP: new int[] { 0, 0, 0, 0 },
        trashPrices: new int[] { 0, 0, 0, 0 },
        baseBuyPrices: new int[] { 0, 0, 0, 0 });

    public static ComponentData CaseComponentData = new(
        salePrices: new int[] { 10, 15, 25, 40 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 1.4f);

    public static ComponentData PowerSupplyComponentData = new(
        salePrices: new int[] { 12, 18, 30, 48 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 1.4f);

    public static ComponentData HddComponentData = new(
        salePrices: new int[] { 15, 23, 37, 60 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 1.6f);

    public static ComponentData CpuComponentData = new(
        salePrices: new int[] { 30, 45, 75, 120 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 2f);

    public static ComponentData GpuComponentData = new(
        salePrices: new int[] { 30, 45, 75, 120 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 2.3f);

    public static ComponentData RamComponentData = new(
        salePrices: new int[] { 20, 30, 50, 80 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 1.8f);

    public static ComponentData MBComponentData = new(
        salePrices: new int[] { 25, 38, 63, 101 },
        saleXP: new int[] { 0, 0, 0, 0 },
        salePriceToBaseBuyPriceFactor: 1.6f);

    public static ComponentData TrashComponentData = new(
        trashPrices: new int[] { 30, 20, 50 });

    public static Element GetRandomElement()
    {
        // used for SpawnChanceWHenTrashDiscarded       
        Element[] elements = GetComponents();

        int randomIndex = Random.Range(0, elements.Length);
        
        return elements[randomIndex];
    }

    public static Element GetElementByName(string name)
    {
        int elementIndex = GetComponentNames().FindIndex(componentName => componentName == name);

        return GetComponents()[elementIndex];
    }

    public static Element[] GetComponents()
    {
        return new Element[]
        {
            new CaseComponent(1),
            new PowersupplyComponent(1),
            new HDDComponent(1),
            new MBComponent(1),
            new GPUComponent(1),
            new RAMComponent(1),
            new GPUComponent(1),
            new Trash()
        };
    }

    public static List<string> GetComponentNames()
    {
        return new List<string> {
            CaseComponent.Name,
            PowersupplyComponent.Name,
            HDDComponent.Name,
            MBComponent.Name,
            CPUComponent.Name,
            RAMComponent.Name,
            GPUComponent.Name,
            Trash.Name };
    }
}