/**********************************************************************************************************************
Name:          Component
Description:   Elements data structure for pc component.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent
{
    public Element? Merge(Element element);
}