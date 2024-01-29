using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllObjectsSO", menuName = "ScriptableObjects/AllObjectsSO", order = 1)]
public class AllObjectsSO : ScriptableObject
{
    public List<ObjectData> objectsData;
}

/// <summary>
/// Serializable class representing data for in-game objects.
/// </summary>
[System.Serializable]
public class ObjectData
{
    // Name of the object.
    [field: SerializeField] public string Name {get; private set;}

    // Unique identifier for the object.
    [field: SerializeField] public int Id {get; private set;}

    // Size of the object in grid cells.
    [field: SerializeField] public Vector2Int Size {get; private set;} = Vector2Int.one;

    // Main prefab representing the object in the game scene.
    [field: SerializeField] public GameObject Prefab {get; private set;}

    // Prefab for previewing the object.
    [field: SerializeField] public GameObject PreviewPrefab {get; private set;}

    // Sprite representing the object.
    [field: SerializeField] public Sprite Sprite {get; private set;}

    // Maximum health value for the object.
    [field: SerializeField] public float MaxHealth {get; private set;}
}