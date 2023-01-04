using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;


public class WarpManager : MonoBehaviour
{
/////////////////////////
    public static WarpManager instance;
    public int count = 0;
    // public DownloadHandlerTexture(bool readable);
    
    public Transform[] hexPos;

    public GameObject roomItemFactory;
    
    string[] id = new string[7];
    int[] memberCode = new int[7];
    public Text roomOwnerText;

    public int finishDownload = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loadRoom(HttpManager.instance.memberCode);
    }

    // Update is called once per frame
    void Update()
    {
        if (finishDownload == 14)
        {
            HttpManager.instance.LoadingCanvas.SetActive(false);
            finishDownload = 0;
        }
    }

    public void loadRoom(int centerMemberCode)
    {
        print("loadRoom");
        HttpRequester requester = new HttpRequester();
        requester.url = "http://13.124.225.86:8080/api/v1/member/roomin/" + centerMemberCode.ToString();
        requester.requestType = RequestType.GET;
        requester.onComplete = OnClickSet;
        StartCoroutine(HttpManager.instance.SendWarp(requester, centerMemberCode));
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainScenes");
        //CreateRoom();
        //JoinRoom();
    }

    private void OnClickSet(DownloadHandler handler){
        print("handler");
        JObject json = JObject.Parse(handler.text);
        int tmpCnt = 0;
        while (7 > tmpCnt)
        {
            if (tmpCnt == 0)
            {
                roomOwnerText.text = id[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["id"].ToString();
            }
            GameObject Room = Instantiate(roomItemFactory, hexPos[tmpCnt]);
            RoomItem roomItem = Room.GetComponent<RoomItem>();
            roomItem.memberCode = int.Parse(json["data"]["memberDtoList"][tmpCnt]["memberCode"].ToString());
            roomItem.id.text = json["data"]["memberDtoList"][tmpCnt]["id"].ToString();
            StartCoroutine(roomItem.DownloadRoomImg(json["data"]["memberDtoList"][tmpCnt]["memberRoomImage"].ToString()));
            StartCoroutine(roomItem.DownloadAvatarImg(json["data"]["memberDtoList"][tmpCnt]["avatarImagePath"].ToString()));
            tmpCnt++;
        }
    }
}