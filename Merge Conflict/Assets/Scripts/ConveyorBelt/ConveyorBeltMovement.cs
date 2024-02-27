/**********************************************************************************************************************
Name:          ConveyorBeltMovement
Description:   Handle the movement of objects on the Conveyor Belt.
Author(s):     Simeon Baumann
Date:          2024-02-23
Version:       V1.0 
**********************************************************************************************************************/

using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBeltMovement : MonoBehaviour
    {
        [SerializeField] public float MovingSpeed;

        [SerializeField] public MovingDirection MovingDirection;

        void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("ConveyorBelt"))
            {
                var velocity = MovingSpeed * Time.deltaTime;
            
                collision.transform.Translate(MovingDirection.GetVector3() * velocity);
            }
        }
    }
}