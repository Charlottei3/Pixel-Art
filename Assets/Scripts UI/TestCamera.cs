using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCamera : BaseButton
{
    public Image image;
    public GetCameraImage getCam;
    protected override void OnClick()
    {

        image.sprite = getCam.TakePicture();
        getCam.SaveImageToFile(image.sprite.texture);
    }
}
