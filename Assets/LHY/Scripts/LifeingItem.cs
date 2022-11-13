using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class LifeingImageInfo
{
    public byte[] lifingImage;
    public string lifingImageName;
}

public class LifeingInfo
{
    public int lifingNo;
    public string lifingContent;
}



public class LifeingItem : MonoBehaviour
{
    public RawImage image;

    public byte[] lifingImg;
    public string lifingImgName;

    private int lifingNo = 1;
    public InputField lifingText;

    

    public void OnClickImageLoad()
    {
        //갤러리를 연다
        NativeGallery.GetImageFromGallery((file) =>
        {
            FileInfo selected = new FileInfo(file);

            //이미지 용량 제한하기
            if(selected.Length > 50000000)
            {
                return;
            }
            //만약 이미지가 있다면 이미지 불러오기 
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

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        File.WriteAllBytes(savePath + filename + ".png", fileData);

        var temp = File.ReadAllBytes(savePath + filename + ".png");

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);

        lifingImg = File.ReadAllBytes(savePath + filename + ".png");
        lifingImgName = filename;
        image.texture = tex;
    }

    public void OnCilckImageSave()
    {
        LifeingImageInfo lifeingImageInfo = new LifeingImageInfo();
        lifeingImageInfo.lifingImage = File.ReadAllBytes(Application.dataPath + "/Resources/02.Story/StoryPhoto/Bar.png");
        lifeingImageInfo.lifingImageName = Path.GetFileName(Application.dataPath + "/Resources/02.Story/StoryPhoto/Bar.png").Split('.')[0];

        print(lifeingImageInfo.lifingImageName);

        HttpRequester requester = new HttpRequester();
        //url경로
        requester.url = "http://13.125.174.193:8080/api/v1/lifing/image";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(lifeingImageInfo, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);

    }

    public void OnClickLifeingSave()
    {
        lifingNo = PlayerPrefs.GetInt("lifeingNo");
        LifeingInfo lifeing = new LifeingInfo();
        lifeing.lifingNo = lifingNo++;
        lifeing.lifingContent = lifingText.text;
        PlayerPrefs.SetInt("lifeingNo", lifeing.lifingNo);

        HttpRequester requester = new HttpRequester();

        requester.url = "http://13.125.174.193:8080/api/v1/lifing";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(lifeing, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);
    }

}
