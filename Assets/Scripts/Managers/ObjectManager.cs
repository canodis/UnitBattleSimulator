using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private List<GridObject> _instatiatedObjects = new();

    public int InstantiateObject(ObjectData objectData, Vector3Int gridPosition)
    {
        GridObject newObject = Instantiate(objectData.Prefab, gridPosition,
            Quaternion.identity).GetComponent<GridObject>();
        _instatiatedObjects.Add(newObject);
        int index = _instatiatedObjects.Count - 1;
        newObject.Init(objectData, gridPosition, index);
        return index;
    }

    public void DestroyObject(int index)
    {
        Destroy(_instatiatedObjects[index].gameObject);
        _instatiatedObjects.RemoveAt(index);
    }

    public GameObject GetObject(int index)
    {
        return _instatiatedObjects[index].gameObject;
    }
}