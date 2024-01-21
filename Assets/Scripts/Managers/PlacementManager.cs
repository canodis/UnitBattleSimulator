using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private ObjectManager _objectManager;

    private IPlacementState _placementState;
    private Vector3Int _lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        _placementState = new PlacementState(_previewSystem, _objectManager);
    }

    public void StartPlacement(int Id)
    {
        StopPlacement();

        _placementState.StartState(Id);
        _inputManager.OnClicked += PlaceObject;
        _inputManager.OnExit += StopPlacement;
    }

    private void PlaceObject()
    {
        if (_inputManager.IsMouseOverUI())
            return;
        Vector2 mousePosition = _inputManager.GetMousePosition();
        Vector3Int gridPositionInt = _grid.WorldToCell(mousePosition);
        _placementState.OnAction(gridPositionInt);
        StopPlacement();
    }

    public void PlaceObjectAutomatically(Vector3Int gridPositionInt, int Id)
    {
        _placementState.StartState(Id);
        _placementState.OnAction(gridPositionInt);
        StopPlacement();
    }

    private void StopPlacement()
    {
        _placementState.EndState();
        _inputManager.OnClicked -= PlaceObject;
        _inputManager.OnExit -= StopPlacement;
        UIManager.Instance.ShowBuildingsMenu();
    }

    private void Update()
    {
        if (_placementState.IsIdleState())
            return;
        Vector2 mousePosition = _inputManager.GetMousePosition();
        Vector3Int gridPositionInt = _grid.WorldToCell(mousePosition);
        if (gridPositionInt != _lastDetectedPosition)
        {
            _placementState.UpdateState(gridPositionInt);
            _lastDetectedPosition = gridPositionInt;
        }
    }
}
