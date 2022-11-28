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

    int cnt = 0;
    
    public Transform[] hexPos;

    public GameObject roomItemFactory;

    string[] roomImagePath = new string[7];
    string[] avatarImagePath = new string[7];
    public int downLoadRoomCount = 0;
    public int downLoadAvatarCount = 0;
    
    public RawImage[] warpRoomImage;
    string[] id = new string[7];
    public RawImage[] warpAvatarImage;
    int[] memberCode = new int[7];
    public Text CenterNickName;

    public Text roomOwnerText;

    private int finishDownload = 0;

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
        while (7 > count) {
            GameObject Room = Instantiate(roomItemFactory, hexPos[count]);
            count++;
        }
        loadRoom(HttpManager.instance.memberCode);
    }

    // Update is called once per frame
    void Update()
    {
        if (finishDownload == 2)
        {
            HttpManager.instance.LoadingCanvas.SetActive(false);
            finishDownload = 0;
        }
    }

    public void loadRoom(int centerMemberCode)
    {
        print("loadRoom");
        roomOwnerText.text = centerMemberCode;
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/roomin/" + centerMemberCode.ToString();
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

    public void DownloadImg(){
        print("워프한 룸: " + memberCode[0]);
        int roomImgIdx = 0;
        while (roomImgIdx < 7)
        {
            RoomItem roomItem = this.transform.Find("WarpPosManager").transform.GetChild(roomImgIdx).transform.GetChild(0).GetComponent<RoomItem>();
            StartCoroutine(DownloadRoomImg(roomImgIdx, roomItem));
            StartCoroutine(DownloadAvatarImg(roomImgIdx, roomItem));
            roomItem.id.text = id[roomImgIdx];
            roomItem.memberCode = memberCode[roomImgIdx];
            //while ((downLoadAvatarCount + downLoadRoomCount) < (roomImgIdx + 1)*2) yield return null;
            roomImgIdx++;
        }
    }

    private void OnClickSet(DownloadHandler handler){
        JObject json = JObject.Parse(handler.text);
        int tmpCnt = 0;
        while(tmpCnt < 7){
            memberCode[tmpCnt] = int.Parse(json["data"]["memberDtoList"][tmpCnt]["memberCode"].ToString());
            id[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["id"].ToString();
            roomImagePath[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["memberRoomImage"].ToString();
            avatarImagePath[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["avatarImagePath"].ToString();
            tmpCnt++;
        }
    }

    private IEnumerator DownloadRoomImg(int imageIdx, RoomItem roomItem)
    { 
        UnityWebRequest wwwRoom = UnityWebRequestTexture.GetTexture(roomImagePath[imageIdx]);
        
        yield return wwwRoom.SendWebRequest();
        if (wwwRoom.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwRoom.error);
        }
        else
        {
            roomItem.roomImage.texture = ((DownloadHandlerTexture)wwwRoom.downloadHandler).texture;
        }
        downLoadRoomCount++;
        wwwRoom.Dispose();
        if (imageIdx == 6)
        {
            finishDownload += 1;
        }
    }

    private IEnumerator DownloadAvatarImg(int imageIdx, RoomItem roomItem)
    {
        UnityWebRequest wwwAvatar = UnityWebRequestTexture.GetTexture(avatarImagePath[imageIdx]);
        
        yield return wwwAvatar.SendWebRequest();
        if (wwwAvatar.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwAvatar.error);
        }
        else
        {
            roomItem.avatarImage.texture = ((DownloadHandlerTexture)wwwAvatar.downloadHandler).texture;
        }
        downLoadAvatarCount++;
        wwwAvatar.Dispose();
        if (imageIdx == 6)
        {
            finishDownload += 1;
        }
    }

}