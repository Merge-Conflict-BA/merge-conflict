/**********************************************************************************************************************
Name:          Trash
Description:   Elements data structure for trash.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Element
{
    public int trashVariant;   // 0 == banana_peel     1 == bug     2 == can

    public Trash(string newID, string newName, int newTrashValue, int newSalesValue)
    {
        System.Random random = new();

        id = newID;
        name = newName;
        trashValue = newTrashValue;
        salesValue = newSalesValue;
        canMove = false;
        trashVariant = random.Next(0, 3);  // gives back a random value from 0 till 2
    }
}
