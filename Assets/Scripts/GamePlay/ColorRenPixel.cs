using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorRenPixel : Button
{
    public Image _imageCompelete;
    public TMP_Text Id_text;
    public Slider slider;
    public Image Background;
    public int Id;
    public bool Completed;
    bool Selected;
    public Color Color { get; set; }
    private void Start()
    {
        slider.transform.localPosition = new Vector3(0, -Screen.width / 10 * 2 / 3, 0);
        slider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 2 / 15, Screen.width * 2 / 15 / 8);
    }
    public Button getButon()
    {
        return this.GetComponent<Button>();
    }
    public void SetData(int id, Color color)
    {
        Id = id;
        Id_text.text = id.ToString();
        Background.color = color;
        Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 1);
        this.Color = color;
    }

    public void SetSelected(bool selected)
    {
        if (!Completed)
        {
            Selected = selected;
            if (Selected)
            {
                Background.color = Color.yellow;
            }
            else
            {
                Background.color = Color.black;
            }
        }
    }
}
