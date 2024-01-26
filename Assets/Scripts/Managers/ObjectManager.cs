using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private List<GridObject> _instatiatedObjects = new();

    public int InstantiateObject(ObjectData objectData, Vector3Int gridPosition)
    {
        GridObject newObject = Instantiate(objectData.Prefab, gridPosition,
            Quaternion.identity, _objectPrefab.transform).GetComponent<GridObject>();
        _instatiatedObjects.Add(newObject);
        int index = _instatiatedObjects.Count - 1;
        newObject.Init(objectData, gridPosition, index);
        return index;
    }

    public void DestroyObject(int index)
    {
        Destroy(_instatiatedObjects[index].gameObject);
        _instatiatedObjects[index] = null;
    }

    public GridObject GetGridObjectWithPosition(Vector3Int position)
    {
        int index = GameManager.Instance.gridData.GetObjectIndex(position);
        if (index != -1)
        {
            return _instatiatedObjects[index];
        }
        return null;
    }

    public GameObject GetObject(int index)
    {
        return _instatiatedObjects[index].gameObject;
    }
}