using System;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : IUnitState
{
    private Vector3Int _targetPosition;
    private Unit _unit;
    private UnitStateManager _stateManager;
    private GameObject _targetGameObject;
    private Vector3 _targetPositionVector3;
    private float _counter;


    public AttackState(Vector3Int targetPosition, Unit unit, UnitStateManager stateManager, GameObject attackableTarget)
    {
        this._targetPosition = targetPosition;
        this._unit = unit;
        this._stateManager = stateManager;
        _targetPositionVector3 = attackableTarget.transform.position;
        _targetGameObject = attackableTarget;
        _counter = unit.attackSpeed / 2;
    }

    public void UpdateState(Vector3Int unitPosition)
    {
        _counter += Time.deltaTime;
        if (_counter < _unit.attackSpeed)
            return;
        if (IsTargetMoved())
        {
            bool isAlive = _unit.Attack(_targetPosition);
            if (!isAlive)
            {
                _stateManager.SetUnitState(null);
            }
            _counter = 0;
        }
        else
        {
            _unit.PlayAnimation("Idle");
            _stateManager.SetUnitState(null);
        }
    }

    private bool IsTargetMoved()
    {
        return _targetGameObject != null && _targetPositionVector3 == _targetGameObject.transform.position;
    }

    public void OnExit()
    {
    }
}