/**********************************************************************************************************************
Name:          ComponentHandler
Description:   Contains the methode to drag the component-objects and the methode to merge them.
Author(s):     Markus Haubold, Hanno Witzleb, Simeon Baumann
Date:          2024-03-01
Version:       V1.2
TODO:          - call xp/money controller (when its implemented) after put component into trashcan
**********************************************************************************************************************/
using System;
using Unity.VisualScripting;
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

    // store current count of collision with conveyor belt parts
    public int CountCollisionConveyorBelt = 0;
    public bool IsOnConveyorBeltDiagonal = false;

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
    }

    private void OnMouseUp()
    {
        HandleOverlappingObjects();
        isDraggingActive = false;
    }

    // IsComponent compares Unity Tags, is useful for checking colissions
    private bool IsComponent(GameObject gameObject)
    {
        return gameObject.CompareTag("component");
    }

    //unity object tagged with "trashcan"
    private bool IsTrashcan(GameObject gameObject)
    {
        return gameObject.CompareTag("trashcan");
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
            if (IsComponent(staticComponent.gameObject))
            {


                // TODO Daniel
                // check if compnents can be merged
                // mergedComponent = draggedComponent.Element.Merge(staticComponent.Element)
                // if(mergedComponent != null)
                //      MERGE COMPONENTS

                // TODO Daniel + Markus 

                /*
                // zum testen werden den GameObjects Klassen hinzugefügt
                staticComponent.AddComponent<CaseComponent>();
                draggedComponent.AddComponent<CPUComponent>();

                // prüfen der Klasse des ersten GameObjects
                Debug.Log("staticComponent");
                Debug.Log(staticComponent.GetComponent<Element>());

                // setzen des Level's des Components
                staticComponent.GetComponent<Element>().level = 1;
                Debug.Log("staticComponent Level = " + staticComponent.GetComponent<Element>().level);

                // hinzufügen der "Child"-Klassen für den Gehäuse-Component und setzen des Level's für die CPU
                staticComponent.GetComponent<CaseComponent>().motherboard = staticComponent.gameObject.AddComponent<MBComponent>();
                staticComponent.GetComponent<CaseComponent>().motherboard.cpu = staticComponent.gameObject.AddComponent<CPUComponent>();
                staticComponent.GetComponent<CaseComponent>().motherboard.cpu.level = 3;
                Debug.Log("staticComponent MB-CPU-Slot-Level = " + staticComponent.GetComponent<CaseComponent>().motherboard.cpu.level);

                // prüfen der Klasse des zweiten GameObjects
                Debug.Log("draggedComponent");
                Debug.Log(draggedComponent.GetComponent<Element>());

                // NUR EIN TEST : ausführen einer Mergefunktion in einer tieferen Ebene
                // Debug.Log("(ComponentHandler) Test-Merge");
                // Debug.Log(staticComponent.GetComponent<CaseComponent>().motherboard.cpu.Merge(draggedComponent.GetComponent<Element>()));



                // -----     Test. Anlegen eines Objekts (CaseComponent) mittels Konstruktor der Klassen     --------------------------------

                CaseComponent newCase = Components.CreateCase(hdd: Components.HDD);
                newCase.level = 3;

                // --------------------------------------------------------------------------------------------------------------------------
                */


                // needs to get Element of ComponentHandler Script!!!

                // Beim instanziieren dran denken, dass das GameObject den Tag "Component" erhalten muss !!

                //TODO: 4 markus
                //wenn daniel fertig, dann Spawn mehtode anpassen
                //es werden beide Merge() der KOmponenten aufgerufen 
                //sobald eine Methode eine Klasse zurückgibt, werden die Beiden gelöscht und eine Instanz der erhaltenenen Klasse erstellt 
                //(Klasse ist dann schon komplett vorbereitet)
                //wenn nicht mergebar -> returnt Merge() null 

                Debugger.LogMessage("two components overlapp => merge?!");
                Destroy(draggedComponent, timeToDestroyObject);
                Destroy(staticComponent.gameObject, timeToDestroyObject);

                ComponentSpawner.Instance.SpawnOnBelt(spawnedObjectAfterMerge);
                return;
            }

            //put component in the trashcan -> delete it
            if (IsTrashcan(staticComponent.gameObject))
            {
                Debugger.LogMessage("Component was put in the trashcan! Thx for recycling!");
                Destroy(draggedComponent, timeToDestroyObject);
                //TODO: call xp/money controller
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ConveyorBelt"))
        {
            CountCollisionConveyorBelt++;
        }

        if (col.gameObject.CompareTag("ConveyorBeltDiagonal"))
        {
            CountCollisionConveyorBelt++;
            IsOnConveyorBeltDiagonal = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt"))
        {
            CountCollisionConveyorBelt--;
        }

        if (other.gameObject.CompareTag("ConveyorBeltDiagonal"))
        {
            CountCollisionConveyorBelt--;
            IsOnConveyorBeltDiagonal = false;
        }
    }
}