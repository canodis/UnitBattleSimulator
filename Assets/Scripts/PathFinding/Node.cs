using UnityEngine;

public class Node
{
    public Vector3Int position;
    public int gCost;
    public int hCost;
    public int fCost {
        get {
            return gCost + hCost;
        }
    }
    public Node parent;

    public Node(Vector3Int position)
    {
        this.position = position;
    }

    public Node()
    {

    }
}