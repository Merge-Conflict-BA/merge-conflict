/**********************************************************************************************************************
Name:          PcComponentMB
Description:   Elements data structure for motherboard.  

Author(s):     Daniel Rittrich
Date:          2024-02-26
Version:       V1.0 
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Own/Element/PcComponent/MB")]
public class PcComponentMB : PcComponent
{
    public int cpuSlotElementLevel;
    public int gpuSlotElementLevel;
    public int ramSlotElementLevel;
}
