/**********************************************************************************************************************
Name:          ComponentMovement
Description:   Manages the Movement of the components if they are on the desk
Author(s):     Simeon Baumann, Hanno Witzleb, Daniel Rittrich
Date:          2024-03-15
Version:       V2.2
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentMovement : MonoBehaviour
{
    [Header("General Movement Settings")]
    public float MoveMinDistance = 30;
    public float MoveMaxDistance = 70;
    public float MoveSpeed = 50;
    public float ReturningMovingSpeed = 2000;
    private readonly float _smoothMovementFactor = 0.02f;
    // Distance by which the component should move during the current movement
    private Vector2 CenterOfDesk;

    [Header("Idle Movement Settings")]
    public float MinSecondsWithoutIdleMove = 2;
    public float MaxSecondsWithoutIdleMove = 4;
    public float IdleMoveStartDelay = 2;

    [Header("Scaling Settings")]
    public float MaxScaleFactor = 1.05f;

    /*
     * [Header("Calculated in Start()")]
     */
    private Rect _deskRect;
    private Vector2 _deskOffsetFromBottomLeft = new(180, 180);
    // Space between Component and border of desk
    private float _marginToDeskBorder = 5;
    // Default size and scale of the component
    private Vector2 _size;
    private Vector3 _defaultScale;

    /*
     * [Header("Constantly changing")]
     */
    private bool _isMoving = false;
    private Vector2? _startPositionOfCurrentMove;
    private Vector2 _intermediateTargetForSmoothMovement;
    private Vector2 _currentMoveDirection;
    private float _currentRemainingMoveDistance;
    private float _startTimeOfIdleMovement = 0;
    private float _remainingSecondsUntilIdleMoveStarts;
    private bool _isCurrentlyScalingBackToDefault = false;
    // Store the last positions of the component while dragging to calculate the direction of the further movement after dragging
    private List<Vector2> _lastPositionsWhileBeingDragged;
    private bool _isReturningToDesk;
    private bool _isWalking;

    /// <summary>
    /// Changes important properties of the movement of the component
    /// </summary>
    public void InitializeProperties(
        float moveMinDistance,
        float moveMaxDistance,
        float moveSpeed,
        float returningMovingSpeed,
        float minSecondsWithoutIdleMove,
        float maxSecondsWithoutIdleMove,
        float idleMoveStartDelay,
        float maxScaleFactor)
    {
        MoveMinDistance = moveMinDistance;
        MoveMaxDistance = moveMaxDistance;
        MoveSpeed = moveSpeed;
        ReturningMovingSpeed = returningMovingSpeed;
        MinSecondsWithoutIdleMove = minSecondsWithoutIdleMove;
        MaxSecondsWithoutIdleMove = maxSecondsWithoutIdleMove;
        IdleMoveStartDelay = idleMoveStartDelay;
        MaxScaleFactor = maxScaleFactor;
    }

    private void Start()
    {
        _size = GetComponent<RectTransform>().rect.size;
        _defaultScale = gameObject.GetComponent<RectTransform>().localScale;
        _size *= _defaultScale;

        GameObject deskObject = GameObject.FindWithTag("desk");
        _deskRect = deskObject.GetComponent<RectTransform>().rect;
        // move desk to its actual position. Values need to be updated here, everytime deskObject is moved
        _deskRect.Set(
            _deskRect.x + _deskOffsetFromBottomLeft.x,
            _deskRect.y + _deskOffsetFromBottomLeft.y,
            _deskRect.width,
            _deskRect.height);

        _remainingSecondsUntilIdleMoveStarts = IdleMoveStartDelay;

        // Recalculate Margin of Component because the size of the component is bigger if it is scaled
        float deltaWidth = (_size.x * MaxScaleFactor) - _size.x;
        _marginToDeskBorder += _marginToDeskBorder + deltaWidth;

        _lastPositionsWhileBeingDragged = new List<Vector2>();

        CenterOfDesk = GetPositionOfElementsCenter(deskObject);
    }


    #region Functions called in ComponentHandler

    /// <summary>
    /// Resets the moving properties if the component is released after being dragged
    /// </summary>
    public void HandleDraggingStop()
    {
        ResetMovementProperties();
        _startTimeOfIdleMovement = Time.time;
    }

    /// <summary>
    /// Moves the component random over the desk
    /// </summary>
    public void HandleIdleMovement()
    {
        if (_remainingSecondsUntilIdleMoveStarts > 0)
        {
            _remainingSecondsUntilIdleMoveStarts -= Time.deltaTime;
            return;
        }

        // check if component is currently moving
        if (_isMoving)
        {
            IdleMoveToDestination();
        }
        else
        {
            CalculateNewIdleMoveDestination();
            _remainingSecondsUntilIdleMoveStarts = Random.Range(MinSecondsWithoutIdleMove, MaxSecondsWithoutIdleMove);
        }

    }

    /// <summary>
    /// Simulates a breathing animation of the component
    /// </summary>
    public void HandleIdleScaling()
    {
        // check if component is currently scaling back to the default size
        if (_isCurrentlyScalingBackToDefault)
        {
            return;
        }

        // get a smooth scaleFactor to have a smooth scaling animation
        double scaleFactor = ((Math.Sin(Time.time - _startTimeOfIdleMovement - Math.PI / 2) + 1) * 0.5 * (MaxScaleFactor - 1)) + 1;

        transform.localScale = _defaultScale * (float)scaleFactor;
    }

    /// <summary>
    /// Dragging start: Scale the component up
    /// Dragging end: Scale the component the to default and move it a little
    /// </summary>
    public void HandleDraggingAnimation()
    {
        ScaleToFactor(MaxScaleFactor);
        _isCurrentlyScalingBackToDefault = true;

        // Add position to array of last positions
        if (_lastPositionsWhileBeingDragged.Count == 0
            || _lastPositionsWhileBeingDragged[^1] != GetCanvasPosition())
        {
            _lastPositionsWhileBeingDragged.Add(GetCanvasPosition());
        }

        if (_lastPositionsWhileBeingDragged.Count > 5)
        {
            _lastPositionsWhileBeingDragged.RemoveAt(0);
        }
    }

    public void HandleDraggingAnimationEnd()
    {
        if (_isCurrentlyScalingBackToDefault == false)
        {
            return;
        }

        ScaleToFactor(1f);
        MoveAfterDragging();

        if (Vector3.Distance(transform.localScale, _defaultScale) <= 0.1f)
        {
            _isCurrentlyScalingBackToDefault = false;
            _lastPositionsWhileBeingDragged.Clear();
        }
    }

    #endregion

    // returns the position of the center of an object on playfield
    private Vector2 GetPositionOfElementsCenter(GameObject objectOnPlayfield)
    {
        RectTransform playfieldRect = GameObject.FindWithTag("UIManager").GetComponent<RectTransform>();
        RectTransform objectRect = objectOnPlayfield.GetComponent<RectTransform>();
        Vector2 playfieldSize = playfieldRect.sizeDelta;

        Vector2 objectSize = new Vector2(
            playfieldSize.x - objectRect.offsetMin.x - objectRect.offsetMax.x,
            playfieldSize.y - objectRect.offsetMin.y - objectRect.offsetMax.y
        );

        Vector2 objectCenter = new Vector2(
            objectRect.offsetMin.x + (objectSize.x / 2) + objectRect.offsetMax.x,
            objectRect.offsetMin.y + (objectSize.y / 2) + objectRect.offsetMax.y
        );

        // Debugger.LogMessage($"Position of the center of {objectOnPlayfield.name} on Playfield is :  {objectCenter}");

        return objectCenter;
    }

    private void ScaleToFactor(float scaleFactor)
    {
        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = _defaultScale * scaleFactor;
        Vector3 deltaScale = (targetScale - currentScale) * 10;

        transform.localScale = Vector3.SmoothDamp(currentScale, targetScale, ref deltaScale, 0.1f, 1f);
    }

    private void MoveAfterDragging()
    {
        // Direction of the movement after being dragged
        Vector2 direction = (_lastPositionsWhileBeingDragged[^1] - _lastPositionsWhileBeingDragged[0]).normalized;
        Vector2 vectorToNextPosition = direction * (MoveSpeed * Time.deltaTime);

        if (IsPositionOnDesk(GetCanvasPosition() + vectorToNextPosition))
        {
            transform.Translate(vectorToNextPosition, Space.World);
        }
    }

    private void CalculateNewIdleMoveDestination()
    {
        Vector3 position = GetCanvasPosition();

        _currentRemainingMoveDistance = Random.Range(MoveMinDistance, MoveMaxDistance);
        _startPositionOfCurrentMove = position;
        _currentMoveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        _intermediateTargetForSmoothMovement = position;
        _isMoving = true;

        int tries = 0, maxTries = 10;
        Vector3 vectorToPositionInDirection = _currentMoveDirection * (_currentRemainingMoveDistance / 2);
        while (!IsPositionOnDesk(position + vectorToPositionInDirection) && tries < maxTries)
        {
            _currentMoveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            vectorToPositionInDirection = _currentMoveDirection * (_currentRemainingMoveDistance / 2);
            tries++;
        }

        Debugger.LogWarningIf(tries == maxTries, "Couldn't find position on Desk for IdleMove");
    }

    private void IdleMoveToDestination()
    {
        Vector2 vectorToNextPosition = _currentMoveDirection * (MoveSpeed * Time.deltaTime);
        float movedDistance = ((_startPositionOfCurrentMove ?? _intermediateTargetForSmoothMovement) - _intermediateTargetForSmoothMovement).magnitude;

        if (movedDistance > 3f && !_isWalking)
        {
            _isWalking = true;
            AudioManager.Instance.StartComponentFootstepLoop();

        }
        else if (_currentRemainingMoveDistance - movedDistance < 1.5f)
        {
            AudioManager.Instance.StopComponentFootstepLoop();
        }

        if (IsPositionOnDesk(_intermediateTargetForSmoothMovement + vectorToNextPosition) && movedDistance <= _currentRemainingMoveDistance)
        {
            _intermediateTargetForSmoothMovement += vectorToNextPosition;
        }

        // follow _leaderPosition - is smooth because of the _smoothMovementFactor
        SetCanvasPosition(GetCanvasPosition() + (_intermediateTargetForSmoothMovement - GetCanvasPosition()) * _smoothMovementFactor);

        if (Vector3.Distance(_intermediateTargetForSmoothMovement, GetCanvasPosition()) <= 0.1)
        {
            ResetMovementProperties();
        }
    }

    private void CalculateNewReturningDestinationOnDesk()
    {
        Vector3 position = GetCanvasPosition();
        _currentRemainingMoveDistance = Vector2.Distance(position, CenterOfDesk);
        _startPositionOfCurrentMove = position;
        Vector2 directionVector = CenterOfDesk - (Vector2)position;
        _currentMoveDirection = directionVector.normalized;
        _intermediateTargetForSmoothMovement = position;
        _isMoving = true;

        int tries = 0, maxTries = 50;
        Vector3 vectorToPositionInDirection = _currentMoveDirection * (_currentRemainingMoveDistance / 2);

        while (!IsPositionOnDesk(position + vectorToPositionInDirection) && tries < maxTries)
        {
            _currentMoveDirection = directionVector.normalized;
            vectorToPositionInDirection = _currentMoveDirection * (_currentRemainingMoveDistance / 2);
            tries++;
        }

        Debugger.LogWarningIf(tries == maxTries, "Couldn't find desk position for Returning");
    }

    public void MoveBackToDesk()
    {
        if (_isMoving)
        {
            Vector2 vectorToNextPosition = _currentMoveDirection * (ReturningMovingSpeed * Time.deltaTime);
            float acceptedDistance = _deskRect.width < _deskRect.height ? _deskRect.width / 4 : _deskRect.height / 4;

            if (Vector3.Distance(CenterOfDesk, GetCanvasPosition()) > acceptedDistance)
            {
                _isReturningToDesk = true;
                _intermediateTargetForSmoothMovement += vectorToNextPosition;
            }

            SetCanvasPosition(GetCanvasPosition() + (_intermediateTargetForSmoothMovement - GetCanvasPosition()) * _smoothMovementFactor);

            if (Vector3.Distance(CenterOfDesk, GetCanvasPosition()) <= acceptedDistance)
            {
                ResetMovementProperties();
                HandleDraggingStop();
            }
        }
        else
        {
            CalculateNewReturningDestinationOnDesk();
        }
    }

    public bool IsComponentOnDesk()
    {
        return IsPositionOnDesk(GetCanvasPosition());
    }

    public bool IsPositionOnDesk(Vector3 position)
    {
        float marginX = _marginToDeskBorder + _size.x;
        float marginY = _marginToDeskBorder + _size.y;

        Vector2 topLeft = ((Vector2)position) + new Vector2(-marginX, -marginY);
        Vector2 bottomLeft = ((Vector2)position) + new Vector2(-marginX, +marginY);
        Vector2 topRight = ((Vector2)position) + new Vector2(+marginX, -marginY);
        Vector2 bottomRight = ((Vector2)position) + new Vector2(+marginX, +marginY);

        return _deskRect.Contains(topLeft)
            && _deskRect.Contains(bottomLeft)
            && _deskRect.Contains(topRight)
            && _deskRect.Contains(bottomRight);
    }

    private void ResetMovementProperties()
    {
        _startPositionOfCurrentMove = null;
        _currentMoveDirection = Vector3.zero;
        _currentRemainingMoveDistance = 0;
        _remainingSecondsUntilIdleMoveStarts = Random.Range(MinSecondsWithoutIdleMove, MaxSecondsWithoutIdleMove);
        _isReturningToDesk = false;
        _isMoving = false;
        StartCoroutine(SetIsNotWalkingAfterDelay());
        AudioManager.Instance.StopComponentFootstepLoop();
    }

    private Vector2 GetCanvasPosition()
    {
        return GetComponent<RectTransform>().anchoredPosition;
    }

    private void SetCanvasPosition(Vector2 canvasPosition)
    {
        GetComponent<RectTransform>().anchoredPosition = canvasPosition;
    }

    public bool GetIsReturningToDesk()
    {
        return _isReturningToDesk;
    }

    private IEnumerator SetIsNotWalkingAfterDelay()
    {
        float delay = Random.Range(2f, 4f);
        yield return new WaitForSeconds(delay);

        _isWalking = false;
    }
}