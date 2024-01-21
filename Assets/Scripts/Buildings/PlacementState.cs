using UnityEngine;

public class PlacementState : IPlacementState
{
    private int _selectedObjectIndex = -1;
    private int _Id;
    private PreviewSystem _previewSystem;
    private ObjectManager _objectManager;
    private AllObjectsSO _allObjects;
    private TilemapController _gridData;

    public PlacementState(int Id, PreviewSystem previewSystem, ObjectManager objectManager, AllObjectsSO allObjects, TilemapController gridData)
    {
        this._Id = Id;
        this._previewSystem = previewSystem;
        this._objectManager = objectManager;
        this._allObjects = allObjects;
        this._gridData = gridData;

        UIManager.Instance.HideBuildingsMenu();
        _selectedObjectIndex = allObjects.objectsData.FindIndex(index => index.Id == Id);
        if (_selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(allObjects.objectsData[_selectedObjectIndex].Prefab,
                allObjects.objectsData[_selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"Object with id {Id} not found");
        }
    }

    public void EndState()
    {
        _previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        if (_gridData.placeObjectToCells(gridPosition, _allObjects.objectsData[_selectedObjectIndex].Size) == false)
            return;
        _objectManager.InstantiateObject(_allObjects.objectsData[_selectedObjectIndex], gridPosition);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool canPlace = _gridData.canPlaceObject(gridPosition, _allObjects.objectsData[_selectedObjectIndex].Size);

        _previewSystem.UpdatePosition(gridPosition, canPlace);
    }
}