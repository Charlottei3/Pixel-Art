using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PageDotIndicator : MonoBehaviour
{
    [SerializeField] private PageDot _prefab;
    [SerializeField] private List<PageDot> _dots;

    [Header("Event")]
    public UnityEvent<int> onDotPress;

    public bool IsVisible
    {
        get { return gameObject.activeInHierarchy; }
        set { gameObject.SetActive(value); }
    }

    public void Add()
    {
        PageDot dot = null;

        if(!Application.isPlaying)
        {
            dot = (PageDot)PrefabUtility.InstantiatePrefab(_prefab, transform);
        }

        if(dot == null)
        {
            dot = Instantiate(_prefab, transform);
        }

        dot.Index = _dots.Count;
        dot.ChangeActiveState(_dots.Count == 0);
        _dots.Add(dot);

        if (Application.isPlaying) return;
        EditorUtility.SetDirty(this);
    }

    public void ChangeActiveDot(int fromIndex, int toIndex)
    {
        _dots[fromIndex].ChangeActiveState(false);
        _dots[toIndex].ChangeActiveState(true);
    }

    [CustomEditor(typeof(PageDotIndicator))]
    public class PageDotIndicatorEditor : Editor
    {
        private PageDotIndicator _target;

        private void OnEnable()
        {
            _target = (PageDotIndicator)target;
        }
    }
}
