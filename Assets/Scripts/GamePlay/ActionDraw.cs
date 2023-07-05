using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Windows;

public class ActionDraw
{
    //public static Action<Pixel> act_FillBoomb;
    //public static Action<Pixel> act_DrawStick;

    public static void FillBoomb(Pixel input)
    {
        Debug.Log("FillBomb");
        for (int m = input.x - 5; m <= input.x + 5; m++)
        {
            for (int n = input.y - 5; n <= input.y + 5; n++)
            {
                Debug.Log("FillBomb");
                if (m < 0 || m >= GameManager.Instance.Pixels.GetLength(0)) continue;
                if (n < 0 || n >= GameManager.Instance.Pixels.GetLength(1)) continue;
                if (GameManager.Instance.Pixels[m, n] == null)
                {
                    continue;
                }
                if (Math.Pow(input.x - m, 2) + Math.Pow(input.y - n, 2) <= 26)
                {
                    if (/*GameManager.Instance.Pixels[m, n].id == input.id*/1 == 1)
                    {
                        if (!GameManager.Instance.Pixels[m, n].isFilledInTrue)
                        {
                            GameManager.Instance.Pixels[m, n].Fill();
                        }
                    }
                }

            }
        }
    }



    public static void DrawAround(Pixel input)
    {
        for (int m = input.x - 1; m <= input.x + 1; m++)
        {
            for (int n = input.y - 1; n <= input.y + 1; n++)
            {
                if (m < 0 || m >= GameManager.Instance.Pixels.GetLength(0)) continue;
                if (n < 0 || n >= GameManager.Instance.Pixels.GetLength(1)) continue;
                if (GameManager.Instance.Pixels[m, n] == null)
                {
                    continue;
                }
                ///kiem tra xong
                if (GameManager.Instance.Pixels[m, n].id == input.id)
                {
                    //id giong
                    if (!GameManager.Instance.Pixels[m, n].isFilledInTrue)//chua to moi lay lan dc
                    {
                        GameManager.Instance.Pixels[m, n].Fill();
                        DrawAround(GameManager.Instance.Pixels[m, n]);
                    }
                    else if (GameManager.Instance.Pixels[m, n].isFilledInTrue && !GameManager.Instance.Pixels[m, n].isCheckDrawStick)
                    {
                        GameManager.Instance.Pixels[m, n].isCheckDrawStick = true;
                        DrawAround(GameManager.Instance.Pixels[m, n]);

                    }


                }

            }
        }
    }



}

