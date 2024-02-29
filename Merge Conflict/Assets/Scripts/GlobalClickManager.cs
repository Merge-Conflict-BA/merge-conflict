using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalClickManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject objectCPU;
    public GameObject objectRAM;
    public GameObject objectGPU;


    public GameObject baseObjectToSpawn;


    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

                /* GameObject test = GameObject.Instantiate(baseObjectToSpawn);
                test.AddComponent<CaseComponent>();
                ComponentSpawnerDaniel.Instance.SpawnObject(test, new Vector2(50, 400)); */


                /* Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0; // Stellt sicher, dass der Z-Wert nicht die Raycast-Abfrage beeinflusst

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null)
                {
                    GameObject touchedGameObject = hit.collider.gameObject;


                    Debug.Log("Test_touch_!!!");
                    counter += 1;


                    GameObject newChild = Instantiate(counter == 1 ? objectCPU : counter == 2 ? objectRAM : objectGPU, touchedGameObject.transform.position, touchedGameObject.transform.rotation, touchedGameObject.transform.parent);
                    newChild.transform.SetParent(touchedGameObject.transform, true);
                    newChild.name = touchedGameObject.name + (counter == 1 ? "_objectCPU" : counter == 2 ? "_objectRAM" : "_objectGPU");

                    SpriteRenderer toucedGameObjectSpriteRenderer = touchedGameObject.GetComponent<SpriteRenderer>();
                    SpriteRenderer newChildSpriteRenderer = newChild.GetComponent<SpriteRenderer>();
                    newChildSpriteRenderer.sortingOrder = toucedGameObjectSpriteRenderer.sortingOrder + 1;




                    PcPartManagerTest retrievedData = touchedGameObject.GetComponent<PcPartManagerTest>();

                    if (retrievedData != null)
                    {
                        Debug.Log("Abgerufene Daten:     Name: " + retrievedData.data.name);
                    }
                } */
            }
        }
    }
}
