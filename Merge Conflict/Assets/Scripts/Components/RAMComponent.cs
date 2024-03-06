/**********************************************************************************************************************
Name:          RAMComponent
Description:   Elements data structure for the RAM.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAMComponent : Element, IComponent
{

    public RAMComponent(string newID, string newName, int newLevel, int newTrashValue, int newSalesValue, bool newCanMove)
    {
        id = newID;
        name = newName;
        level = newLevel;
        trashValue = newTrashValue;
        salesValue = newSalesValue;
        canMove = newCanMove;
    }

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
}
