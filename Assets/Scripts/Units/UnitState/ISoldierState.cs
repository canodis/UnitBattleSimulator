using UnityEngine;

/// <summary>
/// Interface for unit states
/// </summary>
public interface ISoldierState
{
    void UpdateState(Vector3Int UnitPosition);
}