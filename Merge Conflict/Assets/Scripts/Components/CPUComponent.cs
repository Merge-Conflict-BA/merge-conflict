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

    public CPUComponent(int level, int trashValue, int salesValue) : base(level, trashValue, salesValue) { }

    public Element? Merge(Element element)
    {

        if (element is CPUComponent otherCPU)
        {

            if ((this.level == otherCPU.level) && this.level < 4)
            {
                this.level++;
                return this;
            }
        }

        return null;
    }
}
