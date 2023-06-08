using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BannerSlider _slider;
    [SerializeField] private ItemBanner _prefab;
    [SerializeField] private Transform parent;

    [Header("Configuration")]
    [SerializeField] private Banner[] _items;

    private void Start()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            var page = Instantiate(_prefab,parent);
            page.Text = _items[i].Text;
            page.Image = _items[i].Image;

            _slider.AddPage(page.GetComponent<BannerView>());
        }
    }
}

[Serializable]
public class Banner
{
    [SerializeField] private string _text;
    [SerializeField] private Sprite _sprite;

    public string Text { get { return _text; }}
    public Sprite Image { get { return _sprite; }}
}
