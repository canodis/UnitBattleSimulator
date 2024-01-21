using UnityEngine;

public class Barracks : Building
{
    [SerializeField] private Unit[] units;
    private Barracks()
    {
        isProducer = true;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        _infoPanelController.ShowProductPanel(units);
    }
}