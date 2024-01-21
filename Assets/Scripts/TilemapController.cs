using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private int _neighborThreshold;

    private Dictionary<Vector3Int, bool> tileMapData = new();
    // 100.000 cell = 1.3mb memory usage
    void Start()
    {
        InitTileMapData();
        AdjustTilemapAccessibility(_neighborThreshold);
    }

    public bool canPlaceObject(Vector3Int cellPosition, Vector2Int size)
    {
        for (int y = cellPosition.y; y < cellPosition.y + size.y; y++)
        {
            for (int x = cellPosition.x; x < cellPosition.x + size.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (IsCellEmpty(cell) == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool placeObjectToCells(Vector3Int cellPosition, Vector2Int size)
    {
        if (canPlaceObject(cellPosition, size) == false)
        {
            Debug.LogError($"Cannot place object at {cellPosition}");
            return false;
        }
        for (int y = cellPosition.y; y < cellPosition.y + size.y; y++)
        {
            for (int x = cellPosition.x; x < cellPosition.x + size.x; x++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                tileMapData[cell] = false;
            }
        }
        return true;
    }

    private bool IsCellEmpty(Vector3Int cellPosition)
    {
        if (tileMapData.ContainsKey(cellPosition))
        {
            return tileMapData[cellPosition];
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
                    tileMapData.Add(cellPosition, true);
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
        Dictionary<Vector3Int, bool> updatedTileMapData = new Dictionary<Vector3Int, bool>(tileMapData);

        foreach (var cell in tileMapData.Keys)
        {
            int neighborCount = CountTrueNeighbors(cell, bounds);
            if (neighborCount < neighborThreshold)
            {
                updatedTileMapData[cell] = false;
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
                if (bounds.Contains(neighborPosition) && tileMapData.TryGetValue(neighborPosition, out bool isTrue) && isTrue)
                {
                    count++;
                }
            }
        }

        return count;
    }
}