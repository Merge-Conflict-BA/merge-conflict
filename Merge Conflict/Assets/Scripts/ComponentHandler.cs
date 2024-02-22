/**********************************************************************************************************************
Name:          CoomponentHandler
Description:   Contains all functions to handle one component.
Author(s):     Markus Haubold
Date:          2024-02-20
Version:       V1.0 
TODO:          - its the 1st prototype
**********************************************************************************************************************/
using UnityEngine;

public class Dragging : MonoBehaviour
{
    private Debugger debug = new Debugger();
    private bool draggingActive = false;
    private Vector3 offsetMouseToCamera;


    void Update()
    {

        if (draggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
            mergeTwoComponents();
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

    private void mergeTwoComponents()
    {
        float radiusToDetectSpritesOverlapping = 1.0f;
        string spriteTypeIsComponent = "component"; //tag from the inspector-window

        //check if there is an sprites-overlapping situation
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusToDetectSpritesOverlapping);

        if(colliders == null) { return; };

        foreach (Collider2D collider in colliders)
        {
            //check if the overlapping sprite is an component too (so maybe its mergable)
            if (collider.gameObject != gameObject && collider.CompareTag(spriteTypeIsComponent))
            {
                debug.logMessage("two components overlapp => merge?!");
                draggingActive = false;
                Destroy(gameObject, 1.5f);
                Destroy(collider.gameObject, 1.5f);
            }
        }
    }
}
