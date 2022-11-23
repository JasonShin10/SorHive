using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LifeingDetailed : MonoBehaviour
{
    public RawImage Profilephoto;
    public Text IDText;
    public Text NickNameText;

    public RawImage lifeingImage;

    public string profileimg;
    public string Lifeingimg;

    void Start()
    {
        StartCoroutine(GetTextureR(lifeingImage));
    }

    IEnumerator GetTextureR(RawImage LifeingImage)
    {
        
            //lifeingRoomItem.roomImage = friendList[i].roomImage
         
         var urlL = Lifeingimg;

        print(urlL);

 
         UnityWebRequest wwwL = UnityWebRequestTexture.GetTexture(urlL);
         yield return wwwL.SendWebRequest();
      

         if (wwwL.result != UnityWebRequest.Result.Success)
                Debug.Log(wwwL.error);
         else
             LifeingImage.texture = ((DownloadHandlerTexture)wwwL.downloadHandler).texture;

        yield return null;

            //yield return WaitForSeconds(0.1);
    }
}
