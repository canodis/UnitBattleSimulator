using UnityEngine;

/// <summary>
/// Represents the attack state of a unit, handling attacking logic.
/// </summary>
public class AttackState : IUnitState
{
    private UnitStateManager _stateManager;
    private Unit _unit;
    private GameObject _targetGameObject;
    private Vector3Int _targetPosition;
    private Vector3 _targetPositionVector3;

    private float _counter;
    private float _attackSpeed;

    /// <summary>
    /// Initializes the AttackState with the target position, unit, state manager, and attackable target.
    /// </summary>
    public AttackState(Vector3Int targetPosition, Unit unit, UnitStateManager stateManager, GameObject attackableTarget)
    {
        this._targetPosition = targetPosition;
        this._unit = unit;
        this._stateManager = stateManager;
        _targetPositionVector3 = attackableTarget.transform.position;
        _targetGameObject = attackableTarget;
        _attackSpeed = unit.GetAttackSpeed();
        _counter = _attackSpeed / 2;
    }

    /// <summary>
    /// Updates the attack state.
    /// </summary>
    public void UpdateState(Vector3Int unitPosition)
    {
        // Increment the attack counter over time.
        _counter += Time.deltaTime;

        // Wait for the attack cooldown before attempting to attack again.
        if (_counter < _attackSpeed)
            return;

        if (!IsTargetMoved())
        {
            // Perform the attack and check if the target is still alive.
            bool isAlive = _unit.Attack(_targetPosition);
            if (!isAlive)
            {
                // Transition to the null state if the target is no longer alive.
                _stateManager.SetUnitState(null);
            }
            _counter = 0;
        }
        else
        {
            // Transition to the null state if the target has moved.
            _unit.PlayAnimation("Idle");
            _stateManager.SetUnitState(null);
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
