using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Color colorNow { get; set; }
    public int idNow { get; set; }
    public bool isClick = false;
    public bool isFirstClick = false;
    public bool isChosseFirstColor = false;
    public float camMaxsize;
    public Transform _trs, _colorButonParen;
    public Texture2D texture;
    [SerializeField] Pixel objPrefab;
    [SerializeField] ColorRenPixel colorPrefabs;
    [SerializeField] private Pixel[,] Pixels;
    [SerializeField] public Slider slider;
    [SerializeField] List<Pixel> _list;
    [SerializeField] private int _countColor = 1;
    Camera Camera;
    public float zoomMultiplier = 2;
    public float defaultFov = 90;
    public float zoomDuration = 2;
    private Dictionary<Color, int> _allTypeOfColor = new Dictionary<Color, int>();
    public Dictionary<int, List<Pixel>> _allPixelGroups = new Dictionary<int, List<Pixel>>();
    List<ColorRenPixel> ColorSwatches = new List<ColorRenPixel>();



    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Camera = Camera.main;
        Application.targetFrameRate = 60;
        CreatePixelMap();

        CreateColorSwatches();
    }

    public void CreatePixelMap()
    {
        Color[] colors = texture.GetPixels();
        Camera.transform.position = new Vector3((float)texture.width / 2, (float)texture.height / 2, -10);
        camMaxsize = Mathf.Max(texture.width, texture.height) + 3;
        Camera.GetComponent<Camera>().orthographicSize = camMaxsize;

        Pixels = new Pixel[texture.width, texture.height];

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (colors[x + y * texture.width].a != 0)
                {
                    Pixel pixel = Instantiate(objPrefab, _trs);
                    pixel.transform.position = new Vector3(x, y);
                    pixel.transform.name = $"square{x}-{y}";
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
    }

    void CreateColorSwatches()
    {
        foreach (KeyValuePair<Color, int> kvp in _allTypeOfColor)
        {
            ColorRenPixel colorRenPixel = Instantiate(colorPrefabs, _colorButonParen);

            //   float offset = 1.2f;
            colorRenPixel.SetData(kvp.Value, kvp.Key);
            colorRenPixel.getButon().onClick.AddListener(() => SetColor(colorRenPixel));

            ColorSwatches.Add(colorRenPixel);
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

}