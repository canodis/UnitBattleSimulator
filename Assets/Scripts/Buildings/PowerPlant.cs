public class PowerPlant : Building, ISelectable
{
    private PowerPlant()
    {
    }

    public void OnSelect()
    {
        ShowInfo();
    }

    public void OnDeselect()
    {
        HideInfo();
    }


}