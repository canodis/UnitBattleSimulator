using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : GridObject
{
    protected bool isProducer;
    protected InfoPanelController _infoPanelController;

    private void Start()
    {
        _infoPanelController = GameObject.FindWithTag("InfoPanelController").GetComponent<InfoPanelController>();
    }

    protected virtual void OnMouseUp()
    {
        if (objectData == null || InputManager.Instance.IsMouseOverUI())
            return;
        _infoPanelController.UpdateInfoPanel(objectData.Name, objectData.Sprite);
    }
}