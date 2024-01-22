using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private ObjectManager _objectManager;

    private IPlacementState _placementState;
    private Vector3Int _lastDetectedPosition = Vector3Int.zero;

    public void StartPlacement(int Id)
    {
        StopPlacement();

        _placementState = new PlacementState(Id, _previewSystem, _objectManager);
        _inputManager.OnLeftClicked += PlaceObject;
        _inputManager.OnExit += StopPlacement;
    }

    public void StartDestroying()
    {
        StopPlacement();

        _placementState = new DestroyState(_previewSystem, _objectManager);
        _inputManager.OnLeftClicked += PlaceObject;
        _inputManager.OnExit += StopPlacement;
    }

    private void PlaceObject()
    {
        if (_inputManager.IsMouseOverUI())
            return;
        Vector3Int gridPositionInt = _inputManager.GetMouseGridPosition();
        _placementState.OnAction(gridPositionInt);
        StopPlacement();
    }

    public void PlaceObjectAutomatically(Vector3Int gridPositionInt, int Id)
    {
        // _placementState = new PlacementState(Id, _previewSystem, _objectManager);
        // _placementState.OnAction(gridPositionInt);
        // StopPlacement();
    }

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
