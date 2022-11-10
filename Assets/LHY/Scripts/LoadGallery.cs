using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AvatarImageInfo
{
    public byte[] avatarImage;
}

public class LoadGallery : MonoBehaviour
{
    public RawImage image;

    public byte[] avatarImg;

    public void OnClickImageLoad()
    {
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
        image.texture = tex; 
    }

    public void OnCilckImageSave()
    {
        AvatarImageInfo avatarImageInfo = new AvatarImageInfo();
        avatarImageInfo.avatarImage = File.ReadAllBytes(Application.dataPath + "/Resources/01.Pictures/human1.png");
        //avatarImageInfo.avatarImage = avatarImg;

        HttpRequester requester = new HttpRequester();
        //url경로
        requester.url = "http://13.125.174.193:8080/api/v1/avatar/image";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(avatarImageInfo);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);
        print("sucssasSand");
    }
}
