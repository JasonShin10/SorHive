using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class LifeingImageInfoAI
{
    public byte[] lifingImage;
    public string lifingImageName;
}

public class LifeingImageInfo
{
    public byte[] lifingImage;
    public string lifingImageName;
    public string lifingContent;
}

[System.Serializable]
public class LifeingImagesData
{
    public string lifingContent; 
    public List<LifeingImageInfo> lifingImages;
}



[System.Serializable]
public class LifeingInfo
{
    public int lifingNo;
    public int lifingCategoryNo;
    public string lifingContent;
    public int lifingId;
}



public class LifeingItem : MonoBehaviour
{
    public RawImage image;

    public byte[] lifingImg;
    public string lifingImgName;

    public int lifingId;

    public int lifingNo = 1;
    public int lifingCategoryNo = -1;
    public InputField lifingText;

    //public string[] filess;

    public List<LifeingImageInfo> lifeingImageList = new List<LifeingImageInfo>();

    public void OnClickImageLoad()
    {
        //갤러리를 연다
        NativeGallery.GetImageFromGallery((file) =>
        {
                //List<FileInfo> selectedes = new List<FileInfo(files[i]);
                FileInfo selectede = new FileInfo(file);

                //이미지 용량 제한하기
                if (selectede.Length > 50000000)
                {
                    return;
                }
                //만약 이미지가 있다면 이미지 불러오기 
                if (!string.IsNullOrEmpty(file))
                {
                    //불러와라.
                    StartCoroutine(LoadImage(file));
                }
        });
    }

  /*  NativeGallery.GetImagesFromGallery((files) =>
        {

            for(int i = 0; i<files.Length; i++)
            {
                //List<FileInfo> selectedes = new List<FileInfo(files[i]);
                FileInfo selectede = new FileInfo(files[i]);

                //이미지 용량 제한하기
                if (selectede.Length > 50000000)
                {
                    return;
                }
                //만약 이미지가 있다면 이미지 불러오기 
                if (!string.IsNullOrEmpty(files[i]))
                {
                    //불러와라.
                    StartCoroutine(LoadImage(i, files[i]));
}

             
            }
        });*/

    IEnumerator LoadImage(string path)
    {
        yield return null;

        byte[] fileData = File.ReadAllBytes(path);
        string filename = Path.GetFileName(path).Split('.')[0];
        string savePath = Application.persistentDataPath + "/lifeingImages";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        File.WriteAllBytes(savePath + filename + ".png", fileData);
        var temp = File.ReadAllBytes(savePath + filename + ".png");
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);

        lifingImg = File.ReadAllBytes(savePath + filename + ".png");
        lifingImgName = Path.GetFileName(savePath + filename + ".png").Split('.')[0];
        image.texture = tex;
        image.SetNativeSize();
        ImageSizeSetting(image, 1000, 1000);

        LifeingImageInfo lifeingImageInfo = new LifeingImageInfo();
        lifeingImageInfo.lifingImage = lifingImg;
        lifeingImageInfo.lifingImageName = lifingImgName;

        //lifeingImageInfo[i].

        lifeingImageList.Add(lifeingImageInfo);
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

    public void OnCilckImageSaveAI()
    {
        //여러개 배열
       /* LifeingImagesData imagesData = new LifeingImagesData();
        imagesData.lifingContent = lifingText.text;
        imagesData.lifingImages = lifeingImageList;*/
        //print(JsonUtility.ToJson(imagesData));
     

        LifeingImageInfoAI lifeingImageInfo = new LifeingImageInfoAI();
        lifeingImageInfo.lifingImage = lifingImg;
        lifeingImageInfo.lifingImageName = lifingImgName;
        
        //PC버전
        //lifeingImageInfo.lifingImage = File.ReadAllBytes(Application.persistentDataPath + "/Resources/02.Story/StoryPhoto/Bar.png");
        //lifeingImageInfo.lifingImageName = Path.GetFileName(Application.persistentDataPath + "/Resources/02.Story/StoryPhoto/Bar.png").Split('.')[0];

        //print(lifeingImageInfo.lifingImageName);

        HttpRequester requester = new HttpRequester();
        //url경로
        requester.url = "http://13.124.225.86:8080/api/v1/lifing/image/ai";
        requester.requestType = RequestType.POST;
       

        //requester.postData = JsonUtility.ToJson(imagesData, true);
        requester.postData = JsonUtility.ToJson(lifeingImageInfo, true);
        print(requester.postData);

        requester.onComplete = OnClickDownload;

        HttpManager.instance.SendRequest(requester);
    }

    public void OnCilckImageSave()
    {
        //여러개 배열
        /* LifeingImagesData imagesData = new LifeingImagesData();
         imagesData.lifingContent = lifingText.text;
         imagesData.lifingImages = lifeingImageList;*/
        //print(JsonUtility.ToJson(imagesData));


        LifeingImageInfo lifeingImageInfo = new LifeingImageInfo();
        lifeingImageInfo.lifingImage = lifingImg;
        lifeingImageInfo.lifingContent = lifingText.text;
        lifeingImageInfo.lifingImageName = lifingImgName;

        //PC버전
        //lifeingImageInfo.lifingImage = File.ReadAllBytes(Application.persistentDataPath + "/Resources/02.Story/StoryPhoto/Bar.png");
        //lifeingImageInfo.lifingImageName = Path.GetFileName(Application.persistentDataPath + "/Resources/02.Story/StoryPhoto/Bar.png").Split('.')[0];

        //print(lifeingImageInfo.lifingImageName);

        HttpRequester requester = new HttpRequester();
        //url경로
        requester.url = "http://13.124.225.86:8080/api/v1/lifing/image";
        requester.requestType = RequestType.POST;


        //requester.postData = JsonUtility.ToJson(imagesData, true);
        requester.postData = JsonUtility.ToJson(lifeingImageInfo, true);
        print(requester.postData);

        requester.onComplete = OnClickDownload;

        HttpManager.instance.SendRequest(requester);
    }

    private void OnClickDownload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        //lifingNo = (int)json["data"]["lifingNo"];
        lifingCategoryNo = (int)json["data"]["lifingCategoryNo"];
        lifingId = (int)json["data"]["lifingId"];


        print("조회 완료");
    }

    public void OnClickLifeingSave()
    {
        //lifingNo = PlayerPrefs.GetInt("lifeingNo");
        LifeingInfo lifeing = new LifeingInfo();
        lifeing.lifingNo = lifingNo;
        lifeing.lifingCategoryNo = lifingCategoryNo;
        lifeing.lifingContent = lifingText.text;
        lifeing.lifingId = lifingId;
        PlayerPrefs.SetInt("lifeingNo", lifeing.lifingNo);

        HttpRequester requester = new HttpRequester();

        requester.url = "http://13.124.225.86:8080/api/v1/lifing";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(lifeing, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);
    }

}
