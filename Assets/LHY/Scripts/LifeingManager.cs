using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[System.Serializable]
public class LifeingItemInfo
{
    public int memberCode;
    public string memberName;
    public string memberId;
    public string avatarImage;
    public string roomImage;
    public int lifingNo;
    public char lifingYn;
}

[System.Serializable]
public class LIfeingData<T>
{
    public List<T> userData;
}


public class LifeingManager : MonoBehaviour
{
    public LifeingItemInfo info;

    public List<LifeingItemInfo> friendList = new List<LifeingItemInfo>();

    public Transform[] hexPos;

    public GameObject llifeingItemFactory;

    public bool isUpLoad = true;
    //public bool end = false;
    private void Awake()
    {
        GetMambersList();
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetPostAll();
     

      

        /*   for (int i = 0; i < 7; i++)
           {
               GameObject Lifeing = Instantiate(llifeingItemFactory, hexPos[i]);

               LifeingRoomItem lifeingRoomItem = Lifeing.GetComponent<LifeingRoomItem>();
               lifeingRoomItem.roomImg = friendList[i].roomImage;
               lifeingRoomItem.avatarImg = friendList[i].avatarImage;
               lifeingRoomItem.memberName.text = friendList[i].memberName;

           }*/
        //StartCoroutine(GetTextureA(avatarImage));


    }

    // Update is called once per frame
    void Update()
    {
        if (isUpLoad == true)
        {


            for (int i = 0; i < friendList.Count; i++)
            {
                if(llifeingItemFactory != null)
                {

                GameObject Lifeing = Instantiate(llifeingItemFactory, hexPos[i]);
                
                LifeingRoomItem lifeingRoomItem = Lifeing.GetComponent<LifeingRoomItem>();
                lifeingRoomItem.roomImg = friendList[i].roomImage;
                lifeingRoomItem.avatarImg = friendList[i].avatarImage;
                lifeingRoomItem.memberName.text = friendList[i].memberName;
                isUpLoad = false;
            }
                }
        }

        /*
                if (isUpLoad == true)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        GameObject Lifeing = Instantiate(llifeingItemFactory, hexPos[i]);
                        LifeingRoomItem lifeingRoomItem = Lifeing.GetComponent<LifeingRoomItem>();

                        //StartCoroutine(GetTextureR(i, lifeingRoomItem.roomImage, lifeingRoomItem.avatarImage));              
                        lifeingRoomItem.memberName.text = friendList[i].memberName;
                        isUpLoad = false;
                    }
                }*/

    }


    public void GetMambersList()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/list/0";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPostAll;


        HttpManager.instance.SendRequest(requester);
    }

    private void OnCompleteGetPostAll(DownloadHandler handler)
    {
        JObject jsonData = JObject.Parse(handler.text);
        string lifingsData = "{\"userData\":" + jsonData["data"]["memberSummary"].ToString() + "}";

        print(lifingsData);

        LIfeingData<LifeingItemInfo> lIfeingInfo = JsonUtility.FromJson<LIfeingData<LifeingItemInfo>>(lifingsData);
        friendList = lIfeingInfo.userData;
       


        //print("¡¶¿ÃΩºø°º≠ ø»" + jsonData);



        // ArrayJson<LifeingItemInfo> lifeingInfo = JsonUtility.FromJson<ArrayJson<LifeingItemInfo>>(mamberList);
    }

    /* IEnumerator GetTextureR(int count, RawImage roomImage, RawImage avatarImage)
     {
         //lifeingRoomItem.roomImage = friendList[i].roomImage
         var urlR = friendList[count].roomImage;
         var urlA = friendList[count].avatarImage;


         UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
         yield return wwwR.SendWebRequest();

         UnityWebRequest wwwA = UnityWebRequestTexture.GetTexture(urlA);
         yield return wwwA.SendWebRequest();

         if (wwwR.result != UnityWebRequest.Result.Success)
             Debug.Log(wwwR.error);
         else
             roomImage.texture = ((DownloadHandlerTexture)wwwR.downloadHandler).texture;

         if (wwwA.result != UnityWebRequest.Result.Success)
             Debug.Log(wwwA.error);
         else
             avatarImage.texture = ((DownloadHandlerTexture)wwwA.downloadHandler).texture;
     }*/

    public void GetPostAll()
    {

    }



}