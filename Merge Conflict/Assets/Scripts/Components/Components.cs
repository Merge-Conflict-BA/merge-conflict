/**********************************************************************************************************************
Name:          Components
Description:   Starting Point when wanting to create a Component 

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

/*
 * Usage:
 * 
 * CPUComponent cpu = Components.CPU;
 * cpu.level = 3;
 * 
 * MBComponent motherboard = Components.CreateMB();
 * MBComponent motherboardWithCPU = Components.CreateMB(cpu: cpu);
 * 
 * Trash trash = Components.CreateTrash(TrashVariant.Can);
 * Trash trashRandom = Components.CreateTrash();
 */
public static class Components
{
    // _StartPrice: start price of a component when the player wants to buy its first component in the element menu
    // _IncreaseFactor: factor of how much the price increases if the player buys a component

    public static int[] caseTrashPrice = { 5, 14, 26, 44 };
    public static int[] caseSalesPrice = { 21, 57, 107, 178 };
    public static int[] caseSalesXP = { 50, 120, 280, 600 };
    public static float[] caseStartPrice = { 21, 57, 107, 178 };
    public static float[] caseIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static CreateCaseDelegate CreateCase = (motherboard, powersupply, hdd)
            => new CaseComponent(1, caseTrashPrice[0], caseSalesPrice[0], caseSalesXP[0], motherboard, powersupply, hdd);

    public static int[] powersupplyTrashPrice = { 5, 14, 26, 44 };
    public static int[] powersupplySalesPrice = { 21, 57, 107, 178 };
    public static int[] powersupplySalesXP = { 50, 120, 280, 600 };
    public static float[] powersupplyStartPrice = { 21, 57, 107, 178 };
    public static float[] powersupplyIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static PowersupplyComponent Powersupply = new(1, powersupplyTrashPrice[0], powersupplySalesPrice[0], powersupplySalesXP[0]);

    public static int[] hddTrashPrice = { 7, 19, 35, 59 };
    public static int[] hddSalesPrice = { 28, 76, 142, 237 };
    public static int[] hddSalesXP = { 50, 120, 280, 600 };
    public static float[] hddStartPrice = { 28, 76, 142, 237 };
    public static float[] hddIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static HDDComponent HDD = new(1, hddTrashPrice[0], hddSalesPrice[0], hddSalesXP[0]);

    public static int[] cpuTrashPrice = { 16, 42, 80, 133 };
    public static int[] cpuSalesPrice = { 64, 171, 321, 535 };
    public static int[] cpuSalesXP = { 50, 120, 280, 600 };
    public static float[] cpuStartPrice = { 64, 171, 321, 535 };
    public static float[] cpuIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static CPUComponent CPU = new(1, cpuTrashPrice[0], cpuSalesPrice[0], cpuSalesXP[0]);

    public static int[] gpuTrashPrice = { 23, 61, 116, 193 };
    public static int[] gpuSalesPrice = { 92, 247, 464, 773 };
    public static int[] gpuSalesXP = { 50, 120, 280, 600 };
    public static float[] gpuStartPrice = { 92, 247, 464, 773 };
    public static float[] gpuIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static GPUComponent GPU = new(1, gpuTrashPrice[0], gpuSalesPrice[0], gpuSalesXP[0]);

    public static int[] ramTrashPrice = { 8, 23, 44, 74 };
    public static int[] ramSalesPrice = { 35, 95, 178, 297 };
    public static int[] ramSalesXP = { 50, 120, 280, 600 };
    public static float[] ramStartPrice = { 35, 95, 178, 297 };
    public static float[] ramIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static RAMComponent RAM = new(1, ramTrashPrice[0], ramSalesPrice[0], ramSalesXP[0]);

    public static int[] mbTrashPrice = { 8, 23, 44, 74 };
    public static int[] mbSalesPrice = { 35, 95, 178, 297 };
    public static int[] mbSalesXP = { 50, 120, 280, 600 };
    public static float[] mbStartPrice = { 35, 95, 178, 297 };
    public static float[] mbIncreaseFactor = { 1.2f, 1.3f, 1.4f, 1.5f };
    public static CreateMBDelegate CreateMB = (cpu, ram, gpu)
            => new MBComponent(1, mbTrashPrice[0], mbSalesPrice[0], mbSalesXP[0], cpu, ram, gpu);

    // TODO make sure that trash variants assign themself the correct values
    // currently not in createTrash implemented!
    public static int[] trashTrashPrice = { 2, 1, 3 };
    public static int[] trashSalesPrice = { 3, 2, 5 };
    public static int[] trashSalesXP = { 10, 10, 10 };
    public static CreateTrashDelegate CreateTrash = (variant)
            => new Trash(trashTrashPrice[0], trashSalesPrice[0], trashSalesXP[0], variant);


    // ! catch the returning tuple like this :
    //      var (trashPrice, salesPrice, salesXP) = Components.GetComponentValues(MyComponentClass);
    public static (int trashPrice, int salesPrice, int salesXP) GetComponentValues(Element? element)
    {
        if (element == null)
        {
            Debugger.LogError("No component existing for the return of trash and sales values.");
            return (5, 10, 10);
        }

        int index = element.tier - 1;

        switch (element)
        {
            case CaseComponent:
                return (caseTrashPrice[index], caseSalesPrice[index], caseSalesXP[index]);

            case PowersupplyComponent:
                return (powersupplyTrashPrice[index], powersupplySalesPrice[index], powersupplySalesXP[index]);

            case HDDComponent:
                return (hddTrashPrice[index], hddSalesPrice[index], hddSalesXP[index]);

            case MBComponent:
                return (mbTrashPrice[index], mbSalesPrice[index], mbSalesXP[index]);

            case CPUComponent:
                return (cpuTrashPrice[index], cpuSalesPrice[index], cpuSalesXP[index]);

            case RAMComponent:
                return (ramTrashPrice[index], ramSalesPrice[index], ramSalesXP[index]);

            case GPUComponent:
                return (gpuTrashPrice[index], gpuSalesPrice[index], gpuSalesXP[index]);

            case Trash trash:
                return (trashTrashPrice[(int)trash.trashVariant], trashSalesPrice[(int)trash.trashVariant], trashSalesXP[(int)trash.trashVariant]);

            default:
                Debugger.LogError("No matching component for the return of trash and sales values.");
                return (5, 10, 10);
        }
    }

    public static Element GetRandomElement()
    {
        // only for testing purpose
        // TODO implement when needed for spawner logic

        Element[] array = { CreateCase(), Powersupply.Clone(), HDD.Clone(), CreateMB(), CPU.Clone(), RAM.Clone(), GPU.Clone(), CreateTrash() };
        int randomIndex = Random.Range(0, array.Length);
        Element randomElement = array[randomIndex];


        return randomElement;
    }

    public static List<string> GetComponentNames()
    {
        return new List<string> { CaseComponent.Name, PowersupplyComponent.Name, HDDComponent.Name, MBComponent.Name, CPUComponent.Name, RAMComponent.Name, GPUComponent.Name, Trash.Name };
    }
}

public delegate MBComponent CreateMBDelegate(CPUComponent? cpu = null, RAMComponent? ram = null, GPUComponent? gpu = null);
public delegate CaseComponent CreateCaseDelegate(MBComponent? motherboard = null, PowersupplyComponent? powersupply = null, HDDComponent? hdd = null);
public delegate Trash CreateTrashDelegate(TrashVariant? variant = null);