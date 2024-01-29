using UnityEngine;

/// <summary>
/// This class is used to represent objects on the grid.
/// </summary>
public class GridObject : MonoBehaviour
{
    protected HealthSystem healthSystem;
    protected ObjectData objectData;
    protected Vector3Int gridPosition;
    [HideInInspector] public int index;

    public void Init(ObjectData objectData, Vector3Int gridPosition, int index)
    {
        this.objectData = objectData;
        this.gridPosition = gridPosition;
        this.index = index;
        healthSystem = new HealthSystem(objectData.MaxHealth, transform.Find("HealthBar"));
    }

    public ObjectData GetObjectData()
    {
        return objectData;
    }

    public void SetGridPosition(Vector3Int gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public void TakeDamage(float damage)
    {
        if (gameObject.GetComponent<IAttackable>() == null)
        {
            return;
        }
        healthSystem.TakeDamage(damage);
        if (healthSystem.GetHealth() <= 0)
        {
            gameObject.GetComponent<IAttackable>().DestroySelf();
        }
    }

    public Vector2Int GetSize()
    {
        return objectData.Size;
    }

    public int GetIndex()
    {
        return index;
    }

    public Vector3Int GetGridPosition()
    {
        return gridPosition;
    }

}