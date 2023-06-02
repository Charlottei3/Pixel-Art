using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BannerScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float _minDeltaDrag = 0.1f;
    [SerializeField] private float _snapDuration = 0.3f;

    public UnityEvent<int, int> OnChangeStarted;
    public UnityEvent<int, int> OnChangeEnded;

    public ScrollRect _scrollRect;
    private int _currentPage;
    private int _targetPage;
    private float _startNormalizedPosition;

    private float _targetNormalizedPosition;
    private float _moveSpeed;

    private void Update()
    {
        if (_moveSpeed == 0) return;
        var position = _scrollRect.horizontalNormalizedPosition;
        position += _moveSpeed * Time.deltaTime;

        var min = _moveSpeed > 0 ? position : _targetNormalizedPosition;
        var max = _moveSpeed > 0 ? _targetNormalizedPosition : position;
        position = Mathf.Clamp(position, min, max);

        _scrollRect.horizontalNormalizedPosition = position;

        if (Mathf.Abs(_targetNormalizedPosition - position) < Mathf.Epsilon)
        {
            _moveSpeed = 0;
            OnChangeEnded?.Invoke(_currentPage, _targetPage);
            _currentPage = _targetPage;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startNormalizedPosition = _scrollRect.horizontalNormalizedPosition;
        if (_currentPage != _targetPage)
        {
            OnChangeEnded?.Invoke(_currentPage, _targetPage);
            _currentPage = _targetPage;
        }
        _moveSpeed = 0;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var pageWidth = 1f / GetPageCount();
        var pagePosition = _currentPage * pageWidth;

        var currentPosition = _scrollRect.horizontalNormalizedPosition;
        var minPageDrag = pageWidth * _minDeltaDrag;

        var isForwardDrag = _scrollRect.horizontalNormalizedPosition > _startNormalizedPosition;
        var switchPageBreakpoint = pagePosition + (isForwardDrag ? minPageDrag : -minPageDrag);

        var page = _currentPage;
        if (isForwardDrag && currentPosition > switchPageBreakpoint)
        {
            page++;
        }
        else if (!isForwardDrag && currentPosition < switchPageBreakpoint)
        {
            page--;
        }
        ScrollPage(page);
    }

    private void ScrollPage(int page)
    {
        _targetNormalizedPosition = page * (1f / GetPageCount());
        _moveSpeed = (_targetNormalizedPosition - _scrollRect.horizontalNormalizedPosition) / _snapDuration;
        _targetPage = page;

        if (_targetPage != _currentPage)
        {
            OnChangeStarted?.Invoke(_currentPage, _targetPage);
        }
    }
    private int GetPageCount()
    {
        var contentWidth = _scrollRect.content.rect.width;
        var rectWidth = ((RectTransform)_scrollRect.transform).rect.size.x;
        return Mathf.RoundToInt(contentWidth / rectWidth) - 1;
    }
}
