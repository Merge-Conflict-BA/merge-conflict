/**********************************************************************************************************************
Name:          ConveyorBeltMovement
Description:   Handle the movement of objects on the Conveyor Belt.
Author(s):     Simeon Baumann
Date:          2024-02-23
Version:       V1.0 
**********************************************************************************************************************/

using System;
using UnityEngine;

namespace ConveyorBelt
{
    public class ConveyorBeltMovement : MonoBehaviour
    {
        [SerializeField] public float MovingSpeed;

        [SerializeField] public MovingDirection MovingDirection;

        [SerializeField] public Boolean IsEndPart = false;

        void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("ConveyorBelt"))
            {
                var velocity = MovingSpeed * Time.deltaTime;
                var translationVector = MovingDirection.GetVector3() * velocity;
                
                collision.transform.Translate(translationVector, Space.World);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ConveyorBelt"))
            {
                return;
            }
            
            if (IsEndPart)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}