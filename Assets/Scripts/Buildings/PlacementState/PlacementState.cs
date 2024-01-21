using UnityEngine;

public class PlacementState : IPlacementState
{
    private int _selectedObjectIndex = -1;
    private PreviewSystem _previewSystem;
    private ObjectManager _objectManager;

    private bool isIdle;

    public PlacementState(PreviewSystem previewSystem, ObjectManager objectManager)
    {
        this._previewSystem = previewSystem;
        this._objectManager = objectManager;
        isIdle = true;
    }

    public void StartState(int Id)
    {
        isIdle = false;
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
        isIdle = true;
    }

    public void OnAction(Vector3Int gridPosition)
    {
        ObjectData objectData = GameManager.Instance.FindObjectDataWithIndex(_selectedObjectIndex);
        if (GameManager.Instance.gridData.placeObjectToCells(gridPosition, objectData.Size) == false)
            return;
        _objectManager.InstantiateObject(objectData, gridPosition);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool canPlace = GameManager.Instance.gridData.canPlaceObject(gridPosition,
            GameManager.Instance.allObjects.objectsData[_selectedObjectIndex].Size);

        _previewSystem.UpdatePosition(gridPosition, canPlace);
    }

    public bool IsIdleState()
    {
        return isIdle;
    }
}