using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private GameObject _cellCursor;
    [SerializeField] private Sprite _cursorSprite;
    [SerializeField] private Sprite _closedThrashCanSprite;
    [SerializeField] private Sprite _openedThrashCanSprite;

    private GameObject _gameObjectPreview = null;
    private SpriteRenderer _cellCursorSpriteRenderer;
    private bool isActive = false;

    private void Start()
    {
        _cellCursor.SetActive(false);
        _cellCursorSpriteRenderer = _cellCursor.GetComponentInChildren<SpriteRenderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        isActive = true;
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

    public void UpdatePreviewPosition(Vector3 position)
    {
        _gameObjectPreview.transform.position = position;
    }

    public void StopShowingPreview()
    {
        isActive = false;
        if (_gameObjectPreview != null)
        {
            Destroy(_gameObjectPreview);
            _gameObjectPreview = null;
        }
        _cellCursor.SetActive(false);
        _cellCursor.transform.localScale = new Vector3(1, 1, 1);
        _cellCursorSpriteRenderer.color = Color.white;
    }

    public void ChangeCursorsColor(bool validity)
    {
        Color color = validity ? Color.green : Color.red;
        _cellCursorSpriteRenderer.color = color;
    }

    public void StartUnitMovementPreview()
    {
        isActive = true;
        _cellCursor.SetActive(true);
    }

    void Update()
    {
        if (!isActive)
            return ;
        _cellCursor.transform.position = GameManager.Instance.inputManager.GetMouseGridPosition();
    }

}