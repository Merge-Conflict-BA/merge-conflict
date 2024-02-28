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

    public void Merge(GameObject obj)
    {
        // TODO implement actual merge

        // compare elements and decide if can merge
        // example:
        // element1 = Cpu
        // element2 = MB
        //
        // returns MB (with CPU)

        Debug.Log("Test_Merge_CaseComponent");

        if ((obj.GetComponent<PowersupplyComponent>() != null) && (powersupplySlotElementLevel == 0))
        {
            ComponentSpawnerDaniel.Instance.SpawnObject(this.gameObject, this.gameObject.transform.position);
        }
        else if ((obj.GetComponent<HDDComponent>() != null) && (hddSlotElementLevel == 0))
        {
            // TODO weiter machen
        }
        else if ((obj.GetComponent<MBComponent>() != null) && (mbSlotElementLevel == 0))
        {
            // TODO weiter machen
        }


        // case 2
        // element1 & element2 = MB
        // if same lvl
        // return MB (with lvl +1)
        // if not
        // return null

        else if ((obj.GetComponent<CaseComponent>() != null) && (itemLevel == obj.GetComponent<CaseComponent>().itemLevel) && (itemLevel < 4))
        {
            // return element lvl+1;
        }
        else
        {
            Debugger.LogError("Merching not possible.");
        }


        // https://stackoverflow.com/questions/983030/type-checking-typeof-gettype-or-is

    }
}
