/**********************************************************************************************************************
Name:          PowersupplyComponent
Description:   Elements data structure for the powersupply.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersupplyComponent : Element, IComponent
{
    public static string Name = "PowerSupply";

    public PowersupplyComponent(int tier, int trashPrice, int salesPrice, int salesXP) : base(tier, trashPrice, salesPrice, salesXP, Name) { }

    public Element? Merge(Element element)
    {

        if (element is PowersupplyComponent otherPowersupply)
        {

            if ((this.tier == otherPowersupply.tier) && this.tier < 4)
            {
                this.tier++;
                return this;
            }
        }

        return null;
    }

    public PowersupplyComponent Clone()
    {
        return new PowersupplyComponent(tier, trashPrice, salesPrice, salesXP);
    }
}
