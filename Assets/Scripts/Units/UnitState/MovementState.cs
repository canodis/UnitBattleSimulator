using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the movement state of a unit, handling pathfinding and movement logic.
/// </summary>
public class MovementState : IUnitState
{
    private PathFinding _pathFinder;
    private UnitStateManager _stateManager;
    private Unit _unit;
    private GameObject _attackableTarget;
    private List<Node> _path;
    private Vector3Int _oldPosition;
    private Vector3Int _newPosition;
    private Vector3Int _targetPosition;
    private Vector3Int _attackableTargetPosition;

    private bool _positionChanged = true;
    private float _speed;

    /// <summary>
    /// Initializes the MovementState with the starting and target positions, the unit, and the state manager.
    /// </summary>
    public MovementState(Vector3Int startPosition, Vector3Int targetPosition, Unit unit, UnitStateManager stateManager)
    {
        _unit = unit;
        _pathFinder = new PathFinding();
        _oldPosition = startPosition;
        _targetPosition = targetPosition;
        _attackableTargetPosition = targetPosition;
        _speed = _unit.GetSpeed();
        _stateManager = stateManager;
        _attackableTarget = _pathFinder.CheckTargetCell(startPosition, ref _targetPosition);
        _path = _pathFinder.FindPath(startPosition, _targetPosition);
        if (_path == null || _path.Count == 0)
            _stateManager.SetUnitState(null);
    }

    /// <summary>
    /// Updates the movement state based on the unit's current position.
    /// </summary>
    public void UpdateState(Vector3Int unitPosition)
    {
         // Check for conditions to transition to other states.
        if (_path == null || _path.Count == 0 || _oldPosition == _targetPosition)
        {
            _unit.PlayAnimation("Idle");
            _stateManager.SetUnitState(null);
            // Generate attack state if there's an attackable target.
            if (_attackableTarget != null && _path != null)
                _stateManager.GenerateAttackState(_attackableTargetPosition, _unit, _attackableTarget);
            return;
        }
        // Move along the path and handle animations and rotations.
        if (_path != null && _path.Count > 0)
        {
            _newPosition = _path[0].position;

            // Handle position changes in the grid.
            if (_positionChanged)
            {
                GameManager.Instance.gridData.DestroyObject(_oldPosition, _unit.GetSize());
                GameManager.Instance.gridData.PlaceObjectToCells(_newPosition, _unit.GetSize(), _unit.GetIndex(), _unit.Id);
                _unit.SetGridPosition(_newPosition);
                _positionChanged = false;
            }
            // Play run animation and rotate towards the new position.
            _unit.PlayAnimation("Run");
            _unit.RotateToDirection(_oldPosition, _newPosition);
        
            // Move the unit towards the new position.
            _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _newPosition, _speed * Time.deltaTime);

            // If the unit has reached the new location check the path again..
            if (_unit.transform.position == _newPosition)
            {
                _positionChanged = true;
                _path = _pathFinder.FindPath(_newPosition, _targetPosition);
                _oldPosition = _newPosition;
            }
        }
    }
}
