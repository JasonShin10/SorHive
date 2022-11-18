using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public InputField ID;
    public InputField Password;

    public Button btnConnect;

    void Start()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        OnCilckConnect();

        ID.onValueChanged.AddListener(OnValueChanged);
        Password.onValueChanged.AddListener(OnPassValueChanged);
    }

    public void OnValueChanged(string s)
    {
        btnConnect.interactable = s.Length > 0 && Password.text.Length > 0;
        print("OnValueChanged : " + s);
    }

    public void OnPassValueChanged(string b)
    {
        btnConnect.interactable = b.Length > 0 && ID.text.Length > 0;
    }


    public void OnCilckConnect()
    {
        //���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
    }


    

    //������ ���� ���� ������ ȣ��(Lobby�� ���� �� �� ���� ����)
    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("OnConneted");
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� �ִ� ����)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");

    }

    //�κ����� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();


        //�κ� ����
        PhotonNetwork.JoinLobby();

        print("OnJoinedLobby");
        //Lobby�� ���� �̵�
        //PhotonNetwork.LoadLevel("CreatCharactorScene");
        PhotonNetwork.LoadLevel("MainScenes");
        //SceneManager.LoadScene("MainScenes");
    }

    public void OnclickGoMain()
    {
        //PhotonNetwork.LoadLevel("MainScenes");
    }

  /*  public void OnClickConnect()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }*/

    void Update()
    {
        
    }
}
