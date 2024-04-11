/**********************************************************************************************************************
Name:          Trash
Description:   Elements data structure for trash.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum TrashVariant
{
    BananaPeel = 0,
    Bug = 1,
    Can = 2,
}

public class Trash : Element
{
    public TrashVariant trashVariant;
    public static string Name = "Trash";

    public Trash(TrashVariant? variant = null) : base(0, Name)
    {
        if (variant != null)
        {
            trashVariant = (TrashVariant)variant;
            return;
        }

        trashVariant = (TrashVariant)Random.Range(0, 3);
    }

    public override int GetSalesPrice()
    {
        return 0;
    }

    public override int GetSalesXP()
    {
        return 0;
    }

    public override int GetTrashPrice()
    {
        return GetComponentData().TrashPrices[(int)trashVariant];
    }

    public override ComponentData GetComponentData()
    {
        return Components.TrashComponentData;
    }

    public override SavedElement ToSavedElement()
    {
        return new(name, (int)trashVariant);
    }

    public override Element FromSavedElement(SavedElement savedElement)
    {
        return new Trash((TrashVariant)savedElement.Tier);
    }
}
