using UnityEngine;

public class DestroyState : IPlacementState
{
    private ObjectManager _objectManager;
    private PreviewSystem _previewSystem;

    public DestroyState(PreviewSystem previewSystem,ObjectManager objectManager)
    {
        this._objectManager = objectManager;
        this._previewSystem = previewSystem;

        UIManager.Instance.HideBuildingsMenu();
        _previewSystem.StartShowingDestroyPreview(Vector3.zero);
    }

    public void EndState()
    {
        _previewSystem.StopShowingDestroyPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        // _objectManager.DestroyObject(gridPosition);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        _previewSystem.UpdateDestroyPreview(gridPosition, true);
    }
}