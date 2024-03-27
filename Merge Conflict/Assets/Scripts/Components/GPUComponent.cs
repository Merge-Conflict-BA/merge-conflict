/**********************************************************************************************************************
Name:          GPUComponent
Description:   Elements data structure for the GPU.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUComponent : Element, IComponent
{
    public static string Name = "GPU";

    public GPUComponent(int tier, int trashValue, int salesValue) : base(tier, trashValue, salesValue, Name) { }

    public Element? Merge(Element element)
    {

        if (element is GPUComponent otherGPU)
        {

            if ((this.tier == otherGPU.tier) && this.tier < 4)
            {
                this.tier++;
                return this;
            }
        }

        return null;
    }

    public GPUComponent Clone()
    {
        return new GPUComponent(tier, trashValue, salesValue);
    }
}
