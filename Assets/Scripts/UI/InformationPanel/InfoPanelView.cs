using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private TMP_Text _selectedObjectText;
    [SerializeField] private Image _selectedObjectImage;
    [SerializeField] private GameObject _productionPanel;
    [SerializeField] private GameObject _productionButtonPrefab;

    void Start()
    {
        _infoPanel.SetActive(false);
        _productionPanel.SetActive(false);
    }

    public void ShowInfo(InfoPanelModel objectData)
    {
        _infoPanel.SetActive(true);
        _productionPanel.SetActive(false);
        _selectedObjectText.text = objectData.Name;
        _selectedObjectImage.sprite = objectData.Sprite;
    }

    public void ShowProductPanel(Unit[] units)
    {
        foreach (Transform child in _productionPanel.transform)
        {
            Destroy(child.gameObject);
        }
        _productionPanel.SetActive(true);
        // foreach (Unit unit in units)
        // {
        //     GameObject button = Instantiate(_productionButtonPrefab, _productionPanel.transform);
        //     button.GetComponentInChildren<TMP_Text>().text = unit.GetObjectData().Name;
        // //     button.GetComponent<Button>().onClick.AddListener(() =>
        // //         GameManager.Instance.placementManager.PlaceObjectAutomatically(unit.GetObjectData().Id));
        // }

    }

    public void HideInfo()
    {
        _infoPanel.SetActive(false);
    }
}