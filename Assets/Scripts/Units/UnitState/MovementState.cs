using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the movement state of a soldier, handling pathfinding and movement logic.
/// </summary>
public class MovementState : ISoldierState
{
    private PathFinding _pathFinder;
    private SoldierStateManager _stateManager;
    private Soldier _soldier;
    private GameObject _attackableTarget;
    private List<Node> _path;
    private Vector3Int _oldPosition;
    private Vector3Int _newPosition;
    private Vector3Int _targetPosition;
    private Vector3Int _attackableTargetPosition;

    private bool _positionChanged = true;
    private float _speed;

    /// <summary>
    /// Initializes the MovementState with the starting and target positions, the soldier, and the state manager.
    /// </summary>
    public MovementState(Vector3Int startPosition, Vector3Int targetPosition, Soldier soldier, SoldierStateManager stateManager)
    {
        _soldier = soldier;
        _pathFinder = new PathFinding();
        _oldPosition = startPosition;
        _targetPosition = targetPosition;
        _attackableTargetPosition = targetPosition;
        _speed = soldier.GetSpeed();
        _stateManager = stateManager;
        _attackableTarget = _pathFinder.CheckTargetCell(startPosition, ref _targetPosition);
        _path = _pathFinder.FindPath(startPosition, _targetPosition);
        if (_path == null || _path.Count == 0)
            _stateManager.SetSoldierState(null);
    }

    /// <summary>
    /// Updates the movement state based on the soldier's current position.
    /// </summary>
    public void UpdateState(Vector3Int soldierPosition)
    {
        // Check for conditions to transition to other states.
        if (_path == null || _path.Count == 0 || _oldPosition == _targetPosition)
        {
            _soldier.PlayAnimation("Idle");
            _stateManager.SetSoldierState(null);
            // Generate attack state if there's an attackable target.
            if (_attackableTarget != null && _path != null)
                _stateManager.GenerateAttackState(_attackableTargetPosition, _soldier, _attackableTarget);
            return;
        }
        // Move along the path and handle animations and rotations.
        if (_path != null && _path.Count > 0)
        {
            _newPosition = _path[0].position;

            // Handle position changes in the grid.
            if (_positionChanged)
            {
                GameManager.Instance.gridData.DestroyObject(_oldPosition, _soldier.GetSize());
                GameManager.Instance.gridData.PlaceObjectToCells(_newPosition, _soldier.GetSize(), _soldier.GetIndex(), _soldier.Id);
                _soldier.SetGridPosition(_newPosition);
                _positionChanged = false;
            }
            // Play run animation and rotate towards the new position.
            _soldier.PlayAnimation("Run");
            _soldier.RotateToDirection(_oldPosition, _newPosition);
        
            // Move the unit towards the new position.
            _soldier.transform.position = Vector3.MoveTowards(_soldier.transform.position, _newPosition, _speed * Time.deltaTime);

            // If the unit has reached the new location check the path again..
            if (_soldier.transform.position == _newPosition)
            {
                _positionChanged = true;
                _path = _pathFinder.FindPath(_newPosition, _targetPosition);
                _oldPosition = _newPosition;
            }
        }
    }
}
