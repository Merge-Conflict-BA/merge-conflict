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

    public Trash(int trashPrice, int salesPrice, int salesXP, TrashVariant? variant = null) : base(0, trashPrice, salesPrice, salesXP, Name)
    {
        if (variant != null)
        {
            trashVariant = (TrashVariant)variant;
            return;
        }

        trashVariant = (TrashVariant)Random.Range(0, 3);
    }

    public Trash Clone()
    {
        return new Trash(trashPrice, salesPrice, salesXP, trashVariant);
    }
}
