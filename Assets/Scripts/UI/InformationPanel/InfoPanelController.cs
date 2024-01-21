using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField] private InfoPanelView _infoPanelView;
    private InfoPanelModel _model;

    private void Start()
    {
        _model = new InfoPanelModel("", null);
    }

    public void UpdateInfoPanel(string name, Sprite sprite)
    {
        _model.Name = name;
        _model.Sprite = sprite;
        _infoPanelView.ShowInfo(_model);
        InputManager.Instance.OnClicked += HideInfoPanel;
    }

    public void ShowProductPanel(Unit[] units)
    {
        _infoPanelView.ShowProductPanel(units);
    }

    public void HideInfoPanel()
    {
        _infoPanelView.HideInfo();
        InputManager.Instance.OnClicked -= HideInfoPanel;
    }
}

