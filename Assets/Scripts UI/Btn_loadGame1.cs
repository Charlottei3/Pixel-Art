using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Btn_loadGame1 : BaseButton
{
    private Texture2D _texture;
    public GameObject picture;
    public GameObject loadPicture;
    bool[,] matrix = null;
    public string key;

    private void Awake()
    {


    }
    public override void Start()
    {
        base.Start();
        _texture = picture.GetComponent<Image>().sprite.texture;
        key = _texture.name;
        UpdatePicture();
    }
    public void UpdatePicture()
    {

        // picture.GetComponent<Image>().sprite = _texture;
        Texture2D loadTexure2 = new Texture2D(_texture.width, _texture.height, TextureFormat.RGBA32, false);
        Data.Load();
        matrix = Data.GetMatrix(key);
        if (matrix == null)
        {
            matrix = new bool[_texture.width, _texture.height];
        }


        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                if (matrix[x, y] == false && _texture.GetPixel(x, y).a >= 0.5f)
                {
                    Color color = new Color();
                    color = _texture.GetPixel(x, y);
                    color = Color.Lerp(Color.white * color.grayscale, Color.white, 0f);
                    loadTexure2.SetPixel(x, y, color);
                    loadTexure2.Apply();
                }
                else
                {
                    Color color = new Color();
                    color = _texture.GetPixel(x, y);
                    loadTexure2.SetPixel(x, y, color);
                    loadTexure2.Apply();
                }
            }
        }

        //tao sprite
        Sprite create = Sprite.Create(loadTexure2, new Rect(0, 0, loadTexure2.width, loadTexure2.height), Vector2.one * 0.5f);
        loadPicture.GetComponent<Image>().sprite = create;
        Debug.Log("update");
    }
    protected override void OnClick()
    {
        PictureControll.Instance_picture.nowTexure = _texture;
        GameManager.Instance.nowKey = key;
        GameManager.Instance.Menu.SetActive(false);
        GameManager.Instance.btnOutGame.gameObject.SetActive(true);
        GameManager.Instance.NewGame();
        GameManager.Instance.update = this;
        //on button outgame
        //
        // LoadData
    }
}

