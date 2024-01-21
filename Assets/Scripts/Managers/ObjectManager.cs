using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private Dictionary<int, GameObject> _instatiatedObjects = new();

    public int InstantiateObject(ObjectData objectData, Vector3Int gridPosition)
    {
        GameObject gameObject = Instantiate(objectData.Prefab, gridPosition, Quaternion.identity);
        int id = _instatiatedObjects.Count == 0 ? 0 : _instatiatedObjects.Keys.Count + 1;
        _instatiatedObjects.Add(id, gameObject);
        if (gameObject.GetComponent<GridObject>() != null)
            gameObject.GetComponent<GridObject>().Init(objectData);
        return id;
    }
}