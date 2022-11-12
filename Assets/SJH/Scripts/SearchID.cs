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
    public int totalElements;
    //public Text search;
    public UserInfo userInfo;
    public string id;
    public List<UserGetInfo> userInfoList = new List<UserGetInfo>();

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
        OnClickLogin();
        GetRoomAll();     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            GetRoomAll();
        }
    }

    public void OnClickLogin()
    {
        LoginInfo2 logdata = new LoginInfo2();
        logdata.memberId = "john12";
        logdata.password = "qwer1234!";
        HttpRequester requester = new HttpRequester();
        requester.url = "http://13.125.174.193:8080/api/v1/auth/login";
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

    public void GetRoomAll()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://13.125.174.193:8080/api/v1/member/j";
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
        for(int i =0; i< userInfoList.Count; i++)
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
        print("조회완료");

    }
    public void CreateObject(UserGetInfo info)
    {
        //search.text = info.id;
        
        GameObject idImage = Instantiate(IDFactory, ContentHolder);
        IdImageItem idImageItem = idImage.GetComponent<IdImageItem>();
        idImageItem.id.text = info.id;
        
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

     foreach(GameObject ele in Element)
        {
            searchedElements += 1;
            //ele.transform.GetComponent<Button>().onClick.AddListener(SceneLoad);
            if(ele.transform.GetChild(0).GetComponent<Text>().text.Length >= searchTextLength)
            {
                if(searchText == ele.transform.GetChild(0).GetComponent<Text>().text.Substring(0,searchTextLength))
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

    public void OnClickVisit()
    {
        myPageButton.onClick.Invoke();
        myPage.transform.GetChild(2).gameObject.SetActive(false);
        myPage.transform.GetChild(8).gameObject.SetActive(true);
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.GetComponentInChildren<Text>().text);
        string id = clickObject.GetComponentInChildren<Text>().text;
        HttpManager.instance.id = id;
        
    }

    public void OnClickFollowing()
    {

    }


}
