/**********************************************************************************************************************
Name:          ConveyorBeltMovement
Description:   Handle the movement of objects on the Conveyor Belt. If these objects arrive at the end, they will be destroyed.
Author(s):     Simeon Baumann
Date:          2024-02-28
Version:       V1.1
**********************************************************************************************************************/

using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBeltMovement : MonoBehaviour
    {
        [SerializeField] public float MovingSpeed;
        [SerializeField] public MovingDirection MovingDirection;

        // Indicates if this BeltPart is the end, so every item can be destroyed if they collide
        [SerializeField] public bool IsEndPart = false;

        private void OnCollisionStay2D(Collision2D collision)
        {
            // BeltParts could touch each other (Overlay of 1px)
            if (collision.gameObject.CompareTag("ConveyorBelt"))
            {
                return;
            }
            
            if (collision.gameObject.CompareTag("component"))
            {
                bool success = collision.gameObject.TryGetComponent(out ComponentHandler component);

                if (success && component.CountCollisionConveyorBelt > 0)
                {
                    // divide with CountCollisionConveyorBelt because:
                    // component can collide with multiple beltParts. Each beltPart can call this event.
                    // It could happen, that 2 beltParts add the same velocity --> the component is two times faster
                    
                    float velocity = (MovingSpeed * Time.deltaTime) / component.CountCollisionConveyorBelt;
                    Vector3 translationVector = MovingDirection.GetVector3() * velocity;
                
                    collision.transform.Translate(translationVector, Space.World);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ConveyorBelt"))
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