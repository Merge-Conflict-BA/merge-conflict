/**********************************************************************************************************************
Name:          CaseComponent
Description:   Elements data structure for the case.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseComponent : Element, IComponent
{
    public int mbSlotElementLevel;
    public int powersupplySlotElementLevel;
    public int hddSlotElementLevel;

    public Element Merge(Element element)
    {
        // TODO implement actual merge

        // compare elements and decide if can merge
        // example:
        // element1 = Cpu
        // element2 = MB
        //
        // returns MB (with CPU)


        // case 2
        // element1 & element2 = MB
        // if same lvl
        // return MB (with lvl +1)
        // if not
        // return null


        // https://stackoverflow.com/questions/983030/type-checking-typeof-gettype-or-is

        return element;
    }
}
