using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a grid of nodes for pathfinding and checks node availability using a TilemapController.
/// </summary>
public class CellMap : MonoBehaviour
{
    [SerializeField] private TilemapController _tilemapController;
    static public CellMap Instance;
    private Dictionary<Vector3Int, Node> _nodes = new();

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Retrieves or creates a node at the specified grid position.
    /// </summary>
    /// <param name="x">X-coordinate of the grid position.</param>
    /// <param name="y">Y-coordinate of the grid position.</param>
    /// <returns>The node at the specified position.</returns>
    public Node getNode(int x, int y)
    {
        Vector3Int position = new Vector3Int(x, y, 0);
        if (_nodes.ContainsKey(position))
            return _nodes[position];
        Node newNode = new Node(position);
        _nodes.Add(position, newNode);
        return newNode;
    }

    /// <summary>
    /// Checks if a node at the specified position is available for placement.
    /// </summary>
    public bool IsNodeAvailable(Vector3Int position)
    {
        return _tilemapController.CanPlaceObject(position, Vector2Int.one);
    }
}