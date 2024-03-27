/**********************************************************************************************************************
Name:          CPUComponent
Description:   Elements data structure for the CPU.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUComponent : Element, IComponent
{
    public static string Name = "CPU";

    public CPUComponent(int tier, int trashValue, int salesValue) : base(tier, trashValue, salesValue, Name) { }

    public Element? Merge(Element element)
    {

        if (element is CPUComponent otherCPU)
        {

            if ((this.tier == otherCPU.tier) && this.tier < 4)
            {
                this.tier++;
                return this;
            }
        }

        return null;
    }

    public CPUComponent Clone()
    {
        return new CPUComponent(tier, trashValue, salesValue);
    }
}
