using Cinemachine;
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
    public bool isClick = false;
    public bool canMoveCam = true;
    public bool isFirstClick = false;
    public bool isChosseFirstColor = false;
    public float camMaxsize;
    public PageSwipe pageSwipe;
    public Transform _trs, _colorButonParen, pageParent;
    [SerializeField] Pixel objPrefab;
    [SerializeField] GameObject pagePrefabs;
    [SerializeField] ColorRenPixel colorPrefabs;
    [SerializeField] private Pixel[,] Pixels;
    [SerializeField] public Slider slider;
    [SerializeField] List<Pixel> _list;
    [SerializeField] private int _countColor = 1;
    Camera Camera;
    [SerializeField] private CinemachineVirtualCamera virturalcam;
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
    public btn_OutGame btnOutGame;
    public int[,] checkWin;

    [Header("Save")]
    public Texture2D textureBlackandWhite;
    public string nowKey;
    public bool[,] matrix;
    public Dictionary<string, bool[,]> allSaves = new Dictionary<string, bool[,]>();
    public Btn_loadGame1 update;
    void Awake()
    {

        Instance = this;

        Camera = Camera.main;


    }
    private void Start()
    {


    }

    public void NewGame()
    {

        texture = PictureControll.Instance_picture.nowTexure;
        CreatePixelMap();

        CreateColorSwatches();
        Application.targetFrameRate = 60;
        canMoveCam = true;
        checkWin = new int[_countColor - 1, 2];
        LoadFilled();

    }

    private void LoadFilled()
    {

        bool[,] matrix = Data.GetMatrix(nowKey);
        Debug.Log($"{matrix.GetLength(0)}-{matrix.GetLength(1)}");
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                if (matrix[x, y])
                {
                    if (Pixels[x, y] != null)
                        Pixels[x, y].FillOnLoad();


                }
            }
        }

    }

    public void CreatePixelMap()
    {
        Color[] colors = texture.GetPixels();
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


        centerCam = new Vector3((float)texture.width / 2, (float)texture.height / 2, -10) + Vector3.down * 3;
        camlookat.transform.position = centerCam;
        camMaxsize = Mathf.Max(texture.width, texture.height) + 3;
        virturalcam.m_Lens.OrthographicSize = camMaxsize;

        allButon = new ColorRenPixel[0];
        Pixels = new Pixel[texture.width, texture.height];
        _countColor = 1;
        _allTypeOfColor = new Dictionary<Color, int>();
        _allPixelGroups = new Dictionary<int, List<Pixel>>();
        ColorSwatches = new List<ColorRenPixel>();
        //
        matrix = new bool[texture.width, texture.height];


        Data.AddData(nowKey, matrix);

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

    void CreateColorSwatches()
    {
        _colorButonParen.transform.position = Vector3.zero + new Vector3(0, Screen.width / 2.5f, 0);

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
            x.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 5, Screen.width / 5);
            x.transform.position = _colorButonParen.transform.position + new Vector3(Screen.width, 0, 0) * (i - 1);
            for (int k = (i - 1) * 10; k < i * 10; k++)
            {
                if (k >= allButon.Length) break;
                allButon[k].transform.parent = x.transform;

            }

        }
        // colorNow = ColorSwatches[0].Color;
    }

    private void SetColor(ColorRenPixel colorRenPixel)
    {
        if (isChosseFirstColor)
        {
            SetHighlight(false);
        }
        this.colorNow = colorRenPixel.Color;
        this.idNow = colorRenPixel.Id;

        if (!isChosseFirstColor) isChosseFirstColor = true;
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

        for (int i = 0; i < _trs.transform.childCount; i++)
        {
            Destroy(_trs.transform.GetChild(i).gameObject);

        }
        for (int i = 0; i < pageParent.transform.childCount; i++)
        {
            Destroy(pageParent.transform.GetChild(i).gameObject);

        }


    }
}