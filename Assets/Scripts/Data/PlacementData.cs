using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacementData
{
    public List<Vector2Int> rentedCells = new();
    public int Index;

    public PlacementData(List<Vector2Int> rentedCells, int index)
    {
        this.rentedCells = rentedCells;
        Index = index;
    }
}