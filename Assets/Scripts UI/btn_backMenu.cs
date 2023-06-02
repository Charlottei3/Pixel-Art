using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_backMenu : BaseButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
