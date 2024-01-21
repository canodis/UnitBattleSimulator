using Unity.VisualScripting;
using UnityEngine;

public class Building : GridObject
{
    protected bool isProducer;
    private InfoPanelController _infoPanelController;

    private void Start()
    {
        _infoPanelController = GameObject.FindWithTag("InfoPanelController").GetComponent<InfoPanelController>();
    }

    protected void OnMouseUp()
    {
        if (objectData == null || InputManager.Instance.IsMouseOverUI())
            return;
        _infoPanelController.UpdateInfoPanel(objectData.Name, objectData.Sprite, isProducer);
    }
}