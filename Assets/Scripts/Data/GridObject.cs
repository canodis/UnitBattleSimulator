using UnityEngine;

public class GridObject : MonoBehaviour
{
    protected float health;
    protected ObjectData objectData;
    protected Vector3Int gridPosition;
    protected int index;

    public void Init(ObjectData objectData, Vector3Int gridPosition, int index)
    {
        this.objectData = objectData;
        this.gridPosition = gridPosition;
        this.index = index;
        health = objectData.MaxHealth;
    }

    public ObjectData GetObjectData()
    {
        return objectData;
    }
}