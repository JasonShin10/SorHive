using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;


public class SearchID : MonoBehaviour
{
    public GameObject ContentHolder;
    public GameObject[] Element;
    public GameObject SearchBar;

    public int totalElements;
    public Text search;
    public UserInfo userInfo;
    public List<UserInfo> userInfoList = new List<UserInfo>();

    //public GameObject ContentHolder;

    //public GameObject[] Element;

    //public GameObject SearchBar;

    //public int totalElements;
    // Start is called before the first frame update
    void Start()
    {
        totalElements = ContentHolder.transform.childCount;
        Element = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.transform.GetChild(i).gameObject;
        }
        OnClickLogin();
        //GetRoomAll();
        //totalElements = ContentHolder.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
        ArrayJsonID<UserInfo> userInfo = JsonUtility.FromJson<ArrayJsonID<UserInfo>>(userData);
        userInfoList = userInfo.data;
        print(userInfo);


        print("조회완료");

    }
    public void CreateObject(UserInfo info)
    {
        search.text = info.memberId;
        //if (info.furnitureCategoryNumber == 0)
        //{
        //    info.obj = bedItems[info.furnitureNumber];
        //    GameObject createObj = Instantiate(info.obj);
        //    if (createObj.gameObject.name == info.name)
        //    {
        //        createObj.gameObject.name = info.name + 1;
        //    }
        //    else
        //    {
        //        createObj.gameObject.name = info.name;
        //    }
        //    createObj.transform.position = info.position;
        //    createObj.transform.localScale = info.scale;
        //    createObj.transform.eulerAngles = info.angle;
        //    createObj.GetComponent<BoxCollider>().center = info.boxPosition;
        //    //objectInfoList.Add(info);
        //    //info.obj.GetComponent<Furniture>().located = true;
        //}
    }
}
