public class PowerPlant : Building, ISelectable
{
    public void OnSelect()
    {
        ShowInfo();
    }

    public void OnDeselect()
    {
        HideInfo();
    }
}