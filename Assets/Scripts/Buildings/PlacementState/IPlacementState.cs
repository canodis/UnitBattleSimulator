using UnityEngine;

/// <summary>
/// Interface for placement states
/// </summary>
interface IPlacementState
{
    void EndState();
    void OnAction(Vector3Int gridPosition);
    void UpdateState(Vector3Int gridPosition);
}