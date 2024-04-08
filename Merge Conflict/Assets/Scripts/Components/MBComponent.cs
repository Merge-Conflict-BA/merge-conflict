/**********************************************************************************************************************
Name:          MBComponent
Description:   Elements data structure for motherboard.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.5
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBComponent : Element, IComponent
{
    public CPUComponent? cpu;
    public RAMComponent? ram;
    public GPUComponent? gpu;
    public static string Name = "Motherboard";


    public MBComponent(int tier, CPUComponent? cpu = null, RAMComponent? ram = null, GPUComponent? gpu = null)
        : base(tier, Name)
    {
        this.cpu = cpu;
        this.ram = ram;
        this.gpu = gpu;
    }


    // Element in der funktion merge ist das Element, dass auf den MBComponent drauf gemerged wird

    // !   Mögliches Problem : Was passiert beim Drag&Drop (mergen) wenn ein CaseComponent einen unbestückten MBComponent in sich trägt 
    // !   und dann ein weiterer unbestückter MBComponent darauf abgelegt wird ? Muss man sich in diesem Fall rückversichern, dass der 
    // !   MBComponent in einem CaseComponent sitzt und somit nicht gemerged werden darf .

    public Element? Merge(Element element)
    {

        if (element is MBComponent otherMB)
        {
            if (HasComponents() || otherMB.HasComponents()) { return null; }

            if ((this.tier == otherMB.tier) && this.tier < 4)
            {
                this.tier++;
                return this;
            }
        }
        else if (element is CPUComponent otherCPU)
        {
            if (cpu != null) { return null; }

            this.cpu = otherCPU;
            return this;
        }
        else if (element is RAMComponent otherRAM)
        {
            if (ram != null) { return null; }

            this.ram = otherRAM;
            return this;
        }
        else if (element is GPUComponent otherGPU)
        {
            if (gpu != null) { return null; }

            this.gpu = otherGPU;
            return this;
        }

        return null;
    }

    override public bool HasComponents()
    {
        return cpu != null || ram != null || gpu != null;
    }

    override public int GetTrashPrice()
    {
        int cpuTrashPrice = cpu != null ? cpu.GetTrashPrice() : 0;
        int ramTrashPrice = ram != null ? ram.GetTrashPrice() : 0;
        int gpuTrashPrice = gpu != null ? gpu.GetTrashPrice() : 0;

        return base.GetTrashPrice() + cpuTrashPrice + ramTrashPrice + gpuTrashPrice;
    }

    override public int GetSalesPrice()
    {
        int cpuSalesPrice = cpu != null ? cpu.GetSalesPrice() : 0;
        int ramSalesPrice = ram != null ? ram.GetSalesPrice() : 0;
        int gpuSalesPrice = gpu != null ? gpu.GetSalesPrice() : 0;

        return base.GetSalesPrice() + cpuSalesPrice + ramSalesPrice + gpuSalesPrice;
    }

    override public int GetSalesXP()
    {
        int cpuSalesXP = cpu != null ? cpu.GetSalesXP() : 0;
        int ramSalesXP = ram != null ? ram.GetSalesXP() : 0;
        int gpuSalesXP = gpu != null ? gpu.GetSalesXP() : 0;

        return base.GetSalesXP() + cpuSalesXP + ramSalesXP + gpuSalesXP;
    }

    public override bool IsEqual(Element element)
    {
        if (base.IsEqual(element) == false)
        {
            return false;
        }

        MBComponent mBComponent = (MBComponent)element;

        bool isCpuEqual = (cpu == null && mBComponent.cpu == null) ? true : (cpu != null && mBComponent.cpu != null) ? cpu.IsEqual(mBComponent.cpu) : false;
        bool isGPUEqual = (gpu == null && mBComponent.gpu == null) ? true : (gpu != null && mBComponent.gpu != null) ? gpu.IsEqual(mBComponent.gpu) : false;
        bool isRAMEqual = (ram == null && mBComponent.ram == null) ? true : (ram != null && mBComponent.ram != null) ? ram.IsEqual(mBComponent.ram) : false;

        return isCpuEqual && isGPUEqual && isRAMEqual;
    }

    public MBComponent Clone()
    {
        return new MBComponent(tier, cpu, ram, gpu);
    }

    public override ComponentData GetComponentData()
    {
        return Components.MBComponentData;
    }
}
