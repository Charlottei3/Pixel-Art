using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureControll : MonoBehaviour
{
    public Texture2D nowTexure;
    public static PictureControll Instance_picture { get; private set; }
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
    }
}
