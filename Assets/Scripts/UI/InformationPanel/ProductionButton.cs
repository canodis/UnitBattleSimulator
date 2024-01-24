using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionButton : MonoBehaviour
{
    [SerializeField] private int _id;
    
    private PlacementManager _placementManager;

    void Start()
    {
        _placementManager = GameObject.FindGameObjectWithTag("PlacementManager").GetComponent<PlacementManager>();
        InitButtonListener();
    }

    private void InitButtonListener()
    {
        GetComponent<Button>().onClick.AddListener(() => _placementManager.StartPlacement(_id));
    }
}
