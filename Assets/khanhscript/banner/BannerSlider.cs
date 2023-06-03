using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BannerSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PageDotIndicator _dotsIndicator;
    [SerializeField] private List<BannerView> _pages;

    [Header("Event")]
    public UnityEvent<BannerView> OnPageChange;

    [SerializeField] private BannerScroll _scroller;
    private Coroutine autoScroll;

    private void Start()
    {
        _scroller.OnChangeStarted.AddListener(PageScroller_PageChangeStarted);
        _scroller.OnChangeEnded.AddListener(PageScroll_PageChangeEnded);

        //StartAutoScroll();
        //StartCoroutine(nameof(ScrollAfterTime));
    }

    private void Update()
    {
        StartCoroutine(nameof(ScrollAfterTime));
    }
    private void StartAutoScroll()
    {
        if (autoScroll != null)
        {
            StopCoroutine(autoScroll);
        }

        autoScroll = StartCoroutine(nameof(ScrollAfterTime));
    }
    public void AddPage(BannerView bannerView)
    {
        if (_pages.Count == 0)
        {
            bannerView.ChangingToActiveState();
            bannerView.ChangingActiveState(true);
        }

        _pages.Add(bannerView);

        for (int i = 0; i < _pages.Count; i++)
        {
            if (i > _pages.Count)
            {
                i = 0;
                bannerView.ChangingToActiveState();
                bannerView.ChangingActiveState(true);
            }
        }

        if (_dotsIndicator != null)
        {
            _dotsIndicator.Add();
            _dotsIndicator.IsVisible = _pages.Count > 1;
        }
#if UNITY_EDITOR
        if (Application.isPlaying) { return; }
        EditorUtility.SetDirty(this);
#endif
    }

    private void PageScroller_PageChangeStarted(int fromIndex, int toIndex)
    {
        _pages[fromIndex].ChangingInactiveState();
        _pages[toIndex].ChangingToActiveState();
    }

    private void PageScroll_PageChangeEnded(int fromIndex, int toIndex)
    {
        _pages[fromIndex].ChangingActiveState(false);
        _pages[toIndex].ChangingActiveState(true);

        _dotsIndicator?.ChangeActiveDot(fromIndex, toIndex);
        OnPageChange?.Invoke(_pages[toIndex]);
    }
    private int currentPageIndex = 0;
    private IEnumerable ScrollAfterTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            currentPageIndex++;
            if (currentPageIndex > _scroller._scrollRect.content.childCount)
            {
                currentPageIndex = 0;
            }
            float normalizedPosition = currentPageIndex / (float)(_scroller._scrollRect.content.childCount - 1);
            _scroller._scrollRect.horizontalNormalizedPosition = normalizedPosition;
            Debug.Log(normalizedPosition);
            Debug.Log(currentPageIndex);
        }
    }
}

