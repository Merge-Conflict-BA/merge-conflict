using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcComponent : MonoBehaviour
{
    //define all spawnable components here!
    public GameObject componentA;


    void Update()
    {
        //test the function -> spawn object on current mouse position
        //TODO: delete this, if the gamelogic for the spawning is implemented!!!
        if (Input.GetKeyDown("s"))
        {
            spawnObject(componentA, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void spawnObject(GameObject componentToSpawn, Vector3 spawnPosition)
    {
        if (componentToSpawn != null)
        {
            spawnPosition.z = 0;
            GameObject spawnedObject = Instantiate(componentToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab not assigned to spawnObject script. Please assign a prefab in the Unity Editor.");
        }
    }
}
