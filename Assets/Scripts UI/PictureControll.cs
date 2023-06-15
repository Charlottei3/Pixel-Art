using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureControll : MonoBehaviour
{
    public Btn_loadGame1 btnLoadPrefabs;
    public static PictureControll Instance_picture { get; private set; }
    public Texture2D nowTexure;

    public List<Sprite> testSprites;
    public List<Sprite> testBlackSprites;
    public Transform testMainEvent;

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
        Data.Load();
        Application.targetFrameRate = 100;
    }
    private void Start()
    {
        CreateAllBtnLoad();
    }
    private void CreateAllBtnLoad()
    {
        int min = Mathf.Min(testSprites.Count, testBlackSprites.Count);
        for (int i = 0; i < min; i++)
        {
            CreateBtnLoad(testSprites[i], testBlackSprites[i], testMainEvent);
        }
    }
    public void CreateBtnLoad(Sprite picture, Sprite loadPicture, Transform parent)
    {
        Btn_loadGame1 create = Instantiate(btnLoadPrefabs, parent);
        create.name = picture.name;
        create.picture.GetComponent<Image>().sprite = picture;
        create.loadPicture.GetComponent<Image>().sprite = loadPicture;


    }


}
