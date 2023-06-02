using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDot : MonoBehaviour
{
    [SerializeField] private Color _colorDefault;
    [SerializeField] private Color _colorSelected;

    private Image _image;
    private PageDot _pageDot;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _pageDot = GetComponent<PageDot>();

        _pageDot.OnActiveStateChange.AddListener(PageDot_onChangeActive);
    }
    private void PageDot_onChangeActive(bool active)
    {
        _image.color = active ? _colorSelected : _colorDefault;
    }
}
