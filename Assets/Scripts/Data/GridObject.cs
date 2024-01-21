using UnityEngine;

public class GridObject : MonoBehaviour
{
    protected float health;
    protected ObjectData objectData;

    public void Init(ObjectData objectData)
    {
        this.objectData = objectData;
        health = objectData.MaxHealth;
    }

    public ObjectData GetObjectData()
    {
        return objectData;
    }
}