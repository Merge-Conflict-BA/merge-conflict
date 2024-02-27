using System;
using UnityEngine;

public enum MovingDirection
{
    DOWN,
    RIGHT,
    DIAGONAL
}

static class MovingDirectionMethode
{
    public static Vector3 GetVector3(this MovingDirection direction)
    {
        switch (direction)
        {
            case MovingDirection.DOWN:
                return new Vector3(0, -1, 0);
            case MovingDirection.RIGHT:
                return new Vector3(1, 0, 0);
            case MovingDirection.DIAGONAL:
                return new Vector3(1, -1, 0).normalized;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}

