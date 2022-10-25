using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text userName;

    Dictionary<string, int> roomCache = new Dictionary<string, int>();

    public int myRoom = 0;

    void Start()
    {
        //roomCache["a"];
    }

    void Update()
    {
        
    }

    //�� ����
    public void CreateRoom()
    {
        //�� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        //�ִ��ο�
        roomOptions.MaxPlayers = 19;
        //�� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;

        //�� ���� ��û(�ش� �ɼ��� �̿��ؼ�)
        PhotonNetwork.CreateRoom(userName.text, roomOptions);
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
        
        print("OnCreateRoomFailed, " + returnCode + ", " + message);
    }

    //�� ����
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(userName.text);
    }

    public void OnClickModifyProfile()
    {
        myRoom = 0;
        CreateRoom();
        JoinRoom();
    }

    public void OnClickRoomIn()
    {
        myRoom = 1;
        CreateRoom();
        JoinRoom();
    }

    //�� ������ �Ϸ�Ǿ����� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        if (myRoom == 0)
        {
            PhotonNetwork.LoadLevel("RoomChange");
        }
        else if(myRoom == 1)
        {
            PhotonNetwork.LoadLevel("RoomInScene");
        }
        else if (myRoom == 2)
        {
            //����Ÿ�� ��
            //hotonNetwork.LoadLevel("RoomInScene");
        }
    }

    //�� ������ ���� �Ǿ��� �� ȣ��Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //�� ����� ���� �Ǿ��� ��(����, ��������, ����)ȣ�� ���ִ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //�� ����Ʈ ���� ����   
        UPdateRoomListUI();
        //�� ����Ʈ ����
        CreateRoomListUI();
    }

    void UPdateRoomListUI()
    {
        //foreach (RoomInfo info) ;
    }

    void CreateRoomListUI()
    {
        //foreach (RoomInfo info in roomCache.Values) ;
    }

}
