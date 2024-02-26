/**********************************************************************************************************************
Name:          PcComponentCase
Description:   Elements data structure for the case.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Own/Element/PcComponent/Case")]
public class PcComponentCase : PcComponent
{
    public int mbSlotElementLevel;
    public int powersupplySlotElementLevel;
    public int hddSlotElementLevel;
}
