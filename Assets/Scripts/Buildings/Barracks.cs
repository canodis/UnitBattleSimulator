using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building, ISelectable, IMovable
{
    [SerializeField] private Unit[] units;
    private SpawnLocationController _spawnLocationPreview;

    new void Start()
    {
        base.Start();
        _spawnLocationPreview = GetComponentInChildren<SpawnLocationController>();
        _spawnLocationPreview.SetNeighbourCells(_neighbourCells);
        _spawnLocationPreview.gameObject.SetActive(false);
    }

    public void OnDeselect()
    {
        HideInfo();
        _spawnLocationPreview.gameObject.SetActive(false);
        _spawnLocationPreview.OnDeselect();
    }

    public void OnSelect()
    {
        ShowInfo();
        _spawnLocationPreview.gameObject.SetActive(true);
        _spawnLocationPreview.OnSelect();
    }

    protected override void ShowInfo()
    {
        base.ShowInfo();
        _infoPanelController.ShowProductPanel(units, this);
    }

    public void SpawnUnit(int Id)
    {
        Vector3Int _unitSpawnPosition = _spawnLocationPreview.GetLocation();
        GameManager.Instance.placementManager.PlaceObjectImmediately(_unitSpawnPosition, Id);
        _spawnLocationPreview.GoNextLocation();
    }

    public void Move(Vector3Int targetPosition)
    {
        _spawnLocationPreview.Move(targetPosition);
    }
}