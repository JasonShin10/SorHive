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
        //���� ����
        PhotonNetwork.ConnectUsingSettings();
    }
    

    //������ ���� ���� ������ ȣ��(Lobby�� ���� �� �� ���� ����)
    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConneted");
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� �ִ� ����)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("OnConnectedToMaster");

        //�κ� ����
        PhotonNetwork.JoinLobby();
    }

    //�κ����� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
        //Lobby�� ���� �̵�
        SceneManager.LoadScene("MainScenes");
    }

    void Update()
    {
        
    }
}
