using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the positioning of unit spawn locations in the game.
/// </summary>
public class SpawnLocationController : MonoBehaviour
{
    private List<Vector3Int> _neighbourCells;
    private Vector3Int _unitSpawnPosition;
    private bool _positionManuallyChanged = false;

    /// <summary>
    /// Moves the spawn location to the specified target position if it is a valid placement.
    /// </summary>
    public void Move(Vector3Int targetPosition)
    {
        if (GameManager.Instance.gridData.CanPlaceObject(targetPosition, Vector2Int.one))
            transform.position = targetPosition;
        _positionManuallyChanged = true;
    }

    /// <summary>
    /// Deselects the spawn location and stops showing any preview.
    /// </summary>
    public void OnDeselect()
    {
        GameManager.Instance.previewSystem.StopShowingPreview();
        _positionManuallyChanged = false;
    }

    /// <summary>
    /// Selects the spawn location and starts showing unit movement preview.
    /// </summary>
    public void OnSelect()
    {
        GameManager.Instance.previewSystem.StartUnitMovementPreview();
        GetLocation();
    }

    public void SetNeighbourCells(List<Vector3Int> neighbourCells)
    {
        _neighbourCells = neighbourCells;
    }

    /// <summary>
    /// Gets the unit spawn location either by manual input or by finding a valid position.
    /// </summary>
    public Vector3Int GetLocation()
    {
        if (_positionManuallyChanged)
            _unitSpawnPosition = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        else
            FindSpawnLocation();
        return _unitSpawnPosition;
    }

    /// <summary>
    /// Finds a valid spawn location among the neighboring cells.
    /// </summary>
    private void FindSpawnLocation()
    {
        foreach (Vector3Int cell in _neighbourCells)
        {
            if (GameManager.Instance.gridData.CanPlaceObject(cell, Vector2Int.one))
            {
                _unitSpawnPosition = cell;
                transform.position = _unitSpawnPosition;
                return;
            }
        }
    }

    /// <summary>
    /// Moves to the next valid spawn location if the position has not been manually changed.
    /// </summary>
    public void GoNextLocation()
    {
        if (_positionManuallyChanged)
            return;
        FindSpawnLocation();
    }
}
