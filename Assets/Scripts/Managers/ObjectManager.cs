using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the instantiation and destruction of grid objects in the game.
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private List<GridObject> _instatiatedObjects = new();

    /// <summary>
    /// Instantiates a new object based on the provided object data at the specified grid position.
    /// </summary>
    /// <param name="objectData">Data representing the object to be instantiated.</param>
    /// <param name="gridPosition">Grid position where the object will be instantiated.</param>
    /// <returns>The index of the instantiated object in the list.</returns>
    public int InstantiateObject(ObjectData objectData, Vector3Int gridPosition)
    {
        GridObject newObject = Instantiate(objectData.Prefab, gridPosition,
            Quaternion.identity, _objectPrefab.transform).GetComponent<GridObject>();
        _instatiatedObjects.Add(newObject);
        int index = _instatiatedObjects.Count - 1;
        newObject.Init(objectData, gridPosition, index);
        return index;
    }

    /// <summary>
    /// Destroys the object at the specified index.
    /// </summary>
    /// <param name="index">Index of the object to destroy.</param>
    public void DestroyObject(int index)
    {
        Destroy(_instatiatedObjects[index].gameObject);
        _instatiatedObjects[index] = null;
    }

    /// <summary>
    /// Retrieves the grid object at the specified position.
    /// </summary>
    /// <param name="position">Position to check for a grid object.</param>
    /// <returns>The grid object at the specified position, or null if none is found.</returns>
    public GridObject GetGridObjectWithPosition(Vector3Int position)
    {
        int index = GameManager.Instance.gridData.GetObjectIndex(position);
        if (index != -1)
        {
            return _instatiatedObjects[index];
        }
        return null;
    }

    /// <summary>
    /// Retrieves the GameObject of the grid object at the specified index.
    /// </summary>
    /// <param name="index">Index of the grid object.</param>
    /// <returns>The GameObject of the grid object at the specified index.</returns>
    public GameObject GetObject(int index)
    {
        return _instatiatedObjects[index]?.gameObject;
    }
}