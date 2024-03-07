/**********************************************************************************************************************
Name:          Components
Description:   Starting Point when wanting to create a Component 

Author(s):     Hanno Witzleb
Date:          2024-03-07
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Usage:
 * 
 * CPUComponent cpu = Components.CPU;
 * 
 * MBComponent motherboard = Components.CreateMB();
 * MBComponent motherboardWithCPU = Components.CreateMB(cpu: cpu);
 * 
 * Trash trash = Components.CreateTrash(TrashVariant.Can);
 * Trash trashRandom = Components.CreateTrash();
 */
public static class Components
{
    public static CPUComponent CPU =    new(1,  3,  5);
    public static GPUComponent GPU =    new(1,  3,  5);
    public static HDDComponent HDD =    new(1,  3,  5);
    public static RAMComponent RAM =    new(1,  3,  5);

    public static CreateMBDelegate CreateMB = (cpu, ram, gpu)
        => new MBComponent(1, 3, 5, cpu, ram, gpu);

    public static CreateCaseDelegate CreateCase = (motherboard, powersupply, hdd)
        => new CaseComponent(1, 3, 5, motherboard, powersupply, hdd);

    public static CreateTrashDelegate CreateTrash = (variant)
        => new Trash(3, 5, variant);
}

public delegate MBComponent CreateMBDelegate(CPUComponent? cpu = null, RAMComponent? ram = null, GPUComponent? gpu = null);
public delegate CaseComponent CreateCaseDelegate(MBComponent? motherboard = null, PowersupplyComponent? powersupply = null, HDDComponent? hdd = null);
public delegate Trash CreateTrashDelegate(TrashVariant? variant = null);