using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayItem : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Image _loadImage;
    [SerializeField] TMP_Text _lable;

    public string Text
    {
        get { return _lable.text; }
        set { _lable.text = value; }
    }

    public Sprite Image
    {
        get { return _image.sprite; }
        set { _image.sprite = value; }
    }
    public Sprite LoadImage
    {
        get { return _loadImage.sprite; }
        set { _loadImage.sprite = value; }
    }
}
