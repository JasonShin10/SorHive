using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

public class LifeingRoomItem : MonoBehaviour
{

    public int memberCode;
    public Text memberName;
    public Text memberId;
    public Text detailID;
    public RawImage avatarImage;
    public RawImage roomImage;
  
    public GameObject lifeingisTrue;


    public string lifingYn;
    public int lifingCategoryNo;
    public int lifingNo;

    public string roomImg;
    public string avatarImg;

    public string roomImageprofile;
    public string lifingConetent;

    public string profileImage;
    public string lifeingImage;

    public GameObject LifeingDetailed;

    public GameObject LifeingName;

    public Transform LDPos;

    public LifeingManager lifeingManager;

    public bool LifeingLoad = false;

    public bool roomY = false;

    public bool LifeingDetails = false;

    // Start is called before the first frame update
    void Start()
    {
        // loadingCanvas = GameObject.Find("LoadingCanvas").gameObject;
        // loadingCanvas.SetActive(false);
        GameObject lifeingmanager = GameObject.Find("LifeingPosManager");
        lifeingManager = lifeingmanager.GetComponent<LifeingManager>();
        StartCoroutine(GetTextureR(roomImage, avatarImage));

        //LifeingDetailed = GameObject.Find("Lifeing_Item");
        

        GameObject LDManager = GameObject.Find("LifeingDetailedCanvas");

        LDPos = LDManager.transform;

        detailID.text = memberId.text;

        
        lifeingManager.names[0] = LifeingName; 
        //for()
    }

    // Update is called once per frame
    void Update()
    {

        if (LifeingDetails == true)
        {
            GameObject lifeingDetail = Instantiate(LifeingDetailed, LDPos);

            LifeingDetailed lifeingDetailed = lifeingDetail.GetComponent<LifeingDetailed>();
            lifeingDetailed.profileimg = roomImg;
            lifeingDetailed.IDText.text = detailID.text;
            lifeingDetailed.NickNameText.text = memberName.text;
            lifeingDetailed.Lifeingimg = lifeingImage;
            lifeingDetailed.memberCode = memberCode;
            lifeingDetailed.LifeingContent.text = lifingConetent;

            LifeingDetails = false;
        }

        if (lifingYn == "Y")
        {
            if (LifeingLoad == false)
            {
                roomY = true;
               
                //Texture2D tex = new Texture2D(0, 0);
                //Resources.Load("/ 02.Story / StoryRoom / " + lifingCategoryNo + "_" + lifingNo + ".png");
  
                if(!Directory.Exists(Application.persistentDataPath + "/Resources/RoomImages/"))
                {
                    return;
                }
                else
                {
                    if (lifingCategoryNo >= 0 && lifingNo >= 0)
                    {
                        var temp = File.ReadAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + lifingCategoryNo + "_" + lifingNo + ".png");
                        print(lifingCategoryNo + ("???????? ????") + lifingNo + ("?????? ?????? ????"));
                        //tex = Resources.Load("02.Story / StoryRoom /" + lifingCategoryNo + "_" + lifingNo + ".png", typeof(Texture2D)) as Texture2D;
                        print("?????? ???????? ????!!");
                        Texture2D tex = new Texture2D(0, 0);
                        tex.LoadImage(temp);

                        //tex.LoadImage(temp);
                        lifeingisTrue.SetActive(true);
                        roomImage.texture = tex;
                        LifeingLoad = true;
                    }                  
                }
                
                
              
                       
            }

        }
        else
        {
            lifeingisTrue.SetActive(false);
            return;
        }

 
        

    }

    IEnumerator GetTextureR(RawImage roomImage, RawImage avatarImage)
    {
        
            //lifeingRoomItem.roomImage = friendList[i].roomImage
            var urlR = roomImg;
            var urlA = avatarImg;


            UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
            yield return wwwR.SendWebRequest();

            UnityWebRequest wwwA = UnityWebRequestTexture.GetTexture(urlA);
            yield return wwwA.SendWebRequest();

        if (roomY == false)
        {
            if (wwwR.result != UnityWebRequest.Result.Success)
                Debug.Log(wwwR.error);
            else
                roomImage.texture = ((DownloadHandlerTexture)wwwR.downloadHandler).texture;
        }
      

            if (wwwA.result != UnityWebRequest.Result.Success)
                Debug.Log(wwwA.error);
            else
                avatarImage.texture = ((DownloadHandlerTexture)wwwA.downloadHandler).texture;

        yield return null;

            //yield return WaitForSeconds(0.1);
    }

    public GameObject loadingCanvas;
    public void OnClickStoryView()
    {
        // loadingCanvas.SetActive(true);
        print(memberCode);
        HttpRequester requester = new HttpRequester();
        requester.url = "http://13.124.225.86:8080/api/v1/lifing/"+ memberCode.ToString();
        requester.requestType = RequestType.GET;
        //requester.onComplete = OnCompleteGetPostAll;
        requester.requestName = "GetLifeingDetails";
        requester.onComplete = OnCompleteGetLifeDetails;

        HttpManager.instance.SendRequest(requester);
        //HttpManager.instance.secondId = false;       
    }

   

    public void OnCompleteGetLifeDetails(DownloadHandler handler)
    {
        JObject jsonData = JObject.Parse(handler.text);
        string lifingsDetailData = jsonData["data"]["lifingImagePath"]["lifingImagePath"].ToString();
        print(lifingsDetailData);

        string lifeingConetent = jsonData["data"]["lifingSummary"]["lifingConetent"].ToString();

        lifeingImage = lifingsDetailData;
        lifingConetent = lifeingConetent;
        LifeingDetails = true;
        // loadingCanvas.SetActive(false);
    }

    public void OnClickReload()
    {
        LifeingLoad = false;
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}