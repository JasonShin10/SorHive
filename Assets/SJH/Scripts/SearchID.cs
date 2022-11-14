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
    public GameObject[] Element;
    public GameObject SearchBar;
    public GameObject IDFactory;
    public GameObject myPage;
    public Button myPageButton;
    public RawImage img;
    public Text following;
    public Text follower;
    public Text feedNum;
    public int totalElements;
    //public Text search;
    public UserInfo userInfo;
    public string id;
    public int memberCode;
    public List<UserGetInfo> userInfoList = new List<UserGetInfo>();
    public List<UserGetInfo> userThreeList = new List<UserGetInfo>();
    public UserGetInfo userGetInfo;

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
        GetThree();
        //GetRoomImage();
        GetFollower();
        GetRoomAll();
    }

    // Update is called once per frame
    void Update()
    {
        //print(HttpManager.instance.id);
        //print(HttpManager.instance.userId);
        if(HttpManager.instance.id == HttpManager.instance.userId)
        {
            myPage.transform.GetChild(2).gameObject.SetActive(true);
            myPage.transform.GetChild(8).gameObject.SetActive(false);
        }
        else
        {
            myPage.transform.GetChild(2).gameObject.SetActive(false);
            myPage.transform.GetChild(8).gameObject.SetActive(true);
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
        print("��ȸ �Ϸ�");
    }

    public void GetRoomImage()
    {
        //HttpManager.instance.img = true;
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/random";
        requester.requestType = RequestType.GET;
        //requester.onImgComplete = OnCompleteGetRoomImage;
        HttpManager.instance.SendRequest(requester);

    }
    public void OnCompleteGetRoomImage(DownloadHandlerTexture handler)
    {
        print(2);
        img.GetComponent<RawImage>().texture = handler.texture;
        //sHandler = handler.text;
        print(sHandler);
        HttpManager.instance.img = false;
        //JObject jsonData = JObject.Parse(sHandler);

        ////JArray jarry = jsonData["data"]["furnitures"].ToObject<JArray>();

        ////for(int i = 0; i < jarry.Count; i++)
        ////{
        ////    ObjectInfo info = new ObjectInfo();

        ////    info.wallNumber = jarry[i]["wallNumber"].ToObject<int>();

        ////    objectInfoList.Add(info);
        ////}

        ////int status = jsonData["status"].ToObject<int>();
        //string userData = "{\"data\":" + jsonData["data"].ToString() + "}";

        //print("��ȸ�Ϸ�");

    }

    public void GetFollower()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/follower";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetFollower;
        HttpManager.instance.SendRequest(requester);
    }
    public void OnCompleteGetFollower(DownloadHandler handler)
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

        print("��ȸ�Ϸ�");

    }

    public void GetThree()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/mypage";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetThree;
        HttpManager.instance.SendRequest(requester);
    }
    public void OnCompleteGetThree(DownloadHandler handler)
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
        string userData = jsonData["data"].ToString();
        UserGetInfo userThree = JsonUtility.FromJson<UserGetInfo>(userData);
        follower.text = "�ȷο�" + " " + userThree.followerCount;
        following.text = "�ȷ���" + " " + userThree.followingCount;
        feedNum.text = "�Խù�" + " " + userThree.feedCount;
        HttpManager.instance.memberCode = userThree.memberCode;
        userGetInfo.followingCount = userThree.followingCount;
        userGetInfo.feedCount = userThree.feedCount;
        print(userData);
    
        print("��ȸ�Ϸ�");
    }
    public void GetRoomAll()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/id/j";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetRoomAll;
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
            CreateObject(userInfoList[i]);
        }
        totalElements = ContentHolder.childCount;
        Element = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.GetChild(i).gameObject;
            ContentHolder.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(OnClickVisit);
        }
        print("��ȸ�Ϸ�");
    }
    public void CreateObject(UserGetInfo info)
    {
        //search.text = info.id;
        GameObject idImage = Instantiate(IDFactory, ContentHolder);
        IdImageItem idImageItem = idImage.GetComponent<IdImageItem>();
        idImageItem.id.text = info.id;
        idImageItem.memberCode.text =info.memberCode.ToString();
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
        HttpManager.instance.id = id;
        HttpManager.instance.fakeId = id;
        HttpManager.instance.memberCode = memberCode;
    }

    public void OnClickFollowing()
    {
        OnSaveSignIn();
    }
    public void OnSaveSignIn()
    {
        UserGetInfo info = new UserGetInfo();
        info.id = id;
        //info.memberCode = memberCode;

        //arrayJson.furnitures = objectInfoList;
        //������ �Խù� ��ȸ ��û(/posts/1 , Get)
        HttpRequester requester = new HttpRequester();
        /// POST, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requester.url = "http://52.79.209.232:8080/api/v1/follow/" + memberCode;
        requester.requestType = RequestType.POST;
        //post data ����
        requester.postData = JsonUtility.ToJson(info, true);
        requester.onComplete = OnCompleteSignIn;
        //HttpManager���� ��û
        HttpManager.instance.SendRequest(requester);
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
        HttpManager.instance.id = HttpManager.instance.userId;
    }

}