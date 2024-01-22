using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private GameObject _selectedObject;

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
        selectableObject.OnSelect();
    }

    private void DeselectObject()
    {
        if (_selectedObject == null)
            return;
        _selectedObject.GetComponent<ISelectable>().OnDeselect();
    }

    private void MoveObject()
    {
        if (_selectedObject == null)
            return;
        Vector3Int targetPosition = _inputManager.GetMouseGridPosition();
        MoveIfIsMoveable(_selectedObject, targetPosition);
    }

    private void MoveIfIsMoveable(GameObject obj, Vector3Int targetPosition)
    {
        IMovable movableObject = obj.GetComponent<IMovable>();
        if (movableObject == null)
        {
            Debug.Log("Object is not moveable");
            return;
        }
        movableObject.Move(targetPosition);
    }
}