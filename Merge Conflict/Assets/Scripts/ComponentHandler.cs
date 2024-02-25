/**********************************************************************************************************************
Name:          ComponentHandler
Description:   Contains the methode to drag the component-objects and the methode to merge them.
Author(s):     Markus Haubold
Date:          2024-02-25
Version:       V1.0 
TODO:          - its the 1st prototype
**********************************************************************************************************************/
using UnityEngine;

public class ComponentHandler : MonoBehaviour
{
    private Debugger debug = new Debugger();
    private bool draggingActive = false;
    private Vector3 offsetMouseToCamera;

    public GameObject spawnedObjectAfterMerge;
    private ComponentSpawner spawner;

    //its for the test first
    public GameObject componentToSpawn;

    private void Awake()
    {
        spawner = GameObject.Find("conveyorBelt").GetComponent<ComponentSpawner>();
    }

    private void Update()
    {


        if (draggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
            mergeTwoComponents("component");
        }
    }

    private void OnMouseDown()
    {
        handleSpriteSorting();
        offsetMouseToCamera = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingActive = true;
    }

    private void OnMouseUp()
    {

        draggingActive = false;
    }

    private void handleSpriteSorting()
    {
        // get the raycast from the clicked object
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                //set current sprite on top of all
                spriteRenderer.sortingOrder = getHighestSpritePosition() + 1;
            }
        }
    }

    int getHighestSpritePosition()
    {
        SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
        int highestSortingOrder = int.MinValue;

        //loop trough all sprites to get the highest position
        foreach (SpriteRenderer spriteRenderer in allSprites)
        {
            if (spriteRenderer.sortingOrder > highestSortingOrder)
            {
                highestSortingOrder = spriteRenderer.sortingOrder;
            }
        }

        return highestSortingOrder;
    }

    private void mergeTwoComponents(string mergableComponentType)
    {
        const float timeToDestroyObject = 1.0f;
        const float radiusToDetectSpritesOverlapping = 1.0f;
        GameObject draggedComponent = gameObject;

        //check if there is an sprites-overlapping situation
        Collider2D[] overlappedStaticComponents = Physics2D.OverlapCircleAll(draggedComponent.transform.position, radiusToDetectSpritesOverlapping);

        if (overlappedStaticComponents == null) { return; };

        foreach (Collider2D staticComponent in overlappedStaticComponents)
        {
            //check if the overlapping sprite is an component too (so maybe its mergable)
            if (staticComponent.gameObject != draggedComponent && staticComponent.CompareTag(mergableComponentType))
            {
                debug.logMessage("two components overlapp => merge?!");
                draggingActive = false;
                Destroy(draggedComponent, timeToDestroyObject);
                Destroy(staticComponent.gameObject, timeToDestroyObject);

                //create new object
                spawner.spawnOnBelt(spawnedObjectAfterMerge);
            }
        }
    }
}
