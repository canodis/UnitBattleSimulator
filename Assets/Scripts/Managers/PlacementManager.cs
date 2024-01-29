using UnityEngine;

/// <summary>
/// Manages object placement and handles user input for object placement.
/// </summary>
public class PlacementManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private ObjectManager _objectManager;

    private IPlacementState _placementState;
    private Vector3Int _lastDetectedPosition = Vector3Int.zero;

    /// <summary>
    /// Starts the placement process for the specified object ID.
    /// </summary>
    public void StartPlacement(int Id)
    {
        StopPlacement();

        _placementState = new PlacementState(Id, _previewSystem, _objectManager);
        _inputManager.OnLeftClicked += PlaceObject;
        _inputManager.OnExit += StopPlacement;
    }

    /// <summary>
    /// Handles the placement of the object when the left mouse button is clicked.
    /// </summary>
    private void PlaceObject()
    {
        if (_inputManager.IsMouseOverUI())
            return;
        Vector3Int gridPositionInt = _inputManager.GetMouseGridPosition();
        _placementState.OnAction(gridPositionInt);
        StopPlacement();
    }

    /// <summary>
    /// Places an object immediately at the specified grid position with the given ID.
    /// </summary
    public void PlaceObjectImmediately(Vector3Int gridPositionInt, int Id)
    {
        _placementState = new PlacementState(Id, _previewSystem, _objectManager);
        _placementState.OnAction(gridPositionInt);
        StopPlacement();
    }

    /// <summary>
    /// Stops the placement process and resets relevant events.
    /// </summary>
    private void StopPlacement()
    {
        if (_placementState == null)
            return;
        _placementState.EndState();
        _inputManager.OnLeftClicked -= PlaceObject;
        _inputManager.OnExit -= StopPlacement;
        UIManager.Instance.ShowBuildingsMenu();
        _placementState = null;
    }

    private void Update()
    {
        if (_placementState == null)
            return;
        Vector3Int gridPositionInt = _inputManager.GetMouseGridPosition();
        if (gridPositionInt != _lastDetectedPosition)
        {
            _placementState.UpdateState(gridPositionInt);
            _lastDetectedPosition = gridPositionInt;
        }
    }
}
