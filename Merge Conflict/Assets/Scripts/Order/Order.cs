/**********************************************************************************************************************
Name:           Order
Description:    Contains all Data for an order => is the result of OrderGeneration
Author(s):      Hanno Witzleb, Markus Haubold
Date:           2024-03-27
Version:        V2.0
TODO:           - 
**********************************************************************************************************************/

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
        CPUComponent cpu = Components.CPU.Clone();
        cpu.tier = CpuTier;

        RAMComponent ram = Components.RAM.Clone();
        ram.tier = RamTier;

        GPUComponent gpu = Components.GPU.Clone();
        gpu.tier = GpuTier;

        MBComponent motherboard = Components.CreateMB(cpu, ram, gpu);
        motherboard.tier = MotherboardTier;

        HDDComponent hdd = Components.HDD.Clone();
        hdd.tier = HddTier;

        PowersupplyComponent powersupply = Components.Powersupply.Clone();
        powersupply.tier = PowersupplyTier;

        CaseComponent pcCase = Components.CreateCase(motherboard, powersupply, hdd);
        pcCase.tier = CaseTier;

        return pcCase;
    }
}