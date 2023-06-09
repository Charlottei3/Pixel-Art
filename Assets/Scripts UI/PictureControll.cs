using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureControll : MonoBehaviour
{
    public static PictureControll Instance_picture { get; private set; }
    public Texture2D nowTexure;

    void Awake()
    {
        if (Instance_picture != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance_picture = this;
            DontDestroyOnLoad(gameObject);
        }
        Application.targetFrameRate = 100;
    }


}
