/**********************************************************************************************************************
Name:          RAMComponent
Description:   Elements data structure for the RAM.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAMComponent : Element, IComponent
{
    public static string Name = "RAM";

    public RAMComponent(int tier, int trashPrice, int salesPrice, int salesXP) : base(tier, trashPrice, salesPrice, salesXP, Name) { }

    public Element? Merge(Element element)
    {

        if (element is RAMComponent otherRAM)
        {

            if ((this.tier == otherRAM.tier) && this.tier < 4)
            {
                this.tier++;
                return this;
            }
        }

        return null;
    }

    public RAMComponent Clone()
    {
        return new RAMComponent(tier, trashPrice, salesPrice, salesXP);
    }
}
