using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

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

    List<ChatLog> chatLog = new List<ChatLog>();

    //int memberCode = HttpManager.instance.memberCode;

    int chatLogBarLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        // print(PlayerPrefs.GetString("token"));
        LoadChatList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // chatList�� ��ü �� �ҷ����� ��
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
        int processNum = 0;
        for (int i = 0; i < chatLogBarLength; i++)
        {
            processNum++;
            StartCoroutine(DownloadRoomImg(i));
            while (processNum > 1) yield return null;
            processNum--;
        }
        ApplyChatList();
    }

    private IEnumerator DownloadRoomImg(int imageIdx = 0)
    {            
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(chatLog[imageIdx].profileImageUrl);
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

    // �ڷ�ƾ ���� �� ê����Ʈ �������ִ� ��
    public void ApplyChatList()
    {
        for (int i = 0; i < chatLogBarLength; i++)
        {
            GameObject chatLogBar = Instantiate(ChatLogBarUIFactory, ChatLogBarListContent);
            ChatLogBarItem chatLogBarItem = chatLogBar.GetComponent<ChatLogBarItem>();

            chatLogBarItem.nickName.text = chatLog[i].nickName;
            chatLogBarItem.chatLogBarText.text = chatLog[i].lastMessage;
            chatLogBarItem.lastTime.text = chatLog[i].lastTime;
            chatLogBar.transform.GetChild(0).GetComponent<RawImage>().texture = chatLog[i].profileImage.texture;
            // chatLogBarItem.profileImage.texture = chatLog[i].profileImage.texture;
        }
    }

    public void LoadChat()
    {
        print("loadChatList");
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/chatting";
        requester.requestType = RequestType.GET;

        requester.onComplete = OnClickSet;
        StartCoroutine(HttpManager.instance.DownLoadChatList(requester));
    }

    private void OnClickSet(DownloadHandler handler)
    {
        JsonString jsonString = JsonUtility.FromJson<JsonString>(handler.text);
        ChatLog newChatLogBar = new ChatLog();
        chatLogBarLength = jsonString.data.Count;
        for (int i = 0; i < jsonString.data.Count; i++)
        {
            print(jsonString.data.Count);
            /*if (int.Parse(jsonString.data[i].memberCode1.ToString()) == memberCode)
            {   // ����ڵ�1�� �ڱ� �ڽ��� ��Ȳ�̶��
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
                newChatLogBar.guestMemberCode = int.Parse(jsonString.data[i].memberCode1.ToString());
                newChatLogBar.nickName = jsonString.data[i].memberName1.ToString();
                newChatLogBar.profileImageUrl = jsonString.data[i].memberRoomImage1.ToString();
                Messages jsonMessage = JsonUtility.FromJson<Messages>(jsonString.data[i].lastMessage.ToString());
                newChatLogBar.lastMessage = jsonMessage.message.ToString();
                newChatLogBar.lastTime = jsonMessage.chatTime.ToString();
            }
            chatLog.Add(newChatLogBar);*/
        }
    }

    public void OnCreateChatLog()
    {
        print("��ư�� ����");
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
