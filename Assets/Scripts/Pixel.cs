using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    private bool isClick = false;
    public int id { get; set; }
    public Color _colorTrue { get; set; }
    public Color _colorWrongMax { get; set; }
    public Color _colorWrongMin { get; set; }


    [SerializeField] SpriteRenderer _colorRen;
    [SerializeField] SpriteRenderer _lineRen;
    [SerializeField] public SpriteRenderer _highlight;
    [SerializeField] TextMeshPro _text;
    private bool isFlilled = false;


    public bool isFilledInTrue
    {
        get
        {
            if (_colorRen.color == _colorTrue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void Start()
    {
        _text.text = id.ToString();
        _colorRen.color = Color.Lerp(Color.white * _colorTrue.grayscale * 2, Color.white, 0.3f);
        _text.color = new Color(0, 0, 0, Mathf.Clamp01(0));
        GameManager.Instance.slider.onValueChanged.AddListener(OnSliderValueChanged);


    }
    private void Update()
    {
    }
    private void OnSliderValueChanged(float value)
    {
        if (!isFilledInTrue)
        {
            if (!isFlilled)//chưa tô
            {
                _colorRen.color = Color.Lerp(Color.white * _colorTrue.grayscale * 2, Color.white, Mathf.Max(0.3f, value));
            }
            else
                _colorRen.color = Color.Lerp(_colorWrongMin, _colorWrongMax, value);

            _text.color = new Color(0, 0, 0, Mathf.Clamp01(value));
        }
    }

    public void Fill()
    {
        isFlilled = true;
        _colorRen.color = _colorTrue;
        _lineRen.color = _colorTrue;
        _text.text = "";
        _highlight.enabled = false;
        //Update thanh slide
        GameManager.Instance.allButon[GameManager.Instance.idNow - 1].slider.value = UpdateSlide(GameManager.Instance.idNow);
        //hoàn thành xong màu chưa
        if (CheckCompleteColorNow(GameManager.Instance.idNow))
        {
            GameManager.Instance.allButon[GameManager.Instance.idNow - 1]._imageCompelete.enabled = true;
        }
        if (CheckCompleteAllColor()) { Debug.Log("Wingame"); }
    }
    private float UpdateSlide(int id)
    {

        float a = 0;

        foreach (Pixel px in GameManager.Instance._allPixelGroups[id])
        {
            if (px.isFilledInTrue)
            {
                a++;
            }

        }
        return (a / GameManager.Instance._allPixelGroups[id].Count);
    }
    private bool CheckCompleteColorNow(int id)
    {
        foreach (Pixel px in GameManager.Instance._allPixelGroups[id])
        {
            if (!px.isFilledInTrue)
                return false;
        }
        return true;
    }
    private bool CheckCompleteAllColor()
    {
        foreach (ColorRenPixel bt in GameManager.Instance.allButon)
        {
            if (bt._imageCompelete.enabled == false)
            {
                if (bt == null) continue;
                return false;

            }
        }
        return true;
    }

    private bool CheckColor()
    {
        if (isFilledInTrue) return false;
        if (_colorTrue == GameManager.Instance.colorNow) return true;
        else return false;
    }

    public void FillWrong()
    {
        if (!isFilledInTrue)
        {
            isFlilled = true;
            Color _color = Color.Lerp(GameManager.Instance.colorNow, _colorTrue, 0.35f);
            _colorWrongMax = Color.Lerp(_color, Color.white, 0.7f);
            _colorWrongMin = Color.Lerp(_color, Color.white, 0.4f);
            Color _colorWrongNow = Color.Lerp(_colorWrongMin, _colorWrongMax, GameManager.Instance.slider.value);

            _colorRen.color = _colorWrongNow;
        }
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.isChosseFirstColor)
        {
            GameManager.Instance.isFirstClick = CheckColor();
            GameManager.Instance.isClick = true;
            if (GameManager.Instance.isFirstClick) Fill();
        }
    }
    private void OnMouseUp()
    {
        GameManager.Instance.isClick = false;
        GameManager.Instance.isFirstClick = false;
    }
    private void OnMouseOver()
    {
        if (GameManager.Instance.isClick && GameManager.Instance.isFirstClick && CheckColor()) Fill();
        else if (GameManager.Instance.isClick && GameManager.Instance.isFirstClick && !CheckColor()) FillWrong();
    }
}
