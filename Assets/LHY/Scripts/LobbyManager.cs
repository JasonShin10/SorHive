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
using UnityEngine.Android;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instence;

    public Text userID;

    public int memberCode;

    public int MymemberCode;

    //Dictionary<string, int> roomCache = new Dictionary<string, int>();

    public int myRoom = 0;

    public Transform feedListContent;

    public GameObject feedUIFactory;

    

    public bool creat = false;

    private void Awake()
    {
        instence = this;
    }
    void Start()
    {
        //roomCache["a"];
        //CreateFeedUI();
        //creat = false;
        //JoinRoom();

        MymemberCode = HttpManager.instance.memberCode;

#if ANDROID
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
#endif

    }

    void Update()
    {
        
    }

    public void OnClickRoomIn()
    {
        if (MymemberCode.ToString() != null)
        {
            //JoinOrCreateRoom();
           
            CreateRoom();         
        }

    }

    //public string[] curUsersNames;

    //�� ����
    public void CreateRoom()
    {
        //�� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        //�ִ��ο�
        roomOptions.MaxPlayers = 2;
        //�� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;

        //�� ���� ��û(�ش� �ɼ��� �̿��ؼ�)
        //PhotonNetwork.CreateRoom (userName.text, roomOptions);

        //PhotonNetwork.CreateRoom(userName.text, roomOptions);
        PhotonNetwork.LocalPlayer.NickName = MymemberCode.ToString();
        PhotonNetwork.CreateRoom(memberCode.ToString(), roomOptions);

        //PhotonNetwork.JoinOrCreateRoom(memberCode.ToString(), roomOptions, default);
    }

    public void JoinOrCreateRoom()
    {
        //�� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        //�ִ��ο�
        roomOptions.MaxPlayers = 2;
        //�� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;

        //�� ���� ��û(�ش� �ɼ��� �̿��ؼ�)
        //PhotonNetwork.CreateRoom (userName.text, roomOptions);

        //PhotonNetwork.CreateRoom(userName.text, roomOptions);
        PhotonNetwork.LocalPlayer.NickName = MymemberCode.ToString();
        PhotonNetwork.CreateRoom(memberCode.ToString(), roomOptions);

        //PhotonNetwork.JoinOrCreateRoom(memberCode.ToString(), roomOptions, default);
    }


    //���� �����Ǹ� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //�� ������ ���� �ɶ� ȣ��Ǵ� �Լ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        JoinRoom();
        print("OnCreateRoomFailed, " + returnCode + ", " + message);
    }

    //�� ����
    public void JoinRoom()
    {
        //PhotonNetwork.JoinRoom(userName.text); 
        PhotonNetwork.LocalPlayer.NickName = MymemberCode.ToString();
        PhotonNetwork.JoinRoom(memberCode.ToString());
        //PhotonNetwork.JoinRoom(HttpManager.instance.memberCode.ToString());
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

    public void OnClickChatPage()
    {
        SceneManager.LoadScene("ChatScene");
    }


    /*public void OnClickRoomIn()
    {
        //myRoom = 1;

        //PhotonNetwork.LoadLevel("RoomInScene");
        //SceneManager.LoadScene("RoomInScene");
   
    }*/

    //�� ������ �Ϸ�Ǿ����� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("RoomInScene");
        /* print("OnJoinedRoom");
         if (myRoom == 0)
         {
             SceneManager.LoadScene("RoomChange");
             //PhotonNetwork.LoadLevel("RoomChange");
         }
         else if(myRoom == 1)
         {

             //SceneManager.LoadScene("RoomInScene");
             PhotonNetwork.LoadLevel("RoomInScene");
         }
         else if (myRoom == 2)
         {
             //����Ÿ�� ��
             //hotonNetwork.LoadLevel("RoomInScene");
         }*/
    }

    //�� ������ ���� �Ǿ��� �� ȣ��Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //�� ����� ���� �Ǿ��� ��(����, ��������, ����)ȣ�� ���ִ� �Լ�
   /* public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //�� ����Ʈ ���� ����   
        UPdateRoomListUI();
        //�� ����Ʈ ����
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


    



  /*  public void CreateFeedUI()
    {
        FeedNum = Directory.GetFiles(Application.persistentDataPath + "/LHY/FeedData/").Length;
        //�ǵ��� ������ �ҷ�����
        //LoadFeedData();
        // FeedManager.FeedNum;
        for(int i = 1; i <= FeedNum; i++)
        {
            string path = Application.persistentDataPath + "/LHY/FeedData/feedData" + i + ".txt";

            print(FeedNum+"�ǵ尳��");

            string jsonData = File.ReadAllText(path);

            //�ǵ� �������� ������ش�.
            GameObject feed = Instantiate(feedUIFactory, feedListContent);

            FeedInfo info = JsonUtility.FromJson<FeedInfo>(jsonData);

            FeedItem feedItem = feed.GetComponent<FeedItem>();
            
            feedItem.myfeedNum = info.myfeedNum;
            feedItem.feedText.text = info.feedText;
            feedItem.feedtexture.texture = Resources.Load<Texture>("01.Pictures/" + info.feedtextureNum);
            //feedItem.feedtexture.texture = info.feedtexture;
        }
    }*/
}
