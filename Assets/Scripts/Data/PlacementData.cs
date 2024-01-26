using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacementData
{
    public List<Vector2Int> rentedCells = new();
    public Vector3Int CellPosition;
    public int Index;
    public int Id;

    public PlacementData(List<Vector2Int> rentedCells, int index, int id, Vector3Int cellPosition)
    {
        this.rentedCells = rentedCells;
        Index = index;
        Id = id;
        CellPosition = cellPosition;
    }
}