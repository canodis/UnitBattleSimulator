using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocationController : MonoBehaviour
{
    private List<Vector3Int> _neighbourCells;
    private Vector3Int _unitSpawnPosition;
    private bool positionManuallyChanged = false;

    public void Move(Vector3Int targetPosition)
    {
        if (GameManager.Instance.gridData.CanPlaceObject(targetPosition, Vector2Int.one))
            transform.position = targetPosition;
        positionManuallyChanged = true;
    }

    public void OnDeselect()
    {
        GameManager.Instance.previewSystem.StopShowingPreview();
        positionManuallyChanged = false;
    }

    public void OnSelect()
    {
        GameManager.Instance.previewSystem.StartUnitMovementPreview();
        GetLocation();
    }

    public void SetNeighbourCells(List<Vector3Int> neighbourCells)
    {
        _neighbourCells = neighbourCells;
    }

    public Vector3Int GetLocation()
    {
        if (positionManuallyChanged)
            _unitSpawnPosition = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        else
            FindSpawnLocation();
        return _unitSpawnPosition;
    }

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

    public void GoNextLocation()
    {
        if (positionManuallyChanged)
            return;
        FindSpawnLocation();
    }
}
