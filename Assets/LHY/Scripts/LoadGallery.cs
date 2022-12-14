using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class AvatarImageInfo
{
    public byte[] avatarImage;
    public string avatarImageName;
}

public class LoadGallery : MonoBehaviour
{
    public RawImage image;
    public LHY_PlayerItem playerItem;

    public byte[] avatarImg;
    public string avatarImgName;

    public GameObject Analysiserror;

    public GameObject UpLoadFacePhoto;

    public void OnClickImageLoad()
    {
        Analysiserror.SetActive(false);
        NativeGallery.GetImageFromGallery((file) =>
        {
            FileInfo selected = new FileInfo(file);          

            //이미지 용량제한하기
            if(selected.Length > 50000000)
            {
                return;
            }

            //불러오기
            if(!string.IsNullOrEmpty(file))
            {
                //불러와라.
                StartCoroutine(LoadImage(file));
            }

        });
    }


    IEnumerator LoadImage(string path)
    {
        yield return null;

        byte[] fileData = File.ReadAllBytes(path);
        string filename = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/avatarImage";

        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        File.WriteAllBytes(savePath + filename + ".png", fileData);

        var temp = File.ReadAllBytes(savePath + filename + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);
        avatarImg = File.ReadAllBytes(savePath + filename + ".png");
        avatarImgName = Path.GetFileName(savePath + filename + ".png").Split('.')[0];
        image.texture = tex;
        image.SetNativeSize();
        ImageSizeSetting(image, 1000, 1000);
    }

    void ImageSizeSetting(RawImage image, float x, float y)
    {
        var imgX = image.rectTransform.sizeDelta.x;
        var imgY = image.rectTransform.sizeDelta.y;

        if (x / y > imgX / imgY) //이미지의 세로길이가 더 길다
        {
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgX * (y / imgY));
        }
        else
        {
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgY * (x / imgX));
        }
    }

    public void OnCilckImageSave()
    {
        string accessToken = PlayerPrefs.GetString("token");
        print("accessToken::"+ accessToken);

        AvatarImageInfo avatarImageInfo = new AvatarImageInfo();
        avatarImageInfo.avatarImage = avatarImg;
        avatarImageInfo.avatarImageName = avatarImgName;

        //PC
        //avatarImageInfo.avatarImage = File.ReadAllBytes(Application.persistentDataPath + "/Resources/01.Pictures/human1.png");
        //avatarImageInfo.avatarImageName = Path.GetFileName(Application.persistentDataPath + "/Resources/01.Pictures/human1.png").Split('.')[0];

        print(avatarImageInfo.avatarImage);
        print(avatarImageInfo.avatarImageName);
        //avatarImageInfo.avatarImage = avatarImg;

        HttpRequester requester = new HttpRequester();
        //url경로
        requester.url = "http://13.124.225.86:8080/api/v1/avatar/image";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(avatarImageInfo);
        print(requester.postData);
        requester.onComplete = OnClickDownload;

        HttpManager.instance.SendRequest(requester);

        print("sucssasSand");
    }
     
    private void OnClickDownload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int face  = (int)json["data"]["face"];
        int eyebrows = (int)json["data"]["eyebrows"];
        int eye = (int)json["data"]["eye"];

        if(face >= 7 || eyebrows >= 8 || eye >= 9)
        {
            playerItem.FaceType = 0;
            playerItem.EyebrowsType = 0;
            playerItem.Eyelashestype = 0;
            Analysiserror.SetActive(true);

        }
        else
        {
            playerItem.FaceType = face;
            playerItem.EyebrowsType = eyebrows;
            playerItem.Eyelashestype = eye;
            Analysiserror.SetActive(false);
            UpLoadFacePhoto.SetActive(false);
        }

        print("조회 완료");
    }

}
