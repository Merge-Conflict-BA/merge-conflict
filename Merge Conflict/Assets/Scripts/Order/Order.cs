/**********************************************************************************************************************
Name:           Order
Description:    Contains all Data for an order => is the result of OrderGeneration
Author(s):      Hanno Witzleb, Markus Haubold
Date:           2024-03-27
Version:        V2.0
TODO:           - 
**********************************************************************************************************************/

using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Order
{
    public readonly CaseComponent PC;

    public Order(int caseTier, int hddTier, int powersupplyTier, int motherboardTier, int gpuTier, int cpuTier, int ramTier)
    {
        PC = CreatePC(caseTier, hddTier, powersupplyTier, motherboardTier, gpuTier, cpuTier, ramTier);
    }

    public Order(CaseComponent pc)
    {
        PC = pc;
    }

    private CaseComponent CreatePC(int caseTier, int hddTier, int powersupplyTier, int motherboardTier, int gpuTier, int cpuTier, int ramTier)
    {
        CPUComponent cpu = new CPUComponent(cpuTier);
        RAMComponent ram = new RAMComponent(ramTier);
        GPUComponent gpu = new GPUComponent(gpuTier);

        MBComponent motherboard = new MBComponent(motherboardTier, cpu, ram, gpu);

        HDDComponent hdd = new HDDComponent(hddTier);
        PowersupplyComponent powersupply = new PowersupplyComponent(powersupplyTier);

        CaseComponent pcCase = new CaseComponent(caseTier, motherboard, powersupply, hdd);

        return pcCase;
    }
}