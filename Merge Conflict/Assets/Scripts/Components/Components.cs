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
    // the following ComponentData get referenced in the corresponding Element.GetComponentData()

    public static readonly ComponentData EmptyComponentData = new(
        salePrices: new int[] { 0, 0, 0, 0 },
        saleXP: new int[] { 0, 0, 0, 0 },
        trashPrices: new int[] { 0, 0, 0, 0 },
        baseBuyPrices: new int[] { 0, 0, 0, 0 });

    public static readonly ComponentData CaseComponentData = new(
        salePrices: new int[] { 10, 15, 25, 40 },
        saleXP: new int[] { 15, 20, 25, 30 },
        salePriceToBaseBuyPriceFactor: 1.4f);

    public static readonly ComponentData PowerSupplyComponentData = new(
        salePrices: new int[] { 12, 18, 30, 48 },
        saleXP: new int[] { 20, 27, 34, 40 },
        salePriceToBaseBuyPriceFactor: 1.4f);

    public static readonly ComponentData HddComponentData = new(
        salePrices: new int[] { 15, 23, 37, 60 },
        saleXP: new int[] { 30, 40, 50, 60 },
        salePriceToBaseBuyPriceFactor: 1.6f);

    public static readonly ComponentData CpuComponentData = new(
        salePrices: new int[] { 30, 45, 75, 120 },
        saleXP: new int[] { 70, 93, 117, 140 },
        salePriceToBaseBuyPriceFactor: 2f);

    public static readonly ComponentData GpuComponentData = new(
        salePrices: new int[] { 30, 45, 75, 120 },
        saleXP: new int[] { 70, 93, 117, 140 },
        salePriceToBaseBuyPriceFactor: 2.3f);

    public static readonly ComponentData RamComponentData = new(
        salePrices: new int[] { 20, 30, 50, 80 },
        saleXP: new int[] { 50, 67, 84, 100 },
        salePriceToBaseBuyPriceFactor: 1.8f);

    public static readonly ComponentData MBComponentData = new(
        salePrices: new int[] { 25, 38, 63, 101 },
        saleXP: new int[] { 60, 80, 100, 120 },
        salePriceToBaseBuyPriceFactor: 1.6f);

    public static readonly ComponentData TrashComponentData = new(
        trashPrices: new int[] { 30, 20, 50 });

    public static Element GetRandomElement()
    {
        // used for SpawnChanceWhenTrashDiscarded       
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