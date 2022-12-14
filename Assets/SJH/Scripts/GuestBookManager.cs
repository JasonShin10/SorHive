using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.EventSystems;

//방명록정보
public class GuestBookInfo
{
    //피드 번호
    public int myGuestBookNum = 0;

    //피드를 올린 사람(ID)
    public string UserID; //==계정이름(아이디)

    //피드에 쓴 글
    public string guestBookText;

    //피드 댓글 택스트
    public string[] comments; 
}


public class GuestBookManager : MonoBehaviour
{
    public Text roomOwner;
    public Texture[] photos;
    //public GameObject GBoxManager;

    //임시 오브젝트 정보 담을 변수
    GuestBookInfo guestBookInfo = new GuestBookInfo();
    public string upLoadText;

    //개시물 번호
    public static int GuestBookNum = 0; //서버에 올라갈 때 이 피드가 몇번째로 올라간 피드인지 확인하기 위함 


    public Text mytext;


    void Start()
    {
        roomOwner.text = HttpManager.instance.roomOwner;
    }

    void Update()
    {
      
    }

    public void OnClickfill()
    {
        //사용자가 입력한 Text를 스트링값으로 변환해 저장한다.
        upLoadText = mytext.text;
        
        //GuestInfo에 피드 텍스트를 upLoadText의 입력값과 동기화한다.
        guestBookInfo.guestBookText = upLoadText;             
    }

    public void OnClickSave()
    {
        RoomInManager.instance.GuestBookNum++;
        guestBookInfo.myGuestBookNum = 0;
        //피드정보를 Json으로 변환한다.
        string jsonData = JsonUtility.ToJson(guestBookInfo, true);
        //print(jsonData);
        GuestBookNum = Directory.GetFiles(Application.persistentDataPath + "/SJH/GuestBookData/").Length/2; 
        GuestBookNum++;

        string path = Application.persistentDataPath + "/SJH/GuestBookData";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/guestBookData" + GuestBookNum + ".txt", jsonData);
        print(GuestBookNum);
        SceneManager.LoadScene("RoomInScene");
    }
    #region GuestBookPost
    public void OnSaveGuestBook()
    {
        GuestBookJsonInfo info = new GuestBookJsonInfo();
        info.guestBookContent = upLoadText;
        print(upLoadText);
        //info.offlineRoomImage = File.ReadAllBytes(Application.persistentDataPath + "/Resources/ZRoomImage/my0.png");
        info.roomId = HttpManager.instance.roomId;
        //ArrayJson<ObjectInfo> arrayJson = new ArrayJson<ObjectInfo>();
        //arrayJson.furnitures = objectInfoList;
        //서버에 게시물 조회 요청(/posts/1 , Get)
        HttpRequester requester = new HttpRequester();
        /// POST, 완료되었을 때 호출되는 함수
        requester.url = "http://13.124.225.86:8080/api/v1/guestbook";
        requester.requestType = RequestType.POST;
        //post data 셋팅
        requester.postData = JsonUtility.ToJson(info, true);
        requester.onComplete = OnCompleteSaveGuestBook;
        requester.requestName = "OnSaveGuestBook";
        //HttpManager에게 요청
        HttpManager.instance.SendRequest(requester);
        SceneManager.LoadScene("RoomInScene");
    }
    #endregion
    /* public void OnClickLoad()
     {
         if (FeedNum != 0)
         {
             for(int i =0; i< FeedNum; i++)
             {
                 //불 러오기               
             }

         }
     }*/
    public void OnCompleteSaveGuestBook(DownloadHandler handler)
    {
        print(handler);
        string s = "{\"furniture\":" + handler.text + "}";
        PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);

    }


    
    //public void OnDeleteGuestBook()
    //{
    //    GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        
    //    HttpManager.instance.guestBookId = int.Parse(clickObject.transform.GetChild(0).GetComponent<Text>().text);

    //}

    //public void GuestBookDelete()
    //{
    //    서버에 게시물 조회 요청(/posts/1 , Get)
    //    HttpRequester requester = new HttpRequester();
    //    / POST, 완료되었을 때 호출되는 함수
    //    requester.url = "http://13.124.225.86:8080/api/v1/guestbook/" + HttpManager.instance.guestBookId;
    //    requester.requestType = RequestType.GET;
    //    post data 셋팅
        
    //    requester.onComplete = OnCompleteSaveGuestBook;
    //    HttpManager에게 요청
    //    HttpManager.instance.SendRequest(requester);
       
    //}
    //public void OnCompleteDeleteGuestBook(DownloadHandler handler)
    //{
    //    print(handler);
    //    string s = "{\"furniture\":" + handler.text + "}";
    //    PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);

    //}
}
