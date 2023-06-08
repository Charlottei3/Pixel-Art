using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_OutGame : BaseButton
{
    protected override void OnClick()
    {

        GameManager.Instance.Menu.SetActive(true);
        GameManager.Instance.Clear();

        GameManager.Instance.btnOutGame.gameObject.SetActive(false);
        GameManager.Instance.update.UpdatePicture();
        //SaveData
    }
}
