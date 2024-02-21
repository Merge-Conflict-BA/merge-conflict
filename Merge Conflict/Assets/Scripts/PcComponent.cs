using UnityEngine;

public class PcComponent : MonoBehaviour
{
    private Debugger debug = new Debugger();
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
        if (spawnPosition.Equals(Vector3.zero))
        {
            debug.logWarning("Spawnposition (0,0,0) given?!");   
            return;
        }

        if (componentToSpawn.Equals(null))
        {
            debug.logError("Prefab not assigned to spawnObject script. Please assign a prefab in the Unity Editor.");
            return;
        }

        spawnPosition.z = 0;
        GameObject spawnedObject = Instantiate(componentToSpawn, spawnPosition, Quaternion.identity);   
    }
}