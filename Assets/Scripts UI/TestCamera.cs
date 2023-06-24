using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TestCamera : BaseButton
{

    public Image image;
    public GetCameraImage getCam;
    Sprite sprite;
    Sprite blacksprite;
    public Transform gridCreate;
    public Sprite Apple;
    protected override void OnClick()
    {
        sprite = getCam.TakePicture();//chup
        image.sprite = sprite;
        //getCam.saveSprite.Add(sprite);//luu vao list
        //image.sprite = getCam.saveSprite[getCam.saveSprite.Count - 1];//lay anh tu list
        blacksprite = PictureControll.Instance_picture.CreatBlackAndWhiteSprite(sprite);//tao anh den trang
        PictureControll.Instance_picture.CreateBtnLoad(sprite, blacksprite, gridCreate);


        getCam.SaveImageToFile(sprite.texture, "Create" + $"{Data.gameData.WebCamPictureCount}", "/CamPicture");//luu anh thuong vao folder
        getCam.SaveImageToFile(blacksprite.texture, "CreateBlack" + $"{Data.gameData.WebCamPictureCount}", "/CamPictureBlack");//luu anh den trang


        //tang data count
        Data.IncreaseNumberWebCamPicture();
    }
}
