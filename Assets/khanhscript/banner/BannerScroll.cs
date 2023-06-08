using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

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


    /*public float scrollSpeed = 10f;
    public float snapThreshold = 0.1f;

    private float[] snapPositions;
    private bool isDragging = false;*/
 
    private void Start()
    {
        
    }

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
        //isDragging = true;
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
        //isDragging = false;
       
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


        /*int pageCount = _scrollRect.content.childCount;
        float _pageWidth = 1f / pageCount;
        snapPositions = new float[pageCount];

        for (int i = 0; i < pageCount; i++)
        {
            snapPositions[i] = i * _pageWidth;
        }
        float closeSnapPosition = float.MaxValue;
        for (int i = 0; i < snapPositions.Length; i++)
        {
            float distance = Mathf.Abs(snapPositions[i] - currentPosition);
            if (distance < closeSnapPosition)
            {
                closeSnapPosition = distance;
                _currentPage = i;
            }

            Debug.Log($"currentPosition: {currentPosition}");
            Debug.Log($"distance: {distance}");
            Debug.Log($"closeSnapPosition: {closeSnapPosition}");
        }

        if (closeSnapPosition >= snapThreshold)
        {
            _currentPage = 0;
        }*/

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

    private IEnumerable ScrollAfterTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            _currentPage++;
            if (_currentPage > _scrollRect.content.childCount)
            {
                _currentPage = 0;
            }
            float normalizedPosition = _currentPage / (float)(_scrollRect.content.childCount - 1);
            _scrollRect.horizontalNormalizedPosition = normalizedPosition;
            Debug.Log(normalizedPosition);
            Debug.Log(_currentPage);
        }
    }
}
