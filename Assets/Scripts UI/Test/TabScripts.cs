using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabScripts : MonoBehaviour
{
    public CanvasGroup[] alltab;
    public TextMeshProUGUI titleText;
    public int i = 0;


    public void TurnOnTab(int tab)
    {

        for (int i = 0; i < alltab.Length; i++)
        {
            alltab[i].alpha = 0;
            alltab[i].blocksRaycasts = false;
        }
        alltab[tab - 1].alpha = 1;
        alltab[tab - 1].blocksRaycasts = true;
        if (tab - 1 > i)
        {
            StartCoroutine(AnimationHelper.SlideIn(alltab[tab - 1].GetComponent<RectTransform>(), Vector3.zero, Direction.LEFT, 5f, null));
            StartCoroutine(AnimationHelper.SlideOut(alltab[i].GetComponent<RectTransform>(), Vector3.zero, Direction.LEFT, 5f, null));
        }
        else if (tab - 1 < i)
        {
            StartCoroutine(AnimationHelper.SlideIn(alltab[tab - 1].GetComponent<RectTransform>(), Vector3.zero, Direction.RIGHT, 5f, null));
            StartCoroutine(AnimationHelper.SlideOut(alltab[i].GetComponent<RectTransform>(), Vector3.zero, Direction.RIGHT, 5f, null));
        }
        i = tab - 1;

    }
    public void ChanggeText(string text)
    {
        titleText.text = text;
    }

}
