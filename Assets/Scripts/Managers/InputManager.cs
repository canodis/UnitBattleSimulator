using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages mouse input events and provides mouse position information.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Grid _grid;

    public event Action OnRightClicked;
    public event Action OnLeftClicked;
    public event Action OnExit;
    public event Action OnMiddleClicked;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (IsMouseOverUI())
            return;
        if (Input.GetMouseButtonDown(0))
            OnLeftClicked?.Invoke();
        if (Input.GetMouseButtonDown(1))
        {
            OnRightClicked?.Invoke();
            OnExit?.Invoke();
        }
        if (Input.GetMouseButton(2))
            OnMiddleClicked?.Invoke();
    }

    /// <summary>
    /// Checks if the mouse is currently over a UI element.
    /// </summary>
    public bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector2 GetMousePosition()
    {
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3Int GetMouseGridPosition()
    {
        Vector2 mousePosition = GetMousePosition();
        return _grid.WorldToCell(mousePosition);
    }
}
