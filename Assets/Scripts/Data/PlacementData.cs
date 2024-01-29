using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class for storing placement data
/// </summary>
[System.Serializable]
public class PlacementData
{
    public List<Vector2Int> rentedCells = new();
    public Vector3Int CellPosition;
    public int index;
    public int id;

    public PlacementData(List<Vector2Int> rentedCells, int index, int id, Vector3Int cellPosition)
    {
        this.rentedCells = rentedCells;
        this.index = index;
        this.id = id;
        CellPosition = cellPosition;
    }
}