/**********************************************************************************************************************
Name:          ComponentSpawner
Description:   Spawn the given object at the conveor-belt-object or at an custom postion
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-02-28
Version:       V1.2
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;

public class ComponentSpawner : MonoBehaviour
{
    private static ComponentSpawner _instance;
    public static ComponentSpawner Instance { get { return _instance; } }

    //define all spawnable components here!
    public GameObject componentPrefab;
    private Vector3 spawnPositionOnBelt;

    public GameObject ConveyorBeltGameObject;

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        // check if componentPrefab has ComponentHandler
        if (componentPrefab.TryGetComponent(out ComponentHandler _) == false)
        {
            Debugger.LogError("ComponentSpawner: componentPrefab does not have ComponentHandler attached!!!");
        }

        // set position of spawning (in the center of the conveyor belt)
        ConveyorBelt.ConveyorBelt conveyorBelt = ConveyorBeltGameObject.GetComponent<ConveyorBelt.ConveyorBelt>();
        var prefabSize = conveyorBelt.PrefabConveyorBeltVertical.GetComponent<RectTransform>().rect.size;

        spawnPositionOnBelt = new Vector3(prefabSize.x / 2, Screen.height + prefabSize.y, 0);

        // Current System of spawning: each 4 seconds a random component is spawning
        InvokeRepeating("SpawnRandomComponentOnBelt", 0f, 4f);
    }

    void Update()
    {
        //test the function -> spawn object on current mouse position
        //TODO: delete this, if the gamelogic for the spawning is implemented!!!
        if (Input.GetKeyDown(KeyCode.S))
        {
            Components.GetRandomElement().InstantiateGameObjectAndAddTexture(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Components.GetRandomElement().InstantiateGameObjectAndAddTexture(spawnPositionOnBelt);
        }
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
        GameObject slotComponentObject = Instantiate(new GameObject(""));
        slotComponentObject.name = $"{element.GetType()}_child";
        slotComponentObject.tag = Tags.Untagged.ToString();
        slotComponentObject.transform.position = spawnPosition;
        slotComponentObject.transform.SetParent(parentComponentObject.transform, true);
        slotComponentObject.AddComponent<SpriteRenderer>();
        slotComponentObject.AddComponent<RectTransform>();

        return slotComponentObject;
    }

    public void SpawnRandomComponentOnBelt()
    {
        Components.GetRandomElement().InstantiateGameObjectAndAddTexture(spawnPositionOnBelt);
    }
}