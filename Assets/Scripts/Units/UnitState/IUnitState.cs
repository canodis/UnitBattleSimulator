using UnityEngine;

/// <summary>
/// Interface for unit states
/// </summary>
public interface IUnitState
{
    void UpdateState(Vector3Int UnitPosition);
}