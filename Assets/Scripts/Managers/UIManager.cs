using UnityEngine;

public class UIManager : MonoBehaviour
{
    static public UIManager Instance { get; private set; }
    [SerializeField] private GameObject _buildingsMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buildingsMenu.SetActive(true);
    }

    public void ShowBuildingsMenu()
    {
        _buildingsMenu.SetActive(true);
    }

    public void HideBuildingsMenu()
    {
        _buildingsMenu.SetActive(false);
    }
}
