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
        return direction switch
        {
            MovingDirection.DOWN => new Vector3(0, -1, 0),
            MovingDirection.RIGHT => new Vector3(1, 0, 0),
            MovingDirection.DIAGONAL => new Vector3(1, -1, 0).normalized,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public static Vector2 GetVector2(this MovingDirection direction)
    {
        return direction switch
        {
            MovingDirection.DOWN => new Vector2(0, -1),
            MovingDirection.RIGHT => new Vector2(1, 0),
            MovingDirection.DIAGONAL => new Vector2(1, -1).normalized,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}

