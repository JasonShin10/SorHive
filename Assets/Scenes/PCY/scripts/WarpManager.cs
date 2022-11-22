using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

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
        cnt = 0;
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
            roomItem.memberCode = memberCode[cnt];
            WP.GetComponent<RawImage>().texture = warpRoomImage[cnt].texture;
            Character.GetComponent<RawImage>().texture = warpAvatarImage[cnt].texture;
            NNT.GetComponent<Text>().text = nickNmae[cnt];

            print("불러온 사람: " + memberCode[cnt].ToString());
            // 카운트 세줌
            cnt++;
            if (cnt > 6)
                break;
        }
    }

    public IEnumerator DownloadImg(){
        int imageIdx = 0;
        while (imageIdx < 7)
        {
            StartCoroutine(DownloadRoomImg(imageIdx));
            StartCoroutine(DownloadAvatarImg(imageIdx));
            while ((downLoadAvatarCount + downLoadRoomCount) < (imageIdx+1)*2) yield return null;
            imageIdx++;
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

    private IEnumerator DownloadRoomImg(int imageIdx = 0){            // var url = $"https://combucket.s3.ap-northeast-2.amazonaws.com/images/room_11.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(roomImagePath[imageIdx]);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            warpRoomImage[imageIdx].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        downLoadRoomCount++;
        www.Dispose();
    }

    private IEnumerator DownloadAvatarImg(int imageIdx = 0){
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(avatarImagePath[imageIdx]);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            warpAvatarImage[imageIdx].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        downLoadAvatarCount++;
        www.Dispose();
    }

}