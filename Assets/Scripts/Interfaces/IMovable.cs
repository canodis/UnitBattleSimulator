using UnityEngine;

/// <summary>
/// Interface for movable objects
/// </summary>
public interface IMovable
{
    void Move(Vector3Int targetPosition);
}