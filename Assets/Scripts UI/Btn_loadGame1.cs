using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Btn_loadGame1 : BaseButton
{
    private Texture2D _texture;
    public GameObject picture;
    private void Awake()
    {
        _texture = picture.GetComponent<Image>().sprite.texture;
    }
    protected override void OnClick()
    {
        PictureControll.Instance_picture.nowTexure = _texture;
        SceneManager.LoadScene("GamePlay");

    }
}
