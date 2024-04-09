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

    public PowersupplyComponent(int tier) : base(tier, Name) { }

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

    public override ComponentData GetComponentData()
    {
        return Components.PowerSupplyComponentData;
    }

    public override JSONComponent CreateJSONComponentFromElement()
    {
        JSONComponent component = new JSONComponent(tier, Name);

        return component;
    }
}
