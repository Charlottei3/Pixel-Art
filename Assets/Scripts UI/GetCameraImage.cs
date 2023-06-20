using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

//using AForge.Imaging;

public class GetCameraImage : MonoBehaviour
{
    // Tạo mới một bitmap với kích thước 100x100
    public Slider slider;
    WebCamTexture webcamTexture;
    public string path;
    public RawImage imgDisplay;
    private Material grayScaleMaterial;
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        grayScaleMaterial = new Material(Shader.Find("Custom/GrayScaleShader"));
    }
    private void OnSliderValueChanged(float value)
    {

    }
    public void TurnOnCam()
    {

        WebCamDevice[] devices = WebCamTexture.devices;
        // Chạy máy ảnh đầu tiên
        webcamTexture = new WebCamTexture(devices[0].name, 512, 512, 30);


        webcamTexture.Play();
        webcamTexture.filterMode = FilterMode.Trilinear;

    }
    private void Update()
    {
        grayScaleMaterial.SetTexture("_MainTex", webcamTexture);
        this.GetComponent<Image>().material = grayScaleMaterial;
    }
    public Sprite TakePicture()
    {
        Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);

        Texture2D test = ChangeToTexture2D(grayScaleMaterial.mainTexture);
        texture.SetPixels(test.GetPixels());
        texture.Apply();
        Sprite create = Sprite.Create(test, new Rect(0, 0, test.width, test.height), Vector2.one * 0.5f);
        return create;
    }
    public void SaveImageToFile(Texture2D textureToSave)
    {
        byte[] bytes = textureToSave.EncodeToPNG();
        string filename = "savedImage1";
        File.WriteAllBytes(Application.dataPath + "/CamPicture/" + filename, bytes);
    }

    public void TurnOffCam()
    {

        webcamTexture.Pause();
        webcamTexture.Stop();
    }
    private Texture2D ChangeToTexture2D(Texture input)
    {
        Texture texture = input;// get your Texture here

        // Create a RenderTexture with the same size as the Texture
        RenderTexture renderTexture = new RenderTexture(texture.width, texture.height, 0, RenderTextureFormat.ARGB32);

        // Set the active RenderTexture
        RenderTexture.active = renderTexture;

        // Draw the Texture to the active RenderTexture
        Graphics.Blit(texture, renderTexture);

        // Create a new Texture2D and read the RenderTexture data into it
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();

        // Free up resources
        RenderTexture.active = null;
        GameObject.Destroy(renderTexture);
        return texture2D;

    }
    public Sprite ReadFileToSprite(string imageFileName)
    {
        Texture2D imageTexture;
        byte[] imageBytes = File.ReadAllBytes(Application.dataPath + "/CamPicture/" + imageFileName);
        imageTexture = new Texture2D(2, 2);
        imageTexture.LoadImage(imageBytes);
        imageTexture.Apply();

        Sprite sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }


}

