using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadImage : MonoBehaviour
{
    string imageFileName = "savedImage1";
    Texture2D imageTexture;
    Image Image;
    private void Awake()
    {
        Image = GetComponent<Image>();
    }
    private void Start()
    {
        // Load ảnh từ đường dẫn và tạo texture
        byte[] imageBytes = File.ReadAllBytes(Application.dataPath + "/CamPicture/" + imageFileName);
        imageTexture = new Texture2D(2, 2);
        imageTexture.LoadImage(imageBytes);
        imageTexture.Apply();

        Image.sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));
    }
}
