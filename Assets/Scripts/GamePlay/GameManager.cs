﻿using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool Nextlevel = false;
    public Texture2D texture, nextTexture;
    public Color colorNow { get; set; }
    public int idNow { get; set; }
    public ColorRenPixel nowColorRenPixel { get; set; }
    public bool isClick = false;
    public bool canMoveCam = true;
    public bool isFirstClick = false;
    public bool isChosseFirstColor = false;
    public float camMaxsize;
    public PageSwipe pageSwipe;
    public GameObject HelpDraw;
    public Transform _trs, _colorButonParen, pageParent;
    [SerializeField] Pixel objPrefab;
    [SerializeField] GameObject pagePrefabs;
    [SerializeField] ColorRenPixel colorPrefabs;
    [SerializeField] public Pixel[,] Pixels;
    [SerializeField] public Slider slider;
    [SerializeField] List<Pixel> _list;
    [SerializeField] private int _countColor = 1;
    Camera camera;
    [SerializeField] private CinemachineVirtualCamera virturalcam;
    public CinemachineTransposer transposer;
    [SerializeField] private Transform camlookat;
    public Vector3 centerCam;
    public float zoomMultiplier = 2;
    public float defaultFov = 90;
    public float zoomDuration = 2;
    private Dictionary<Color, int> _allTypeOfColor = new Dictionary<Color, int>();
    public Dictionary<int, List<Pixel>> _allPixelGroups = new Dictionary<int, List<Pixel>>();
    List<ColorRenPixel> ColorSwatches = new List<ColorRenPixel>();
    [SerializeField] public ColorRenPixel[] allButon;
    public GameObject Menu;
    public GameObject AllBook;
    public btn_OutGame btnOutGame;
    public int[,] checkWin;

    [Header("Save")]
    public ListDrawed allListDrawed;
    public Texture2D textureBlackandWhite;
    public string nowKey;

    public bool[,] matrix;
    public Dictionary<string, bool[,]> allSaves = new Dictionary<string, bool[,]>();
    public Btn_loadGame1 nowBtnLoadGame;
    public StatusGame.STATUS status;
    public GameObject Line;
    GameObject help;
    List<Pixel> listDrawStick = new List<Pixel>();
    void Awake()
    {

        Instance = this;

        camera = Camera.main;
        camera.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null) camera.gameObject.AddComponent<CinemachineBrain>();
        brain.m_DefaultBlend.m_Time = 1;
        brain.m_ShowDebugText = true;

        CinemachineTransposer Addtransposer = virturalcam.AddCinemachineComponent<CinemachineTransposer>();
        transposer = Addtransposer;


    }
    private void Start()
    {


    }

    public void NewGame()
    {
        //_slider.value = 0;
        //Pixels
        isClick = false;
        pageSwipe.currentPage = 2;
        canMoveCam = true;
        isFirstClick = false;

        isChosseFirstColor = false;
        _countColor = 1;
        texture = PictureControll.Instance_picture.nowTexure;
        pageParent.gameObject.SetActive(true);
        Line.SetActive(true);
        CreatePixelMap();

        CreateColorSwatches();
        SetColor(ColorSwatches[0]);
        canMoveCam = true;
        checkWin = new int[_countColor - 1, 2];
        LoadFilled();

    }

    private void LoadFilled()
    {
        if (Data.gameData.matrix.ContainsKey(nowKey))
        {
            bool[,] matrix2 = Data.GetMatrix(nowKey);
            for (int x = 0; x < matrix2.GetLength(0); x++)
            {
                for (int y = 0; y < matrix2.GetLength(1); y++)
                {
                    if (matrix2[x, y])
                    {
                        if (Pixels[x, y] != null)
                            Pixels[x, y].FillOnLoad();


                    }
                }
            }
        }
    }

    public void CreatePixelMap()
    {
        slider.value = 0;
        Color[] colors = texture.GetPixels();

        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i].a >= 0.5f)
            {
                colors[i] = new Color(RoundColor(colors[i].r, 0.2f), RoundColor(colors[i].g, 0.2f), RoundColor(colors[i].b, 0.2f));
            }
        }

        //lay anh den trang
        Color[] colorblackwhite = texture.GetPixels();
        for (int i = 0; i < colorblackwhite.Length; i++)
        {
            float gray = (colorblackwhite[i].r + colorblackwhite[i].g + colorblackwhite[i].b) / 3;
            colorblackwhite[i] = new Color(gray, gray, gray, colors[i].a);
        }

        /*   textureBlackandWhite = new Texture2D(texture.width, texture.height);
           textureBlackandWhite.SetPixels(colorblackwhite);
           textureBlackandWhite.Apply();*/
        transposer.m_XDamping = 0f;
        transposer.m_YDamping = 0f;
        StartCoroutine(TurnOnDamping());
        Debug.Log("start");
        camMaxsize = Mathf.Max(texture.width, texture.height) + 3;
        centerCam = new Vector3((float)(texture.width - 1) / 2, ((float)texture.height - 1) / 2, -10) + Vector3.down * 3;
        camlookat.transform.position = centerCam;

        virturalcam.m_Lens.OrthographicSize = camMaxsize;
        ////////transposer.m_XDamping = 0.75f;
        ////////transposer.m_YDamping = 0.75f;
        allButon = new ColorRenPixel[0];
        Pixels = new Pixel[texture.width, texture.height];
        _countColor = 1;
        _allTypeOfColor = new Dictionary<Color, int>();
        _allPixelGroups = new Dictionary<int, List<Pixel>>();
        ColorSwatches = new List<ColorRenPixel>();
        //
        matrix = new bool[texture.width, texture.height];
        Debug.Log($"{texture.width}-{texture.height}");




        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (colors[x + y * texture.width].a >= 0.5f)
                {
                    Pixel pixel = Instantiate(objPrefab, _trs);
                    pixel.transform.position = new Vector3(x, y);
                    pixel.transform.name = $"square{x}-{y}";
                    pixel.x = x;
                    pixel.y = y;
                    Pixels[x, y] = pixel;

                    if (!_allTypeOfColor.ContainsKey(colors[x + y * texture.width]))//màu mới
                    {
                        _allTypeOfColor.Add(colors[x + y * texture.width], _countColor);
                        _allPixelGroups.Add(_countColor, new List<Pixel>());
                        _allPixelGroups[_countColor].Add(Pixels[x, y]);
                        Pixels[x, y].id = _countColor;
                        Pixels[x, y]._colorTrue = colors[x + y * texture.width];
                        _countColor++;
                    }
                    else//màu cũ
                    {
                        int foundId = _allTypeOfColor.GetValueOrDefault(colors[x + y * texture.width]);
                        _allPixelGroups[foundId].Add(Pixels[x, y]);
                        Pixels[x, y].id = foundId;
                        Pixels[x, y]._colorTrue = colors[x + y * texture.width];
                    }

                }
            }
        }
        pageSwipe.totalPages = (_countColor - 1) / 10 + 1;
        if ((_countColor - 1) % 10 == 0)
        {
            pageSwipe.totalPages = (_countColor - 1) / 10;
        }

    }

    IEnumerator TurnOnDamping()
    {
        yield return new WaitForEndOfFrame();
        transposer.m_XDamping = 0.1f;
        transposer.m_YDamping = 0.1f;
    }
    void CreateColorSwatches()
    {
        _colorButonParen.transform.position = Vector3.zero + new Vector3(0, Screen.width / 2.5f, 0);
        help = Instantiate(HelpDraw, pageParent);
        help.transform.position = _colorButonParen.transform.position + new Vector3(0, -Screen.width / 2.5f, 0);

        allButon = new ColorRenPixel[_allTypeOfColor.Count];
        foreach (KeyValuePair<Color, int> kvp in _allTypeOfColor)
        {
            ColorRenPixel colorRenPixel = Instantiate(colorPrefabs, _colorButonParen);
            colorRenPixel.name = "Button" + kvp.Value;
            allButon[kvp.Value - 1] = colorRenPixel;

            //   float offset = 1.2f;
            colorRenPixel.SetData(kvp.Value, kvp.Key);
            colorRenPixel.getButon().onClick.AddListener(() => SetColor(colorRenPixel));
            ColorSwatches.Add(colorRenPixel);
        }

        for (int i = 1; i <= pageSwipe.totalPages; i++)
        {
            GameObject x = Instantiate(pagePrefabs, pageParent);
            x.name = "page" + i;
            x.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1080 / 5, 1080 / 5);
            x.transform.position = _colorButonParen.transform.position + new Vector3(Screen.width, 0, 0) * (i - 1);
            for (int k = (i - 1) * 10; k < i * 10; k++)
            {
                if (k >= allButon.Length) break;
                //   allButon[k].transform.parent = x.transform;
                allButon[k].transform.SetParent(x.transform);


            }

        }
        // colorNow = ColorSwatches[0].Color;
        pageSwipe.totalPages += 1;
    }

    private void SetColor(ColorRenPixel colorRenPixel)
    {
        //highlight

        if (isChosseFirstColor)
        {
            Debug.Log("Setcolor");
            nowColorRenPixel.SetSliderCanvasGroup(0);//tat nut mau cu di
            nowColorRenPixel.Id_text.fontSize = 60;
            SetHighlight(false);
        }
        //id ==1
        nowColorRenPixel = colorRenPixel;
        this.colorNow = colorRenPixel.Color;
        this.idNow = colorRenPixel.Id;
        help.GetComponent<HelpDraw>().TurnOffAllHighlight();
        SetStatus(StatusGame.STATUS.NORMAL);
        //id ==2
        if (!isChosseFirstColor) isChosseFirstColor = true;
        nowColorRenPixel.Id_text.fontSize = 90;
        nowColorRenPixel.SetSliderCanvasGroup(1);
        SetHighlight(true);
    }

    private void SetHighlight(bool turn)
    {
        this._list = _allPixelGroups[idNow];
        for (int i = 0; i < _list.Count; i++)
        {
            if (!_list[i].isFilledInTrue)
            {
                _list[i]._highlight.enabled = turn;
            }
        }
    }

    public void LoadPicture()
    {
        Nextlevel = true;
        SceneManager.LoadScene("GamePlay");

    }
    public void Clear()
    {
        Menu.SetActive(true);
        AllBook.SetActive(true);
        btnOutGame.gameObject.SetActive(false);
        pageParent.gameObject.SetActive(false);
        Line.SetActive(false);
        for (int i = 0; i < _trs.transform.childCount; i++)
        {
            Destroy(_trs.transform.GetChild(i).gameObject);

        }
        for (int i = 0; i < pageParent.transform.childCount; i++)
        {
            Destroy(pageParent.transform.GetChild(i).gameObject);

        }


    }

    private float RoundColor(float input, float step)

    {
        if (input % step >= step / 2)
            return step * ((int)(input / step) + 1);
        else return step * ((int)(input / step));

    }
    public void SetStatus(StatusGame.STATUS stastus)
    {
        status = stastus;
    }
}
public class StatusGame
{
    public STATUS status = STATUS.NORMAL;
    public enum STATUS
    {
        NORMAL,
        STICK,
        BOMB
    }

}