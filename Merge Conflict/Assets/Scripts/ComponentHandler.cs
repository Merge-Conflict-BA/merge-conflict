/**********************************************************************************************************************
Name:          ComponentHandler
Description:   Contains the methode to drag the component-objects and the methode to merge them.
Author(s):     Markus Haubold, Hanno Witzleb
Date:          2024-02-25
Version:       V1.1
TODO:          - its the 1st prototype
**********************************************************************************************************************/
using UnityEngine;

public class ComponentHandler : MonoBehaviour
{
    private bool isDraggingActive = false;
    // camera is located on the bottom left corner, so we have an offset to the mouse
    // gets set every time dragging starts
    private Vector3 offsetMouseToCamera;

    // TODO remove, when spawning functionality is implemented
    public GameObject spawnedObjectAfterMerge;
    //for the testing only
    public GameObject componentToSpawn;

    public Element element;

    private void Update()
    {
        if (isDraggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
            MergeTwoComponents();
        }
    }

    private void OnMouseDown()
    {
        HandleSpriteSorting();
        offsetMouseToCamera = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDraggingActive = true;
    }

    private void OnMouseUp()
    {
        isDraggingActive = false;
    }

    // IsComponent compares Unity Tags, is useful for checking colissions
    private bool IsComponent(GameObject gameObject)
    {
        return gameObject.CompareTag("component");
    }

    private void HandleSpriteSorting()
    {
        // get the raycast from the clicked object
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if(hit.collider == null) { return; }

        if (hit.collider.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            //set current sprite on top of all
            spriteRenderer.sortingOrder = GetHighestSpritePosition() + 1;
        }
    }

    private int GetHighestSpritePosition()
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

            spriteRenderer.sortingOrder--;
        }

        return highestSortingOrder;
    }

    private void MergeTwoComponents()
    {
        const float timeToDestroyObject = 0.5f;
        const float radiusToDetectSpritesOverlapping = 1.0f;
        GameObject draggedComponent = gameObject;

        //check if there is an sprites-overlapping situation
        Collider2D[] overlappedStaticComponents = Physics2D.OverlapCircleAll(draggedComponent.transform.position, radiusToDetectSpritesOverlapping);

        if (overlappedStaticComponents == null) { return; };

        //go trough all overlapped sprites and check if there is an mergabel one 
        foreach (Collider2D staticComponent in overlappedStaticComponents)
        {
            //check if the overlapping sprite is an component too
            if (staticComponent.gameObject != draggedComponent && IsComponent(staticComponent.gameObject))
            {

                // check if compnents can be merger
                // mergedComponent = draggedComponent.Element.Merge(staticComponent.Element)
                // if(mergedComponent != null)
                //      MERGE COMPONENTS

                // needs to get Element of COmponentHandler Script!!!


                Debugger.LogMessage("two components overlapp => merge?!");
                isDraggingActive = false;
                Destroy(draggedComponent, timeToDestroyObject);
                Destroy(staticComponent.gameObject, timeToDestroyObject);

                ComponentSpawner.Instance.SpawnOnBelt(spawnedObjectAfterMerge);
            }
        }
    }
}