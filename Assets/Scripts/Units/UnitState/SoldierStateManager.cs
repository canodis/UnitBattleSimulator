using UnityEngine;

public class SoldierStateManager
{
    private ISoldierState _soldierState = null;

    public void GenerateMovementState(Vector3Int startPosition, Vector3Int targetPosition, Soldier soldier)
    {
        _soldierState = new MovementState(startPosition, targetPosition, soldier, this);
    }

    public void GenerateAttackState(Vector3Int attackableTargetPosition, Soldier soldier, GameObject attackableTarget)
    {
        _soldierState = new AttackState(attackableTargetPosition, soldier, this, attackableTarget);
    }

    public void UpdateState(Vector3Int soldierPosition)
    {
        if (_soldierState != null)
        {
            _soldierState.UpdateState(soldierPosition);
        }
    }

    public void SetSoldierState(ISoldierState state)
    {
        _soldierState = state;
    }
}