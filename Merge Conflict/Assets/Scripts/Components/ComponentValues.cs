/**********************************************************************************************************************
Name:          ComponentValues
Description:   Collects the sales and trash values of all components in one place.  

Author(s):     Daniel Rittrich
Date:          2024-03-07
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentValues
{
    public static int[] caseTrashValues = { 5, 14, 26, 44 };
    public static int[] caseSalesValues = { 21, 57, 107, 178 };
    public static int[] powersupplyTrashValues = { 5, 14, 26, 44 };
    public static int[] powersupplySalesValues = { 21, 57, 107, 178 };
    public static int[] hddTrashValues = { 7, 19, 35, 59 };
    public static int[] hddSalesValues = { 28, 76, 142, 237 };
    public static int[] cpuTrashValues = { 16, 42, 80, 133 };
    public static int[] cpuSalesValues = { 64, 171, 321, 535 };
    public static int[] gpuTrashValues = { 23, 61, 116, 193 };
    public static int[] gpuSalesValues = { 92, 247, 464, 773 };
    public static int[] ramTrashValues = { 8, 23, 44, 74 };
    public static int[] ramSalesValues = { 35, 95, 178, 297 };
    public static int[] mbTrashValues = { 8, 23, 44, 74 };
    public static int[] mbSalesValues = { 35, 95, 178, 297 };
    public static int[] trashTrashValues = { 2, 1, 3 };
    public static int[] trashSalesValues = { 3, 2, 5 };


    // ! catch the returning tuple like this :          var (trashValue, salesValue) = ComponentValues.GetComponentValues(MyComponentClass);
    public static (int trashValue, int salesValue) GetComponentValues(Element element)
    {
        int index = element.level - 1;

        switch (element)
        {
            case CaseComponent:
                return (caseTrashValues[index], caseSalesValues[index]);

            case PowersupplyComponent:
                return (powersupplyTrashValues[index], powersupplySalesValues[index]);

            case HDDComponent:
                return (hddTrashValues[index], hddSalesValues[index]);

            case MBComponent:
                return (mbTrashValues[index], mbSalesValues[index]);

            case CPUComponent:
                return (cpuTrashValues[index], cpuSalesValues[index]);

            case RAMComponent:
                return (ramTrashValues[index], ramSalesValues[index]);

            case GPUComponent:
                return (gpuTrashValues[index], gpuSalesValues[index]);

            case Trash trash:
                return (trashTrashValues[(int)trash.trashVariant], trashSalesValues[(int)trash.trashVariant]);

            default:
                Debugger.LogError("No matching component for the return of trash and sales values.");
                return (5, 10);
        }
    }

}
