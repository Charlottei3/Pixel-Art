using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BannerContainer : MonoBehaviour
{
    [SerializeField] private BannerView banView;

    public void AssignContent(RectTransform content)
    {
        if (content == null)
        {
            var contentObject = new GameObject("Content", typeof(RectTransform), typeof(BannerView));

            content = contentObject.GetComponent<RectTransform>();
        }

        content.SetParent(transform);

        content.anchorMin = Vector2.zero;
        content.anchorMax = Vector2.one;
        content.offsetMin = Vector2.zero;
        content.offsetMax = Vector2.zero;

        content.anchoredPosition = Vector2.zero;
        content.localScale = Vector3.one;

        banView = GetComponent<BannerView>();

#if UNITY_editor
         EditorUtility.SetDirty(this);
#endif
    }

    public void ChangingToActiveState()
    {
        banView.ChangingToActiveState();
    }
    public void ChangingInactiveState()
    {
        banView.ChangingInactiveState();
    }

    public void ChangingActiveState(bool active)
    {
        banView.ChangingActiveState(active);
    }
}
