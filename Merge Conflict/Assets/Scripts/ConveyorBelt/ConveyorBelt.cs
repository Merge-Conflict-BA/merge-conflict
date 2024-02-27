/**********************************************************************************************************************
Name:          InstantiateConveyorBelt
Description:   Instantiate the prefabs for the Conveyor Belt.
Author(s):     Simeon Baumann
Date:          2024-02-23
Version:       V1.0 
**********************************************************************************************************************/

using System;
using Unity.Mathematics;
using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBelt : MonoBehaviour
    {
        public static ConveyorBelt Instance { get; private set; }
    
        [SerializeField] public GameObject PrefabConveyorBeltHorizontal;
        [SerializeField] public GameObject PrefabConveyorBeltVertical;

        [SerializeField] public float MovingSpeed;

        private void Awake() 
        { 
            // If there is an instance, and it's not me, delete myself.
    
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
    
        public void Instantiate()
        {
            Vector2 prefabSizeHorizontal = PrefabConveyorBeltHorizontal.GetComponent<RectTransform>().rect.size;
            Vector2 prefabSizeVertical = PrefabConveyorBeltVertical.GetComponent<RectTransform>().rect.size;

            InitializeConveyorBeltHorizontal(prefabSizeHorizontal);
            InitializeConveyorBeltVertical(prefabSizeVertical, prefabSizeHorizontal.y);
        }


        private void InitializeConveyorBeltHorizontal(Vector2 sizeOfPrefab)
        {
            int screenWidth = Screen.width;
            float prefabWidth = sizeOfPrefab.x;
            Vector2 centerOfPrefab = sizeOfPrefab / 2;
        
            for (int i = 0; (i * prefabWidth) < screenWidth; i++)
            {
                float posX = centerOfPrefab.x + i * prefabWidth;
            
                Vector3 position = new Vector3(posX, centerOfPrefab.y, 0);
                var beltPart = Instantiate(PrefabConveyorBeltHorizontal, position, Quaternion.identity, transform);
                
                AddConveyorBeltMovementComponent(beltPart, MovingDirection.RIGHT);
            }
        }
    
        private void InitializeConveyorBeltVertical(Vector2 sizeOfPrefab, float offsetY)
        {
            int screenHeight = Screen.height;
            float prefabHeight = sizeOfPrefab.y;
            Vector2 centerOfPrefab = sizeOfPrefab / 2;
        
            // Add one more above Screen
            for (int i = 0; ((i - 1) * prefabHeight) < screenHeight; i++)
            {
                float posY = centerOfPrefab.y + i * prefabHeight + offsetY;
            
                Vector3 position = new Vector3(centerOfPrefab.x, posY, 0);
                var beltPart = Instantiate(PrefabConveyorBeltVertical, position, Quaternion.identity, transform);
                
                AddConveyorBeltMovementComponent(beltPart, MovingDirection.DOWN);
            }
        }


        private void AddConveyorBeltMovementComponent(GameObject beltPart, MovingDirection direction)
        {
            ConveyorBeltMovement component = beltPart.AddComponent<ConveyorBeltMovement>();
            component.MovingSpeed = MovingSpeed;
            component.MovingDirection = direction;
        }
    }
}
