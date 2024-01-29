using UnityEngine;

public abstract class Building : GridObject, IAttackable
{
    protected InfoPanelController infoPanelController;

    protected void Start()
    {
        infoPanelController = GameObject.FindWithTag("InfoPanelController").GetComponent<InfoPanelController>();
    }

    protected virtual void ShowInfo()
    {
        if (objectData == null || InputManager.Instance.IsMouseOverUI())
            return;
        infoPanelController.UpdateInfoPanel(objectData.Name, objectData.Sprite);
    }

    protected virtual void HideInfo()
    {
        infoPanelController.HideInfoPanel();
    }

    public void DestroySelf()
    {
        GameManager.Instance.gridData.DestroyObject(gridPosition, objectData.Size);
        GameManager.Instance.objectManager.DestroyObject(index);
        infoPanelController.HideInfoPanel();
    }
}