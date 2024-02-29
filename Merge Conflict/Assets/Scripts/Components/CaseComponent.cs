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

    public MBComponent? motherboard;
    public PowersupplyComponent? powerSupply;
    public HDDComponent? hdd;



    // Element in der funktion merge ist das Element, dass auf den CaseComponent drauf gemerged wird
    public Element Merge(Element element)
    {

        if (element is CaseComponent otherCase)
        {
            if (HasComponents() || otherCase.HasComponents()) { return null; }

            if (element.level == otherCase.level)
            {
                this.level++;
                return this;
            }
        }
        else if (element is MBComponent otherMB)
        {
            if(motherboard) { return null; }

            this.motherboard = otherMB;
            return this;
        }

        return null;
    }

    private bool HasComponents(){
        return motherboard || powerSupply || hdd;
    }


/*
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

        else if ((obj.GetComponent<CaseComponent>() != null) && (level == obj.GetComponent<CaseComponent>().level) && (level < 4))
        {
            // return element lvl+1;
        }
        else
        {
            Debugger.LogError("Merching not possible.");
        }


        // https://stackoverflow.com/questions/983030/type-checking-typeof-gettype-or-is

    }
    */

}
