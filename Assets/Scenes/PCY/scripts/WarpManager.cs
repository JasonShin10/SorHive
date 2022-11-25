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
    string[] nickNmae = new string[7];
    public RawImage[] warpAvatarImage;
    int[] memberCode = new int[7];
    public Text CenterNickName;

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
       
    }

    public void loadRoom(int centerMemberCode)
    {
        print("loadRoom");
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/roomin/" + centerMemberCode.ToString();
        requester.requestType = RequestType.GET;
        requester.onComplete = OnClickSet;
        StartCoroutine(HttpManager.instance.SendWarp(requester, centerMemberCode));
    }

    // 오른쪽부터 시작해서 달팽이 모양으로 룸 리로드 하는 코드
    public void reloadRoom(int centerMemberCode)
    {
        print("reloadRoom");
        // 해야 될 거
        print("워프한 룸: " + memberCode[0]);
        // 트랜스폼을 이용해서 각 방 하나씩 접근 후 설정
        Transform WPM = this.transform.Find("WarpPosManager");
        // 2번째 부터 7번째 자식까지만 순회
        int roomCount = 0;
        // child가 각 방의 번호
        foreach (Transform child in WPM.transform)
        {
            //child 는 룸포스0 ,1 ,2 
            // 각 방의 정보 담는 객체 불러오기

            Transform WP = child.transform.GetChild(0);
            Transform Character = WP.transform.GetChild(1);
            Transform NNT = WP.transform.GetChild(2).transform.GetChild(0);

            // 룸아이템의 멤버코드, 룸이미지를 배열에서 가져와서 변경
            // 다운로드 받기
            RoomItem roomItem = WP.GetComponent<RoomItem>();
            print(roomItem.name);
            roomItem.memberCode = memberCode[roomCount];
            roomItem.avatarImage.texture = warpAvatarImage[roomCount].texture;
            roomItem.roomImage.texture = warpRoomImage[roomCount].texture;
            roomItem.nickName.text = nickNmae[roomCount];

            /*WP.transform.GetChild(0).GetComponent<RawImage>().texture = warpRoomImage[roomCount].texture;
            Character.GetComponent<RawImage>().texture = warpAvatarImage[roomCount].texture;
            NNT.GetComponent<Text>().text = nickNmae[roomCount];*/

            print("불러온 사람: " + memberCode[roomCount].ToString());
            // 카운트 세줌
            roomCount++;
            if (roomCount > 6)
                break;
        }
    }

    public void BackToMain()
    {

        SceneManager.LoadScene("MainScenes");
        //CreateRoom();
        //JoinRoom();
    }

    public IEnumerator DownloadImg(){
        int roomImgIdx = 0;
        while (roomImgIdx < 7)
        {
            StartCoroutine(DownloadRoomImg(roomImgIdx));
            while (downLoadRoomCount < (roomImgIdx + 1)) yield return null;
            roomImgIdx++;
        }

        int imgIdx = 0;
        while (imgIdx < 7)
        {
            StartCoroutine(DownloadAvatarImg(imgIdx));
            while (downLoadAvatarCount < (imgIdx + 1)) yield return null;
            imgIdx++;
        }
    }

    private void OnClickSet(DownloadHandler handler){
        JObject json = JObject.Parse(handler.text);
        int tmpCnt = 0;
        while(tmpCnt < 7){
            memberCode[tmpCnt] = int.Parse(json["data"]["memberDtoList"][tmpCnt]["memberCode"].ToString());
            nickNmae[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["id"].ToString();
            roomImagePath[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["memberRoomImage"].ToString();
            avatarImagePath[tmpCnt] = json["data"]["memberDtoList"][tmpCnt]["avatarImagePath"].ToString();
            tmpCnt++;
        }
    }

    private IEnumerator DownloadRoomImg(int imageIdx = 0){ 
        UnityWebRequest wwwRoom = UnityWebRequestTexture.GetTexture(roomImagePath[imageIdx]);
        print("Room" + imageIdx.ToString());
        print(roomImagePath[imageIdx]);
        yield return wwwRoom.SendWebRequest();
        if (wwwRoom.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwRoom.error);
        }
        else
        {
            warpRoomImage[imageIdx].texture = ((DownloadHandlerTexture)wwwRoom.downloadHandler).texture;
        }
        downLoadRoomCount++;
        wwwRoom.Dispose();
    }

    private IEnumerator DownloadAvatarImg(int imageIdx = 0){
        UnityWebRequest wwwAvatar = UnityWebRequestTexture.GetTexture(avatarImagePath[imageIdx]);
        print("Avatar" + imageIdx.ToString());
        print(avatarImagePath[imageIdx]);
        yield return wwwAvatar.SendWebRequest();
        if (wwwAvatar.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwAvatar.error);
        }
        else
        {
            warpAvatarImage[imageIdx].texture = ((DownloadHandlerTexture)wwwAvatar.downloadHandler).texture;
        }
        downLoadAvatarCount++;
        wwwAvatar.Dispose();
    }

}