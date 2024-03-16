/**********************************************************************************************************************
Name:          DeskCreator
Description:   Changes the size of the DeskGameObject to the right size and position
Author(s):     Simeon Baumann
Date:          2024-03-08
Version:       V1.0
**********************************************************************************************************************/
using UnityEngine;

// Currently just for ComponentMovement purposes! 

public class DeskCreator : MonoBehaviour
{
    public GameObject ConveyorBeltDiagonalGameObject;
    public int SpaceAboveDesk = 200;
    
    // Desk Size Position Data
    public float Width { get; set; }
    public float Height { get; set; }
    public Vector2 CenterPosition { get; set; }
    

    void Start()
    {
        Vector2 conveyorBeltSize = ConveyorBeltDiagonalGameObject.GetComponent<RectTransform>().rect.size;
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        Width = screenWidth - conveyorBeltSize.x;
        Height = screenHeight - conveyorBeltSize.y - SpaceAboveDesk;

        transform.localScale = new Vector3(Width, Height, 0);

        CenterPosition = new Vector2(Width / 2 + conveyorBeltSize.x, Height / 2 + conveyorBeltSize.y);

        Vector3 position = new Vector3(CenterPosition.x, CenterPosition.y, 1);
        
        transform.SetPositionAndRotation(position, Quaternion.identity);
    }
}
