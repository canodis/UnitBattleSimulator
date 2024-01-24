using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllObjectsSO", menuName = "ScriptableObjects/AllObjectsSO", order = 1)]
public class AllObjectsSO : ScriptableObject
{
    public List<ObjectData> objectsData;
}

[System.Serializable]
public class ObjectData
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public int Id {get; private set;}
    [field: SerializeField] public Vector2Int Size {get; private set;} = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab {get; private set;}
    [field: SerializeField] public GameObject PreviewPrefab {get; private set;}
    [field: SerializeField] public Sprite Sprite {get; private set;}
    [field: SerializeField] public float MaxHealth {get; private set;}
}