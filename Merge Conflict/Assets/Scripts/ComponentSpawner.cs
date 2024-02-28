/**********************************************************************************************************************
Name:          ComponentSpawner
Description:   Spawn the given object at the conveor-belt-object or at an custom postion
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-02-28
Version:       V1.2
TODO:          - 
**********************************************************************************************************************/

using ConveyorBelt;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentSpawner : MonoBehaviour
{
    private static ComponentSpawner _instance;
    public static ComponentSpawner Instance { get { return _instance; } }

    //define all spawnable components here!
    public GameObject testObjectToSpawn;
    public GameObject[] ObjectsToSpawn;
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
            SpawnComponent(GetRandomGameObject(), Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnOnBelt(GetRandomGameObject());
        }
    }

    public void SpawnComponent(GameObject componentToSpawn, Vector2 spawnPosition)
    {
        if (spawnPosition.Equals(Vector2.zero))
        {
            Debugger.LogWarning("Spawnposition (0,0) given?!");
            return;
        }

        if (componentToSpawn.Equals(null))
        {
            Debugger.LogError("Prefab not assigned to SpawnComponent script. Please assign a prefab in the Unity Editor.");
            return;
        }

        GameObject component = Instantiate(componentToSpawn, spawnPosition, Quaternion.identity, transform.parent);
        // move Component in Front of the Conveyor Belt
        component.transform.position += new Vector3(0, 0, -1);
    }

    public void SpawnOnBelt(GameObject componentToSpawn)
    {
        SpawnComponent(componentToSpawn, spawnPositionOnBelt);
    }

    private void SpawnRandomComponentOnBelt()
    {
        SpawnOnBelt(GetRandomGameObject());
    }

    private GameObject GetRandomGameObject()
    {
        return ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)];
    }
}