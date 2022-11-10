using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class avatarImageInfo
{
    public string avatarImage;
}

public class LoadGallery : MonoBehaviour
{
    public RawImage image;

    public void OnClickImageLoad()
    {
        NativeGallery.GetImageFromGallery((file) =>
        {
            FileInfo selected = new FileInfo(file);

            //�̹��� �뷮�����ϱ�
            if(selected.Length > 50000000)
            {
                return;
            }

            //�ҷ�����
            if(!string.IsNullOrEmpty(file))
            {
                //�ҷ��Ͷ�.
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

        image.texture = tex;
    }

    public void OnCilckImageSave()
    {
        
    }
}
