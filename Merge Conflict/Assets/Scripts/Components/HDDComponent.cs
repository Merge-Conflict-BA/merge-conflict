/**********************************************************************************************************************
Name:          HDDComponent
Description:   Elements data structure for the HDD.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDDComponent : Element, IComponent
{

    public HDDComponent(int tier, int trashValue, int salesValue) : base(tier, trashValue, salesValue) { }

    public Element? Merge(Element element)
    {

        if (element is HDDComponent otherHDD)
        {

            if ((this.tier == otherHDD.tier) && this.tier < 4)
            {
                this.tier++;
                return this;
            }
        }

        return null;
    }

    public HDDComponent Clone()
    {
        return new HDDComponent(tier, trashValue, salesValue);
    }
}
