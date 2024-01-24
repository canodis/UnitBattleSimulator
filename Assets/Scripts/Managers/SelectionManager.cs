using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    [SerializeField] private GameObject _selectedObject;

    private void Start()
    {
        _inputManager.OnLeftClicked += SelectObject;
        _inputManager.OnRightClicked += MoveObject;
    }

    private void SelectObject()
    {
        DeselectObject();
        Vector3Int gridPositionInt = _inputManager.GetMouseGridPosition();

        int index = GameManager.Instance.gridData.GetObjectIndex(gridPositionInt);
        if (index == -1)
            return;

        GameObject obj = GameManager.Instance.objectManager.GetObject(index);
        SelectIfIsSelectable(obj);
    }

    private void SelectIfIsSelectable(GameObject obj)
    {
        ISelectable selectableObject = obj.GetComponent<ISelectable>();
        if (selectableObject == null)
        {
            Debug.Log("Object is not selectable");
            return;
        }
        _selectedObject = obj;
        if (_selectedObject.GetComponent<IMovable>() != null)
            _previewSystem.StartUnitMovementPreview();
        selectableObject.OnSelect();
    }

    private void DeselectObject()
    {
        if (_selectedObject == null)
            return;
        _selectedObject.GetComponent<ISelectable>().OnDeselect();
        _selectedObject = null;
        _previewSystem.StopShowingPreview();
    }

    private void MoveObject()
    {
        if (_selectedObject == null)
            return;
        Vector3Int targetPosition = _inputManager.GetMouseGridPosition();
        MoveIfIsMoveable(targetPosition);
    }

    private void MoveIfIsMoveable(Vector3Int targetPosition)
    {
        IMovable movableObject = _selectedObject.GetComponent<IMovable>();
        if (movableObject == null)
        {
            Debug.Log("Object is not moveable");
            return;
        }
        movableObject.Move(targetPosition);
    }
}