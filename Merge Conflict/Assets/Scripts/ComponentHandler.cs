/**********************************************************************************************************************
Name:          ComponentHandler
Description:   Contains the methode to drag the component-objects and the methode to merge them.
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-03-01
Version:       V1.3
TODO:          - call xp/money controller (when its implemented) after put component into trashcan
**********************************************************************************************************************/
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ComponentHandler : MonoBehaviour
{
    public Element element;

    private bool isDraggingActive = false;
    // camera is located on the bottom left corner, so we have an offset to the mouse
    // gets set every time dragging starts
    private Vector3 offsetMouseToCamera;

    // store current count of collision with conveyor belt parts
    public int CountCollisionConveyorBelt = 0;
    public bool IsOnConveyorBeltDiagonal = false;

    private Coroutine moveComponent;

    private void Update()
    {
        if (isDraggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
        }
    }

    private void OnMouseDown()
    {
        HandleSpriteSorting();
        offsetMouseToCamera = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDraggingActive = true;

        // stops the automated movement of the component when component cannot be sold
        if ((moveComponent != null) && isDraggingActive)
        {
            StopCoroutine(moveComponent);
            moveComponent = null;
        }
    }

    private void OnMouseUp()
    {
        HandleOverlappingObjects();
        isDraggingActive = false;
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

            spriteRenderer.sortingOrder--;

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

                GameObject mergedComponentObject = mergedElement.InstantiateGameObjectAndAddTexture(staticComponent.transform.position);

                Debugger.LogMessage("two components overlapp => merge?!");
                Destroy(draggedComponent, timeToDestroyObject);
                Destroy(staticComponent.gameObject, timeToDestroyObject);

                return;
            }

            //put component in the trashcan -> delete it
            if (Tags.Trashcan.UsedByGameObject(staticComponent.gameObject))
            {
                Debugger.LogMessage("Component was put in the trashcan! Thx for recycling!");
                Destroy(draggedComponent, timeToDestroyObject);
                //TODO: call xp/money controller  =>  use trashValues of the components
            }

            // drop component (PC) on the selling station -> if possible/matching with quest -> sells it
#nullable enable
            if (Tags.SellingStation.UsedByGameObject(staticComponent.gameObject))
            {

                // TODO: change this later to the correct "requiredQuestComponent" from actual quest
                // GameObject requiredQuestComponent = Components.HDD.InstantiateGameObjectAndAddTexture(new Vector2(300, 400));
                // GameObject requiredQuestComponent = Components.CreateCase().InstantiateGameObjectAndAddTexture(new Vector2(300, 400));
                // GameObject requiredQuestComponent = Components.CreateCase(powersupply: null, hdd: Components.HDD, motherboard: null).InstantiateGameObjectAndAddTexture(new Vector2(300, 400));
                GameObject requiredQuestComponent = Components.CreateCase(
                    powersupply: null,
                    hdd: Components.HDD.Clone(),
                    motherboard: Components.CreateMB(
                        cpu: null,
                        ram: Components.RAM.Clone(),
                        gpu: Components.GPU.Clone())
                    ).InstantiateGameObjectAndAddTexture(new Vector2(300, 400));

                Element? requiredQuestElement
                    = requiredQuestComponent.TryGetComponent(out ComponentHandler requiredQuestComponentHandler)
                    ? requiredQuestComponentHandler.element
                    : null;

                Element? draggedElement
                    = draggedComponent.TryGetComponent(out ComponentHandler draggedComponentHandler)
                    ? draggedComponentHandler.element
                    : null;

                if (draggedElement == null)
                {
                    return;
                }

                if (draggedElement.IsEqual(requiredQuestElement))
                {

                    //TODO: call xp/money controller  ->  give more xp/money than putting it in trashcan  =>  use salesValues of the components                    
                    int trash = draggedElement.GetTrashValue();
                    int sales = draggedElement.GetSalesValue();

                    Debug.Log($"     trashV : {trash}          salesV : {sales}");


                    Destroy(draggedComponent, timeToDestroyObject);
                    Debugger.LogMessage("Component was sold. Congratulations! You have completed a quest.");
                }
                else
                {
                    Debugger.LogMessage("Component cannot be sold. It does not correspond to the required order from the quest.");
                    // if component cannot be sold -> automatically move it back onto the playfield
                    draggedComponent.GetComponent<ComponentHandler>().MoveComponent(new Vector2(200, 400), 100f);
                }
                
            }
#nullable restore
        }
    }

    public void MoveComponent(Vector2 targetPosition, float movingSpeed)
    {
        if (moveComponent != null)
        {
            StopCoroutine(moveComponent);
        }

        moveComponent = StartCoroutine(MoveToPosition(targetPosition, movingSpeed));
    }

    // coroutine for automated gameObject movement 
    IEnumerator MoveToPosition(Vector2 targetPosition, float movingSpeed)
    {
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movingSpeed * Time.deltaTime);
            yield return null;
        }
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

/* 
{
    Trash,
    CaseComponent:{
        PowersupplyComponent,
        HDDComponent,
        MBComponent:{
            CPUComponent,
            RAMComponent,
            GPUComponent
        }
    }
} */