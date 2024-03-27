/**********************************************************************************************************************
Name:          Components
Description:   Starting Point when wanting to create a Component 

Author(s):     Hanno Witzleb
Date:          2024-03-07
Version:       V1.0
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

    public static int[] caseTrashValues = { 5, 14, 26, 44 };
    public static int[] caseSalesValues = { 21, 57, 107, 178 };
    public static CreateCaseDelegate CreateCase = (motherboard, powersupply, hdd)
        => new CaseComponent(1, caseTrashValues[0], caseSalesValues[0], motherboard, powersupply, hdd);

    public static int[] powersupplyTrashValues = { 5, 14, 26, 44 };
    public static int[] powersupplySalesValues = { 21, 57, 107, 178 };
    public static PowersupplyComponent Powersupply = new(1, powersupplyTrashValues[0], powersupplySalesValues[0]);

    public static int[] hddTrashValues = { 7, 19, 35, 59 };
    public static int[] hddSalesValues = { 28, 76, 142, 237 };
    public static HDDComponent HDD = new(1, hddTrashValues[0], hddSalesValues[0]);

    public static int[] cpuTrashValues = { 16, 42, 80, 133 };
    public static int[] cpuSalesValues = { 64, 171, 321, 535 };
    public static CPUComponent CPU = new(1, cpuTrashValues[0], cpuSalesValues[0]);

    public static int[] gpuTrashValues = { 23, 61, 116, 193 };
    public static int[] gpuSalesValues = { 92, 247, 464, 773 };
    public static GPUComponent GPU = new(1, gpuTrashValues[0], gpuSalesValues[0]);

    public static int[] ramTrashValues = { 8, 23, 44, 74 };
    public static int[] ramSalesValues = { 35, 95, 178, 297 };
    public static RAMComponent RAM = new(1, ramTrashValues[0], ramSalesValues[0]);

    public static int[] mbTrashValues = { 8, 23, 44, 74 };
    public static int[] mbSalesValues = { 35, 95, 178, 297 };
    public static CreateMBDelegate CreateMB = (cpu, ram, gpu)
        => new MBComponent(1, mbTrashValues[0], mbSalesValues[0], cpu, ram, gpu);

    // TODO make sure that trash variants assign themself the correct values
    // currently not in createTrash implemented!
    public static int[] trashTrashValues = { 2, 1, 3 };
    public static int[] trashSalesValues = { 3, 2, 5 };
    public static CreateTrashDelegate CreateTrash = (variant)
        => new Trash(trashTrashValues[0], trashSalesValues[0], variant);


    // ! catch the returning tuple like this :
    //      var (trashValue, salesValue) = ComponentValues.GetComponentValues(MyComponentClass);
    public static (int trashValue, int salesValue) GetComponentValues(Element element)
    {
        int index = element.tier - 1;

        switch (element)
        {
            case CaseComponent:
                return (caseTrashValues[index], caseSalesValues[index]);

            case PowersupplyComponent:
                return (powersupplyTrashValues[index], powersupplySalesValues[index]);

            case HDDComponent:
                return (hddTrashValues[index], hddSalesValues[index]);

            case MBComponent:
                return (mbTrashValues[index], mbSalesValues[index]);

            case CPUComponent:
                return (cpuTrashValues[index], cpuSalesValues[index]);

            case RAMComponent:
                return (ramTrashValues[index], ramSalesValues[index]);

            case GPUComponent:
                return (gpuTrashValues[index], gpuSalesValues[index]);

            case Trash trash:
                return (trashTrashValues[(int)trash.trashVariant], trashSalesValues[(int)trash.trashVariant]);

            default:
                Debugger.LogError("No matching component for the return of trash and sales values.");
                return (5, 10);
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

}

public delegate MBComponent CreateMBDelegate(CPUComponent? cpu = null, RAMComponent? ram = null, GPUComponent? gpu = null);
public delegate CaseComponent CreateCaseDelegate(MBComponent? motherboard = null, PowersupplyComponent? powersupply = null, HDDComponent? hdd = null);
public delegate Trash CreateTrashDelegate(TrashVariant? variant = null);