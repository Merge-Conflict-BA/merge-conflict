/**********************************************************************************************************************
Name:          CaseComponent
Description:   Elements data structure for the case.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V2.4
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
    public static string Name = "Case";


    public CaseComponent(int tier, MBComponent? motherboard = null, PowersupplyComponent? powersupply = null, HDDComponent? hdd = null)
        : base(tier, Name)
    {
        this.motherboard = motherboard;
        this.powersupply = powersupply;
        this.hdd = hdd;
    }


    // Element in der funktion merge ist das Element, dass auf den CaseComponent drauf gemerged wird
    public Element? Merge(Element element)
    {
        if (element is CaseComponent otherCase)
        {
            if (HasComponents() || otherCase.HasComponents()) { return null; }

            if ((this.tier == otherCase.tier) && this.tier < 4)
            {
                this.tier++;
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

    override public bool HasComponents()
    {
        return motherboard != null || powersupply != null || hdd != null;
    }

    override public int GetTrashPrice()
    {
        int hddTrashPrice = hdd != null ? hdd.GetTrashPrice() : 0;
        int powersupplyTrashPrice = powersupply != null ? powersupply.GetTrashPrice() : 0;
        int motherboardTrashPrice = motherboard != null ? motherboard.GetTrashPrice() : 0;

        return base.GetTrashPrice() + hddTrashPrice + powersupplyTrashPrice + motherboardTrashPrice;
    }

    override public int GetSalesPrice()
    {
        int hddSalesPrice = hdd != null ? hdd.GetSalesPrice() : 0;
        int powersupplySalesPrice = powersupply != null ? powersupply.GetSalesPrice() : 0;
        int motherboardSalesPrice = motherboard != null ? motherboard.GetSalesPrice() : 0;

        return base.GetSalesPrice() + hddSalesPrice + powersupplySalesPrice + motherboardSalesPrice;
    }

    override public int GetSalesXP()
    {
        int hddSalesXP = hdd != null ? hdd.GetSalesXP() : 0;
        int powersupplySalesXP = powersupply != null ? powersupply.GetSalesXP() : 0;
        int motherboardSalesXP = motherboard != null ? motherboard.GetSalesXP() : 0;

        return base.GetSalesXP() + hddSalesXP + powersupplySalesXP + motherboardSalesXP;
    }

    public override bool IsEqual(Element element)
    {
        if (base.IsEqual(element) == false)
        {
            return false;
        }

        CaseComponent caseComponent = (CaseComponent)element;

        bool isMotherboardEqual = (motherboard == null && caseComponent.motherboard == null) ? true : (motherboard != null && caseComponent.motherboard != null) ? motherboard.IsEqual(caseComponent.motherboard) : false;
        bool isHDDEqual = (hdd == null && caseComponent.hdd == null) ? true : (hdd != null && caseComponent.hdd != null) ? hdd.IsEqual(caseComponent.hdd) : false;
        bool isPowerSupplyEqual = (powersupply == null && caseComponent.powersupply == null) ? true : (powersupply != null && caseComponent.powersupply != null) ? powersupply.IsEqual(caseComponent.powersupply) : false;

        return isMotherboardEqual && isHDDEqual && isPowerSupplyEqual;
    }

    public override ComponentData GetComponentData()
    {
        return Components.CaseComponentData;
    }

    public override JSONElement ToJSONElement()
    {
        List<JSONElement> children = new();

        if (hdd != null)
        {
            children.Add(hdd.ToJSONElement());
        }
        if (powersupply != null)
        {
            children.Add(powersupply.ToJSONElement());
        }
        if (motherboard != null)
        {
            children.Add(motherboard.ToJSONElement());
        }

        return new(name, tier, children);
    }

    public override Element FromJSONElement(JSONElement jsonElement)
    {
        CaseComponent caseComponent = new(jsonElement.Tier);

        if (jsonElement.Children.Count == 0)
        {
            return caseComponent;
        }

        foreach (JSONElement childElement in jsonElement.Children)
        {
            switch (childElement.Name)
            {
                case var _ when childElement.Name.Equals(HDDComponent.Name):
                    caseComponent.hdd = new HDDComponent(childElement.Tier);
                    break;

                case var _ when childElement.Name.Equals(PowersupplyComponent.Name):
                    caseComponent.powersupply = new PowersupplyComponent(childElement.Tier);
                    break;

                case var _ when childElement.Name.Equals(MBComponent.Name):
                    caseComponent.motherboard = new MBComponent(childElement.Tier);
                    caseComponent.motherboard = (MBComponent)caseComponent.motherboard.FromJSONElement(childElement);
                    break;

                default:
                    break;
            }
        }

        return caseComponent;
    }
}
