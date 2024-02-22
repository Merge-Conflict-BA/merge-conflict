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
            CheckForOverlap();
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

    private void CheckForOverlap()
    {
        // Definiere den Radius des Überlappungsbereichs (kann je nach Bedarf angepasst werden)
        float overlapRadius = 10.0f;

        // Überprüfe, ob andere Collider im Überlappungsbereich sind
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        // Durchlaufe alle überlappenden Collider
        foreach (Collider2D collider in colliders)
        {
            // Überprüfe, ob es sich um ein anderes Sprite handelt (kann je nach Bedarf angepasst werden)
            if (collider.gameObject != gameObject && collider.CompareTag("component"))
            {
                // Handle die Überlappung hier, z.B. schreibe ein Log
                Debug.Log("Die Sprites überlappen sich!");
            }
        }
    }
}
