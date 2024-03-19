/**********************************************************************************************************************
Name:          InstantiateConveyorBelt
Description:   Instantiate the prefabs for the Conveyor Belt.
Author(s):     Simeon Baumann
Date:          2024-02-29
Version:       V1.0 
**********************************************************************************************************************/

using System;
using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBeltCreator : MonoBehaviour
    {
        [SerializeField] public GameObject PrefabConveyorBeltHorizontal;
        [SerializeField] public GameObject PrefabConveyorBeltVertical;
        [SerializeField] public GameObject PrefabConveyorBeltDiagonal;
        [SerializeField] public GameObject PrefabConveyorBeltEnd;

        [SerializeField] public float MovingSpeed;

        private Vector2 _prefabSizeHorizontal;
        private Vector2 _prefabSizeVertical;
        private Vector2 _prefabSizeDiagonal;

        public void Start()
        {
            _prefabSizeHorizontal = PrefabConveyorBeltHorizontal.GetComponent<RectTransform>().rect.size;
            _prefabSizeVertical = PrefabConveyorBeltVertical.GetComponent<RectTransform>().rect.size;
            _prefabSizeDiagonal = PrefabConveyorBeltDiagonal.GetComponent<RectTransform>().rect.size;

            InitializeConveyorBeltDiagonal();
            InitializeConveyorBeltHorizontal();
            InitializeConveyorBeltVertical();
        }

        private void InitializeConveyorBeltHorizontal()
        {
            int screenWidth = Screen.width;
            float prefabWidth = _prefabSizeHorizontal.x;
            Vector2 centerOfPrefab = _prefabSizeHorizontal / 2;
            
            int i;
            for (i = 0; (i * prefabWidth) < screenWidth; i++)
            {
                float posX = centerOfPrefab.x + i * prefabWidth + _prefabSizeVertical.x;
                Vector3 position = new Vector3(posX, centerOfPrefab.y, 0);
                              
                var beltPart = Instantiate(PrefabConveyorBeltHorizontal, Vector3.zero, Quaternion.identity, transform);
                beltPart.GetComponent<RectTransform>().anchoredPosition = position;

                AddConveyorBeltMovementComponent(beltPart, MovingDirection.RIGHT);
            }
            
            // endPart (to destroy moving elements)
            Vector3 positionEndPart = new Vector3( centerOfPrefab.x + i * prefabWidth + _prefabSizeVertical.x, centerOfPrefab.y, 0);

            var beltEndPart = Instantiate(PrefabConveyorBeltEnd, Vector3.zero, Quaternion.identity, transform);
            beltEndPart.GetComponent<RectTransform>().anchoredPosition = positionEndPart;

            AddConveyorBeltMovementComponent(beltEndPart, MovingDirection.RIGHT, true);
        }
    
        private void InitializeConveyorBeltVertical()
        {
            int screenHeight = Screen.height;
            float prefabHeight = _prefabSizeVertical.y;
            Vector2 centerOfPrefab = _prefabSizeVertical / 2;
            
            for (int i = 0; (i * prefabHeight) < screenHeight; i++)
            {
                float posY = centerOfPrefab.y + i * prefabHeight + _prefabSizeHorizontal.y;
                Vector3 position = new Vector3(centerOfPrefab.x, posY, 0);
                
                var beltPart = Instantiate(PrefabConveyorBeltVertical, Vector3.zero, Quaternion.identity, transform);
                beltPart.GetComponent<RectTransform>().anchoredPosition = position;

                AddConveyorBeltMovementComponent(beltPart, MovingDirection.DOWN);
            }
        }

        private void InitializeConveyorBeltDiagonal()
        {
            Vector2 centerOfPrefab = _prefabSizeDiagonal / 2;

            var beltPart = Instantiate(PrefabConveyorBeltDiagonal, Vector3.zero, Quaternion.identity, transform);
            beltPart.GetComponent<RectTransform>().anchoredPosition = centerOfPrefab;

            AddConveyorBeltMovementComponent(beltPart, MovingDirection.DIAGONAL);
        }

        private void AddConveyorBeltMovementComponent(GameObject beltPart, MovingDirection direction, Boolean isEndPart = false)
        {
            ConveyorBeltMovement component = beltPart.AddComponent<ConveyorBeltMovement>();
            component.MovingSpeed = MovingSpeed;
            component.MovingDirection = direction;

            component.SizeOfPart = direction switch
            {
                MovingDirection.DOWN => _prefabSizeVertical,
                MovingDirection.RIGHT => _prefabSizeHorizontal,
                MovingDirection.DIAGONAL => _prefabSizeDiagonal,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            component.IsEndPart = isEndPart;
        }
    }
}
