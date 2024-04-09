/**********************************************************************************************************************
Name:          Element
Description:   Elements data structure.  

Author(s):     Daniel Rittrich, Hanno Witzleb
Date:          2024-02-26
Version:       V1.2
TODO:          - /
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    private int _tier;
    public int tier
    {
        get { return _tier; }
        set { _tier = Mathf.Clamp(value, 1, 4); }
    }

    public readonly string name;

    public Element(int tier, string name)
    {
        this.tier = tier;
        this.name = name;
    }

    public virtual int GetTrashPrice()
    {
        return GetComponentData().TrashPrices[tier];
    }

    public virtual int GetSalesPrice()
    {
        return GetComponentData().SalePrices[tier];
    }

    public virtual int GetSalesXP()
    {
        return GetComponentData().SaleXP[tier];
    }

    // Is used to decide if a merging lvls up a component or adds one component to another.
    // meant for CaseComponent and MBComponent to override. 
    public virtual bool HasComponents()
    {
        return false;
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
            GameObject slotComponentObject = ComponentSpawner.Instance.SpawnSlotComponent(componentObject, this);

            slotComponentObject = slotTexture.ApplyTexture(slotComponentObject);
            slotComponentObject.GetComponent<SpriteRenderer>().sortingOrder = componentObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            // this is to counteract the repositioning of the (anchoredPosition = Vector2.zero), as the SubComponents now need an offset
            if (this is CaseComponent)
            {
                slotComponentObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(2.5f, 2.5f);
            }
            if (this is MBComponent)
            {
                slotComponentObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(2.25f, 3f);
            }
        }

        return componentObject;
    }

    public virtual bool IsEqual(Element element)
    {
        return GetType() == element.GetType()
            && tier == element.tier;
    }

    public virtual ComponentData GetComponentData()
    {
        return Components.EmptyComponentData;
    }

    public virtual JSONComponent CreateJSONComponentFromElement()
    {
        return JSONComponent.EmptyJSONComponent;
    }
}