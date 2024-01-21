using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    [SerializeField] private Camera _mainCamera;
    public event Action OnClicked;
    public event Action OnExit;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (IsMouseOverUI())
            return;
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector2 GetMousePosition()
    {
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
