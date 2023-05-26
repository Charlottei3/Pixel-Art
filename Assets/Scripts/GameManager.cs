using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isClick = false;
    public bool isFirstClickTrue = false;
    public Texture2D texture;
    [SerializeField] GameObject objPrefab;
    // [SerializeField] private Camera _cam;
    [SerializeField] private Pixel[,] Pixels;
    Camera Camera;
    public float zoomMultiplier = 2;
    public float defaultFov = 90;
    public float zoomDuration = 2;

    int _countColor = 1;
    Dictionary<Color, int> _allTypeOfColor = new Dictionary<Color, int>();

    List<ColorSwatch> ColorSwatches = new List<ColorSwatch>();

    Dictionary<int, List<Pixel>> _allPixelGroups = new Dictionary<int, List<Pixel>>();

    RaycastHit2D[] Hits = new RaycastHit2D[1];
    ColorSwatch SelectedColorSwatch;
    public Transform _trs;
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
        CreatePixelMap();

        //   CreateColorSwatches();
    }


    void CreatePixelMap()
    {
        Color[] colors = texture.GetPixels();
        Camera.transform.position = new Vector3((float)texture.width / 2, (float)texture.height / 2, -10);

        Pixels = new Pixel[texture.width, texture.height];

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (colors[x + y * texture.width].a != 0)
                {
                    GameObject go = Instantiate(objPrefab, _trs);
                    go.transform.position = new Vector3(x, y);
                    go.transform.name = $"square{x}-{y}";
                    Pixels[x, y] = go.GetComponent<Pixel>();

                    if (!_allTypeOfColor.ContainsKey(colors[x + y * texture.width]))//màu mới
                    {
                        _allTypeOfColor.Add(colors[x + y * texture.width], _countColor);
                        _allPixelGroups.Add(_countColor, new List<Pixel>());
                        Pixels[x, y].id = _countColor;
                        Pixels[x, y]._color = colors[x + y * texture.width];
                        _countColor++;
                    }
                    else//màu cũ
                    {
                        int foundId = _allTypeOfColor.GetValueOrDefault(colors[x + y * texture.width]);
                        _allPixelGroups[foundId].Add(Pixels[x, y]);
                        Pixels[x, y].id = foundId;
                        Pixels[x, y]._color = colors[x + y * texture.width];
                    }

                }
            }
        }
        Debug.Log(Pixels.Length);


    }

    void CreateColorSwatches()
    {
        foreach (KeyValuePair<Color, int> kvp in _allTypeOfColor)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("ColorSwatch") as GameObject);

            float offset = 1.2f;
            go.transform.position = new Vector2(kvp.Value * 2 * offset, -3);
            ColorSwatch colorswatch = go.GetComponent<ColorSwatch>();
            colorswatch.SetData(kvp.Value, kvp.Key);

            ColorSwatches.Add(colorswatch);
        }
    }

    void DeselectAllColorSwatches()
    {
        for (int n = 0; n < ColorSwatches.Count; n++)
        {
            ColorSwatches[n].SetSelected(false);
        }
    }


    private Pixel GetPixel(Vector2 position)
    {
        if (position.x >= 0 && position.x < Pixels.GetLength(0) && position.y >= 0 && position.y < Pixels.GetLength(1))
        {
            if (Pixels[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] != null)
            {

                var Pixel = Pixels[Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y)];
                Debug.Log(Pixel.name);
                return Pixel;
            }
            else return null;
        }
        else return null;

    }

}