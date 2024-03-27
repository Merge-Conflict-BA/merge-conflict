/**********************************************************************************************************************
Name:          MBComponent
Description:   Elements data structure for motherboard.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
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


    public MBComponent(int tier, int trashValue, int salesValue, CPUComponent? cpu = null, RAMComponent? ram = null, GPUComponent? gpu = null)
        : base(tier, trashValue, salesValue)
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

    private bool HasComponents()
    {
        return cpu != null || ram != null || gpu != null;
    }

    override public int GetTrashValue()
    {
        int cpuTrashValue = cpu != null ? cpu.GetTrashValue() : 0;
        int ramTrashValue = ram != null ? ram.GetTrashValue() : 0;
        int gpuTrashValue = gpu != null ? gpu.GetTrashValue() : 0;

        return this.GetTrashValue() + cpuTrashValue + ramTrashValue + gpuTrashValue;
    }

    override public int GetSalesValue()
    {
        int cpuSalesValue = cpu != null ? cpu.GetSalesValue() : 0;
        int ramSalesValue = ram != null ? ram.GetSalesValue() : 0;
        int gpuSalesValue = gpu != null ? gpu.GetSalesValue() : 0;

        return this.GetSalesValue() + cpuSalesValue + ramSalesValue + gpuSalesValue;
    }

    public MBComponent Clone()
    {
        return new MBComponent(tier, trashValue, salesValue, cpu, ram, gpu);
    }
}
