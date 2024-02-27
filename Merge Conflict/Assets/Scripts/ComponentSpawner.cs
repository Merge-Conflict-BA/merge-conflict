/**********************************************************************************************************************
Name:          ComponentSpawner
Description:   Spawn the given object at the conveor-belt-object or at an custom postion
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-02-27
Version:       V1.2
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;

public class ComponentSpawner : MonoBehaviour
{
    private static ComponentSpawner _instance;
    public static ComponentSpawner Instance { get { return _instance; } }

    //define all spawnable components here!
    public GameObject testObjectToSpawn;
    private Vector3 spawnPositionOnBelt = new Vector2(45, Screen.height + 50);

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

    void Update()
    {
        //test the function -> spawn object on current mouse position
        //TODO: delete this, if the gamelogic for the spawning is implemented!!!
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnComponent(testObjectToSpawn, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnOnBelt(testObjectToSpawn);
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

        Instantiate(componentToSpawn, spawnPosition, Quaternion.identity, transform.parent);
    }

    public void SpawnOnBelt(GameObject componentToSpawn)
    {
        SpawnComponent(componentToSpawn, spawnPositionOnBelt);
    }
}