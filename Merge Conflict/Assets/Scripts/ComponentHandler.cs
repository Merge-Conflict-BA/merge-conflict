/**********************************************************************************************************************
Name:          ComponentHandler
Description:   Contains the methode to drag the component-objects and the methode to merge them.
Author(s):     Markus Haubold, Hanno Witzleb
Date:          2024-02-28
Version:       V1.2
TODO:          - call xp/money controller (when its implemented) after put component into bin
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

    private void Update()
    {
        if (isDraggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
            HandleOverlappedObjects();
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

    //unity object tagged with "bin"
    private bool IsBin(GameObject gameObject)
    {
        return gameObject.CompareTag("bin");
    }

    private void HandleSpriteSorting()
    {
        // get the raycast from the clicked object
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider == null) { return; }

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

    private void HandleOverlappedObjects()
    {
        const float timeToDestroyObject = 0.5f;
        const float radiusToDetectSpritesOverlapping = 1.0f;
        GameObject draggedComponent = gameObject;

        //check if there is an sprites-overlapping situation
        Collider2D[] overlappedStaticObjects = Physics2D.OverlapCircleAll(draggedComponent.transform.position, radiusToDetectSpritesOverlapping);

        if (overlappedStaticObjects == null) { return; };

        //go trough all overlapped sprites and check if there is an mergabel one 
        foreach (Collider2D staticComponent in overlappedStaticObjects)
        {
            //select the action according to the object-types (component, bin etc.)
            if (staticComponent.gameObject != draggedComponent)
            {
                //merge components if possible 
                if (IsComponent(staticComponent.gameObject))
                {
                    Debugger.LogMessage("two components overlapp => merge?!");
                    isDraggingActive = false;
                    Destroy(draggedComponent, timeToDestroyObject);
                    Destroy(staticComponent.gameObject, timeToDestroyObject);

                    ComponentSpawner.Instance.SpawnOnBelt(spawnedObjectAfterMerge);
                    return;
                }

                //put component in the bin -> delete it
                if (IsBin(staticComponent.gameObject))
                {
                    Debugger.LogMessage("Component was put in the bin! Thx for recycling!");
                    isDraggingActive = false;
                    Destroy(draggedComponent, timeToDestroyObject);
                    //TODO: call xp/money controller
                }
            }
        }
    }
}