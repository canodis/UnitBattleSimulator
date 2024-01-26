using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : GridObject, IAttackable
{
    protected InfoPanelController _infoPanelController;

    protected void Start()
    {
        _infoPanelController = GameObject.FindWithTag("InfoPanelController").GetComponent<InfoPanelController>();
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

    public void DestroySelf()
    {
        GameManager.Instance.gridData.DestroyObject(gridPosition, objectData.Size);
        GameManager.Instance.objectManager.DestroyObject(index);
        _infoPanelController.HideInfoPanel();
    }
}