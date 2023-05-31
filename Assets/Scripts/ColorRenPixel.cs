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
