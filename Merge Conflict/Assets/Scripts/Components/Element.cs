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
}