using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    //

    void Start()
    {
        //서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }
    

    //마스터 서버 접속 성공시 호출(Lobby에 진입 할 수 없는 상태)
    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConneted");
    }

    //마스터 서버 접속성공시 호출(Lobby에 진입할 수 있는 상태)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("OnConnectedToMaster");

        //로비 진입
        PhotonNetwork.JoinLobby();
    }

    //로비진입 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
        //Lobby씬 으로 이동
        SceneManager.LoadScene("MainScenes");
    }

    void Update()
    {
        
    }
}
