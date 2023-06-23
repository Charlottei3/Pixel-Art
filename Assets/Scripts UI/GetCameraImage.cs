using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;
using UnityEngine.UI;

//using AForge.Imaging;

public class GetCameraImage : MonoBehaviour
{
    public List<Sprite> saveSprite;
    private float size = 40;
    // Tạo mới một bitmap với kích thước 100x100
    public Slider slider;
    WebCamTexture webcamTexture;
    public string path;
    public RawImage imgDisplay;
    private Material grayScaleMaterial;
    private Texture2D texture;
    public Shader shader;
    //Assets/Material2/GrayScale2.shader
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);


    }

    private void OnSliderValueChanged(float value)
    {
        size = Mathf.Lerp(40, 100, value);
        grayScaleMaterial.SetFloat("_GridWidth", (int)size);
        grayScaleMaterial.SetFloat("_GridHeight", (int)size);
    }
    public void TurnOnCam()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera) || !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) || Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {

            string[] request = new string[3] { Permission.Camera, Permission.ExternalStorageWrite, Permission.ExternalStorageWrite };
            Permission.RequestUserPermissions(request);
        }
        //if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        //{
        //    Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        //}

        grayScaleMaterial = new Material(shader);

        //  WebCamDevice[] devices = WebCamTexture.devices;

        //Debug.Log("SoluongCamtimThay" + $"{devices.Length}");


        webcamTexture = new WebCamTexture(480, 480, 30);
        grayScaleMaterial.mainTexture = webcamTexture;
        webcamTexture.Play();
        webcamTexture.filterMode = FilterMode.Trilinear;

    }
    private void Update()
    {
        grayScaleMaterial.SetTexture("_MainTex", webcamTexture);
        this.GetComponent<Image>().material = grayScaleMaterial;
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


    public void SaveImageToFile(Texture2D textureToSave, string name, string folder)
    {

        byte[] bytes = textureToSave.EncodeToPNG();
        string filename = name + ".png";
        File.WriteAllBytes(Application.streamingAssetsPath + folder + filename, bytes);
    }
    public Sprite ReadFileToSprite(string imageFileName, string foldername)
    {
        Texture2D imageTexture;
        byte[] imageBytes = File.ReadAllBytes(Application.streamingAssetsPath + foldername + imageFileName + ".png");
        imageTexture = new Texture2D(2, 2);
        imageTexture.LoadImage(imageBytes);
        imageTexture.Apply();

        Sprite sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
    public Sprite TakePicture()
    {
        Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);

        Texture2D test = ChangeToTexture2D(grayScaleMaterial.mainTexture);
        texture.SetPixels(test.GetPixels());
        texture.Apply();
        TextureScale.Bilinear(texture, (int)size, (int)size);
        texture.Apply();
        Sprite create = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        create.name = "Create" + $"{Data.gameData.WebCamPictureCount}";
        return create;
    }



    public void TurnOffCam()
    {

        webcamTexture.Pause();
        webcamTexture.Stop();
    }

}
public class TextureScale : MonoBehaviour
{
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
