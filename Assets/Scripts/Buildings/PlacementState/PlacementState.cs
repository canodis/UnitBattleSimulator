using UnityEngine;

/// <summary>
/// Represents the placement state when a player is trying to place an object on the grid.
/// </summary>
public class PlacementState : IPlacementState
{
    private PreviewSystem _previewSystem;
    private ObjectManager _objectManager;
    private int _selectedObjectId = -1;

    /// <summary>
    /// Initializes the PlacementState with the selected object ID, preview system, and object manager.
    /// </summary>
    public PlacementState(int Id, PreviewSystem previewSystem, ObjectManager objectManager)
    {
        this._previewSystem = previewSystem;
        this._objectManager = objectManager;
        UIManager.Instance.HideBuildingsMenu();
        _selectedObjectId = GameManager.Instance.FindObjectIndexWithId(Id);
        if (_selectedObjectId > -1)
        {
            // Start showing the placement preview for the selected object.
            ObjectData objectData = GameManager.Instance.FindObjectDataWithId(_selectedObjectId);
            _previewSystem.StartShowingPlacementPreview(objectData.PreviewPrefab, objectData.Size);
        }
        else
        {
            throw new System.Exception($"Object with id {Id} not found");
        }
    }

    /// <summary>
    /// Ends the placement state and stops showing the placement preview.
    /// </summary>
    public void EndState()
    {
        _previewSystem.StopShowingPreview();
    }

    /// <summary>
    /// Performs the placement action at the specified grid position.
    /// </summary>
    public void OnAction(Vector3Int gridPosition)
    {
        ObjectData objectData = GameManager.Instance.FindObjectDataWithId(_selectedObjectId);

        // Check if the object can be placed at the specified grid position.
        if (GameManager.Instance.gridData.CanPlaceObject(gridPosition, objectData.Size) == false)
            return;

        // Instantiate the object and update grid data.
        int index = _objectManager.InstantiateObject(objectData, gridPosition);
        GameManager.Instance.gridData.PlaceObjectToCells(gridPosition, objectData.Size, index, _selectedObjectId);
    }

    /// <summary>
    /// Updates the placement state based on the specified grid position.
    /// </summary>
    public void UpdateState(Vector3Int gridPosition)
    {
        // Check if the object can be placed at the specified grid position.
        bool canPlace = GameManager.Instance.gridData.CanPlaceObject(gridPosition,
            GameManager.Instance.allObjects.objectsData[_selectedObjectId].Size);

        // Update the preview system with the cursor's color and position.
        _previewSystem.ChangeCursorsColor(canPlace);
        _previewSystem.UpdatePreviewPosition(gridPosition);
    }

}