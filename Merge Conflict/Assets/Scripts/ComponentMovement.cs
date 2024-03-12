/**********************************************************************************************************************
Name:          ComponentMovement
Description:   Manages the Movement of the components if they are on the desk
Author(s):     Simeon Baumann
Date:          2024-03-12
Version:       V1.1
**********************************************************************************************************************/

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentMovement : MonoBehaviour
{
    public float MinDistance = 30;
    public float MaxDistance = 70;
    private float actualDistance;

    public float MovingSpeed = 50;

    public float MinSecondsWithoutMoving = 2;
    public float MaxSecondsWithoutMoving = 4;
    public float TimeToStartMovement = 2;
    private float _deltaTimeToNextMove;

    private ComponentHandler ComponentHandler { get; set; }
    private Vector3 MovingDirection { get; set; }
    private bool _draggingWasInitiated = false;
    private Vector3? StartPoint { get; set; }

    // Borders / desk
    private DeskCreator _deskData;
    private Vector2 _sizeOfGameObject;
    private readonly float _borderOffsetComponent = 5;

    private Vector3 _defaultScale;
    public float MaxScaleFactor = 1.5f;

    private bool _firstFrameAfterDragging = false;
    private float _startTimeOfIdleMovement = 0;

    private bool _scaleBackToDefault;


    private void Start()
    {
        ComponentHandler = gameObject.GetComponent<ComponentHandler>();
        _sizeOfGameObject = GetComponent<RectTransform>().rect.size;
        _defaultScale = gameObject.GetComponent<RectTransform>().localScale;

        _sizeOfGameObject.x *= _defaultScale.x;
        _sizeOfGameObject.y *= _defaultScale.y;

        GameObject deskGameObject = GameObject.FindWithTag("desk");
        bool success = deskGameObject.TryGetComponent<DeskCreator>(out _deskData);
        if (!success)
        {
            Debugger.LogError("Could not get DeskCreator-Component from Desk.");
        }

        _deltaTimeToNextMove = TimeToStartMovement;

        // todo: recalculate _borderOffsetComponent (use MaxScaleFactor)
        MaxScaleFactor = 1.2f;
    }

    void Update()
    {
        if (ComponentHandler.isDraggingActive)
        {
            // reset everything
            _draggingWasInitiated = true;
            _firstFrameAfterDragging = true;
            
            ResetMovementProperties();
            
            ScaleToDragging();
            _scaleBackToDefault = true;
            return;
        }

        // if the component first was dragged, they can be on the desk
        // needs to be checked because the collision with the ConveyorBelt is checked later
        if (_draggingWasInitiated == false)
        {
            return;
        }

        if (_firstFrameAfterDragging)
        {
            _firstFrameAfterDragging = false;
            _startTimeOfIdleMovement = Time.time;
        }

        if (_scaleBackToDefault)
        {
            ScaleToDefault();
            if (transform.localScale.x <= _defaultScale.x + 0.0001f)
            {
                _scaleBackToDefault = false;
            }
            return;
        }

        // component is not on ConveyorBelt && was dragged at least one time
        if (ComponentHandler.CountCollisionConveyorBelt == 0)
        {
            IdleMovement();
            
            if (_deltaTimeToNextMove > 0)
            {
                _deltaTimeToNextMove -= Time.deltaTime;
            }
            else
            {
                // distance between startPoint Location and currentLocation
                var localPosition = transform.localPosition;
                var movedDistance = ((StartPoint ?? localPosition) - localPosition).magnitude;

                if (movedDistance >= actualDistance || StartPoint == null)
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

    private void IdleMovement()
    {
        var scaleFactor = ((Math.Sin(Time.time - _startTimeOfIdleMovement - Math.PI / 2) + 1) * 0.5 * (MaxScaleFactor - 1)) + 1;

        transform.localScale = _defaultScale * (float)scaleFactor;
    }

    private void ScaleToDefault()
    {
        var currentScale = transform.localScale;
        var deltaScale = (_defaultScale - currentScale) * 10;

        transform.localScale = Vector3.SmoothDamp(currentScale, _defaultScale, ref deltaScale, 0.1f, 1f);
    }

    private void ScaleToDragging()
    {
        var currentScale = transform.localScale;
        var maxScale = _defaultScale * MaxScaleFactor;
        var deltaScale = (maxScale - currentScale) * 10;

        transform.localScale = Vector3.SmoothDamp(currentScale, maxScale, ref deltaScale, 0.1f, 1f);
    }

    void CalculateNewDestination()
    {
        actualDistance = Random.Range(MinDistance, MaxDistance);
        StartPoint = transform.localPosition;

        MovingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

        Vector3 nextMove = MovingDirection * (actualDistance / 2);
        while (!PositionIsStillOnDesk(nextMove))
        {
            MovingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            nextMove = MovingDirection * (actualDistance / 2);
        }
    }

    void MoveToDestination()
    {
        Vector3 nextMove = MovingDirection * (MovingSpeed * Time.deltaTime);

        if (PositionIsStillOnDesk(nextMove))
        {
            transform.Translate(nextMove, Space.World);
        }
        else
        {
            ResetMovementProperties();
        }
    }

    bool PositionIsStillOnDesk(Vector3 nextMove)
    {
        var compPos = transform.position;
        var nextPosition = compPos + nextMove;

        // todo: how to handle 0?
        if (MovingDirection.x < 0)
        {
            // left
            var leftSideDesk = _deskData.CenterPosition.x - _deskData.Width / 2;
            var leftSideComp = nextPosition.x - _sizeOfGameObject.x / 2 - _borderOffsetComponent;

            if (leftSideDesk > leftSideComp)
            {
                return false;
            }
        }
        else
        {
            // right
            var rightSideDesk = _deskData.CenterPosition.x + _deskData.Width / 2;
            var rightSideComp = nextPosition.x + _sizeOfGameObject.x / 2 + _borderOffsetComponent;

            if (rightSideDesk < rightSideComp)
            {
                return false;
            }
        }


        if (MovingDirection.y < 0)
        {
            // bottom
            var bottomSideDesk = _deskData.CenterPosition.y - _deskData.Height / 2;
            var bottomSideComp = nextPosition.y - _sizeOfGameObject.y / 2 - _borderOffsetComponent;

            if (bottomSideDesk > bottomSideComp)
            {
                return false;
            }
        }
        else
        {
            // top
            var topSideDesk = _deskData.CenterPosition.y + _deskData.Height / 2;
            var topSideComp = nextPosition.y + _sizeOfGameObject.y / 2 + _borderOffsetComponent;

            if (topSideDesk < topSideComp)
            {
                return false;
            }
        }

        return true;
    }

    void ResetMovementProperties()
    {
        StartPoint = null;
        MovingDirection = Vector3.zero;
        actualDistance = 0;
        _deltaTimeToNextMove = TimeToStartMovement;
    }
}