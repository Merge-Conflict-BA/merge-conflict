/**********************************************************************************************************************
Name:          GPUComponent
Description:   Elements data structure for the GPU.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUComponent : Element, IComponent
{

    public Element? Merge(Element element)
    {

        if (element is GPUComponent otherGPU)
        {

            if ((element.level == otherGPU.level) && element.level < 4)
            {
                this.level++;
                return this;
            }
        }

        return null;
    }
}
