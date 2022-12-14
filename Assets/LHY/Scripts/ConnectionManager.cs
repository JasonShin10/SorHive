using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public static ConnectionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public InputField ID;
    public InputField Password;

    public Button btnConnect;

    void Start()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        OnCilckConnect();

        if(ID != null && Password != null)
        {
            ID.onValueChanged.AddListener(OnValueChanged);
            Password.onValueChanged.AddListener(OnPassValueChanged);
        }
        btnConnect.interactable = false;
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
        //서버 접속 요청
        PhotonNetwork.ConnectUsingSettings();
    }


    //마스터 서버 접속 성공시 호출(Lobby에 진입 할 수 없는 상태)
    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("OnConneted");
    }

    //마스터 서버 접속성공시 호출(Lobby에 진입할 수 있는 상태)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");

    }

    public void ExitToLoginPage()
    {
        SceneManager.LoadScene("ConnectionScene");
    }


    public void OnClickJoinLobby()
    {
        //로비 진입
        PhotonNetwork.JoinLobby();
    }

    //로비진입 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
        //Lobby씬 으로 이동
        if(HttpManager.instance.avatarYn == "Y")
        {
            // PhotonNetwork.LoadLevel("CreatCharactorScene");
            PhotonNetwork.LoadLevel("MainScenes");
        }
        else
        {
            PhotonNetwork.LoadLevel("CreatCharactorScene");
        }

        
        //
        //SceneManager.LoadScene("MainScenes");
    }

    public void OnclickGoMain()
    {
        PhotonNetwork.LoadLevel("MainScenes");
    }

  /*  public void OnClickConnect()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }*/

    void Update()
    {
        
    }
}
