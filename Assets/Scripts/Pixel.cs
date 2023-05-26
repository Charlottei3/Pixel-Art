using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    Slider _slider;
    public int id { get; set; }
    public Color _color { get; set; }

    SpriteRenderer _colorRen;
    SpriteRenderer _lineRen;
    TextMeshPro _text;

    void Awake()
    {
        _lineRen = transform.Find("Line").GetComponent<SpriteRenderer>();
        _colorRen = transform.Find("Color").GetComponent<SpriteRenderer>();
        _text = transform.Find("Text").GetComponent<TextMeshPro>();
        _slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    }
    private void Start()
    {
        _text.text = id.ToString();
        _slider.onValueChanged.AddListener(OnSliderValueChanged);

    }
    private void OnSliderValueChanged(float value)
    {
        _colorRen.color = Color.Lerp(new Color(_color.grayscale, _color.grayscale, _color.grayscale), Color.white, value);
    }
}
