/**********************************************************************************************************************
Name:          ComponentSpawner
Description:   Spawn the given object at the conveor-belt-object or at an custom postion
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-02-28
Version:       V1.3
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class ComponentSpawner : MonoBehaviour
{
    private static ComponentSpawner _instance;
    public static ComponentSpawner Instance { get { return _instance; } }

    [Header("Prefabs")]
    public GameObject componentPrefab;
    public GameObject subComponentPrefab;

    [Header("GameObject Reference")]
    public GameObject spawnPointObject;
    public GameObject componentsHolderObject;

    // Spawn Settings    
    private const float initialSpawnDelaySeconds = 0f;  

    [Header("Idle Movement of components")]
    public bool SamePropertiesForEveryComponent = false;
    public float MinDistance = 30;
    public float MaxDistance = 70;
    public float MovingSpeed = 50;
    public float ReturningMovingSpeed = 2000;
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

        Debugger.LogErrorIf(
            componentsHolderObject == null,
            "ComponentSpawner: No componentsHolderObject GameObject has been set!");

        // set position of spawnPoint
        spawnPointObject.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        spawnPointObject.GetComponent<RectTransform>().anchorMax = Vector2.zero;
        spawnPointObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            50,
            transform.parent.GetComponent<RectTransform>().rect.height + 100);
        
        StartCoroutine(SpawnOnBeltInInterval(initialSpawnDelaySeconds));
    }

    void Update()
    {
#if UNITY_EDITOR
        //test the function -> spawn object on current mouse position
        //TODO: delete this, if the gamelogic for the spawning is implemented!!!
        if (Input.GetKeyDown(KeyCode.S))
        {
            Components.GetRandomElement().InstantiateGameObjectAndAddTexture(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Components.GetRandomElement().InstantiateGameObjectAndAddTexture(GetBeltSpawnPosition());
        }
#endif
    }

    // copied from: https://gamedev.stackexchange.com/questions/139736/how-can-i-change-invokerepeating-time-in-unity
    // after every spawn, this Coroutine reevaluates the current Interval
    private IEnumerator SpawnOnBeltInInterval(float initialDelaySeconds)
    {
        yield return new WaitForSeconds(initialDelaySeconds);
        while (true)
        {
            SpawnRandomComponentOnBelt();
            Debugger.LogMessage("Spawned new Component on Belt");

            int currentInterval = Upgrades.SpawnIntervalUpgrade.GetCurrentSecondsInterval();
            yield return new WaitForSeconds(currentInterval);
        }
    }

    public GameObject SpawnComponent(Vector2 spawnPosition, Element element)
    {
        GameObject componentObject = Instantiate(componentPrefab, transform.parent.position, Quaternion.identity, componentsHolderObject.transform);
        componentObject.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
        componentObject.name = $"{element.GetType()}_lvl_{element.tier}_merged";
        componentObject.tag = Tags.Component.ToString();

        ComponentHandler componentHandler = componentObject.GetComponent<ComponentHandler>();
        componentHandler.element = element;

        if (SamePropertiesForEveryComponent)
        {
            bool success = componentObject.TryGetComponent(out ComponentMovement componentMovement);
            if (success)
            {
                componentMovement.InitializeProperties(MinDistance, MaxDistance, MovingSpeed, ReturningMovingSpeed, MinSecondsWithoutMoving, MaxSecondsWithoutMoving, TimeToStartMovement, MaxScaleFactor);
            }
        }

        // move Component in Front of the Conveyor Belt
        componentObject.transform.position += new Vector3(0, 0, -1);
        
        // Update List with FoundElements to create a new card in the ElementsMenu if necessary
        FoundElementsHandler.Instance.CheckIfElementIsNew(element);

        return componentObject;
    }

    public GameObject SpawnSlotComponent(GameObject parentComponentObject, Element element)
    {
        GameObject slotComponentObject = Instantiate(subComponentPrefab);
        slotComponentObject.name = $"{element.GetType()}_child";
        slotComponentObject.tag = Tags.SubComponent.ToString();
        slotComponentObject.transform.SetParent(parentComponentObject.transform, true);

        return slotComponentObject;
    }

    public void SpawnRandomComponentOnBelt()
    {
        Components.GetRandomElement().InstantiateGameObjectAndAddTexture(GetBeltSpawnPosition());
    }

    public void SpawnRandomComponentOnRandomPositionOnDesk(float delaySeconds = 0.0f)
    {
        StartCoroutine(SpawnRandomComponentOnRandomPositionOnDeskWithDelay(delaySeconds));
    }

    private IEnumerator SpawnRandomComponentOnRandomPositionOnDeskWithDelay(float delaySeconds)
    {
        Vector2 randomPosition = GetRandomPositionOnDesk();
        AnimationManager.Instance.PlayComponentSpawnedOnDeskAnimation(randomPosition);

        yield return new WaitForSeconds(delaySeconds);
        Components.GetRandomElement().InstantiateGameObjectAndAddTexture(randomPosition);
    }

    private Vector2 GetBeltSpawnPosition()
    {
        return spawnPointObject.GetComponent<RectTransform>().anchoredPosition;
    }
    
    public Vector2 GetRandomPositionOnDesk()
    {
        GameObject deskObject = GameObject.FindWithTag("desk");
        RectTransform rectTransform = deskObject.GetComponent<RectTransform>();
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        int padding = 100;

        float x = Random.Range(anchoredPosition.x + padding, anchoredPosition.x + rectTransform.rect.width - padding);
        float y = Random.Range(anchoredPosition.y + padding, anchoredPosition.y + rectTransform.rect.height - padding);

        return new Vector2(x, y);
    }
}