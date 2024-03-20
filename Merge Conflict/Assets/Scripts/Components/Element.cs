/**********************************************************************************************************************
Name:          Element
Description:   Elements data structure.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.1
TODO:          - /
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    private int _level;
    public int level
    {
        get { return _level; }
        set { _level = Mathf.Clamp(value, 1, 4); }
    }
    public readonly int trashValue;
    public readonly int salesValue;

    public Element(int level, int trashValue, int salesValue)
    {
        this.level = level;
        this.trashValue = trashValue;
        this.salesValue = salesValue;
    }

    public virtual int GetTrashValue()
    {
        return trashValue;
    }

    public virtual int GetSalesValue()
    {
        return salesValue;
    }

    public GameObject InstantiateGameObjectAndAddTexture(Vector2 position)
    {
        // instantiate new GameObject from prefab
        GameObject componentObject = ComponentSpawner.Instance.SpawnComponent(position, this);

        // get texture for main component and add it to the new GameObject
        ElementTexture componentTexture = TextureAtlas.Instance.GetComponentTexture(this);
        componentObject = componentTexture.ApplyTexture(componentObject);
        componentObject.GetComponent<BoxCollider2D>().isTrigger = false;
        componentObject.GetComponent<BoxCollider2D>().size = new Vector2(componentTexture.sizeWidth, componentTexture.sizeHeight);

        // check whether the element has subcomponents
        List<ElementTexture> listOfSlotComponentTextures = TextureAtlas.Instance.GetSlotComponentTextures(this);
        if (listOfSlotComponentTextures.Count == 0)
        {
            return componentObject;
        }

        // instantiate a child GameObject for each subcomponent in the element to layer its sprite over the texture of the main element
        foreach (ElementTexture slotTexture in listOfSlotComponentTextures)
        {
            GameObject slotComponentObject = ComponentSpawner.Instance.SpawnSlotComponent(position, componentObject, this);

            slotComponentObject = slotTexture.ApplyTexture(slotComponentObject);
            slotComponentObject.GetComponent<SpriteRenderer>().sortingOrder = componentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }

        return componentObject;
    }

    // function that checks if two components are equal ( => same type and same level) (it also checks the types and levels of the sub components )
#nullable enable
    public virtual bool CompareElements(Element? element1, Element? element2)
    {
        if (element1 == null && element2 == null) return true;
        if (element1 == null || element2 == null) return false;

        if (element1.GetType() != element2.GetType())
        {
            return false;
        }

        if (element1.level != element2.level)
        {
            return false;
        }

        if (element1 is CaseComponent case1 && element2 is CaseComponent case2)
        {
            return (case1.powersupply == null && case2.powersupply == null || CompareElements(case1.powersupply, case2.powersupply)) &&
                   (case1.hdd == null && case2.hdd == null || CompareElements(case1.hdd, case2.hdd)) &&
                   (case1.motherboard == null && case2.motherboard == null || CompareElements(case1.motherboard, case2.motherboard));
        }
        else if (element1 is MBComponent mb1 && element2 is MBComponent mb2)
        {
            return (mb1.cpu == null && mb2.cpu == null || CompareElements(mb1.cpu, mb2.cpu)) &&
                   (mb1.ram == null && mb2.ram == null || CompareElements(mb1.ram, mb2.ram)) &&
                   (mb1.gpu == null && mb2.gpu == null || CompareElements(mb1.gpu, mb2.gpu));
        }
        else
        {
            return true;
        }
    }
#nullable restore

}