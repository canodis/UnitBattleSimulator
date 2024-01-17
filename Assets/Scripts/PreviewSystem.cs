using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private GameObject cellCursor;
    private GameObject gameObjectPreview;
    private SpriteRenderer cellCursorSpriteRenderer;

    private void Start()
    {
        cellCursor.SetActive(false);
        cellCursorSpriteRenderer = cellCursor.GetComponentInChildren<SpriteRenderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        gameObjectPreview = Instantiate(prefab);
        PrepareCursor(size);
        PreparePreview(gameObjectPreview);
        cellCursor.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellCursor.transform.localScale = new Vector3(size.x, size.y, 1);
        }
    }

    private void PreparePreview(GameObject previewGameObject)
    {
        SpriteRenderer spriteRenderer = previewGameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.7f);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color color = validity ? Color.green : Color.red;
        cellCursorSpriteRenderer.color = color;
    }

    private void MoveCursor(Vector3 position)
    {
        cellCursor.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        gameObjectPreview.transform.position = position;
    }

    public void StopShowingPreview()
    {
        Destroy(gameObjectPreview);
        cellCursor.SetActive(false);
    }
}