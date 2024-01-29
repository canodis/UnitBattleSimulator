using UnityEngine;

/// <summary>
/// Represents the attack state of a soldier, handling attacking logic.
/// </summary>
public class AttackState : ISoldierState
{
    private SoldierStateManager _stateManager;
    private Soldier _soldier;
    private GameObject _targetGameObject;
    private Vector3Int _targetPosition;
    private Vector3 _targetPositionVector3;

    private float _counter;
    private float _attackSpeed;

    /// <summary>
    /// Initializes the AttackState with the target position, soldier, state manager, and attackable target.
    /// </summary>
    public AttackState(Vector3Int targetPosition, Soldier soldier, SoldierStateManager stateManager, GameObject attackableTarget)
    {
        this._targetPosition = targetPosition;
        this._soldier = soldier;
        this._stateManager = stateManager;
        _targetPositionVector3 = attackableTarget.transform.position;
        _targetGameObject = attackableTarget;
        _attackSpeed = soldier.GetAttackSpeed();
        _counter = _attackSpeed / 2;
    }

    /// <summary>
    /// Updates the attack state.
    /// </summary>
    public void UpdateState(Vector3Int soldierPosition)
    {
        // Increment the attack counter over time.
        _counter += Time.deltaTime;

        // Wait for the attack cooldown before attempting to attack again.
        if (_counter < _attackSpeed)
            return;

        if (!IsTargetMoved())
        {
            // Perform the attack and check if the target is still alive.
            bool isAlive = _soldier.Attack(_targetPosition);
            if (!isAlive)
            {
                // Transition to the null state if the target is no longer alive.
                _stateManager.SetSoldierState(null);
            }
            _counter = 0;
        }
        else
        {
            // Transition to the null state if the target has moved.
            _soldier.PlayAnimation("Idle");
            _stateManager.SetSoldierState(null);
        }
    }

    /// <summary>
    /// Checks if the target has moved since the attack started.
    /// </summary>
    /// <returns>True if the target has moved, false otherwise.</returns>
    private bool IsTargetMoved()
    {
        return _targetGameObject == null || _targetPositionVector3 != _targetGameObject.transform.position;
    }
}
