/**********************************************************************************************************************
Name:          ComponentMovement
Description:   Manages the Movement of the components if they are on the desk
Author(s):     Simeon Baumann
Date:          2024-03-08
Version:       V1.0
**********************************************************************************************************************/
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


    private void Start()
    {
        ComponentHandler = gameObject.GetComponent<ComponentHandler>();
        _sizeOfGameObject = GetComponent<RectTransform>().rect.size;
        var scale = gameObject.GetComponent<RectTransform>().localScale;

        _sizeOfGameObject.x *= scale.x;
        _sizeOfGameObject.y *= scale.y;
        
        GameObject deskGameObject = GameObject.FindWithTag("desk");
        bool success = deskGameObject.TryGetComponent<DeskCreator>(out _deskData);
        if (!success)
        {
            Debugger.LogError("Could not get DeskCreator-Component from Desk.");
        }

        _deltaTimeToNextMove = TimeToStartMovement;
    }

    void Update()
    {
        if (ComponentHandler.isDraggingActive)
        {
            // reset everything
            _draggingWasInitiated = true;
            ResetMovementProperties();
            return;
        }

        if (_draggingWasInitiated == false)
        {
            return;
        }

        if (_deltaTimeToNextMove > 0)
        {
            _deltaTimeToNextMove -= Time.deltaTime;
        }
        else
        {
            // component is not on ConveyorBelt && was dragged at least one time
            if (ComponentHandler.CountCollisionConveyorBelt == 0)
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
