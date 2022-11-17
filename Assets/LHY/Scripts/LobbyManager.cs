using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json.Linq;



public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instence;

    public Text userName;

    //Dictionary<string, int> roomCache = new Dictionary<string, int>();

    public int myRoom = 0;

    public Transform feedListContent;

    public GameObject feedUIFactory;

    private void Awake()
    {
        instence = this;
    }
    void Start()
    {
        //roomCache["a"];
        //CreateFeedUI();
       

    }

    void Update()
    {
       
    }

    //방 생성
    public void CreateRoom()
    {
        //방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
        //최대인원
        roomOptions.MaxPlayers = 19;
        //룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;

        //방 생성 요청(해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom(userName.text, roomOptions);
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //방 생성이 실패 될때 호출되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        
        print("OnCreateRoomFailed, " + returnCode + ", " + message);
    }

    //방 참가
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(userName.text);
    }

    public void OnClickRoomWarp()
    {
 
        SceneManager.LoadScene("WarpScene");
        //CreateRoom();
        //JoinRoom();
    }

    public void OnClickModifyProfile()
    {
        myRoom = 0;
        SceneManager.LoadScene("RoomChange");
        //CreateRoom();
        //JoinRoom();
    }

    public void OnClickRoomIn()
    {
        myRoom = 1;

        SceneManager.LoadScene("RoomInScene");
        //CreateRoom();
        //JoinRoom();
    }

    //방 참가가 완료되었을때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        if (myRoom == 0)
        {
            SceneManager.LoadScene("RoomChange");
            //PhotonNetwork.LoadLevel("RoomChange");
        }
        else if(myRoom == 1)
        {
            
            SceneManager.LoadScene("RoomInScene");
            //PhotonNetwork.LoadLevel("RoomInScene");
        }
        else if (myRoom == 2)
        {
            //벌집타기 씬
            //hotonNetwork.LoadLevel("RoomInScene");
        }
    }

    //방 참가가 실패 되었을 때 호출되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //방 목록이 변경 되었을 때(생성, 정보갱신, 삭제)호출 해주는 함수
   /* public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //룸 리스트 정보 갱신   
        UPdateRoomListUI();
        //룸 리스트 생성
        CreateRoomListUI();
    }*/

    void UPdateRoomListUI()
    {
        //foreach (RoomInfo info) ;
    }

    void CreateRoomListUI()
    {
        //foreach (RoomInfo info in roomCache.Values) ;
    }

    /*    public void LoadFeedData()
        {


        }*/

    public int FeedNum = 1;


    



    //public void CreateFeedUI()
    //{
    //    FeedNum = Directory.GetFiles(Application.dataPath + "/LHY/FeedData/").Length;
    //    //피드의 정보를 불러오고
    //    //LoadFeedData();
    //    // FeedManager.FeedNum;
    //    for(int i = 1; i <= FeedNum; i++)
    //    {
    //        string path = Application.dataPath + "/LHY/FeedData/feedData" + i + ".txt";

    //        print(FeedNum+"피드개수");

    //        string jsonData = File.ReadAllText(path);

    //        //피드 아이템을 만들어준다.
    //        GameObject feed = Instantiate(feedUIFactory, feedListContent);

    //        FeedInfo info = JsonUtility.FromJson<FeedInfo>(jsonData);

    //        FeedItem feedItem = feed.GetComponent<FeedItem>();
            
    //        feedItem.myfeedNum = info.myfeedNum;
    //        feedItem.feedText.text = info.feedText;
    //        feedItem.feedtexture.texture = Resources.Load<Texture>("01.Pictures/" + info.feedtextureNum);
    //        //feedItem.feedtexture.texture = info.feedtexture;
    //    }
    //}
}
