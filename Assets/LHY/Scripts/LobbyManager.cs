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

    void Start()
    {
        //roomCache["a"];
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

    public void OnClickButton()
    {
        CreateRoom();
        JoinRoom();
    }

    //방 참가가 완료되었을때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("RoomChange");
    }

    //방 참가가 실패 되었을 때 호출되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //방 목록이 변경 되었을 때(생성, 정보갱신, 삭제)호출 해주는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //룸 리스트 정보 갱신   
        UPdateRoomListUI();
        //룸 리스트 생성
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
