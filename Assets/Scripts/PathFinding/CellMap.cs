using System.Collections.Generic;
using UnityEngine;

public class CellMap : MonoBehaviour
{
    [SerializeField] private TilemapController _tilemapController;
    static public CellMap Instance;
    public Dictionary <Vector3Int, Node> Nodes = new();
    

    private void Awake()
    {
        Instance = this;
    }

    public Node getNode(int x, int y)
    {
        Vector3Int position = new Vector3Int(x, y, 0);
        if (Nodes.ContainsKey(position))
            return Nodes[position];
        Node newNode = new Node(position);
        Nodes.Add(position, newNode);
        return newNode;
    }

    public bool IsNodeAvailable(Vector3Int position)
    {
        return _tilemapController.CanPlaceObject(position, Vector2Int.one);
    }
}