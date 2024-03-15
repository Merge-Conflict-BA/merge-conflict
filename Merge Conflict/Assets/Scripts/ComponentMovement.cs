/**********************************************************************************************************************
Name:          ComponentMovement
Description:   Manages the Movement of the components if they are on the desk
Author(s):     Simeon Baumann
Date:          2024-03-14
Version:       V1.1
**********************************************************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentMovement : MonoBehaviour
{
    // Distance to move from a position to another
    public float MinDistance = 30;
    public float MaxDistance = 70;
    // Distance by which the component should move during the current movement
    private float _distanceToMove;

    // Speed to move from a position to another
    public float MovingSpeed = 50;

    // Time between Movement from a position to another
    public float MinSecondsWithoutMoving = 2;
    public float MaxSecondsWithoutMoving = 4;
    public float TimeToStartMovement = 2;
    private float _deltaTimeToNextMove;
    
    // Direction of the Movement from a position to another
    private Vector3 _movingDirection;
    private Vector3? _movingStartPosition;

    private ComponentHandler _componentHandler;

    // Borders / desk
    private DeskCreator _deskData;
    // Space between Component and border of desk
    private float _marginOfComponent = 5;

    // Default size and scale of the component
    private Vector2 _sizeOfComponent;
    private Vector3 _defaultScaleOfComponent;
    
    public float MaxScaleFactor = 1.05f;
    private bool _scaleBackToDefault;

    private bool _firstFrameAfterDragging = false;
    private float _startTimeOfIdleMovement = 0;

    // Store the last positions of the component while dragging to calculate the direction of the further movement after dragging
    private List<Vector3> _lastPositions;

    private void Start()
    {
        _componentHandler = gameObject.GetComponent<ComponentHandler>();
        _sizeOfComponent = GetComponent<RectTransform>().rect.size;
        _defaultScaleOfComponent = gameObject.GetComponent<RectTransform>().localScale;

        _sizeOfComponent.x *= _defaultScaleOfComponent.x;
        _sizeOfComponent.y *= _defaultScaleOfComponent.y;

        GameObject deskGameObject = GameObject.FindWithTag("desk");
        bool success = deskGameObject.TryGetComponent<DeskCreator>(out _deskData);
        if (!success)
        {
            Debugger.LogError("Could not get DeskCreator-Component from Desk.");
        }

        _deltaTimeToNextMove = TimeToStartMovement;
        
        // Recalculate Margin of Component because the size of the component is bigger if it is scaled
        RecalculateMargin();

        _lastPositions = new List<Vector3>();
    }

    void Update()
    {
        if (_componentHandler.isDraggingActive)
        {
            _firstFrameAfterDragging = true;
            
            ScaleToFactor(MaxScaleFactor);
            // indicates that component should be scaled back to default after being dragged
            _scaleBackToDefault = true;
            
            // Add position to array of last positions
            if (_lastPositions.Count == 0)
            {
                _lastPositions.Add(transform.position);
            }
            else if (_lastPositions[^1] != transform.position)
            {
                _lastPositions.Add(transform.position);
            }
            
            if (_lastPositions.Count > 5)
            {
                _lastPositions.RemoveAt(0);
            }
            return;
        }

        if (_firstFrameAfterDragging)
        {
            _firstFrameAfterDragging = false;
            
            ResetMovementProperties();
            _startTimeOfIdleMovement = Time.time;
        }

        if (_scaleBackToDefault)
        {
            ScaleToFactor(1f);
            MoveAfterDragging();
            if (transform.localScale.x <= _defaultScaleOfComponent.x + 0.1f)
            {
                _scaleBackToDefault = false;
                _lastPositions = new List<Vector3>();
            }
            return;
        }

        // component is not on ConveyorBelt
        if (_componentHandler.CountCollisionConveyorBelt == 0)
        {
            IdleMovement();
            
            if (_deltaTimeToNextMove > 0)
            {
                _deltaTimeToNextMove -= Time.deltaTime;
            }
            else
            {
                Vector3 localPosition = transform.localPosition;
                // distance between startPosition and currentPosition
                float movedDistance = ((_movingStartPosition ?? localPosition) - localPosition).magnitude;

                if (movedDistance >= _distanceToMove || _movingStartPosition == null)
                {
                    CalculateNewDestination();
                    _deltaTimeToNextMove = Random.Range(MinSecondsWithoutMoving, MaxSecondsWithoutMoving);
                }
                else
                {
                    MoveToDestination();
                }
            }
        }
    }

    public void SyncComponentMovementProperties(float minDistance,
                                                float maxDistance,
                                                float movingSpeed, 
                                                float minSecondsWithoutMoving, 
                                                float maxSecondsWithoutMoving,
                                                float timeToStartMovement,
                                                float maxScaleFactor)
    {
        MinDistance = minDistance;
        MaxDistance = maxDistance;
        MovingSpeed = movingSpeed;
        MinSecondsWithoutMoving = minSecondsWithoutMoving;
        MaxSecondsWithoutMoving = maxSecondsWithoutMoving;
        TimeToStartMovement = timeToStartMovement;
        MaxScaleFactor = maxScaleFactor;
    }
    
    private void RecalculateMargin()
    {
        float deltaWidth = (_sizeOfComponent.x * MaxScaleFactor) - _sizeOfComponent.x;
        _marginOfComponent += _marginOfComponent + deltaWidth;
    }

    private void IdleMovement()
    {
        // get a smooth scaleFactor to have a smooth scaling animation
        double scaleFactor = ((Math.Sin(Time.time - _startTimeOfIdleMovement - Math.PI / 2) + 1) * 0.5 * (MaxScaleFactor - 1)) + 1;

        transform.localScale = _defaultScaleOfComponent * (float)scaleFactor;
    }

    private void ScaleToFactor(float scaleFactor)
    {
        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = _defaultScaleOfComponent * scaleFactor;
        Vector3 deltaScale = (targetScale - currentScale) * 10;

        transform.localScale = Vector3.SmoothDamp(currentScale, targetScale, ref deltaScale, 0.1f, 1f);
    }

    private void MoveAfterDragging()
    {
        // Direction of the movement after being dragged
        Vector3 direction = (_lastPositions[^1] - _lastPositions[0]).normalized;
        Vector3 vectorToNextPosition = direction * (MovingSpeed * Time.deltaTime);
        
        if (PositionIsStillOnDesk(vectorToNextPosition))
        {
            transform.Translate(vectorToNextPosition, Space.World);
        }
    }

    void CalculateNewDestination()
    {
        _distanceToMove = Random.Range(MinDistance, MaxDistance);
        _movingStartPosition = transform.localPosition;
        _movingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

        Vector3 vectorToPositionInDirection = _movingDirection * (_distanceToMove / 2);
        while (!PositionIsStillOnDesk(vectorToPositionInDirection))
        {
            _movingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            vectorToPositionInDirection = _movingDirection * (_distanceToMove / 2);
        }
    }

    void MoveToDestination()
    {
        Vector3 vectorToNextPosition = _movingDirection * (MovingSpeed * Time.deltaTime);

        if (PositionIsStillOnDesk(vectorToNextPosition))
        {
            transform.Translate(vectorToNextPosition, Space.World);
        }
        else
        {
            ResetMovementProperties();
        }
    }

    bool PositionIsStillOnDesk(Vector3 vectorToNextPosition)
    {
        Vector3 componentPosition = transform.position;
        Vector3 nextPosition = componentPosition + vectorToNextPosition;

        if (_movingDirection.x < 0)
        {
            // left
            float leftSideDesk = _deskData.CenterPosition.x - _deskData.Width / 2;
            float leftSideComp = nextPosition.x - _sizeOfComponent.x / 2 - _marginOfComponent;

            if (leftSideDesk > leftSideComp)
            {
                return false;
            }
        }
        else
        {
            // right
            float rightSideDesk = _deskData.CenterPosition.x + _deskData.Width / 2;
            float rightSideComp = nextPosition.x + _sizeOfComponent.x / 2 + _marginOfComponent;

            if (rightSideDesk < rightSideComp)
            {
                return false;
            }
        }


        if (_movingDirection.y < 0)
        {
            // bottom
            float bottomSideDesk = _deskData.CenterPosition.y - _deskData.Height / 2;
            float bottomSideComp = nextPosition.y - _sizeOfComponent.y / 2 - _marginOfComponent;

            if (bottomSideDesk > bottomSideComp)
            {
                return false;
            }
        }
        else
        {
            // top
            float topSideDesk = _deskData.CenterPosition.y + _deskData.Height / 2;
            float topSideComp = nextPosition.y + _sizeOfComponent.y / 2 + _marginOfComponent;

            if (topSideDesk < topSideComp)
            {
                return false;
            }
        }

        return true;
    }

    void ResetMovementProperties()
    {
        _movingStartPosition = null;
        _movingDirection = Vector3.zero;
        _distanceToMove = 0;
        _deltaTimeToNextMove = Random.Range(MinSecondsWithoutMoving, MaxSecondsWithoutMoving);
    }
}