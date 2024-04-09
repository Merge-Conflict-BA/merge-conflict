/**********************************************************************************************************************
Name:          HDDComponent
Description:   Elements data structure for the HDD.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDDComponent : Element, IComponent
{
    public static string Name = "HDD";

    public HDDComponent(int tier) : base(tier, Name) { }

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

    public override ComponentData GetComponentData()
    {
        return Components.HddComponentData;
    }

    public override JSONComponent CreateJSONComponentFromElement()
    {
        JSONComponent component = new JSONComponent(tier, Name);

        return component;
    }
}
