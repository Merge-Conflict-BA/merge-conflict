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
        [SerializeField] public float MovingSpeed;
        [SerializeField] public MovingDirection MovingDirection;
        [SerializeField] public Vector2 SizeOfPart;

        // Indicates if this BeltPart is the end, so every item can be destroyed if they collide
        [SerializeField] public bool IsEndPart = false;
        private const float SpeedToCenter = 1;

        private void OnCollisionStay2D(Collision2D collision)
        {
            // BeltParts could touch each other (Overlay of 1px)
            if (TagHandler.CompareTags(collision.gameObject, TagHandler.PossibleTags.ConveyorBelt))
            {
                return;
            }
            
            if (TagHandler.CompareTags(collision.gameObject, TagHandler.PossibleTags.component))
            {
                bool success = collision.gameObject.TryGetComponent(out ComponentHandler componentHandler);

                if (success && componentHandler.CountCollisionConveyorBelt > 0)
                {
                    // divide with CountCollisionConveyorBelt because:
                    // component can collide with multiple beltParts. Each beltPart can call this event.
                    // It could happen, that 2 beltParts add the same velocity --> the component is two times faster
                    float velocity = (MovingSpeed * Time.deltaTime) / componentHandler.CountCollisionConveyorBelt;
                    Vector3 translationVector = MovingDirection.GetVector3() * velocity;
                    
                    // vector to center of conveyor belt
                    Vector3 translationToCenter;
                    
                    switch (MovingDirection)
                    {
                        case MovingDirection.DOWN:
                            if (!componentHandler.IsOnConveyorBeltDiagonal)
                            {
                                var centerX = SizeOfPart.x / 2;
                                var deltaX = centerX - collision.transform.position.x;
                            
                                translationToCenter = new Vector3(deltaX * Time.deltaTime * SpeedToCenter, 0, 0);
                            }
                            else
                            {
                                translationToCenter = Vector3.zero;
                            }
                            break;
                        case MovingDirection.RIGHT:
                        case MovingDirection.DIAGONAL:
                            var centerY = SizeOfPart.y / 2;
                            var deltaY = centerY - collision.transform.position.y;
                            
                            translationToCenter = new Vector3(0, deltaY * Time.deltaTime * SpeedToCenter, 0);
                            break;
                        default:
                            translationToCenter = Vector3.zero;
                            break;
                    }

                    translationVector += translationToCenter;

                    collision.transform.Translate(translationVector, Space.World);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (TagHandler.CompareTags(collision.gameObject, TagHandler.PossibleTags.ConveyorBelt))
            {
                return;
            }
            
            // Destroy element if this beltPart is the end
            if (IsEndPart)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}