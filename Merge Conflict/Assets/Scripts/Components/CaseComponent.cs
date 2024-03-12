/**********************************************************************************************************************
Name:          CaseComponent
Description:   Elements data structure for the case.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V2.1
TODO:          - /
**********************************************************************************************************************/

#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseComponent : Element, IComponent
{

    public MBComponent? motherboard;
    public PowersupplyComponent? powersupply;
    public HDDComponent? hdd;


    public CaseComponent(int level, int trashValue, int salesValue, MBComponent? motherboard = null, PowersupplyComponent? powersupply = null, HDDComponent? hdd = null)
        : base(level, trashValue, salesValue)
    {
        this.motherboard = motherboard;
        this.powersupply = powersupply;
        this.hdd = hdd;
    }


    // !  später mal noch testen : evtl. muss bei den Zeilen            if (motherboard) { return null; }     this.motherboard = otherMB;            erst der Component mit AddComponent<MBComponent>() hinzugefügt werden

    // Element in der funktion merge ist das Element, dass auf den CaseComponent drauf gemerged wird
    public Element? Merge(Element element)
    {

        if (element is CaseComponent otherCase)
        {
            if (HasComponents() || otherCase.HasComponents()) { return null; }

            if ((this.level == otherCase.level) && this.level < 4)
            {
                this.level++;
                return this;
            }
        }
        else if (element is MBComponent otherMB)
        {
            if (motherboard != null) { return null; }

            this.motherboard = otherMB;
            return this;
        }
        else if (element is PowersupplyComponent otherPowersupply)
        {
            if (powersupply != null) { return null; }

            this.powersupply = otherPowersupply;
            return this;
        }
        else if (element is HDDComponent otherHDD)
        {
            if (hdd != null) { return null; }

            this.hdd = otherHDD;
            return this;
        }

        return null;
    }

    private bool HasComponents()
    {
        return motherboard != null || powersupply != null || hdd != null;
    }

    override public int GetTrashValue()
    {
        int hddTrashValue           = hdd           != null ? hdd.GetTrashValue() : 0;
        int powersupplyTrashValue   = powersupply   != null ? powersupply.GetTrashValue() : 0;
        int motherboardTrashValue   = motherboard   != null ? motherboard.GetTrashValue() : 0;

        return this.GetTrashValue() + hddTrashValue + powersupplyTrashValue + motherboardTrashValue;
    }

    override public int GetSalesValue()
    {
        int hddSalesValue = hdd != null ? hdd.GetSalesValue() : 0;
        int powersupplySalesValue = powersupply != null ? powersupply.GetSalesValue() : 0;
        int motherboardSalesValue = motherboard != null ? motherboard.GetSalesValue() : 0;

        return this.GetSalesValue() + hddSalesValue + powersupplySalesValue + motherboardSalesValue;
    }

    public CaseComponent Clone()
    {
        return new CaseComponent(level, trashValue, salesValue, motherboard, powersupply, hdd);
    }
}
