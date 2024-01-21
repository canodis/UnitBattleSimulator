using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private GameObject _cellCursor;
    private GameObject _gameObjectPreview;
    private SpriteRenderer _cellCursorSpriteRenderer;

    private void Start()
    {
        _cellCursor.SetActive(false);
        _cellCursorSpriteRenderer = _cellCursor.GetComponentInChildren<SpriteRenderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        _gameObjectPreview = Instantiate(prefab);
        PrepareCursor(size);
        PreparePreview(_gameObjectPreview);
        _cellCursor.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            _cellCursor.transform.localScale = new Vector3(size.x, size.y, 1);
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
        _cellCursorSpriteRenderer.color = color;
    }

    private void MoveCursor(Vector3 position)
    {
        _cellCursor.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        _gameObjectPreview.transform.position = position;
    }

    public void StopShowingPreview()
    {
        Destroy(_gameObjectPreview);
        _cellCursor.SetActive(false);
    }
}