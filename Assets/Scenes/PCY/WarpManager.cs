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

    bool check = false;
    bool check2 = false;
    bool check3 = false;
    bool check4 = false;


    double timer;
    int waitingTime;
    int waitingTime2;

    int centerCode;
    public Transform[] hexPos;

    public GameObject roomItemFactory;

    string[] roomImagePath = new string[7];
    string[] avatarImagePath = new string[7];


    public RawImage img;
    public RawImage[] roomImage;
    string[] nickNmae = new string[7];
    public RawImage[] avatarImage;
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
        
        // using (var uwr = new UnityWebRequest("https://combucket.s3.ap-northeast-2.amazonaws.com/images/room_11.png", UnityWebRequest.kHttpVerbGET))
        // {
        //     uwr.downloadHandler = new DownloadHandlerTexture();
        //     yield return uwr.SendWebRequest();
        //     print(DownloadHandlerTexture.GetContent(uwr));
        //     roomImage[0].texture = DownloadHandlerTexture.GetContent(uwr);
        // }

        // StartCoroutine(GetTexture(img));

        timer = 0.0;
        waitingTime = 1;
        waitingTime2 = 3;

        print("start");
        while (7 > count) {
            GameObject Room = Instantiate(roomItemFactory, hexPos[count]);
            count++;
        }
        loadRoom(1);
        print("start out");
    }

    // Update is called once per frame
    void Update()
    {
        // print(timer);
        if(check)
        {
            check = false; 
            downloadRoom(centerCode); // 얘가 끝난 다음
        }
        if(check2)
        {
            timer += Time.deltaTime;
            if(timer > waitingTime)
            {
                check2 = false;
                print("왜자꾸 넘어가는데....");
                reloadRoom(centerCode);       // 얘가 되면 성공이야.
            }
        }


        if(check4)
        {
            check4 = false; 
            cnt = 0;
            while(cnt < 7){
                DownloadImg(cnt); // 얘가 끝난 다음
                cnt++;
            }
        }
        if(check3){
            timer += Time.deltaTime;
            if(timer > waitingTime2)
            {
                check3 = false;
                // 해야 될 거
                print("워프한 룸: " + nickNmae[0]);
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
                    Transform RIM = WP.transform.GetChild(0);
                    Transform Character = WP.transform.GetChild(1);
                    Transform NNT = WP.transform.GetChild(2).transform.GetChild(0);

                    // 룸아이템의 멤버코드, 룸이미지를 배열에서 가져와서 변경
                    // 다운로드 받기
                    RoomItem roomItem = RIM.GetComponent<RoomItem>();
                    roomItem.memberCode = memberCode[cnt];
                    RIM.GetComponent<RawImage>().texture = roomImage[cnt].texture;
                    Character.GetComponent<RawImage>().texture = avatarImage[cnt].texture;
                    NNT.GetComponent<Text>().text = nickNmae[cnt];
                    
                    print("불러온 사람: " + memberCode[cnt].ToString());
                    // 카운트 세줌
                    cnt++;
                    if (cnt > 6)
                        break;
                }
            }
        }



    }

    public void loadRoom(int centerMemberCode){
        print("asd");
        centerCode = centerMemberCode;
        check = true;
        check2 = true;
        timer = 0;
    }

    private void downloadRoom(int centerMemberCode)
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/member/roomin/" + centerMemberCode.ToString();
        requester.requestType = RequestType.GET;

        requester.onComplete = OnClickSet;
        HttpManager.instance.SendRequest(requester);
    }

    // 오른쪽부터 시작해서 달팽이 모양으로 룸 리로드 하는 코드
    private void reloadRoom(int centerMemberCode)
    {
        check3 = true;
        timer = 0;
        check4 = true;
    }

    private void DownloadImg(int imgCnt){
        StartCoroutine(DownloadRoomImg(imgCnt));
        // StartCoroutine(DownloadAvatarImg(imgCnt));
    }

    // public void GetMambersList()
    // {
    //     HttpRequester requester = new HttpRequester();
    //     requester.url = "http://52.79.209.232:8080/api/v1/member/roomin/1";
    //     requester.requestType = RequestType.GET;
    //     requester.onComplete = OnClickSet;

    //     HttpManager.instance.SendRequest(requester);
    // }

    private void OnClickSet(DownloadHandler handler){
        JObject json = JObject.Parse(handler.text);
        print("handler start");
        cnt = 0;
        while(cnt <7){
            memberCode[cnt] = int.Parse(json["data"]["memberDtoList"][cnt]["memberCode"].ToString());
            nickNmae[cnt] = json["data"]["memberDtoList"][cnt]["id"].ToString();
            roomImagePath[cnt] = json["data"]["memberDtoList"][cnt]["memberRoomImage"].ToString();
            avatarImagePath[cnt] = json["data"]["memberDtoList"][cnt]["avatarImagePath"].ToString();
            cnt++;
        }
    }

    private IEnumerator DownloadRoomImg(int imageIdx){
        // var url = $"https://combucket.s3.ap-northeast-2.amazonaws.com/images/room_11.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(roomImagePath[imageIdx]);
        print(roomImagePath[imageIdx]);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            print((DownloadHandlerTexture)www.downloadHandler);
            roomImage[imageIdx].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    private IEnumerator DownloadAvatarImg(int imageIdx){
        // var url = "https://combucket.s3.ap-northeast-2.amazonaws.com/images/room_11.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(avatarImagePath[imageIdx]);
        print(avatarImagePath[imageIdx]);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            avatarImage[imageIdx].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    // IEnumerator GetTexture(RawImage img)
    // {

    //     using (var uwr = new UnityWebRequest("https://combucket.s3.ap-northeast-2.amazonaws.com/images/room_11.png", UnityWebRequest.kHttpVerbGET))
    //     {
    //         uwr.downloadHandler = new DownloadHandlerTexture();
    //         yield return uwr.SendWebRequest();
    //         // img.texture = DownloadHandlerTexture.GetContent(uwr);
    //         GetComponent<Renderer>().material.mainTexture = DownloadHandlerTexture.GetContent(uwr);
    //     }

    // }

}