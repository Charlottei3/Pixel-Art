using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabScripts : MonoBehaviour
{
    public GameObject[] alltab;
    public TextMeshProUGUI titleText;

    public void TurnOnTab(int tab)
    {
        {
            for (int i = 0; i < alltab.Length; i++)
            {
                alltab[i].SetActive(false);
            }
            alltab[tab - 1].SetActive(true);
        }

    }
    public void ChanggeText(string text)
    {
        titleText.text = text;
    }

}
