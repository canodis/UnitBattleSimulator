using System;
using UnityEngine;

public class UnitStateManager
{
    private IUnitState _unitState = null;

    public void GenerateNewState(Vector3Int startPosition, Vector3Int targetPosition, Unit unit)
    {
        if (_unitState != null)
        {
            _unitState.OnExit();
        }
        _unitState = new MovementState(startPosition, targetPosition, unit, this);
    }

    public void UpdateState(Vector3Int unitPosition)
    {
        if (_unitState != null)
        {
            _unitState.UpdateState(unitPosition);
        }
    }

    public void SetUnitState(IUnitState state)
    {
        if (_unitState != null)
        {
            _unitState.OnExit();
        }
        _unitState = state;
    }

    public void AttackTarget(Vector3Int attackableTargetPosition, Unit unit, GameObject attackableTarget)
    {
        if (_unitState != null)
        {
            _unitState.OnExit();
        }
        _unitState = new AttackState(attackableTargetPosition, unit, this, attackableTarget);
    }
}