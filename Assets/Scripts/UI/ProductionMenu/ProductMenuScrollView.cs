using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductMenuScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private float _outOfBoundsThreashold = 40.0f;
    [SerializeField] private float _childWidth = 200.0f;
    [SerializeField] private float _childHeight = 200.0f;
    [SerializeField] private float _spacing = 30.0f;
    [SerializeField] private int _lineCount;

    private Vector2 _lastDragPosition;
    private bool _positiveDrag = true;
    private int _childCount = 0;
    private float _height = 0.0f;

    /// <summary>
    /// In order for the algorithm to work, it needs to be scrolled manually, so scrolling is done with coroutine.
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        CreateItems();
        _childCount = _scrollRect.content.childCount;
        _height = _transform.GetComponent<RectTransform>().rect.height;

        _scrollRect.content.localPosition = Vector3.up * _height * 3.0f;

        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 20; i++)
        {
            _positiveDrag = true;
            HandleScroll(Vector2.zero);
            _scrollRect.content.transform.Translate(Vector2.up * 10 * Time.deltaTime);
            yield return null;
        }
    }

    void OnEnable()
    {
        _scrollRect.onValueChanged.AddListener(HandleScroll);
    }

    void OnDisable()
    {
        _scrollRect.onValueChanged.RemoveListener(HandleScroll);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _positiveDrag = eventData.position.y > _lastDragPosition.y;
        _lastDragPosition = eventData.position;

    }

    /// <summary>
    /// Determines if the given Transform item is out of bounds vertically.
    /// </summary>
    /// <param name="item">The Transform item to check.</param>
    /// <returns>True if out of bounds, false otherwise.</returns>
    private bool ReachedThreshold(Transform item)
    {
        float positiveYThreshold = _transform.position.y + _height * 0.5f + _outOfBoundsThreashold;
        float negativeYThreshold = _transform.position.y - _height * 0.5f - _outOfBoundsThreashold;
        return _positiveDrag ? item.position.y - _childHeight * 0.5f > positiveYThreshold
            : item.position.y + _childHeight * 0.5f < negativeYThreshold;
    }

    /// <summary>
    /// Handles the scrolling behavior based on the provided scroll value.
    /// </summary>
    /// <param name="value">The scroll value to determine the scrolling direction.</param>
    private void HandleScroll(Vector2 value)
    {
        // Determine the index of the current item and get its Transform.
        int currentItemIndex = _positiveDrag ? _childCount - 1 : 0;
        Transform currentItem = _scrollRect.content.GetChild(currentItemIndex);

        if (!ReachedThreshold(currentItem))
        {
            return;
        }
        // Gets the transform of the last item according to the scroll direction (index 0 if scrolling down, _childcount - 1 if scrolling up)
        int endItemIndex = _positiveDrag ? 0 : _childCount - 1;
        Transform endItem = _scrollRect.content.GetChild(endItemIndex);
        Vector2 newPosition = endItem.position;

        // Calculate the new position for the current item based on the end item's position, height, and spacing.
        if (_positiveDrag)
        {
            newPosition.y = endItem.position.y - _childHeight * 1.5f - _spacing;
        }
        else
        {
            newPosition.y = endItem.position.y + _childHeight * 1.5f + _spacing;
        }
        // Set the new position for the current item and update its sibling index.
        currentItem.position = newPosition;
        currentItem.SetSiblingIndex(endItemIndex);
    }

    private void CreateItems()
    {
        for (int i = 0; i < _lineCount; i++)
        {
            GameObject line = Instantiate(_linePrefab, _content);
            line.GetComponent<RectTransform>().sizeDelta = new Vector2(_childWidth, _childHeight);
            for (int j = 0; j < _items.Length; j++)
            {
                Instantiate(_items[j], line.transform);
            }
        }
    }
}
