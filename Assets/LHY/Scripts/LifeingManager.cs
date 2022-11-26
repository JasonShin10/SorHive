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
    public int lifingCategoryNo;
    public string lifingYn;
}

[System.Serializable]
public class LIfeingData<T>
{
    public List<T> userData;
    public List<T> LifeingImagesData;
}


public class LifeingManager : MonoBehaviour
{
    //public LifeingItemInfo info;

    public LifeingManager instance;

    public List<LifeingItemInfo> friendList = new List<LifeingItemInfo>();

    public Transform[] hexPos;

    public GameObject llifeingItemFactory;

    public GameObject PageUp;

    public GameObject PageDown;

    public GameObject[] Group;

    public GameObject main;

    public GameObject LifeingDelete;

    public Text roomId;

    public GameObject[] names;

    public GameObject[] OnOff;

    public int countgroup;
    public int ListCount;

    public bool isUpLoad = true;
    public bool userSetting = false;

    public bool memberIdsee = true;

    //public bool end = false;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetMambersList();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetMambersList();
        }


        if (isUpLoad == true)
        {
            
            names = new GameObject[friendList.Count];
            for (int i = 0; i < friendList.Count; i++)
            {
               
                //print(friendList.Count);
                GameObject Lifeing = Instantiate(llifeingItemFactory, hexPos[i]);

                LifeingRoomItem lifeingRoomItem = Lifeing.GetComponent<LifeingRoomItem>();
                lifeingRoomItem.memberCode = friendList[i].memberCode;
                lifeingRoomItem.memberName.text = friendList[i].memberName;
                lifeingRoomItem.memberId.text = friendList[i].memberId;
                lifeingRoomItem.avatarImg = friendList[i].avatarImage;
                lifeingRoomItem.roomImg = friendList[i].roomImage;
                lifeingRoomItem.roomImageprofile = friendList[i].roomImage;

                lifeingRoomItem.lifingCategoryNo = friendList[i].lifingCategoryNo;
                lifeingRoomItem.lifingNo = friendList[i].lifingNo;
                lifeingRoomItem.lifingYn = friendList[i].lifingYn;

                //names[0] = GameObject.Find("RoomPos0").transform.Find("LifeingItem");
                names[i] = lifeingRoomItem.LifeingName;

               //lifeingRoomItem.

               roomId.text = friendList[0].memberId;

                isUpLoad = false;
            }

            UpDownbutton();
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

    public void ReLoadMambersList()
    {
        Destroy(GameObject.Find("LifeingItem(Clone)"));
        print("a/Clone");
    
        GetMambersList();
        //main.SetActive(false);
        isUpLoad = true;
    }


    public void GetMambersList()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/list/" + ListCount.ToString();
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPostAll;
        requester.requestName = "GetMambersList";

        HttpManager.instance.SendRequest(requester);
        HttpManager.instance.secondId = false;


    }

    public void OnCompleteGetPostAll(DownloadHandler handler)
    {
        JObject jsonData = JObject.Parse(handler.text);
        string lifingsData = "{\"userData\":" + jsonData["data"]["memberSummary"].ToString() + "}";

        print(lifingsData);

        LIfeingData<LifeingItemInfo> lIfeingInfo = JsonUtility.FromJson<LIfeingData<LifeingItemInfo>>(lifingsData);
        friendList = lIfeingInfo.userData;


        //print("¡¶¿ÃΩºø°º≠ ø»" + jsonData);



        // ArrayJson<LifeingItemInfo> lifeingInfo = JsonUtility.FromJson<ArrayJson<LifeingItemInfo>>(mamberList);
    }

  
    public void GetPostAll()
    {

    }


    public void UpDownbutton()
    {
        
        if(friendList.Count <= 7)
        {
            PageUp.SetActive(false);
            PageDown.SetActive(false);
        }
        else if(friendList.Count >= 8 && friendList.Count < 14)
        {
            if(countgroup <= 0)
            {
                PageUp.SetActive(false);
                PageDown.SetActive(true);
            }
            if (countgroup == 1)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(false);
            }

        }
        else if (friendList.Count >= 14 && friendList.Count < 20)
        {
            if (countgroup <= 0)
            {
                PageUp.SetActive(false);
                PageDown.SetActive(true);
            }
            if (countgroup == 1)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(true);
            }
            if (countgroup == 2)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(false);
            }

        }
        else if (friendList.Count >= 20 && friendList.Count < 26)
        {
            if (countgroup <= 0)
            {
                PageUp.SetActive(false);
                PageDown.SetActive(true);
            }
            if (countgroup == 1)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(true);
            }
            if (countgroup == 2)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(true);
            }
            if (countgroup == 3)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(false);
            }

        }
        else if (friendList.Count >= 26 && friendList.Count < 32)
        {
            if (countgroup <= 0)
            {
                PageUp.SetActive(false);
                PageDown.SetActive(true);
            }
            if (countgroup == 1)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(true);
            }
            if (countgroup == 2)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(true);
            }
            if (countgroup == 3)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(true);
            }
            if (countgroup == 4)
            {
                PageUp.SetActive(true);
                PageDown.SetActive(false);
            }

        }
    }


    public void GroupCountDown()
    {
        countgroup++;
        UpDownbutton();
        if(countgroup > 5)
        {
            ListCount++;
            ReLoadMambersList();
            countgroup = 0;
        }
        for(int i = 0; i < Group.Length; i++)
        {
            Group[i].SetActive(false);
        }
        Group[countgroup].SetActive(true);

    }
    public void GroupCountUP()
    {
        countgroup--;
        UpDownbutton();
        if (countgroup <= 0)
        {
            countgroup = 0;
        }
        for (int i = 0; i < Group.Length; i++)
        {
            Group[i].SetActive(false);
        }
        Group[countgroup].SetActive(true);

    }

    public void OnClickMemberID()
    {
        if (memberIdsee == true)
        {
            for (int i = 0; i < friendList.Count; i++)
            {
                //names[0].SetActive(false);
                names[i].SetActive(false);
                memberIdsee = false;
                OnOff[0].SetActive(false);
                OnOff[1].SetActive(true);
            }
        }
        else if (memberIdsee == false)
        {
            for (int i = 0; i < friendList.Count; i++)
            {
                //names[0].SetActive(true);
                names[i].SetActive(true);
                memberIdsee = true;
                OnOff[0].SetActive(true);
                OnOff[1].SetActive(false);
            }
        }
        
    }

}