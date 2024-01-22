using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private GameObject _cellCursor;
    [SerializeField] private Sprite _cursorSprite;
    [SerializeField] private Sprite _closedThrashCanSprite;
    [SerializeField] private Sprite _openedThrashCanSprite;

    private GameObject _gameObjectPreview;
    private SpriteRenderer _cellCursorSpriteRenderer;
    private int _sortingOrder;

    private void Start()
    {
        _cellCursor.SetActive(false);
        _cellCursorSpriteRenderer = _cellCursor.GetComponentInChildren<SpriteRenderer>();
        _sortingOrder = _cellCursorSpriteRenderer.sortingOrder;
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
        _cellCursorSpriteRenderer.sprite = _cursorSprite;
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
        _gameObjectPreview.transform.position = position;
        _cellCursor.transform.position = position;
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color color = validity ? Color.green : Color.red;
        _cellCursorSpriteRenderer.color = color;
    }

    public void StopShowingPreview()
    {
        Destroy(_gameObjectPreview);
        _cellCursor.SetActive(false);
        _cellCursor.transform.localScale = new Vector3(1, 1, 1);
        _cellCursorSpriteRenderer.color = Color.white;
    }

    public void StartShowingDestroyPreview(Vector3 position)
    {
        _cellCursorSpriteRenderer.sprite = _closedThrashCanSprite;
        _cellCursorSpriteRenderer.sortingOrder = 10;
        _cellCursor.transform.position = position;
        _cellCursor.SetActive(true);
    }

    public void UpdateDestroyPreview(Vector3 position, bool validity)
    {
        _cellCursor.transform.position = position;
        _cellCursorSpriteRenderer.sprite = validity ? _openedThrashCanSprite : _closedThrashCanSprite;
    }

    public void StopShowingDestroyPreview()
    {
        _cellCursor.SetActive(false);
        _cellCursorSpriteRenderer.sortingOrder = _sortingOrder;
    }
}