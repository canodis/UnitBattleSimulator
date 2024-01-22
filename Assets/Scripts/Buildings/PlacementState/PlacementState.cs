using UnityEngine;

public class PlacementState : IPlacementState
{
    private int _selectedObjectIndex = -1;
    private PreviewSystem _previewSystem;
    private ObjectManager _objectManager;


    public PlacementState(int Id, PreviewSystem previewSystem, ObjectManager objectManager)
    {
        this._previewSystem = previewSystem;
        this._objectManager = objectManager;
        UIManager.Instance.HideBuildingsMenu();
        _selectedObjectIndex = GameManager.Instance.FindObjectIndexWithId(Id);
        if (_selectedObjectIndex > -1)
        {
            ObjectData objectData = GameManager.Instance.FindObjectDataWithIndex(_selectedObjectIndex);
            _previewSystem.StartShowingPlacementPreview(objectData.Prefab, objectData.Size);
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
        ObjectData objectData = GameManager.Instance.FindObjectDataWithIndex(_selectedObjectIndex);
        if (GameManager.Instance.gridData.CanPlaceObject(gridPosition, objectData.Size) == false)
            return;
        int index = _objectManager.InstantiateObject(objectData, gridPosition);
        GameManager.Instance.gridData.PlaceObjectToCells(gridPosition, objectData.Size, index);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool canPlace = GameManager.Instance.gridData.CanPlaceObject(gridPosition,
            GameManager.Instance.allObjects.objectsData[_selectedObjectIndex].Size);

        _previewSystem.UpdatePosition(gridPosition, canPlace);
    }

}