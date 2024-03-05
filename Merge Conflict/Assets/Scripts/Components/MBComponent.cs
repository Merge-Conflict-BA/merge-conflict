/**********************************************************************************************************************
Name:          MBComponent
Description:   Elements data structure for motherboard.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
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


    // Element in der funktion merge ist das Element, dass auf den MBComponent drauf gemerged wird

    // !   Mögliches Problem : Was passiert beim Drag&Drop (mergen) wenn ein CaseComponent einen unbestückten MBComponent in sich trägt 
    // !   und dann ein weiterer unbestückter MBComponent darauf abgelegt wird ? Muss man sich in diesem Fall rückversichern, dass der 
    // !   MBComponent in einem CaseComponent sitzt und somit nicht gemerged werden darf .

    public Element? Merge(Element element)
    {

        if (element is MBComponent otherMB)
        {
            if (HasComponents() || otherMB.HasComponents()) { return null; }

            if ((this.level == otherMB.level) && this.level < 4)
            {
                this.level++;
                return this;
            }
        }
        else if (element is CPUComponent otherCPU)
        {
            if (cpu) { return null; }

            this.cpu = otherCPU;
            return this;
        }
        else if (element is RAMComponent otherRAM)
        {
            if (ram) { return null; }

            this.ram = otherRAM;
            return this;
        }
        else if (element is GPUComponent otherGPU)
        {
            if (gpu) { return null; }

            this.gpu = otherGPU;
            return this;
        }

        return null;
    }

    private bool HasComponents()
    {
        return cpu || ram || gpu;
    }
}
