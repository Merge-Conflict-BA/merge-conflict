/**********************************************************************************************************************
Name:          SellingStationHandler
Description:   Handles some function of the selling station.  

Author(s):     Daniel Rittrich
Date:          2024-03-15
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingStationHandler : MonoBehaviour
{
    private static SellingStationHandler _instance;
    public static SellingStationHandler Instance { get { return _instance; } }

    public GameObject sellingStation;
    public GameObject emptyComponentPrefab;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

/* 
    public GameObject SetNewQuestImage(Element element)
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




    public GameObject SpawnComponent(Vector2 spawnPosition, Element element)
    {
        GameObject componentObject = Instantiate(componentPrefab, spawnPosition, Quaternion.Euler(0, 0, 0), transform.parent);
        componentObject.name = $"{element.GetType()}_lvl_{element.level}_merged";
        componentObject.tag = Tags.Component.ToString();
        ComponentHandler componentHandler = componentObject.GetComponent<ComponentHandler>();
        componentHandler.element = element;

        // move Component in Front of the Conveyor Belt
        componentObject.transform.position += new Vector3(0, 0, -1);

        return componentObject;
    }




    public GameObject SpawnSlotComponent(Vector2 spawnPosition, GameObject parentComponentObject, Element element)
    {
        GameObject slotComponentObject = Instantiate(subComponentPrefab);
        slotComponentObject.name = $"{element.GetType()}_child";
        slotComponentObject.tag = Tags.SubComponent.ToString();
        slotComponentObject.transform.position = spawnPosition;
        slotComponentObject.transform.SetParent(parentComponentObject.transform, true);
        slotComponentObject.AddComponent<SpriteRenderer>();
        slotComponentObject.AddComponent<RectTransform>();

        return slotComponentObject;
    }
 */
}
