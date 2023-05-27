using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorRenPixel : MonoBehaviour
{
    public TMP_Text Id_text;
    public Image Background;
    public int Id;
    bool Completed;
    bool Selected;
    public Color Color { get; set; }
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
