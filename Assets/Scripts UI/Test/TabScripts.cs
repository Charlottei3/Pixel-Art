using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabScripts : MonoBehaviour
{
    public CanvasGroup[] alltab;
    public TextMeshProUGUI titleText;


    public void TurnOnTab(int tab)
    {

        for (int i = 0; i < alltab.Length; i++)
        {
            alltab[i].alpha = 0;
            alltab[i].blocksRaycasts = false;
        }
        alltab[tab - 1].alpha = 1;
        alltab[tab - 1].blocksRaycasts = true;
    }
    public void ChanggeText(string text)
    {
        titleText.text = text;
    }

}
