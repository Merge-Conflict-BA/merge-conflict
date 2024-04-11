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

// TODO : Beachten
// When two elements meet, both their Merge() functions should be called (if one succeeds, dont try the other one)
// This is to mitigate a problem, where e.g. a motherboard gets dragged on top of a case or the other way around => a different Merge() function would be called
// Therefore we always call both Merge() functions and only implement Case specific logic in CaseComponent(e.g. adding a motherboard to a case)
public interface IComponent
{
    public Element? Merge(Element element);
}