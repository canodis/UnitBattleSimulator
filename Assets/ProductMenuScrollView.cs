using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

public class ProductMenuScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _content;
    [SerializeField] private float _outOfBoundsThreashold = 40.0f;
    [SerializeField] private float _childWidth = 200.0f;
    [SerializeField] private float _childHeight = 200.0f;
    [SerializeField] private float _spacing = 30.0f;
    [SerializeField] private int _lineCount;

    private Vector2 _lastDragPosition;
    private bool _positiveDrag = true;
    private int _childCount = 0;
    private float _heigth = 0.0f;

    IEnumerator Start()
    {
        CreateItems();
        _childCount = _scrollRect.content.childCount;
        _heigth = _transform.GetComponent<RectTransform>().rect.height;

        _scrollRect.content.localPosition = Vector3.up * _heigth * 3.0f;

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

    private bool ReachedThreshold(Transform item)
    {
        float positiveYThreshold = _transform.position.y + _heigth * 0.5f + _outOfBoundsThreashold;
        float negativeYThreshold = _transform.position.y - _heigth * 0.5f - _outOfBoundsThreashold;
        return _positiveDrag ? item.position.y - _childHeight * 0.5f > positiveYThreshold
            : item.position.y + _childHeight * 0.5f < negativeYThreshold;
    }

    private void HandleScroll(Vector2 value)
    {
        int currentItemIndex = _positiveDrag ? _childCount - 1 : 0;
        var currentItem = _scrollRect.content.GetChild(currentItemIndex);

        if (!ReachedThreshold(currentItem))
        {
            return;
        }
        int endItemIndex = _positiveDrag ? 0 : _childCount - 1;
        Transform endItem = _scrollRect.content.GetChild(endItemIndex);
        Vector2 newPosition = endItem.position;

        if (_positiveDrag)
        {
            newPosition.y = endItem.position.y - _childHeight * 1.5f + _spacing;
        }
        else
        {
            newPosition.y = endItem.position.y + _childHeight * 1.5f - _spacing;
        }

        currentItem.position = newPosition;
        currentItem.SetSiblingIndex(endItemIndex);
    }

    private void CreateItems()
    {
        for (int i = 0; i < _lineCount; i++)
        {
            GameObject line = Instantiate(new GameObject($"Line {i}"), _content);
            line.AddComponent<RectTransform>().sizeDelta = new Vector2(_childWidth, _childHeight);
            line.AddComponent<HorizontalLayoutGroup>();
            for (int j = 0; j < _items.Length; j++)
            {
                Instantiate(_items[j], line.transform);
            }
        }
    }
}
