using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private int _neighborThreshold;

    private Dictionary<Vector3Int, PlacementData> tileMapData = new();

    void Start()
    {
        InitTileMapData();
        AdjustTilemapAccessibility(_neighborThreshold);
    }

    public bool CanPlaceObject(Vector3Int cellPosition, Vector2Int size)
    {
        for (int y = cellPosition.y; y < cellPosition.y + size.y; y++)
        {
            for (int x = cellPosition.x; x < cellPosition.x + size.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y);
                if (IsCellEmpty(cell) == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool PlaceObjectToCells(Vector3Int cellPosition, Vector2Int size, int index)
    {
        List<Vector2Int> rentedCells = GetRentedCells(cellPosition, size);
        PlacementData placementData = new PlacementData(rentedCells, index);
        for (int y = cellPosition.y; y < cellPosition.y + size.y; y++)
        {
            for (int x = cellPosition.x; x < cellPosition.x + size.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y);
                tileMapData[cell] = placementData;
            }
        }
        return true;
    }

    private List<Vector2Int> GetRentedCells(Vector3Int cellPosition, Vector2Int size)
    {
        List<Vector2Int> rentedCells = new List<Vector2Int>();
        for (int y = cellPosition.y; y < cellPosition.y + size.y; y++)
        {
            for (int x = cellPosition.x; x < cellPosition.x + size.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                rentedCells.Add(new Vector2Int(cell.x, cell.y));
            }
        }
        return rentedCells;
    }

    private bool IsCellEmpty(Vector3Int cellPosition)
    {
        if (tileMapData.ContainsKey(cellPosition))
        {
            return tileMapData[cellPosition] == null;
        }
        else
        {
            return false;
        }
    }

    private void InitTileMapData()
    {
        for (int y = _floorTilemap.cellBounds.yMin; y < _floorTilemap.cellBounds.yMax; y++)
        {
            for (int x = _floorTilemap.cellBounds.xMin; x < _floorTilemap.cellBounds.xMax; x++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (_floorTilemap.HasTile(cellPosition))
                {
                    tileMapData.Add(cellPosition, null);
                }
            }
        }
    }

    /// <summary>
    /// Sets tiles to false if they have fewer true neighbors than the specified threshold.
    /// </summary>
    /// <param name="neighborThreshold">Minimum number of true neighbors required to keep a tile true.</param>
    private void AdjustTilemapAccessibility(int neighborThreshold)
    {
        BoundsInt bounds = _floorTilemap.cellBounds;
        Dictionary<Vector3Int, PlacementData> updatedTileMapData = new(tileMapData);

        foreach (var cell in tileMapData.Keys)
        {
            int neighborCount = CountTrueNeighbors(cell, bounds);
            if (neighborCount < neighborThreshold)
            {
                updatedTileMapData[cell] = new PlacementData(new List<Vector2Int>(), -1);
            }
        }

        tileMapData = updatedTileMapData;
    }

    /// <summary>
    /// Counts the number of true-valued neighbors around a cell in all 8 directions.
    /// </summary>
    /// <param name="cell">The position of the cell for which neighbors are to be counted.</param>
    /// <param name="bounds">The cell bounds of the tilemap.</param>
    /// <returns>The number of true-valued neighboring cells around the specified cell.</returns>
    private int CountTrueNeighbors(Vector3Int cell, BoundsInt bounds)
    {
        int count = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector3Int neighborPosition = new Vector3Int(cell.x + x, cell.y + y, cell.z);
                if (bounds.Contains(neighborPosition) && tileMapData.TryGetValue(neighborPosition, out PlacementData isAvailable)
                    && isAvailable == null)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void DestroyObject(Vector3Int cellPosition, Vector2Int size)
    {
        for (int y = cellPosition.y; y < cellPosition.y + size.y; y++)
        {
            for (int x = cellPosition.x; x < cellPosition.x + size.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                tileMapData[cell] = null;
            }
        }
    }

    public int GetObjectIndex(Vector3Int cellPosition)
    {
        if (tileMapData.ContainsKey(cellPosition) && tileMapData[cellPosition] != null)
        {
            return tileMapData[cellPosition].Index;
        }
        else
        {
            return -1;
        }
    }

    public List<Vector3Int> GetObjectsNeighbourCells(Vector3Int cellPosition, Vector2Int size)
    {
        List<Vector3Int> neighbourCells = new List<Vector3Int>();
        Debug.Log("Cell position: " + cellPosition);
        for (int y = cellPosition.y - 1; y < cellPosition.y + size.y + 1; y++)
        {
            for (int x = cellPosition.x - 1; x < cellPosition.x + size.x + 1; x++)
            {
                if (y == cellPosition.y - 1 || y == cellPosition.y + size.y || x == cellPosition.x - 1 || x == cellPosition.x + size.x)
                {
                    Vector3Int cell = new Vector3Int(x, y, 0);
                    if (tileMapData.ContainsKey(cell))
                    {
                        Debug.Log("Neighbour cell: " + cell);
                        neighbourCells.Add(new Vector3Int(x, y, 0));
                    }
                }
            }
        }
        return neighbourCells;
    }

}