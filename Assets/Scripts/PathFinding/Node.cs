using UnityEngine;

public class Node
{
    public Vector3Int position;

    /// <summary> Cost of moving from the starting node to this node. </summary>
    public int gCost;

    /// <summary> Estimated cost of moving from this node to the target node (heuristic). </summary>
    public int hCost;

    /// <summary> Total cost of the node (gCost + hCost). </summary>
    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    /// <summary> Parent node in the path. </summary>
    public Node parent;

    public Node(Vector3Int position)
    {
        this.position = position;
    }

    public Node() { }
}