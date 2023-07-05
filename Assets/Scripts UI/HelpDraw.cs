using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDraw : MonoBehaviour
{
    public GameObject[] highlight;
    public List<btnStatus> Buttons = new List<btnStatus>();

    public void TurnOnHighlight(int input)
    {
        for (int i = 0; i < highlight.Length; i++)
        {
            highlight[i].gameObject.SetActive(false);
        }
        highlight[input].SetActive(true);

    }
    public void TurnOffAllHighlight()
    {
        for (int i = 0; i < highlight.Length; i++)
        {
            highlight[i].gameObject.SetActive(false);
        }
    }



}
