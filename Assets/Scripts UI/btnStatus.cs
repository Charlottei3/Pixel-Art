using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnStatus : BaseButton
{
    public StatusGame.STATUS type;


    protected override void OnClick()
    {
        if (!GameManager.Instance.isChosseFirstColor) GameManager.Instance.isChosseFirstColor = true;
        GameManager.Instance.SetStatus(type);
    }
}
