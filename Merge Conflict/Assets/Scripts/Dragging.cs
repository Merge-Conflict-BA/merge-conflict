using UnityEngine;

public class Dragging : MonoBehaviour
{
    private bool draggingActive = false;
    private Vector3 offsetMouseToCamera;


    void Update()
    {
        if (draggingActive)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetMouseToCamera;
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

}
