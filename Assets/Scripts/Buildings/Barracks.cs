using UnityEngine;

public class Barracks : Building, ISelectable
{
    [SerializeField] private Unit[] units;
    private Barracks()
    {
        isProducer = true;
    }

    public void OnDeselect()
    {
        HideInfo();
    }

    public void OnSelect()
    {
        ShowInfo();
    }

    protected override void ShowInfo()
    {
        base.ShowInfo();
        _infoPanelController.ShowProductPanel(units);
    }

    // protected override void OnMouseUp()
    // {
    //     base.OnMouseUp();
    //     _infoPanelController.ShowProductPanel(units);
    // }

    // private void ProduceUnit(int id)
    // {
    //     GameManager.Instance.placementManager.PlaceObjectAutomatically(gridPosition, id);
    // }


}