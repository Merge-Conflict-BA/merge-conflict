/**********************************************************************************************************************
Name:          GPUComponent
Description:   Elements data structure for the GPU.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUComponent : Element, IComponent
{
    public static string Name = "GPU";

    public GPUComponent(int tier) : base(tier, Name) { }

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

    public override ComponentData GetComponentData()
    {
        return Components.GpuComponentData;
    }

    public override Element FromJSONElement(JSONElement jsonElement)
    {
        return new GPUComponent(jsonElement.Tier);
    }
}
