using System.Collections.Generic;
using UnityEngine;

public class MovementState : IUnitState
{
    private PathFinding _pathFinder;
    private Unit _unit;
    private List<Node> _path;
    private float _speed;
    private UnitStateManager _stateManager;
    private Vector3Int _oldPosition;
    private Vector3Int _newPosition;
    private Vector3Int _targetPosition;

    private GameObject _attackableTarget;
    private Vector3Int _attackableTargetPosition;
    private bool _positionChanged = true;

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

    public void UpdateState(Vector3Int unitPosition)
    {
        if (_path == null || _path.Count == 0 || _oldPosition == _targetPosition)
        {
            _unit.PlayAnimation("Idle");
            _stateManager.SetUnitState(null);
            if (_attackableTarget != null && _path != null)
                _stateManager.AttackTarget(_attackableTargetPosition, _unit, _attackableTarget);
            return;
        }
        if (_path != null && _path.Count > 0)
        {
            _newPosition = _path[0].position;
            if (_positionChanged)
            {
                GameManager.Instance.gridData.DestroyObject(_oldPosition, _unit.GetSize());
                GameManager.Instance.gridData.PlaceObjectToCells(_newPosition, _unit.GetSize(), _unit.GetIndex(), _unit.Id);
                _unit.SetGridPosition(_newPosition);
                _positionChanged = false;
            }
            _unit.PlayAnimation("Run");
            _unit.RotateToMoveDirection(_oldPosition, _newPosition);
            _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _newPosition, _speed * Time.deltaTime);
            if (_unit.transform.position == _newPosition)
            {
                _positionChanged = true;
                _path = _pathFinder.FindPath(_newPosition, _targetPosition);
                _oldPosition = _newPosition;
            }
        }
    }

    public void OnExit()
    {
        // if (_unit.GetGridPosition() != _newPosition)
        // {
        //     Debug.Log("unit position not equal to new position");
        //     _unit.SetGridPosition(_newPosition);
        // }
    }
}

