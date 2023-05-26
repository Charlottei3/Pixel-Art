using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D texture;
    [SerializeField] GameObject objPrefab;
    Pixel[,] Pixels;
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
        Camera = Camera.main;

        CreatePixelMap();
        //   CreateColorSwatches();
    }

    void CreatePixelMap()
    {
        Color[] colors = texture.GetPixels();

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

    void Update()
    {
        Vector2 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(mousePos.x);
        int y = Mathf.FloorToInt(mousePos.y);

        Pixel hoveredPixel = null;
        if (x >= 0 && x < Pixels.GetLength(0) && y >= 0 && y < Pixels.GetLength(1))
        {
            if (Pixels[x, y] != null)
            {
                hoveredPixel = Pixels[x, y];
            }
        }

        /*   if (Input.GetMouseButtonDown(0))
           {
               // Check if we clicked on a color swatch
               int hitCount = Physics2D.RaycastNonAlloc(mousePos, Vector2.zero, Hits);
               for (int n = 0; n < hitCount; n++)
               {
                   if (Hits[n].collider.CompareTag("ColorSwatch"))
                   {
                       SelectColorSwatch(Hits[n].collider.GetComponent<ColorSwatch>());
                   }
               }
           }*/

        /*  if (Input.GetMouseButton(0))
          {
              if (hoveredPixel != null && !hoveredPixel.IsFilledIn)
              {
                  if (SelectedColorSwatch != null && SelectedColorSwatch.ID == hoveredPixel.ID)
                  {
                      hoveredPixel.Fill();
                      if (CheckIfSelectedComplete())
                      {
                          SelectedColorSwatch.SetCompleted();
                      }
                  }
                  else
                  {
                      hoveredPixel.FillWrong();
                  }
              }
          }*/
    }

    /*   void SelectColorSwatch(ColorSwatch swatch)
       {
           if (SelectedColorSwatch != null)
           {
               for (int n = 0; n < PixelGroups[SelectedColorSwatch.ID].Count; n++)
               {
                   PixelGroups[SelectedColorSwatch.ID][n].SetSelected(false);
               }

               SelectedColorSwatch.SetSelected(false);
           }

           SelectedColorSwatch = swatch;
           SelectedColorSwatch.SetSelected(true);

           for (int n = 0; n < PixelGroups[SelectedColorSwatch.ID].Count; n++)
           {
               PixelGroups[SelectedColorSwatch.ID][n].SetSelected(true);
           }
       }

       bool CheckIfSelectedComplete()
       {
           if (SelectedColorSwatch != null)
           {
               for (int n = 0; n < PixelGroups[SelectedColorSwatch.ID].Count; n++)
               {
                   if (PixelGroups[SelectedColorSwatch.ID][n].IsFilledIn == false)
                       return false;
               }
           }

           return true;
       }*/
}