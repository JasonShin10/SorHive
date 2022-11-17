using Newtonsoft.Json.Linq;
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


[System.Serializable]
public class LifeingInfo
{
    public int lifingNo;
    public int lifingCategoryNo;
    public string lifingContent;
}



public class LifeingItem : MonoBehaviour
{
    public RawImage[] image;

    public byte[] lifingImg;
    public string[] lifingImgName;

    public int lifingNo = 1;
    public int lifingCategoryNo = -1;
    public InputField lifingText;

    public List<LifeingImageInfo> lifeingImageList = new List<LifeingImageInfo>();

    public void OnClickImageLoad()
    {
        //�������� ����
        NativeGallery.GetImagesFromGallery((files) =>
        {
            for (int i = 0; i < files.Length; i++)
            {
                //List<FileInfo> selectedes = new List<FileInfo(files[i]);
                FileInfo selectede = new FileInfo(files[i]);

                //�̹��� �뷮 �����ϱ�
                if (selectede.Length > 50000000)
                {
                    return;
                }
                //���� �̹����� �ִٸ� �̹��� �ҷ����� 
                if (!string.IsNullOrEmpty(files[i]))
                {
                    //�ҷ��Ͷ�.
                    StartCoroutine(LoadImage(i, files[i]));
                }
            }
        });
    }

    IEnumerator LoadImage(int i, string path)
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
        lifingImgName[i] = filename;
        image[i].texture = tex;
    }

    public void OnCilckImageSave()
    {

        LifeingImageInfo lifeingImageInfo = new LifeingImageInfo();
        lifeingImageInfo.lifingImage = File.ReadAllBytes(Application.dataPath + "/Resources/02.Story/StoryPhoto/Bar.png");
        lifeingImageInfo.lifingImageName = Path.GetFileName(Application.dataPath + "/Resources/02.Story/StoryPhoto/Bar.png").Split('.')[0];

        print(lifeingImageInfo.lifingImageName);

        HttpRequester requester = new HttpRequester();
        //url���
        requester.url = "http://52.79.209.232:8080/api/v1/lifing/image";
        requester.requestType = RequestType.POST;

        lifeingImageList.Add(lifeingImageInfo);

        // LIfeingData<LifeingImageInfo> lIfeingImagesData = JsonUtility.ToJson<LIfeingData<LifeingImageInfo>>(lifeingImageInfo);
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
        print("��ȸ �Ϸ�");
    }

    public void OnClickLifeingSave()
    {
        lifingNo = PlayerPrefs.GetInt("lifeingNo");
        LifeingInfo lifeing = new LifeingInfo();
        lifeing.lifingNo = lifingNo;
        lifeing.lifingCategoryNo = lifingCategoryNo;
        lifeing.lifingContent = lifingText.text;
        PlayerPrefs.SetInt("lifeingNo", lifeing.lifingNo);

        HttpRequester requester = new HttpRequester();

        requester.url = "http://52.79.209.232:8080/api/v1/lifing";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(lifeing, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);
    }

}
