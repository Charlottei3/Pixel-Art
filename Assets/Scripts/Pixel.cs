using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    private bool isClick = false;
    private Slider _slider;
    public int id { get; set; }
    public Color _color { get; set; }

    SpriteRenderer _colorRen;
    SpriteRenderer _lineRen;
    TextMeshPro _text;
    public bool isFilledIn
    {
        get
        {
            if (_colorRen.color == _color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
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

    public void Fill()
    {
        if (!isFilledIn)
        {
            _colorRen.color = _color;
            _lineRen.color = _color;
            _text.text = "";
        }
    }

    public void FillWrong()
    {
        if (!isFilledIn)
        {
            _colorRen.color = new Color(1, 170 / 255f, 170 / 255f, 1);
        }
    }
    private void OnMouseDown()
    {
        GameManager.Instance.isClick = true;
        Fill();

    }
    private void OnMouseUp()
    {
        GameManager.Instance.isClick = false;
    }
    private void OnMouseOver()
    {
        if (GameManager.Instance.isClick) Fill();
    }


}
