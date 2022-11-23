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

    public string profileImage;
    public string lifeingImage;

    public GameObject LifeingDetailed;

    public Transform LDPos;

    public LifeingManager lifeingManager;

    public bool LifeingLoad = false;

    public bool roomY = false;

    public bool LifeingDetails = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject lifeingmanager = GameObject.Find("LifeingPosManager");
        lifeingManager = lifeingmanager.GetComponent<LifeingManager>();
        StartCoroutine(GetTextureR(roomImage, avatarImage));

        //LifeingDetailed = GameObject.Find("Lifeing_Item");

        GameObject LDManager = GameObject.Find("LifeingDetailedCanvas");

        LDPos = LDManager.transform;

        detailID.text = memberId.text; 
        //for()
    }

    // Update is called once per frame
    void Update()
    {      
        if (lifingYn == "Y")
        {
            if (LifeingLoad == false)
            {
                roomY = true;
                print("라이핑 룸스토리 있음!!");
                //Texture2D tex = new Texture2D(0, 0);
                //Resources.Load("/ 02.Story / StoryRoom / " + lifingCategoryNo + "_" + lifingNo + ".png");
                try
                {
                    LifeingLoad = true;
                    var temp = File.ReadAllBytes(Application.dataPath + "/Resources/RoomImages/" + lifingCategoryNo + "_" + lifingNo + ".png");
                    print(lifingCategoryNo + ("카테고리 번호") + lifingNo + ("라이핑 이미지 번호"));
                    //tex = Resources.Load("02.Story / StoryRoom /" + lifingCategoryNo + "_" + lifingNo + ".png", typeof(Texture2D)) as Texture2D;

                    Texture2D tex = new Texture2D(0, 0);
                    tex.LoadImage(temp);

                    //tex.LoadImage(temp);
                    lifeingisTrue.SetActive(true);
                    roomImage.texture = tex;
                }
                catch (Exception ex)
                {
                    print("경로에 무거ㅏ없습니다.");
                    print(ex);
                    throw;
                }
            }

        }
        else
        {
            lifeingisTrue.SetActive(false);
            return;
        }

        if(LifeingDetails == true)
        {
            GameObject lifeingDetail = Instantiate(LifeingDetailed, LDPos);

            LifeingDetailed lifeingDetailed = lifeingDetail.GetComponent<LifeingDetailed>();
            lifeingDetailed.Profilephoto.texture = roomImage.texture;
            lifeingDetailed.IDText.text = detailID.text;
            lifeingDetailed.NickNameText.text = memberName.text;
            lifeingDetailed.Lifeingimg = lifeingImage;

            LifeingDetails = false;
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
    public void OnClickStoryView()
    {
        print(memberCode);
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/lifing/"+ memberCode.ToString();
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

        lifeingImage = lifingsDetailData;
        LifeingDetails = true;
    }
}