using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;


public class ChatLog
{
    public RawImage profileImage;
    public string nickName;
    public string lastMessage;
    public string lastTime;
    public int guestMemberCode;
    public string profileImageUrl;
}

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject ChatLogBarUIFactory;

    public Transform ChatLogBarListContent;

    public Text myId;

    List<ChatLog> chatLog = new List<ChatLog>();

    public RawImage tmpProfileImage;

    public string myProfileImageUrl;

    int memberCode;

    int chatLogBarLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        // print(PlayerPrefs.GetString("token"));
        LoadChatList();
        memberCode =  HttpManager.instance.memberCode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // chatList를 전체 다 불러오는 것
    public void LoadChatList()
    {
        print("loadChatList");
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/chatting";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnClickSet;
        StartCoroutine(HttpManager.instance.DownLoadChatList(requester));
    }

    public IEnumerator DownloadImage()
    {
        int i = 0;
        int processNum = 0;
        for (; i < chatLogBarLength; i++)
        {
            processNum++;
            StartCoroutine(DownloadRoomImg(i));
            while (processNum > 1) yield return null;
            processNum--;
        }
        while (i < chatLogBarLength) yield return null;
        ApplyChatList();
    }

    public IEnumerator downloadMyImage()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(myProfileImageUrl);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            ChatPageManager.instance.myProfileImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        www.Dispose();
    }


    private IEnumerator DownloadRoomImg(int imageIdx = 0)
    {            
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(chatLog[imageIdx].profileImageUrl);
        print(chatLog[imageIdx].profileImageUrl);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            chatLog[imageIdx].profileImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        www.Dispose();
    }

    // 코루틴 종료 후 챗리스트 적용해주는 것
    public void ApplyChatList()
    {
        for (int i = 0; i < chatLogBarLength; i++)
        {
            GameObject chatLogBar = Instantiate(ChatLogBarUIFactory, ChatLogBarListContent);
            ChatLogBarItem chatLogBarItem = chatLogBar.GetComponent<ChatLogBarItem>();

            chatLogBarItem.nickName.text = chatLog[i].nickName;
            chatLogBarItem.chatLogBarText.text = chatLog[i].lastMessage;
            chatLogBarItem.lastTime.text = chatLog[i].lastTime;
            chatLogBarItem.memberCode = chatLog[i].guestMemberCode;
            chatLogBar.transform.GetChild(0).GetComponent<RawImage>().texture = chatLog[i].profileImage.texture;
            chatLogBarItem.guestProfileImage = chatLog[i].profileImage;
        }
        StartCoroutine(downloadMyImage());
    }

    private void OnClickSet(DownloadHandler handler)
    {
        JsonString jsonString = JsonUtility.FromJson<JsonString>(handler.text);
        chatLogBarLength = jsonString.data.Count;
        for (int i = 0; i < chatLogBarLength; i++)
        {
            ChatLog newChatLogBar = new ChatLog();
            print(jsonString.data.Count);
            newChatLogBar.profileImage = tmpProfileImage;
            if (int.Parse(jsonString.data[i].memberCode1.ToString()) == memberCode)
            {   // 멤버코드1이 자기 자신인 상황이라면
                myId.text = jsonString.data[i].memberName1;
                newChatLogBar.guestMemberCode = int.Parse(jsonString.data[i].memberCode2.ToString());
                newChatLogBar.nickName = jsonString.data[i].memberName2.ToString();
                newChatLogBar.profileImageUrl = jsonString.data[i].memberRoomImage2.ToString();
                print(jsonString.data[i].lastMessage);
                Messages jsonMessage = JsonUtility.FromJson<Messages>(jsonString.data[i].lastMessage.ToString());
                newChatLogBar.lastMessage = jsonMessage.message.ToString();
                newChatLogBar.lastTime = jsonMessage.chatTime.ToString();
            }
            else
            {
                myId.text = jsonString.data[i].memberName2;
                newChatLogBar.guestMemberCode = int.Parse(jsonString.data[i].memberCode1.ToString());
                newChatLogBar.nickName = jsonString.data[i].memberName1.ToString();
                newChatLogBar.profileImageUrl = jsonString.data[i].memberRoomImage1.ToString();
                Messages jsonMessage = JsonUtility.FromJson<Messages>(jsonString.data[i].lastMessage.ToString());
                newChatLogBar.lastMessage = jsonMessage.message.ToString();
                newChatLogBar.lastTime = jsonMessage.chatTime.ToString();
            }
            chatLog.Add(newChatLogBar);
        }
    }

    public void backToMainPage()
    {
        SceneManager.LoadScene("MainScenes");
    }

    public void OnCreateChatLog()
    {
        print("버튼은 눌림");
        GameObject chatLogBar = Instantiate(ChatLogBarUIFactory, ChatLogBarListContent);
    }

    [System.Serializable]
    public class Messages
    {
        public int fromMemberCode;
        public int toMemberCode;
        public string message;
        public string chatTime;
    }

    [System.Serializable]
    public class JsonData
    {
        public string id;
        public int counter;
        public int memberCode1;
        public string memberName1;
        public int memberCode2;
        public string memberName2;
        public string memberRoomImage1;
        public string memberRoomImage2;
        public string lastMessage;
        public string uploadTime;
    }
    public class JsonString
    {
        public int status;
        public string message;
        public List<JsonData> data;
    }
}
