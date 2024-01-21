using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Grid _grid;
    [SerializeField] private AllObjectsSO _allObjects;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private TilemapController _gridData;
    [SerializeField] private ObjectManager _objectManager;

    private IPlacementState _placementState;
    private Vector3Int _lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int Id)
    {
        StopPlacement();

        _placementState = new PlacementState(Id, _previewSystem, _objectManager, _allObjects, _gridData);
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

    private void StopPlacement()
    {
        if (_placementState != null)
            _placementState.EndState();
        _inputManager.OnClicked -= PlaceObject;
        _inputManager.OnExit -= StopPlacement;
        UIManager.Instance.ShowBuildingsMenu();
        _placementState = null;
    }

    private void Update()
    {
        if (_placementState == null)
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
