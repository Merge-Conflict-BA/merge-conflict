/**********************************************************************************************************************
Name:          ComponentSpawner
Description:   Spawn the given object at the conveor-belt-object or at an custom postion
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-02-28
Version:       V1.2
TODO:          - 
**********************************************************************************************************************/

using ConveyorBelt;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentSpawner : MonoBehaviour
{
    private static ComponentSpawner _instance;
    public static ComponentSpawner Instance { get { return _instance; } }

    [Header("Prefabs")]
    public GameObject componentPrefab;
    public GameObject subComponentPrefab;
    public GameObject spawnPointObject;

    // Spawn Settings
    private const float initialSpawnDelaySeconds = 0f;
    private const float spawnIntervalSeconds = 4f;

    [Header("Idle Movement of components")]
    public bool SamePropertiesForEveryComponent = false;
    public float MinDistance = 30;
    public float MaxDistance = 70;
    public float MovingSpeed = 50;
    public float MinSecondsWithoutMoving = 2;
    public float MaxSecondsWithoutMoving = 4;
    public float TimeToStartMovement = 2;
    public float MaxScaleFactor = 1.05f;

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
        Debugger.LogErrorIf(
            componentPrefab.TryGetComponent(out ComponentHandler _) == false,
            "ComponentSpawner: componentPrefab does not have ComponentHandler attached!!!");
        
        Debugger.LogErrorIf(
            spawnPointObject == null,
            "ComponentSpawner: No Reference SpawnPoint GameObject has been set!");     

        // Current System of spawning: each 4 seconds a random component is spawning
        InvokeRepeating(nameof(SpawnRandomComponentOnBelt), initialSpawnDelaySeconds, spawnIntervalSeconds);
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
            Components.GetRandomElement().InstantiateGameObjectAndAddTexture(GetSpawnPosition());
        }
    }

    public GameObject SpawnComponent(Vector2 spawnPosition, Element element)
    {
        GameObject componentObject = Instantiate(componentPrefab, Vector3.zero, Quaternion.identity, transform.parent);
        componentObject.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        componentObject.name = $"{element.GetType()}_lvl_{element.level}_merged";
        componentObject.tag = Tags.Component.ToString();
        ComponentHandler componentHandler = componentObject.GetComponent<ComponentHandler>();
        componentHandler.element = element;

        if (SamePropertiesForEveryComponent)
        {
            bool success = componentObject.TryGetComponent(out ComponentMovement componentMovement);
            if (success)
            {
                componentMovement.InitializeProperties(MinDistance, MaxDistance, MovingSpeed, MinSecondsWithoutMoving, MaxSecondsWithoutMoving, TimeToStartMovement, MaxScaleFactor);
            }
        }
        
        // move Component in Front of the Conveyor Belt
        componentObject.transform.position += new Vector3(0, 0, -1);
        
        // 
        GameState.Instance.ElementIsInstantiated(element);

        return componentObject;
    }

    public GameObject SpawnSlotComponent(GameObject parentComponentObject, Element element)
    {
        GameObject slotComponentObject = Instantiate(subComponentPrefab);
        slotComponentObject.name = $"{element.GetType()}_child";
        slotComponentObject.tag = Tags.SubComponent.ToString();
        slotComponentObject.transform.SetParent(parentComponentObject.transform, true);     
        slotComponentObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        return slotComponentObject;
    }

    public void SpawnRandomComponentOnBelt()
    {
        Components.GetRandomElement().InstantiateGameObjectAndAddTexture(GetSpawnPosition());
    }

    private Vector2 GetSpawnPosition()
    {
        return spawnPointObject.GetComponent<RectTransform>().anchoredPosition;
    }
}