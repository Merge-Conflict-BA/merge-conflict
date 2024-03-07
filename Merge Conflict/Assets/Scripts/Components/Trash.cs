/**********************************************************************************************************************
Name:          Trash
Description:   Elements data structure for trash.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashVariant
{
    BananaPeel = 0,
    Bug = 1,
    Can = 2,
}

public class Trash : Element
{
    public TrashVariant trashVariant;   

    public Trash(int trashValue, int salesValue, TrashVariant? variant = null) : base(0, trashValue, salesValue)
    {
        if (variant != null)
        {
            trashVariant = (TrashVariant)variant;
            return;
        }

        System.Random random = new();

        trashVariant = (TrashVariant)random.Next(0, 3);  // gives back a random value from 0 till 2
    }
}
