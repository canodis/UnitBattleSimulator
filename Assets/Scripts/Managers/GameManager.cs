using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PreviewSystem previewSystem;
    public PlacementManager placementManager;
    public ObjectManager objectManager;
    public AllObjectsSO allObjects;
    public TilemapController gridData;
    public InputManager inputManager;
    private void Awake()
    {
        Instance = this;
    }

    public int FindObjectIndexWithId(int Id)
    {
        return allObjects.objectsData.FindIndex(index => index.Id == Id);
    }

    public ObjectData FindObjectDataWithIndex(int Id)
    {
        return allObjects.objectsData.Find(index => index.Id == Id);
    }
}