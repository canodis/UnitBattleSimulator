using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask placementLayerMask;
    public event Action OnClicked, OnExit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector2 GetSelectedGridPosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
