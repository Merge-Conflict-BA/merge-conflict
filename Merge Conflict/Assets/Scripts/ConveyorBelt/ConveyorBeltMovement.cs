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

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }

        // void OnCollisionStay2D(Collision2D collision)
        // {
        //     var velocity = MovingSpeed * Time.deltaTime;
        //     var curPos = collision.transform.position;
        //
        //     Vector3 newPos = curPos + MovingDirection.GetVector3() * velocity;
        //
        //     collision.transform.SetPositionAndRotation(newPos, Quaternion.identity);
        // }
    }
}