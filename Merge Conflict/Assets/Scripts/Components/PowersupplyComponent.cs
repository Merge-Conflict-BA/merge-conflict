/**********************************************************************************************************************
Name:          PowersupplyComponent
Description:   Elements data structure for the powersupply.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersupplyComponent : Element, IComponent
{
    public static string Name = "PowerSupply";

    public PowersupplyComponent(int level, int trashValue, int salesValue) : base(level, trashValue, salesValue, Name) { }

    public Element? Merge(Element element)
    {

        if (element is PowersupplyComponent otherPowersupply)
        {

            if ((this.level == otherPowersupply.level) && this.level < 4)
            {
                this.level++;
                return this;
            }
        }

        return null;
    }

    public PowersupplyComponent Clone()
    {
        return new PowersupplyComponent(level, trashValue, salesValue);
    }
}
