using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : GridObject
{
    protected InfoPanelController _infoPanelController;
    protected List<Vector3Int> _neighbourCells = new();

    protected void Start()
    {
        _infoPanelController = GameObject.FindWithTag("InfoPanelController").GetComponent<InfoPanelController>();
        _neighbourCells = GameManager.Instance.gridData.GetObjectsNeighbourCells(gridPosition, objectData.Size);
    }

    protected virtual void ShowInfo()
    {
        if (objectData == null || InputManager.Instance.IsMouseOverUI())
            return;
        _infoPanelController.UpdateInfoPanel(objectData.Name, objectData.Sprite);
    }

    protected virtual void HideInfo()
    {
        _infoPanelController.HideInfoPanel();
    }

    public void DestroyBuilding()
    {
        // GameManager.Instance.gridData.DestroyObject(gridPosition, objectData.Size);
        // GameManager.Instance.objectManager.DestroyObject(gridPosition);
    }
}