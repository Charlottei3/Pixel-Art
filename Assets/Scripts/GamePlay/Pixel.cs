﻿using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    public int x;
    public int y;
    private bool isClick = false;
    public int id { get; set; }
    public Color _colorTrue { get; set; }
    public Color _colorWrongMax { get; set; }
    public Color _colorWrongMin { get; set; }


    [SerializeField] SpriteRenderer _colorRen;
    [SerializeField] SpriteRenderer _lineRen;
    [SerializeField] public SpriteRenderer _highlight;
    [SerializeField] TextMeshPro _text;
    public bool isFlilled = false;

    public bool isFilledInTrue = false;

    private void Start()
    {
        if (!isFilledInTrue)
        {
            _text.text = id.ToString();
            _text.color = new Color(0, 0, 0, 0);
            _colorRen.color = Color.Lerp(Color.white * _colorTrue.grayscale * 2, Color.white, 0f);
        }
        GameManager.Instance.slider.onValueChanged.AddListener(OnSliderValueChanged);

    }
    private void Update()
    {
    }
    private void OnSliderValueChanged(float value)
    {
        if (!isFilledInTrue)//tô sai
        {
            if (!isFlilled)//chưa tô
            {
                _colorRen.color = Color.Lerp(Color.white * _colorTrue.grayscale * 2, Color.white, Mathf.Clamp01(value * 1.2f));
            }
            else //to roi nhung sai
                _colorRen.color = Color.Lerp(_colorWrongMin, _colorWrongMax, value);

            _text.color = new Color(0, 0, 0, Mathf.Clamp01(value * 2));
        }
    }
    public void FillOnLoad()
    {
        GameManager.Instance.idNow = id;
        isFilledInTrue = true;
        isFlilled = true;
        _colorRen.color = _colorTrue;
        _lineRen.color = _colorTrue;

        _text.text = "";
        _highlight.enabled = false;

        GameManager.Instance.allButon[GameManager.Instance.idNow - 1].slider.value = UpdateSlide(GameManager.Instance.idNow);
        //hoàn thành xong màu chưa
        if (CheckCompleteColorNow(GameManager.Instance.idNow))
        {
            GameManager.Instance.allButon[GameManager.Instance.idNow - 1]._imageCompelete.enabled = true;
        }
        if (CheckCompleteAllColor())
        {
            GameManager.Instance.Menu.SetActive(true);
            GameManager.Instance.AllBook.SetActive(true);
            GameManager.Instance.Clear();

            GameManager.Instance.btnOutGame.gameObject.SetActive(false);
            GameManager.Instance.nowBtnLoadGame.UpdatePicture();
            /*GameManager.Instance.LoadPicture();*/
        }
    }
    public void Fill()
    {
        if (!Data.gameData.matrix.ContainsKey(GameManager.Instance.nowKey))
        {
            Data.AddData(GameManager.Instance.nowKey, GameManager.Instance.matrix);
            GameManager.Instance.allListDrawed.AddBtnLoad(GameManager.Instance.nowBtnLoadGame, GameManager.Instance.allListDrawed.saveDrawed);//them vao listdraw btn
        }
        isFilledInTrue = true;
        isFlilled = true;
        _colorRen.color = _colorTrue;
        _lineRen.color = _colorTrue;

        _text.text = "";
        _highlight.enabled = false;
        Data.ClickTrue(GameManager.Instance.nowKey, x, y);
        //Update thanh slide
        GameManager.Instance.allButon[GameManager.Instance.idNow - 1].slider.value = UpdateSlide(GameManager.Instance.idNow);
        //hoàn thành xong màu chưa
        if (CheckCompleteColorNow(GameManager.Instance.idNow))
        {
            GameManager.Instance.allButon[GameManager.Instance.idNow - 1]._imageCompelete.enabled = true;
        }
        if (CheckCompleteAllColor())
        {
            GameManager.Instance.Menu.SetActive(true);
            GameManager.Instance.AllBook.SetActive(true);
            GameManager.Instance.Clear();

            GameManager.Instance.btnOutGame.gameObject.SetActive(false);
            GameManager.Instance.nowBtnLoadGame.UpdatePicture();
            /*GameManager.Instance.LoadPicture();*/

            GameManager.Instance.nowBtnLoadGame.btn.enabled = false;
            GameManager.Instance.nowBtnLoadGame.complete.SetActive(true);
            GameManager.Instance.nowBtnLoadGame.isComplete = true;
            Data.gameData.isComplete[GameManager.Instance.nowKey] = true;


            if (!GameManager.Instance.nowBtnLoadGame.isInDrawed)//ko phai trong drawed
            {
                Transform find = GameManager.Instance.allListDrawed.saveDrawed.Find(GameManager.Instance.nowKey);
                if (find != null) { find.GetComponent<Btn_loadGame1>().UpdatePicture(); }

                GameManager.Instance.allListDrawed.RemoveToCompelete(find.GetComponent<Btn_loadGame1>());

            }
            if (GameManager.Instance.nowBtnLoadGame.isInDrawed)//neu ben trong toi da to thi tim ban goc de update theo
            {
                GameManager.Instance.allListDrawed.DictionaryCopyOnDrawed[GameManager.Instance.nowBtnLoadGame].UpdatePicture();//tim ben ngoai dra

                GameManager.Instance.allListDrawed.RemoveToCompelete(GameManager.Instance.nowBtnLoadGame);
            }

            Data.Save();
        }
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
