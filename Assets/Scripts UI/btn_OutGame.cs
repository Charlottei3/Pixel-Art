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
        if (Data.gameData.isdrawed.ContainsKey(GameManager.Instance.nowKey))
        {
            if (Data.gameData.isdrawed[GameManager.Instance.nowKey] == true)
            {
                GameManager.Instance.nowBtnLoadGame.UpdatePicture();//update anh nut vua nhan
                //update anh trong drawed
                if (!GameManager.Instance.nowBtnLoadGame.isInDrawed)//ko phai trong drawed
                {
                    Debug.Log("1");
                    Transform find = GameManager.Instance.listDrawed.saveDrawed.Find(GameManager.Instance.nowKey);
                    Debug.Log("2");
                    if (find != null) { find.GetComponent<Btn_loadGame1>().UpdatePicture(); Debug.Log("3"); }

                }
                if (GameManager.Instance.nowBtnLoadGame.isInDrawed)//neu ben trong toi da to thi tim ban goc de update theo
                {
                    Debug.Log("4");
                    GameManager.Instance.listDrawed.DictionaryCopyOnDrawed[GameManager.Instance.nowBtnLoadGame].UpdatePicture();//tim ben ngoai draw
                    Debug.Log("5");
                }
            }
        }
    }
}
