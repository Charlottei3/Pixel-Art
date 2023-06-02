using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBanner : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _label;

    public string Text
    {
        get { return _label.text; }
        set { _label.text = value; }
    }

    public Sprite Image
    {
        get { return _image.sprite; }
        set { _image.sprite = value; }
    }

}

