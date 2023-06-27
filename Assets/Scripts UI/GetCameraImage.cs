
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

//using AForge.Imaging;

public class GetCameraImage : MonoBehaviour
{

    public List<Sprite> saveSprite;
    private float size = 40;
    // Tạo mới một bitmap với kích thước 100x100
    public Slider slider;
    WebCamTexture webcamTexture;
    WebCamTexture webcamTexture2;
    public string path;
    public RawImage imgDisplay;
    private Material grayScaleMaterial = null;
    private Texture2D texture;
    public Shader shader;
    bool CamOn = false;
    WebCamDevice[] devices;
    public int nowCAm = 0;
    public GameObject CamScreenFront;
    public RectTransform CamScreenBack;
    //Assets/Material2/GrayScale2.shader
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        ChangeWhenSlideChange(value);
    }

    private void ChangeWhenSlideChange(float value)
    {
        size = Mathf.Lerp(40, 100, value);
        grayScaleMaterial.SetFloat("_GridWidth", (int)size);
        grayScaleMaterial.SetFloat("_GridHeight", (int)size);
    }

    private void Update()
    {

        if (CamOn)
        {
            // grayScaleMaterial.SetTexture("_MainTex", webcamTexture);
            //  this.GetComponent<Image>().material = grayScaleMaterial;
        }
    }

    private Texture2D ChangeToTexture2D(Texture input)
    {
        Texture texture = input;
        RenderTexture renderTexture = new RenderTexture(texture.width, texture.height, 0, RenderTextureFormat.ARGB32);
        RenderTexture.active = renderTexture;
        Graphics.Blit(texture, renderTexture);
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
        GameObject.Destroy(renderTexture);
        return texture2D;
    }

    private IEnumerator AskForPermissions()
    {

#if UNITY_ANDROID
        List<bool> permissions = new List<bool>() { false, false, false };
        List<bool> permissionsAsked = new List<bool>() { false, false, false };
        List<Action> actions = new List<Action>()
    {
        new Action(() => {
            permissions[0] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);
            if (!permissions[0] && !permissionsAsked[0])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                permissionsAsked[0] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.Camera);
            if (!permissions[1] && !permissionsAsked[1])
            {
                Permission.RequestUserPermission(Permission.Camera);
                permissionsAsked[1] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[2] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            if (!permissions[2] && !permissionsAsked[2])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                permissionsAsked[2] = true;
                return;
            }
        }),
        //    new Action(() => {
        //    permissions[3] = Permission.HasUserAuthorizedPermission("android.permission.READ_PHONE_STATE");
        //    if (!permissions[3] && !permissionsAsked[3])
        //    {
        //        Permission.RequestUserPermission("android.permission.READ_PHONE_STATE");
        //        permissionsAsked[3] = true;
        //        return;
        //    }
        //})
    };
        for (int i = 0; i < permissionsAsked.Count;)
        {
            actions[i].Invoke();
            if (permissions[i])
            {
                Debug.Log("Reqquest" + $"{i}");
                ++i;
            }
            yield return new WaitForEndOfFrame();
        }
        TurnOnCam(0);
#endif

    }
    public void Reqquest()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        StartCoroutine(AskForPermissions());
    }
    public void TurnOnCam(int input)
    {
        nowCAm = input;
        CamOn = true;
        devices = WebCamTexture.devices;
        if (grayScaleMaterial != null)
        {
            DestroyImmediate(grayScaleMaterial);
        }
        //if (webcamTexture != null)
        //{
        //    DestroyImmediate(webcamTexture);
        //}
        grayScaleMaterial = new Material(shader);
        switch (input)
        {
            case 0:
                {
                    CamScreenBack.rotation = Quaternion.Euler(0, 0, -90);
                    this.GetComponent<RawImage>().uvRect = new Rect(0, 1, 1, 1);
                    break;
                }
            case 1:
                {
                    CamScreenBack.rotation = Quaternion.Euler(0, 0, 90);
                    this.GetComponent<RawImage>().uvRect = new Rect(0, 1, 1, -1);
                    break;
                }
        }

        webcamTexture = new WebCamTexture(devices[input].name, 480, 480);
        grayScaleMaterial.mainTexture = webcamTexture;
        webcamTexture.Play();
        webcamTexture.filterMode = FilterMode.Trilinear;

        this.GetComponent<RawImage>().material = grayScaleMaterial;
        this.GetComponent<RawImage>().texture = webcamTexture;

    }
    public void TurnOffCam()
    {
        CamOn = false;
        webcamTexture.Pause();
        webcamTexture.Stop();
        if (webcamTexture != null)
        {
            DestroyImmediate(webcamTexture);
        }
    }
    public void ChangeCam()
    {
        if (devices.Length < 2) return;
        switch (nowCAm)
        {
            case 0:
                {
                    TurnOnCam(1);
                    break;
                }
            case 1:
                {
                    TurnOnCam(0);
                    break;
                }
        }
        ChangeWhenSlideChange(slider.value);
    }
    public void SaveImageToFile(Texture2D textureToSave, string name, string folder)
    {

        byte[] bytes = textureToSave.EncodeToPNG();
        string filename = name;
        Debug.LogError("persistenppath" + Application.persistentDataPath);
        string destination = Application.persistentDataPath + "/" + name + ".png";
        File.WriteAllBytes(destination, bytes);
    }
    public Sprite ReadFileToSprite(string imageFileName, string foldername)
    {
        Texture2D imageTexture;
        byte[] imageBytes = File.ReadAllBytes(Application.persistentDataPath + "/" + imageFileName + ".png");
        imageTexture = new Texture2D(2, 2);
        imageTexture.LoadImage(imageBytes);
        imageTexture.Apply();

        Sprite sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));
        sprite.name = imageFileName;
        return sprite;
    }
    public Sprite TakePicture()
    {
        Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);

        //  Texture2D test = ChangeToTexture2D(grayScaleMaterial.mainTexture);
        Texture2D test = ChangeToTexture2D(webcamTexture);

        texture.SetPixels(test.GetPixels());
        texture.Apply();
        TextureScale.Bilinear(texture, (int)size, (int)size);
        texture.Apply();

        switch (nowCAm)
        {
            case 0:
                {
                    texture = TextureScale.rotate270(texture);//cam sau OK
                    break;
                }
            case 1:
                {
                    texture = TextureScale.rotate90(texture);
                    texture = TextureScale.rotate180(texture);
                    break;
                }
        }
        Sprite create = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        create.name = "Create" + $"{Data.gameData.WebCamPictureCount}";
        return create;
    }
}
public class TextureScale : MonoBehaviour
{
    public static Texture2D rotate90(Texture2D orig)
    {
        print("doing rotate90");
        Color32[] origpix = orig.GetPixels32(0);
        Color32[] newpix = new Color32[orig.width * orig.height];
        for (int c = 0; c < orig.height; c++)
        {
            for (int r = 0; r < orig.width; r++)
            {
                newpix[orig.width * orig.height - (orig.height * r + orig.height) + c] =
                  origpix[orig.width * orig.height - (orig.width * c + orig.width) + r];
            }
        }
        Texture2D newtex = new Texture2D(orig.height, orig.width, orig.format, false);
        newtex.SetPixels32(newpix, 0);
        newtex.Apply();
        return newtex;
    }

    public static Texture2D rotate270(Texture2D orig)
    {
        print("doing rotate270");
        Color32[] origpix = orig.GetPixels32(0);
        Color32[] newpix = new Color32[orig.width * orig.height];
        int i = 0;
        for (int c = 0; c < orig.height; c++)
        {
            for (int r = 0; r < orig.width; r++)
            {
                newpix[orig.width * orig.height - (orig.height * r + orig.height) + c] = origpix[i];
                i++;
            }
        }
        Texture2D newtex = new Texture2D(orig.height, orig.width, orig.format, false);
        newtex.SetPixels32(newpix, 0);
        newtex.Apply();
        return newtex;
    }
    public static Texture2D rotate180(Texture2D orig)
    {
        print("doing rotate180");
        Color32[] origpix = orig.GetPixels32(0);
        Color32[] newpix = new Color32[orig.width * orig.height];
        for (int i = 0; i < origpix.Length; i++)
        {
            newpix[origpix.Length - i - 1] = origpix[i];
        }
        Texture2D newtex = new Texture2D(orig.width, orig.height, orig.format, false);
        newtex.SetPixels32(newpix, 0);
        newtex.Apply();
        return newtex;
    }

    public static void Bilinear(Texture2D source, int targetWidth, int targetHeight)
    {
        Color[] pixels = new Color[targetWidth * targetHeight];
        float incX = (1.0f / targetWidth);
        float incY = (1.0f / targetHeight);
        float posX = (incX / 2.0f);
        float posY = (incY / 2.0f);

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = source.GetPixelBilinear(posY, posX);
            posY += incY;
            if (posY >= 1.0f)
            {
                posY = (incY / 2.0f);
                posX += incX;
            }
        }

        source.Reinitialize(targetWidth, targetHeight);
        source.SetPixels(pixels);
        source.Apply();
    }
}
