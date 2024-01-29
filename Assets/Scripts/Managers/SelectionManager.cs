using UnityEngine;

/// <summary>
/// Manages object selection and movement based on user input.
/// </summary>
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PreviewSystem _previewSystem;
    private GameObject _selectedObject;

    private void Start()
    {
        _inputManager.OnLeftClicked += SelectObject;
        _inputManager.OnRightClicked += MoveObject;
    }

    /// <summary>
    /// Handles the selection of an object when the left mouse button is clicked.
    /// </summary>
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

    /// <summary>
    /// Checks if the object is selectable and performs the selection if possible.
    /// </summary>
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

    /// <summary>
    /// Deselects the currently selected object.
    /// </summary>
    private void DeselectObject()
    {
        if (_selectedObject == null)
            return;
        _selectedObject.GetComponent<ISelectable>().OnDeselect();
        _selectedObject = null;
        _previewSystem.StopShowingPreview();
    }

    /// <summary>
    /// Handles the movement of the selected object when the right mouse button is clicked.
    /// </summary>
    private void MoveObject()
    {
        if (_selectedObject == null)
            return;
        Vector3Int targetPosition = _inputManager.GetMouseGridPosition();
        MoveIfIsMoveable(targetPosition);
    }

    /// <summary>
    /// Checks if the selected object is movable and performs the movement if possible.
    /// </summary>
    private void MoveIfIsMoveable(Vector3Int targetPosition)
    {
        IMovable movableObject = _selectedObject.GetComponent<IMovable>();
        if (movableObject == null)
        {
            Debug.Log("Object is not moveable");
            return;
        }
        movableObject.Move(targetPosition);
        DeselectObject();
    }
}