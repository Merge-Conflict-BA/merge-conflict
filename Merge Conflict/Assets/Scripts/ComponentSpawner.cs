/**********************************************************************************************************************
Name:          ComponentSpawner
Description:   Spawn the given object at the conveor-belt-object or at an custom postion
Author(s):     Markus Haubold
Date:          2024-02-25
Version:       V1.0 
TODO:          - 
**********************************************************************************************************************/

using UnityEngine;

public class ComponentSpawner : MonoBehaviour
{
    private Debugger debug = new Debugger();
    //define all spawnable components here!
    public GameObject testObjectToSpawn;
    private Vector3 spawnPositionOnBelt = new Vector3(200, 40 ,0);


    void Update()
    {
        //test the function -> spawn object on current mouse position
        //TODO: delete this, if the gamelogic for the spawning is implemented!!!
        if (Input.GetKeyDown("s"))
        {
            spawnComponent(testObjectToSpawn, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown("b"))
        {
            spawnOnBelt(testObjectToSpawn);
        }
    }

    public void spawnComponent(GameObject componentToSpawn, Vector3 spawnPosition)
    {
        if (spawnPosition.Equals(Vector3.zero))
        {
            debug.logWarning("Spawnposition (0,0,0) given?!");   
            return;
        }

        if (componentToSpawn.Equals(null))
        {
            debug.logError("Prefab not assigned to spawnComponent script. Please assign a prefab in the Unity Editor.");
            return;
        }

        spawnPosition.z = 0;
        Instantiate(componentToSpawn, spawnPosition, Quaternion.identity);   
    }

    public void spawnOnBelt(GameObject componentToSpawn)
    {
        spawnComponent(componentToSpawn, spawnPositionOnBelt);
    }
}