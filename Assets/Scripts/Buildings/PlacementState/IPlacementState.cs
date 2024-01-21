using UnityEngine;

interface IPlacementState
{
    void EndState();
    void StartState(int Id);
    void OnAction(Vector3Int gridPosition);
    void UpdateState(Vector3Int gridPosition);
    bool IsIdleState();
}