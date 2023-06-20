using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Btn_loadGame1 : BaseButton
{
    public bool isInDrawed = false;
    public bool isComplete = false;
    public Texture2D _texture;
    public GameObject picture;
    public GameObject loadPicture;
    public GameObject complete;
    public Button btn;
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
        //neu co san anh den trang thi chi update khi ma  Data.gameData.isdrawed[key] = true;
        if (Data.gameData.isdrawed.ContainsKey(key))
        {
            if (Data.gameData.isdrawed[key])
            {
                UpdatePicture();
                if (!isInDrawed)
                {

                    if (Data.gameData.isComplete.ContainsKey(key))
                    {
                        if (Data.gameData.isComplete[key])
                        {

                            GameManager.Instance.allListDrawed.AddBtnLoad(this, GameManager.Instance.allListDrawed.saveComplete);
                        }
                        else
                            GameManager.Instance.allListDrawed.AddBtnLoad(this, GameManager.Instance.allListDrawed.saveDrawing);
                    }

                }
            }
        }
    }
    public void UpdatePicture()
    {
        if (Data.gameData.isComplete.ContainsKey(key))//neu hoan thanh roi
        {
            if (Data.gameData.isComplete[key])
            {//tat button bat dau tich
                btn.enabled = false;
                complete.SetActive(true);
                isComplete = true;
                Data.gameData.isComplete[GameManager.Instance.nowKey] = true;
                if (isInDrawed)//neu o trong khu vuc toi to mau thi chuyen vao complete
                {
                    GameManager.Instance.allListDrawed.RemoveToCompelete(this);
                }
            }
        }
        // picture.GetComponent<Image>().sprite = _texture
        Texture2D loadTexure2 = new Texture2D(_texture.width, _texture.height, TextureFormat.RGBA32, false);
        Debug.Log("1");
        matrix = Data.GetMatrix(key);
        if (matrix == null)//neu chua tang to tao ma tran zero de tao anh dentrang
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
                    color = Color.Lerp(new Color(color.grayscale, color.grayscale, color.grayscale), Color.white, 0f);
                    loadTexure2.SetPixel(x, y, color);
                    loadTexure2.Apply();
                }
                else if (_texture.GetPixel(x, y).a >= 0.5f)
                {
                    Color color = new Color();
                    color = _texture.GetPixel(x, y);
                    loadTexure2.SetPixel(x, y, color);
                    loadTexure2.Apply();
                }
                else if (_texture.GetPixel(x, y).a < 0.5f)
                {
                    loadTexure2.SetPixel(x, y, Color.white);
                    loadTexure2.Apply();
                }
            }
        }

        //tao sprite
        Sprite create = Sprite.Create(loadTexure2, new Rect(0, 0, loadTexure2.width, loadTexure2.height), Vector2.one * 0.5f);
        loadPicture.GetComponent<Image>().sprite = create;
    }

    protected override void OnClick()
    {
        PictureControll.Instance_picture.nowTexure = _texture;
        GameManager.Instance.nowKey = key;
        GameManager.Instance.Menu.SetActive(false);
        GameManager.Instance.btnOutGame.gameObject.SetActive(true);

        GameManager.Instance.AllBook.SetActive(false);

        GameManager.Instance.NewGame();
        GameManager.Instance.nowBtnLoadGame = this;
        //on button outgame
        //
        // LoadData

    }
}

