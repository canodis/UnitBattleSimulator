using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private TMP_Text _selectedObjectText;
    [SerializeField] private Image _selectedObjectImage;
    [SerializeField] private GameObject _productionPanel;

    public void ShowInfo(InfoPanelModel objectData, bool isProducer)
    {
        _infoPanel.SetActive(true);
        _selectedObjectText.text = objectData.Name;
        _selectedObjectImage.sprite = objectData.Sprite;
        _productionPanel.SetActive(isProducer);
    }

    public void HideInfo()
    {
        _infoPanel.SetActive(false);
    }
}