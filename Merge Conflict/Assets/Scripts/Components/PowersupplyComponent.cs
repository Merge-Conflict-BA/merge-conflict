/**********************************************************************************************************************
Name:          PowersupplyComponent
Description:   Elements data structure for the powersupply.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersupplyComponent : Element, IComponent
{

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
}
