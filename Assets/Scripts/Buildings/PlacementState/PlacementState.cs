using UnityEngine;

public class PlacementState : IPlacementState
{
    private int _selectedObjectId = -1;
    private PreviewSystem _previewSystem;
    private ObjectManager _objectManager;


    public PlacementState(int Id, PreviewSystem previewSystem, ObjectManager objectManager)
    {
        this._previewSystem = previewSystem;
        this._objectManager = objectManager;
        UIManager.Instance.HideBuildingsMenu();
        _selectedObjectId = GameManager.Instance.FindObjectIndexWithId(Id);
        if (_selectedObjectId > -1)
        {
            ObjectData objectData = GameManager.Instance.FindObjectDataWithId(_selectedObjectId);
            _previewSystem.StartShowingPlacementPreview(objectData.PreviewPrefab, objectData.Size);
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
        ObjectData objectData = GameManager.Instance.FindObjectDataWithId(_selectedObjectId);
        if (GameManager.Instance.gridData.CanPlaceObject(gridPosition, objectData.Size) == false)
            return;
        int index = _objectManager.InstantiateObject(objectData, gridPosition);
        GameManager.Instance.gridData.PlaceObjectToCells(gridPosition, objectData.Size, index, _selectedObjectId);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool canPlace = GameManager.Instance.gridData.CanPlaceObject(gridPosition,
            GameManager.Instance.allObjects.objectsData[_selectedObjectId].Size);
        _previewSystem.ChangeCursorsColor(canPlace);
        _previewSystem.UpdatePreviewPosition(gridPosition);
    }

}