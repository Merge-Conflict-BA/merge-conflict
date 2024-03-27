/**********************************************************************************************************************
Name:          RAMComponent
Description:   Elements data structure for the RAM.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAMComponent : Element, IComponent
{
    public static string Name = "RAM";

    public RAMComponent(int level, int trashValue, int salesValue) : base(level, trashValue, salesValue, Name) { }

    public Element? Merge(Element element)
    {

        if (element is RAMComponent otherRAM)
        {

            if ((this.level == otherRAM.level) && this.level < 4)
            {
                this.level++;
                return this;
            }
        }

        return null;
    }

    public RAMComponent Clone()
    {
        return new RAMComponent(level, trashValue, salesValue);
    }
}
