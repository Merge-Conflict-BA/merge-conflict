/**********************************************************************************************************************
Name:          ComponentHandler
Description:   Contains the methode to drag the component-objects and the methode to merge them. Handles the movement of objects on the desk.
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-03-15
Version:       V1.3
TODO:          - call xp/money controller (when its implemented) after put component into trashcan
**********************************************************************************************************************/
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ComponentHandler : MonoBehaviour
{
    public bool isBeingDragged = false;
    public Element element;

    private bool isDraggingActive = false;
    // camera is located on the bottom left corner, so we have an offset to the mouse
    // gets set every time dragging starts
    private Vector3 offsetMouseToCamera;

    // store current count of collision with conveyor belt parts
    public int CountCollisionConveyorBelt = 0;
    public bool IsOnConveyorBeltDiagonal = false;
    // component is dragged at least once
    private bool _isDraggedOnce = false;

    // component movement
    private ComponentMovement ComponentMovement;
    
    private void Start()
    {
        bool success = gameObject.TryGetComponent(out ComponentMovement);
        
        Debugger.LogErrorIf(success == false, "ComponentMovement is missing on Component.");        
    }

    private void Update()
    {
        if (isBeingDragged)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
            _isDraggedOnce = true;
            ComponentMovement.HandleDraggingAnimation();
        }
        else if (IsOnConveyorBelt() == false && _isDraggedOnce)
        {
            ComponentMovement.HandleIdleMovement();
            ComponentMovement.HandleIdleScaling();
        }
        
        ComponentMovement.HandleDraggingAnimationEnd();
    }

    private void OnMouseDown()
    {
        HandleSpriteSorting();
        offsetMouseToCamera = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isBeingDragged = true;
    }

    private void OnMouseUp()
    {
        HandleOverlappingObjects();
        isBeingDragged = false;
        ComponentMovement.HandleDraggingStop();
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
            int highestSpritePosition = GetHighestSpritePosition();
            spriteRenderer.sortingOrder = highestSpritePosition + 1;

            // set sprites of current GameObject-Childs on top of all
            SpriteRenderer childSpriteRenderer;
            for (int i = 0; i < transform.childCount; i++)
            {
                childSpriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if ((childSpriteRenderer != null) && Tags.SubComponent.UsedByGameObject(childSpriteRenderer.gameObject))
                {
                    childSpriteRenderer.sortingOrder = highestSpritePosition + 2;
                }
            }
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
        }

        return highestSortingOrder;
    }

    private Element? GetMergedElement(GameObject draggedComponentObject)
    {
        Element? mergedElement = null;

        if (draggedComponentObject.TryGetComponent(out ComponentHandler draggedComponentHandler))
        {
            Element draggedElement = draggedComponentHandler.element;

            if (draggedElement is not IComponent || this.element is not IComponent) { return null; }

            mergedElement = ((IComponent)draggedElement).Merge(this.element);
            if (mergedElement == null)
            {
                mergedElement = ((IComponent)this.element).Merge(draggedElement);
            }
        }

        return mergedElement;
    }

    private void HandleOverlappingObjects()
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
            //skip the dragged component from the list
            if (staticComponent.gameObject == draggedComponent)
            {
                continue;
            }

            //merge components if possible 
            if (Tags.Component.UsedByGameObject(staticComponent.gameObject))
            {
                Element? mergedElement = GetMergedElement(staticComponent.gameObject);

                if (mergedElement == null)
                {
                    return;
                }

                mergedElement.InstantiateGameObjectAndAddTexture(staticComponent.GetComponent<RectTransform>().anchoredPosition);

                Destroy(draggedComponent, timeToDestroyObject);
                Destroy(staticComponent.gameObject, timeToDestroyObject);

                return;
            }

            //put component in the trashcan -> delete it
            if (Tags.Trashcan.UsedByGameObject(staticComponent.gameObject))
            {
                Debugger.LogMessage("Component was put in the trashcan! Thx for recycling!");
                Destroy(draggedComponent, timeToDestroyObject);
                //TODO: call xp/money controller
            }
        }
    }

    private bool IsOnConveyorBelt()
    {
        return CountCollisionConveyorBelt > 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (Tags.ConveyorBelt.UsedByGameObject(col.gameObject))
        {
            CountCollisionConveyorBelt++;
        }

        if (Tags.ConveyorBeltDiagonal.UsedByGameObject(col.gameObject))
        {
            CountCollisionConveyorBelt++;
            IsOnConveyorBeltDiagonal = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (Tags.ConveyorBelt.UsedByGameObject(other.gameObject))
        {
            CountCollisionConveyorBelt--;
        }

        if (Tags.ConveyorBeltDiagonal.UsedByGameObject(other.gameObject))
        {
            CountCollisionConveyorBelt--;
            IsOnConveyorBeltDiagonal = false;
        }
    }
}