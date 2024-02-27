/**********************************************************************************************************************
Name:          MBComponent
Description:   Elements data structure for motherboard.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBComponent : Element, IComponent
{
    public int cpuSlotElementLevel;
    public int gpuSlotElementLevel;
    public int ramSlotElementLevel;

    public Element Merge(Element element){
        // TODO implement actual merge
        return element;
    }
}
