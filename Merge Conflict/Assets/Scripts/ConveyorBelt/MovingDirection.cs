/**********************************************************************************************************************
Name:          MovingDirection
Description:   Enum: direction of the movement of the elements on the conveyor belt
Author(s):     Simeon Baumann
Date:          2024-02-29
Version:       V1.2
**********************************************************************************************************************/
using System;
using UnityEngine;

public enum MovingDirection
{
    DOWN,
    RIGHT,
    DIAGONAL // indicates that the elements should move diagonal (on corner of Conveyor Belt)
}

static class MovingDirectionMethode
{
    public static Vector3 GetVector3(this MovingDirection direction)
    {
        return direction switch
        {
            MovingDirection.DOWN => new Vector3(0, -1, 0),
            MovingDirection.RIGHT => new Vector3(1, 0, 0),
            MovingDirection.DIAGONAL => new Vector3(1, 0, 0), // is the same as RIGHT --> yet it is just an indicator
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}

