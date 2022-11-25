using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LifeingDetailed : MonoBehaviour
{
    public RawImage profilephoto;
    public Text IDText;
    public Text NickNameText;

    public RawImage lifeingImage;

    public string profileimg;
    public string Lifeingimg;

    public GameObject Lifeing_DetailItem;

    void Start()
    {
        StartCoroutine(GetTextureR(lifeingImage, profilephoto));
    }

    IEnumerator GetTextureR(RawImage LifeingImage, RawImage Profilephoto)
    {
        
            //lifeingRoomItem.roomImage = friendList[i].roomImage
         
        var urlL = Lifeingimg;
        var urlP = profileimg;

        print(urlL);

        HttpManager.instance.LoadingCanvas.SetActive(true);
        UnityWebRequest wwwL = UnityWebRequestTexture.GetTexture(urlL);
        yield return wwwL.SendWebRequest();

        UnityWebRequest wwwP = UnityWebRequestTexture.GetTexture(urlP);
        yield return wwwP.SendWebRequest();


        if (wwwL.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwL.error);
            HttpManager.instance.LoadingCanvas.SetActive(false);
        }

        else
        {
            LifeingImage.texture = ((DownloadHandlerTexture)wwwL.downloadHandler).texture;
            HttpManager.instance.LoadingCanvas.SetActive(false);
        }

        if (wwwP.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwP.error);
            HttpManager.instance.LoadingCanvas.SetActive(false);
        }     
        else
        {
            Profilephoto.texture = ((DownloadHandlerTexture)wwwP.downloadHandler).texture;
            HttpManager.instance.LoadingCanvas.SetActive(false);
        }
            

        yield return null;

            //yield return WaitForSeconds(0.1);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
