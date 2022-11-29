using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LifeingDetailed : MonoBehaviour
{
    
    public Text IDText;
    public Text NickNameText;

    public RawImage profilephoto;
    public RawImage lifeingImage;

    public string profileimg;
    public string Lifeingimg;
    public Text LifeingContent;

    public GameObject Lifeing_DetailItem;

    public SearchID searchID;

    public int memberCode;

    void Start()
    {
        StartCoroutine(GetTextureR(lifeingImage, profilephoto));

        searchID = GameObject.Find("SearchManager").GetComponent<SearchID>();
    }

    IEnumerator GetTextureR(RawImage LifeingImage, RawImage Profilephoto)
    {
        
            //lifeingRoomItem.roomImage = friendList[i].roomImage
         
        var urlL = Lifeingimg;
        var urlP = profileimg;

        print(urlL);
        print(urlP);

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



    public void OnClickLifeingVisit()
    {
        //myPage.transform.GetChild(2).gameObject.SetActive(false);
        //myPage.transform.GetChild(8).gameObject.SetActive(true);
        //GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        //print(clickObject.GetComponentInChildren<Text>().text);
        //id = clickObject.GetComponentInChildren<Text>().text;

        searchID.id = IDText.text;
        searchID.memberCode = memberCode;
        //followId = int.Parse(clickObject.transform.GetChild(2).GetComponent<Text>().text);
        searchID.GetRoomImage();
        //StartCoroutine(GetTextureR(Img));
        searchID.GetThree();
        //GetRoomImage();
        HttpManager.instance.id = searchID.id;
        HttpManager.instance.fakeId = searchID.id;
        HttpManager.instance.memberCode = searchID.memberCode;
        searchID.myPageButton.onClick.Invoke();
        DestroyMe();
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}