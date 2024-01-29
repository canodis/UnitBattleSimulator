using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private TMP_Text _selectedObjectText;
    [SerializeField] private Image _selectedObjectImage;
    [SerializeField] private GameObject _productionPanel;
    [SerializeField] private GameObject _productionPanelButtonsParent;
    [SerializeField] private GameObject _productionButtonPrefab;

    private ObjectPool<GameObject> buttonsPool;

    void Start()
    {
        _infoPanel.SetActive(false);
        _productionPanel.SetActive(false);
        buttonsPool = new ObjectPool<GameObject>(() =>
            Instantiate(_productionButtonPrefab, _productionPanelButtonsParent.transform),
            (button) => { button.SetActive(true); },
            (button) => button.SetActive(false),
             9);
    }

    public void ShowInfo(InfoPanelModel objectData)
    {
        _infoPanel.SetActive(true);
        _productionPanel.SetActive(false);
        _selectedObjectText.text = objectData.Name;
        _selectedObjectImage.sprite = objectData.Sprite;
    }

    /// <summary>
    /// Displays the product panel with buttons for each specified unit, allowing selection and unit spawning.
    /// </summary>
    /// <param name="units">Array of units to display in the panel.</param>
    /// <param name="barracks">The barracks associated with the product panel.</param>
    public void ShowProductPanel(Soldier[] soldiers, Barracks barracks)
    {
        foreach (Transform child in _productionPanelButtonsParent.transform)
        {
            buttonsPool.Return(child.gameObject);
        }
        _productionPanel.SetActive(true);
        foreach (Unit unit in soldiers)
        {
            ObjectData objectData = GameManager.Instance.FindObjectDataWithId(unit.Id);
            GameObject button = buttonsPool.Get();
            button.GetComponentInChildren<TMP_Text>().text = objectData.Name;
            button.GetComponent<Image>().sprite = objectData.Sprite;
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                barracks.SpawnUnit(unit.Id);
            });
        }
    }

    public void HideInfo()
    {
        _infoPanel.SetActive(false);
    }
}