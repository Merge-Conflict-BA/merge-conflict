/**********************************************************************************************************************
Name:          Dragging
Description:   Drag and drop sprites on the playfield. The current clicked sprite will set on top. 
Author(s):     Markus Haubold, Hanno Witzleb
Date:          2024-02-21
Version:       V1.1 
TODO:          - its the 1st prototype
**********************************************************************************************************************/
using UnityEngine;

public class Dragging : MonoBehaviour
{
    private bool draggingActive = false;
    // camera has position in world too, therefore we need the offset to calculate accurate placement
    private Vector3 offsetMouseToCameraAtStartDragging;


    void Update()
    {
        if (draggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCameraAtStartDragging;
        }
    }

    private void OnMouseDown()
    {
        handleSpriteSorting();
        offsetMouseToCameraAtStartDragging = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        if (hit.collider == null) { return; }

        SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null) { return; }
        
        //set current sprite on top of all
        spriteRenderer.sortingOrder = getHighestSpritePosition();
        
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

            spriteRenderer.sortingOrder--;
        }

        return highestSortingOrder;
    }
}
