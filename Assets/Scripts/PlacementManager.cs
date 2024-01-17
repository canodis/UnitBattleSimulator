using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid;
    [SerializeField] private AllObjectsSO allObjects;
    [SerializeField] private PreviewSystem previewSystem;
    [SerializeField] private TilemapController gridData;
    private int selectedObjectIndex = -1;

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int Id)
    {
        StopPlacement();
        selectedObjectIndex = allObjects.objectsData.FindIndex(index => index.Id == Id);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"Object not found with Id {Id}");
            return;
        }
        inputManager.OnClicked += PlaceObject;
        inputManager.OnExit += StopPlacement;
        previewSystem.StartShowingPlacementPreview(allObjects.objectsData[selectedObjectIndex].Prefab, allObjects.objectsData[selectedObjectIndex].Size);
    }

    private void PlaceObject()
    {
        if (inputManager.IsMouseOverUI())
            return;
        Vector2 mousePosition = inputManager.GetSelectedGridPosition();
        Vector3Int gridPositionInt = grid.WorldToCell(mousePosition);
        Debug.Log($"Selected position {gridPositionInt}");
        if (gridData.placeObjectToCells(gridPositionInt, allObjects.objectsData[selectedObjectIndex].Size) == false)
        {
            StopPlacement();
            return;
        }
        GameObject newObject = Instantiate(allObjects.objectsData[selectedObjectIndex].Prefab, gridPositionInt, Quaternion.identity);
        newObject.transform.position = gridPositionInt;
        StopPlacement();
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        previewSystem.StopShowingPreview();
        inputManager.OnClicked -= PlaceObject;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector2 mousePosition = inputManager.GetSelectedGridPosition();
        Vector3Int gridPositionInt = grid.WorldToCell(mousePosition);
        previewSystem.UpdatePosition(gridPositionInt, 
            gridData.canPlaceObject(gridPositionInt, allObjects.objectsData[selectedObjectIndex].Size));
    }
}
