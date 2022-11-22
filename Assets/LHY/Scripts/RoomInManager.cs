using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.EventSystems;

public class RoomInManager : MonoBehaviour
{
    public static RoomInManager instance;

    private void Awake()
    {
        instance = this;
    }
    public int GuestBookNum = 1;

    public Transform GuestBookListContent;

    public GameObject GuestBookUIFactory;

    GameObject clickObject;
    // Start is called before the first frame update
    void Start()
    {
        //CreateFeedUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("MainScenes");
    }

    public void CreateFeedUI()
    {
        GuestBookNum = Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/").Length / 2;
        GuestBookNum++;
        //피드의 정보를 불러오고
        //LoadFeedData();
        // FeedManager.FeedNum;
        // print(Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/").Length);
        for (int i = 1; i < GuestBookNum; i++)
        {
            string path = Application.dataPath + "/SJH/GuestBookData/guestBookData" + i + ".txt";
            //print(Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/")[i]);
            //print(GuestBookNum + "피드개수");

            string jsonData = File.ReadAllText(path);

            //피드 아이템을 만들어준다.
            GameObject guestBook = Instantiate(GuestBookUIFactory, GuestBookListContent);
            GuestBookInfo info = JsonUtility.FromJson<GuestBookInfo>(jsonData);
            GuestBookItem guestBookItem = guestBook.GetComponent<GuestBookItem>();
            guestBookItem.myGuestBookNum = info.myGuestBookNum;
            guestBookItem.guestBookText.text = info.guestBookText;
        }
    }

    //public void GetPostGuestBookAll()
    //{
    //    HttpRequester requester = new HttpRequester();
    //    print(HttpManager.instance.memberCode);
    //    //requester.url = "http://52.79.209.232:8080/api/v1/room/" + HttpManager.instance.memberCode;
    //    requester.url = "http://52.79.209.232:8080/api/v1/room/" + 1;

    //    requester.requestType = RequestType.GET;
    //    requester.onComplete = OnCompleteGetPostGuestBookAll;

    //    HttpManager.instance.SendRequest(requester);
    //}
    string sHandler;
    //public void OnCompleteGetPostGuestBookAll(DownloadHandler handler)
    //{
    //    sHandler = handler.text;
    //    print(sHandler);
    //    JObject jsonData = JObject.Parse(sHandler);
    //    print(jsonData);
    //    //JArray jarry = jsonData["data"]["furnitures"].ToObject<JArray>();


    //    //for (int i =0; i< jarry.Count; i++)
    //    //{
    //    //int roomIdData = jarry[0]["roomId"].ToObject<int>();
    //    //print(roomIdData);

    //    //}
    //    //for(int i = 0; i < jarry.Count; i++)
    //    //{
    //    //    ObjectInfo info = new ObjectInfo();

    //    //    info.wallNumber = jarry[i]["wallNumber"].ToObject<int>();

    //    //    objectInfoList.Add(info);
    //    //}

    //    //int status = jsonData["status"].ToObject<int>();
    //    string guestBookData = "{\"guestBookDataList\":" + jsonData["data"]["guestBookDataList"].ToString() + "}";
    //    //string roomIdData = jsonData["data"]["roomId"].ToObject<int>();

    //    //string data = "{"+ jsonData["data"].ToString() + "}";
    //    print(guestBookData);
    //    //print(roomIdData);
    //    //HttpManager.instance.roomId = roomIdData;
    //    ArrayJson<GuestBookJsonInfo> guestBookInfo = JsonUtility.FromJson<ArrayJson<GuestBookJsonInfo>>(guestBookData);

    //    guestBookJsonInfoList = guestBookInfo.guestBooks;
    //    //HttpManager.instance.roomId = objectInfo.


    //    //n = objectInfoList.Count;

    //    for (int i = 0; i < guestBookJsonInfoList.Count; i++)
    //    {
    //        CreateObject(guestBookJsonInfoList[i]);
    //    }

    //    //PostDataArray array = JsonUtility.FromJson<PostDataArray>(sHandler);
    //    //for(int i=0; i<array.data.Count; i++)
    //    //{

    //    //}

    //    //print(roomStatue.message);
    //    //OnLoadJson(sHandler);
    //    //PostData postData = JsonUtility.FromJson<PostData>(handler.text);
    //    //string s = "{\"furniture\":" + handler.text + "}";
    //    print("조회 완료");
    //}
    public void OnDeleteGuestBook()
    {
        clickObject = EventSystem.current.currentSelectedGameObject;
        HttpManager.instance.guestBookId = int.Parse(clickObject.transform.parent.GetChild(2).GetChild(0).GetComponent<Text>().text);
        GuestBookDelete();

    }

    public void GuestBookDelete()
    {
        //서버에 게시물 조회 요청(/posts/1 , Get)
        HttpRequester requester = new HttpRequester();
        /// POST, 완료되었을 때 호출되는 함수
        requester.url = "http://52.79.209.232:8080/api/v1/guestbook/" + HttpManager.instance.guestBookId;
        requester.requestType = RequestType.DELETE;
        //post data 셋팅

        requester.onComplete = OnCompleteDeleteGuestBook;
        requester.requestName = "GuestBookDelete";
        //HttpManager에게 요청
        HttpManager.instance.SendRequest(requester);

    }
    public void OnCompleteDeleteGuestBook(DownloadHandler handler)
    {

        Destroy(clickObject.transform.parent);

    }
    public void CreateObject(GuestBookJsonInfo info)
    {
        GameObject guestBook = Instantiate(GuestBookUIFactory, GuestBookListContent);
        guestBook.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(OnDeleteGuestBook);
        GuestBookItem guestBookItem = guestBook.GetComponent<GuestBookItem>();
        guestBookItem.guestBookText.text = info.guestBookContent;
        guestBookItem.UserID.text = info.guestBookWriterId;
        guestBookItem.guestBookId.text = info.guestBookId;
    }
}
