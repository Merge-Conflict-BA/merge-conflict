/**********************************************************************************************************************
Name:          ConveyorBeltMovement
Description:   Handle the movement of objects on the Conveyor Belt. If these objects arrive at the end, they will be destroyed.
Author(s):     Simeon Baumann
Date:          2024-02-29
Version:       V1.2
**********************************************************************************************************************/

using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBeltMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] public float MovingSpeed;
        [SerializeField] public MovingDirection MovingDirection;
        [SerializeField] public Vector2 SizeOfPart;

        [Header("Functional Settings")]
        // Indicates if this BeltPart is the end, so every item can be destroyed if they collide
        [SerializeField] public bool IsEndPart = false;

        [Header("Animation Settings")]
        public const float scrollSpeed = 1f;
        private CanvasRenderer? canvasRenderer;

        private const float SpeedToCenterOfBelt = 1;

        private void Start()
        {
            // scale collider to rect
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            RectTransform rectTransform = GetComponent<RectTransform>();
            var rect = rectTransform.rect;
            boxCollider2D.size = new Vector2(rect.width, rect.height);

            transform.TryGetComponent(out canvasRenderer);
            Debugger.LogErrorIf(canvasRenderer == null, "Couldn't get CanvasReader Component. Can't scroll textures!");
        }

        private void Update()
        {
            ScrollTexture();
        }

        private void ScrollTexture()
        {
            if(canvasRenderer == null) { return; }

            Vector2 textureOffset = new(Time.time * scrollSpeed, Time.time * scrollSpeed);

            // different MovingDirections need to have individual materials!
            // else this function will be called from 2 different ConveyorBelts and fight over their shared Material
            switch (MovingDirection)
            {
                case MovingDirection.DOWN:
                    textureOffset *= new Vector2(0, 1);
                    break;

                case MovingDirection.RIGHT:
                    textureOffset *= new Vector2(-1, 0);
                    break;

                case MovingDirection.DIAGONAL:
                default:
                    textureOffset *= new Vector2(-1, 1).normalized;
                    break;
            }

            canvasRenderer.GetMaterial().mainTextureOffset = textureOffset;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            // BeltParts could touch each other (Overlay of 1px)
            if (Tags.ConveyorBelt.UsedByGameObject(collision.gameObject))
            {
                return;
            }

            if (Tags.Component.UsedByGameObject(collision.gameObject))
            {
                bool success = collision.gameObject.TryGetComponent(out ComponentHandler componentHandler);
                Vector2 componentPosition = collision.gameObject.GetComponent<RectTransform>().anchoredPosition;

                if (success && componentHandler.CountCollisionConveyorBelt > 0)
                {
                    // divide with CountCollisionConveyorBelt because:
                    // component can collide with multiple beltParts. Each beltPart can call this event.
                    // It could happen, that 2 beltParts add the same velocity --> the component is two times faster
                    float velocity = (MovingSpeed * Time.deltaTime) / componentHandler.CountCollisionConveyorBelt;
                    Vector2 translationVector = MovingDirection.GetVector3() * velocity;

                    // vector to center of conveyor belt
                    Vector2 translationToCenter = CalculateTranslationToCenterOfBelt(componentPosition, componentHandler.IsOnConveyorBeltDiagonal);
                    translationVector += translationToCenter;

                    collision.transform.Translate(translationVector, Space.World);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Tags.ConveyorBelt.UsedByGameObject(collision.gameObject))
            {
                return;
            }

            // Destroy element if this beltPart is the end
            if (IsEndPart)
            {
                Destroy(collision.gameObject);
            }
        }

        private Vector2 CalculateTranslationToCenterOfBelt(Vector2 componentPosition, bool isOnDiagonalBelt)
        {
            switch (MovingDirection)
            {
                case MovingDirection.DOWN:
                    if (isOnDiagonalBelt)
                    {
                        return Vector3.zero;
                    }
                    else
                    {
                        float centerOfBeltX = SizeOfPart.x / 2;
                        float deltaToCenterOfBeltX = centerOfBeltX - componentPosition.x;

                        return new Vector2(deltaToCenterOfBeltX * Time.deltaTime * SpeedToCenterOfBelt, 0);
                    }
                    
                case MovingDirection.RIGHT:
                case MovingDirection.DIAGONAL:
                    float centerOfBeltY = SizeOfPart.y / 2;
                    float deltaToCenterOfBeltY = centerOfBeltY - componentPosition.y;

                    return new Vector2(0, deltaToCenterOfBeltY * Time.deltaTime * SpeedToCenterOfBelt);
                    
                default:
                    return Vector2.zero;
                    
            }
        }
    }
}