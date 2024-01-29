using UnityEngine;

public class UnitStateManager
{
    private IUnitState _unitState = null;

    public void GenerateMovementState(Vector3Int startPosition, Vector3Int targetPosition, Unit unit)
    {
        _unitState = new MovementState(startPosition, targetPosition, unit, this);
    }

    public void GenerateAttackState(Vector3Int attackableTargetPosition, Unit unit, GameObject attackableTarget)
    {
        _unitState = new AttackState(attackableTargetPosition, unit, this, attackableTarget);
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
        _unitState = state;
    }
}