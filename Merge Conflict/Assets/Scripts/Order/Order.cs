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

    public readonly int CaseTier;
    public readonly int HddTier;
    public readonly int PowersupplyTier;
    public readonly int MotherboardTier;
    public readonly int GpuTier;
    public readonly int CpuTier;
    public readonly int RamTier;

    public Order(int caseTier, int hddTier, int powersupplyTier, int motherboardTier, int gpuTier, int cpuTier, int ramTier)
    {
        CaseTier = caseTier;
        HddTier = hddTier;
        PowersupplyTier = powersupplyTier;
        MotherboardTier = motherboardTier;
        GpuTier = gpuTier;
        CpuTier = cpuTier;
        RamTier = ramTier;

        PC = CreatePC();
    }

    private CaseComponent CreatePC()
    {
        CPUComponent cpu = new CPUComponent(CpuTier);
        RAMComponent ram = new RAMComponent(RamTier);
        GPUComponent gpu = new GPUComponent(GpuTier);

        MBComponent motherboard = new MBComponent(MotherboardTier, cpu, ram, gpu);

        HDDComponent hdd = new HDDComponent(HddTier);
        PowersupplyComponent powersupply = new PowersupplyComponent(PowersupplyTier);

        CaseComponent pcCase = new CaseComponent(CaseTier, motherboard, powersupply, hdd);

        return pcCase;
    }
}