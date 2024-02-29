/**********************************************************************************************************************
Name:          HDDComponent
Description:   Elements data structure for the HDD.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDDComponent : Element, IComponent
{

    public Element? Merge(Element element)
    {

        if (element is HDDComponent otherHDD)
        {

            if ((element.level == otherHDD.level) && element.level < 4)
            {
                this.level++;
                return this;
            }
        }

        return null;
    }
}
