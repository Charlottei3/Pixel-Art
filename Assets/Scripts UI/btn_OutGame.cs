using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_OutGame : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("out");
        GameManager.Instance.Menu.SetActive(true);
        GameManager.Instance.AllBook.SetActive(true);
        GameManager.Instance.Clear();

        GameManager.Instance.btnOutGame.gameObject.SetActive(false);
        if (Data.gameData.isdrawed.ContainsKey(GameManager.Instance.nowKey))
        {
            if (Data.gameData.isdrawed[GameManager.Instance.nowKey] == true)
            {
                GameManager.Instance.nowBtnLoadGame.UpdatePicture();//update anh nut vua nhan
                //update anh trong drawed
                if (!GameManager.Instance.nowBtnLoadGame.isInDrawed)//ko phai trong drawed
                {
                    Transform find = GameManager.Instance.allListDrawed.saveDrawing.Find(GameManager.Instance.nowKey);
                    if (find != null) { find.GetComponent<Btn_loadGame1>().UpdatePicture(); Debug.Log("3"); }

                }
                if (GameManager.Instance.nowBtnLoadGame.isInDrawed)//neu ben trong toi da to thi tim ban goc de update theo
                {
                    GameManager.Instance.allListDrawed.DictionaryCopyOnDrawed[GameManager.Instance.nowBtnLoadGame].UpdatePicture();//tim ben ngoai dra
                }
            }
        }
    }
}
