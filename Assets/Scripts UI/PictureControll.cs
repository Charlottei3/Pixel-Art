
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PictureControll : MonoBehaviour
{
    public Btn_loadGame1 btnLoadPrefabs;
    public static PictureControll Instance_picture { get; private set; }
    public Texture2D nowTexure;

    public List<Sprite> testSprites;
    public List<Sprite> testBlackSprites;
    public Transform testMainEvent;
    public Transform gridCreate;
    public GetCameraImage getCameraImage;
    //public SpriteAtlas atlas;

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
        if (Data.gameData.WebCamPictureCount >= 1)
            LoadbtLoadInAtlas();

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
        create.key = picture.name;


    }
    public Sprite CreatBlackAndWhiteSprite(Sprite input)
    {
        Texture2D _texture = input.texture;
        Texture2D blackwhitteTexture = new Texture2D(_texture.width, _texture.height, TextureFormat.RGBA32, false);

        for (int x = 0; x < _texture.width; x++)
        {
            for (int y = 0; y < _texture.height; y++)
            {
                Color color = new Color();
                color = _texture.GetPixel(x, y);
                color = Color.Lerp(new Color(color.grayscale, color.grayscale, color.grayscale), Color.white, 0f);
                blackwhitteTexture.SetPixel(x, y, color);
                blackwhitteTexture.Apply();
            }
        }
        Sprite create = Sprite.Create(blackwhitteTexture, new Rect(0, 0, blackwhitteTexture.width, blackwhitteTexture.height), Vector2.one * 0.5f);
        create.name = input.name;

        return create;
    }

    //IEnumerator  RequestCameraPermission()
    //{
    //    if (Permission.HasUserAuthorizedPermission(Permission.Camera))
    //    {
    //        // Camera permission is already granted
    //        yield break;
    //    }

    //    // Request permission
    //    yield return Permission.RequestUserPermission(Permission.Camera);

    //}

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
    public void LoadbtLoadInAtlas()
    {
        for (int i = 0; i < Data.gameData.WebCamPictureCount; i++)
        {
            Sprite a = getCameraImage.ReadFileToSprite("Create" + $"{i}", "/CamPicture");
            Sprite a_black = getCameraImage.ReadFileToSprite("CreateBlack" + $"{i}", "/CamPictureBlack");
            CreateBtnLoad(a, a_black, gridCreate);
        }
    }
    //Assets/StreamingAssets/CamPicture/Create0.png
}
