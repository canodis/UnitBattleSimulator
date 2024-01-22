public class PowerPlant : Building, ISelectable
{
    private PowerPlant()
    {
        isProducer = false;
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