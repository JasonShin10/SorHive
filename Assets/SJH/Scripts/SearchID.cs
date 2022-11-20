using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.EventSystems;


public class SearchID : MonoBehaviour
{

    public Transform ContentHolder;
    public Transform FollowingContentHolder;
    public GameObject[] Element;
    public GameObject[] FollowingElement;
    public GameObject SearchBar;
    public GameObject IDFactory;
    public GameObject myPage;
    public Button deleteFollowing;
    public Button myPageButton;
    public RawImage Img;
    public Text following;
    public Text follower;
    public Text feedNum;
    public int totalElements;
    public int followId;
    //public Text search;
    public UserInfo userInfo;
    public string id;
    public string roomImgString;
    public bool followingCheck = false;
    public int memberCode;
    public List<UserGetInfo> userInfoList = new List<UserGetInfo>();
    public List<UserGetInfo> userThreeList = new List<UserGetInfo>();

    public List<int> userFollowList;
    public UserGetInfo userGetInfo;
    public RoomImage img;
    public GameObject followingList;

    //public GameObject ContentHolder;

    //public GameObject[] Element;

    //public GameObject SearchBar;

    //public int totalElements;
    // Start is called before the first frame update
    void Start()
    {
        //totalElements = ContentHolder.childCount;
        //Element = new GameObject[totalElements];

        //for (int i = 0; i < totalElements; i++)
        //{
        //    Element[i] = ContentHolder.GetChild(i).gameObject;
        //}
        //OnClickLogin();
        //GetFollower();
        memberCode = HttpManager.instance.memberCode;
        print(memberCode);
        GetThree();
        GetRoomImage();
        GetRoomAll();
        GetUserFollowing();
    }
  
    // Update is called once per frame
    void Update()
    {
        //if(HttpManager.instance.firstId == true)
        //{
        //    StartGet();
        //    HttpManager.instance.firstId = false;
        //}
        //print(HttpManager.instance.id);
        //print(HttpManager.instance.userId);
        if (followingCheck == false)
        {

            if (HttpManager.instance.id == HttpManager.instance.userId)
            {
                myPage.transform.GetChild(2).gameObject.SetActive(true);
                myPage.transform.GetChild(8).gameObject.SetActive(false);
                myPage.transform.GetChild(12).gameObject.SetActive(false);
            }
            else
            {
                myPage.transform.GetChild(2).gameObject.SetActive(false);
                myPage.transform.GetChild(8).gameObject.SetActive(true);
                myPage.transform.GetChild(12).gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            // GetRoomAll();
            //GetRoomImage();
            GetFollower();
            //GetRoomAll();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //OnClickLogin();
        }

    }

    public void OnClickLogin()
    {
        LoginInfo2 logdata = new LoginInfo2();
        logdata.memberId = "john12";
        logdata.password = "qwer1234!";
        HttpManager.instance.userId = logdata.memberId;
        HttpManager.instance.id = logdata.memberId;
        HttpManager.instance.memberCode = 6;
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/auth/login";
        requester.requestType = RequestType.PUT;
        requester.putData = JsonUtility.ToJson(logdata);
        requester.onComplete = OnClickDownload;
        HttpManager.instance.SendRequest(requester);

    }

    private void OnClickDownload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        string token = json["data"]["accessToken"].ToString();
        print(token);

        PlayerPrefs.SetString("token", token);
        print("조회 완료");
    }

    public void GetRoomImage()
    {
        //HttpManager.instance.img = true;
        HttpRequester requester = new HttpRequester();
        //print(HttpManager.instance.memberCode);
        requester.url = "http://52.79.209.232:8080/api/v1/member/" + memberCode;
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetRoomImage;
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "GetRoomImage";
     
            int j = userFollowList.FindIndex(a => a == memberCode);
            if (j == -1)
            {
                UserFollowingCheckUI();
            }
            else
            {
                followingCheck = false;
                return;
            }

        
    }
    public void OnCompleteGetRoomImage(DownloadHandler handler)
    {
        JObject jsonData = JObject.Parse(handler.text);
        string userData = jsonData["data"]["memberRoomImage"].ToString();
        //RoomImage roomImg = JsonUtility.FromJson<RoomImage>(userData);
        roomImgString = userData;
        StartCoroutine(GetTextureR(Img));
    }
    IEnumerator GetTextureR(RawImage roomImage)
    {
        //lifeingRoomItem.roomImage = friendList[i].roomImage
        var urlR = roomImgString;

        UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
        yield return wwwR.SendWebRequest();

        if (wwwR.result != UnityWebRequest.Result.Success)
            Debug.Log(wwwR.error);
        else
            roomImage.texture = ((DownloadHandlerTexture)wwwR.downloadHandler).texture;
    }


    public void GetThree()
    {
        HttpRequester requester = new HttpRequester();
        print(memberCode);
        requester.url = "http://52.79.209.232:8080/api/v1/member/" + memberCode;

        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetThree;
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "GetThree";
    }
    public void OnCompleteGetThree(DownloadHandler handler)
    {
        //print(2);
        sHandler = handler.text;
        //print(sHandler);
        JObject jsonData = JObject.Parse(sHandler);
        //JArray jarry = jsonData["data"]["furnitures"].ToObject<JArray>();

        //for(int i = 0; i < jarry.Count; i++)
        //{
        //    ObjectInfo info = new ObjectInfo();

        //    info.wallNumber = jarry[i]["wallNumber"].ToObject<int>();

        //    objectInfoList.Add(info);
        //}

        //int status = jsonData["status"].ToObject<int>();
        string userData = jsonData["data"].ToString();
        UserGetInfo userThree = JsonUtility.FromJson<UserGetInfo>(userData);
        follower.text = "팔로워" + " " + userThree.followerCount;
        following.text = "팔로잉" + " " + userThree.followingCount;
        feedNum.text = "게시물" + " " + userThree.feedCount;
        HttpManager.instance.memberCode = userThree.memberCode;
        userGetInfo.followingCount = userThree.followingCount;
        userGetInfo.feedCount = userThree.feedCount;
        print(userData);

        print("조회완료");
    }
    public void GetRoomAll()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/search/j";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetRoomAll;
        requester.requestName = "GetRoomAll";
        HttpManager.instance.SendRequest(requester);
    }
    string sHandler;
    public void OnCompleteGetRoomAll(DownloadHandler handler)
    {
        print(2);
        sHandler = handler.text;
        print(sHandler);
        JObject jsonData = JObject.Parse(sHandler);
        //JArray jarry = jsonData["data"]["furnitures"].ToObject<JArray>();

        //for(int i = 0; i < jarry.Count; i++)
        //{
        //    ObjectInfo info = new ObjectInfo();

        //    info.wallNumber = jarry[i]["wallNumber"].ToObject<int>();

        //    objectInfoList.Add(info);
        //}

        //int status = jsonData["status"].ToObject<int>();
        string userData = "{\"data\":" + jsonData["data"].ToString() + "}";
        ArrayJsonID<UserGetInfo> userInfo = JsonUtility.FromJson<ArrayJsonID<UserGetInfo>>(userData);
        userInfoList = userInfo.data;
     
        print(userInfo);
        for (int i = 0; i < userInfoList.Count; i++)
        {
            CreateObject(userInfoList[i], ContentHolder, userInfoList[i].id);
        }
        totalElements = ContentHolder.childCount;
        Element = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.GetChild(i).gameObject;
            ContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(OnClickVisit);
        }
        print("조회완료");
    }
    public void CreateObject(UserGetInfo info, Transform Content, string id)
    {
        //search.text = info.id;
        GameObject idImage = Instantiate(IDFactory, Content);
        IdImageItem idImageItem = idImage.GetComponent<IdImageItem>();
        idImageItem.id.text = id;

        idImageItem.memberCode.text = info.memberCode.ToString();
        //memberCode = info.memberCode;
        idImage.gameObject.SetActive(false);
    }

    public void Search()
    {
        
        //totalElements = ContentHolder.childCount;
        //Element = new GameObject[totalElements];
        //for (int i = 0; i < totalElements; i++)
        //{
        //    Element[i] = ContentHolder.GetChild(i).gameObject;
        //    ContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(SceneLoad);
        //}
        string searchText = SearchBar.GetComponent<InputField>().text;
        int searchTextLength = searchText.Length;

        int searchedElements = 0;

        foreach (GameObject ele in Element)
        {
            searchedElements += 1;
            //ele.transform.GetComponent<Button>().onClick.AddListener(SceneLoad);
            if (ele.transform.GetChild(0).GetComponent<Text>().text.Length >= searchTextLength)
            {
                if (searchText.Length == 0)
                {
                    ele.SetActive(false);
                }
                else
                {

                    if (searchText == ele.transform.GetChild(0).GetComponent<Text>().text.Substring(0, searchTextLength))
                    {
                        ele.SetActive(true);
                    }
                    else
                    {
                        ele.SetActive(false);
                    }
                }
            }
        }
    }
    public void GetUserFollowing()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/following/" + HttpManager.instance.userMemberCode;
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetUserFollowing;
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "GetUserMember";
    }
    public void OnCompleteGetUserFollowing(DownloadHandler handler)
    {

        sHandler = handler.text;
        print(sHandler);
        JObject jsonData = JObject.Parse(sHandler);

        string userData = "{\"data\":" + jsonData["data"]["followerData"].ToString() + "}";
        ArrayJsonID<UserGetInfo> userInfo = JsonUtility.FromJson<ArrayJsonID<UserGetInfo>>(userData);
        userInfoList = userInfo.data;

        print(userInfo);
        for (int i = 0; i < userInfoList.Count; i++)
        {
            userFollowList.Add(userInfoList[i].memberCode);
        }
        //totalElements = ContentHolder.childCount;
        //Element = new GameObject[totalElements];

        //for (int i = 0; i < totalElements; i++)
        //{
        //    Element[i] = ContentHolder.GetChild(i).gameObject;
        //    ContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(OnClickVisit);
        //}
        print("조회완료");
    }
    public void GetMember()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/" + id;
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetMember;
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "GetMember";
    }
    public void OnCompleteGetMember(DownloadHandler handler)
    {
        print(2);
        sHandler = handler.text;
        print(sHandler);
        JObject jsonData = JObject.Parse(sHandler);

        string userData = "{\"data\":" + jsonData["data"].ToString() + "}";
        ArrayJsonID<UserGetInfo> userInfo = JsonUtility.FromJson<ArrayJsonID<UserGetInfo>>(userData);
     
        print(userInfo);
        for (int i = 0; i < userInfoList.Count; i++)
        {
            CreateObject(userInfoList[i], ContentHolder, userInfoList[i].id);
        }
        totalElements = ContentHolder.childCount;
        Element = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.GetChild(i).gameObject;
            ContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(OnClickVisit);
        }
        print("조회완료");
    }

    public void OnClickVisit()
    {
        myPageButton.onClick.Invoke();
        //myPage.transform.GetChild(2).gameObject.SetActive(false);
        //myPage.transform.GetChild(8).gameObject.SetActive(true);
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.GetComponentInChildren<Text>().text);
        //id = clickObject.GetComponentInChildren<Text>().text;
        id = clickObject.transform.GetChild(0).GetComponent<Text>().text;
        memberCode = int.Parse(clickObject.transform.GetChild(1).GetComponent<Text>().text);
        GetRoomImage();
        //StartCoroutine(GetTextureR(Img));
        GetThree();
        //GetRoomImage();
        HttpManager.instance.id = id;
        HttpManager.instance.fakeId = id;
        HttpManager.instance.memberCode = memberCode;
    }

    public void OnClickFollowingVisit()
    {
        myPageButton.onClick.Invoke();
       
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.GetComponentInChildren<Text>().text);
  
        id = clickObject.transform.GetChild(0).GetComponent<Text>().text;
        memberCode = int.Parse(clickObject.transform.GetChild(1).GetComponent<Text>().text);
        for (int i = 0; i < userFollowList.Count; i++)
        {
            int j = userFollowList.FindIndex(memberCode => memberCode == userFollowList.Count);
            if (j == -1)
            {
                UserFollowingCheckUI();
            }
            else
            {
                return;
            }
 
        }
        GetRoomImage();
        GetThree();
        HttpManager.instance.id = id;
        HttpManager.instance.fakeId = id;
        HttpManager.instance.memberCode = memberCode;
    }

    public void UserFollowingCheckUI()
    {
        followingCheck = true;
        myPage.transform.GetChild(2).gameObject.SetActive(false);
        myPage.transform.GetChild(8).gameObject.SetActive(false);
        myPage.transform.GetChild(12).gameObject.SetActive(true);
    }

    public void OnClickFollowing()
    {
        //OnSaveSignIn();
    }
    public void OnSaveSignIn()
    {
        UserGetInfo info = new UserGetInfo();
        info.id = id;
        //info.memberCode = memberCode;

        //arrayJson.furnitures = objectInfoList;
        //서버에 게시물 조회 요청(/posts/1 , Get)
        HttpRequester requester = new HttpRequester();
        /// POST, 완료되었을 때 호출되는 함수
        requester.url = "http://52.79.209.232:8080/api/v1/follow/" + memberCode;
        requester.requestType = RequestType.POST;
        //post data 셋팅
        requester.postData = JsonUtility.ToJson(info, true);
        requester.onComplete = OnCompleteSignIn;
        //HttpManager에게 요청
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "OnSaveSignIn";
    }

    public void GetFollowing()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/following/" + memberCode;
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetFollowing;
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "GetFollowing";
    }

    public void OnCompleteGetFollowing(DownloadHandler handler)
    {

        sHandler = handler.text;

        JObject jsonData = JObject.Parse(sHandler);
        string userData = "{\"followerData\":" + jsonData["data"]["followerData"].ToString() + "}";
        ArrayJsonID<UserGetInfo> userInfo = JsonUtility.FromJson<ArrayJsonID<UserGetInfo>>(userData);
        //followId = jsonData["data"]["followerData"][0]["followSummary"]["followId"].ToObject<int>();
        userInfoList = userInfo.followerData;

        totalElements = FollowingContentHolder.childCount;
        FollowingElement = new GameObject[totalElements];
        for (int i = 0; i < totalElements; i++)
        {
            FollowingElement[i] = FollowingContentHolder.GetChild(i).gameObject;

            Destroy(FollowingContentHolder.GetChild(i).gameObject);
        }
        print(userInfo);
        for (int i = 0; i < userInfoList.Count; i++)
        {
            CreateObject(userInfoList[i], FollowingContentHolder, userInfoList[i].memberId);
        }
        totalElements = FollowingContentHolder.childCount;
        FollowingElement = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            FollowingElement[i] = FollowingContentHolder.GetChild(i).gameObject;
            FollowingContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(OnClickFollowingVisit);
            FollowingContentHolder.GetChild(i).gameObject.SetActive(true);
        }

        print("조회완료");
    }
    public void GetFollower()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/follower/" + memberCode;
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetFollower;
        HttpManager.instance.SendRequest(requester);
        requester.requestName = "GetFollower";
    }
    public void OnCompleteGetFollower(DownloadHandler handler)
    {
        sHandler = handler.text;
        JObject jsonData = JObject.Parse(sHandler);
        string userData = "{\"followerData\":" + jsonData["data"]["followerData"].ToString() + "}";
        //followId = jsonData["data"]["followerData"]["followSummary"]["followId"].ToObject<int>();
        ArrayJsonID<UserGetInfo> userInfo = JsonUtility.FromJson<ArrayJsonID<UserGetInfo>>(userData);

        userInfoList = userInfo.followerData;

        totalElements = FollowingContentHolder.childCount;
        FollowingElement = new GameObject[totalElements];
        for (int i = 0; i < totalElements; i++)
        {
            FollowingElement[i] = FollowingContentHolder.GetChild(i).gameObject;

            Destroy(FollowingContentHolder.GetChild(i).gameObject);
        }
        print(userInfo);
        for (int i = 0; i < userInfoList.Count; i++)
        {
            CreateObject(userInfoList[i], FollowingContentHolder, userInfoList[i].memberId);
        }
        totalElements = FollowingContentHolder.childCount;
        FollowingElement = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            FollowingElement[i] = FollowingContentHolder.GetChild(i).gameObject;
            FollowingContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(OnClickFollowingVisit);
            FollowingContentHolder.GetChild(i).gameObject.SetActive(true);
        }
        print("조회완료");
    }

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        print(handler);
        //string s = "{\"furniture\":" + handler.text + "}";
        //PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);
    }

    public void OnRoomIn()
    {
        HttpManager.instance.id = HttpManager.instance.fakeId;
        LobbyManager.instence.OnClickRoomIn();
    }

    public void OnClickIdReset()
    {
        memberCode = HttpManager.instance.userMemberCode;
        //GetRoomImage();
        //GetThree();
        //GetRoomAll();
        followingCheck = false;
        HttpManager.instance.id = HttpManager.instance.userId;
        followingList.SetActive(false);
    }

    public void OnClickRoomImage()
    {
        //GetRoomImage();
        //GetThree();
        //StartCoroutine(GetTextureR(Img));
    }

    public void OnClickFollowingList()
    {
        followingList.SetActive(true);

    }

    public void OnclickDeleteFollowing()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/follower/" + memberCode;
        requester.requestType = RequestType.DELETE;
        requester.onComplete = OnCompleteGetFollower;
        requester.requestName = "OnclickDeleteFollowing()";
        HttpManager.instance.SendRequest(requester);
    }

}
