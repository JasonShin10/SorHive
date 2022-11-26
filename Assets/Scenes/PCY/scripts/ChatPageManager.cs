using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System;




public class ChatPageManager : MonoBehaviour
{
    public static ChatPageManager instance;
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
    public Text guestNickName;
    public Text myNickName;
    public int guestMemberCode = 0;
    public RawImage guestProfileImage;
    public RawImage myProfileImage;

    public InputField inputChat;
    int response_status;

    public GameObject chatItemUIFactory;
    public Transform chatItemListContent;

    // Start is called before the first frame update
    void Start()
    {
        inputChat.text = "";
    }

    public void LoadChat()
    {
        print("loadChat");
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/chatting/" + guestMemberCode.ToString();
        print(requester.url);
        requester.requestType = RequestType.GET;
        requester.onComplete = OnClickSet;
        StartCoroutine(HttpManager.instance.DownLoadChat(requester));
    }

    public void SendChat()
    {

    }

    public void OnCreateChat()
    {
        print("버튼은 눌림");
        GameObject chatLogBar = Instantiate(chatItemUIFactory, chatItemListContent);
    }

    [SerializeField] GameObject chatPage;
    [SerializeField] GameObject chatListPage;
    public void OpenChatPage(string guestNickName, string myNickName, int guestMemberCode, RawImage guestProfileImage)
    {
        chatPage.transform.GetChild(0).gameObject.SetActive(true);
        chatListPage.transform.GetChild(0).gameObject.SetActive(false);
        this.guestNickName.text = guestNickName;
        this.myNickName.text = myNickName;
        this.guestMemberCode = guestMemberCode;
        this.guestProfileImage = guestProfileImage;
        print("현재 채팅방");
        print(guestMemberCode);
        LoadChat();
    }


    private void OnClickSet(DownloadHandler handler)
    {
        JsonString jsonString = JsonUtility.FromJson<JsonString>(handler.text);
        if (jsonString.status == 200)
        {
            print(jsonString.data.messages.Count);
            for (int i = 0; i < jsonString.data.messages.Count; i++)
            {
                JsonMessages jsonMessages = JsonUtility.FromJson<JsonMessages>(jsonString.data.messages[i]);
                GameObject chatItemPrefeb = Instantiate(chatItemUIFactory, chatItemListContent);
                ChatItem chatItem = chatItemPrefeb.GetComponent<ChatItem>();
                if (jsonMessages.fromMemberCode == guestMemberCode)
                {   // 보낸 사람이 상대방일 때
                    chatItem.isMe = false;
                    chatItem.nickName.text = guestNickName.text;
                    chatItem.transform.GetChild(0).GetComponent<RawImage>().texture = myProfileImage.texture;
                }
                else
                {
                    chatItem.isMe = true;
                    chatItem.nickName.text = myNickName.text;
                    chatItem.transform.GetChild(0).GetComponent<RawImage>().texture = guestProfileImage.texture;
                }
                chatItem.writeTime.text = jsonMessages.chatTime;
                chatItem.chatText.text = jsonMessages.message;
                // 왼쪽 오른쪽으로 분리 가능 isMe를 이용해서
            }
        }
        else
        {
            print("response_data_error");
        }
    }

    [System.Serializable]
    public class JsonMessages
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
        public string memberRoomImage1;
        public int memberCode2;
        public string memberName2;
        public string memberRoomImage2;
        public List<string> messages;
        public string uploadTime;
    }
    public class JsonString
    {
        public int status;
        public string message;
        public JsonData data;
    }


    public GameObject sendButtonClickedImage;
    public GameObject sendButtonImage;

    private bool m_IsButtonDowning = false;
    // send버튼 눌렸을 때
    public void PointerDown()
    {
        m_IsButtonDowning = true;
    }

    public void PointerUp()
    {
        m_IsButtonDowning = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_IsButtonDowning)
        {
            sendButtonClickedImage.SetActive(true);
            sendButtonImage.SetActive(false);
        }
        else
        {
            sendButtonClickedImage.SetActive(false);
            sendButtonImage.SetActive(true);
        }
        if (inputChat != null)
        {
            if (inputChat.text != "" && Input.GetKey(KeyCode.Return))
            {
                insertMessageToList(inputChat.text, DateTime.Now.ToString());
            }
        }
    }

    // 돌아가기 
    public void backToChatListPage()
    {

        if (total_messages.Count != 0)
        {
            SendChatToServer();
            total_messages.Clear();
        }
        GameObject.Find("ChatPage").transform.GetChild(0).gameObject.SetActive(false);

        Transform chatContent = GameObject.Find("ChatPage").transform.GetChild(0).transform.GetChild(0).transform.GetChild(1);
        print(chatContent.name);
        foreach (Transform child in chatContent.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject.Find("ChatListPageCanvas").transform.GetChild(0).gameObject.SetActive(true);
    }

    // 채팅 보내기
    public List<string> total_messages = new List<string>();
    
    public void insertMessageToList(string currentChat, string nowTime)
    {
        ChatMessageInfo messages = new ChatMessageInfo();
        messages.fromMemberCode = HttpManager.instance.memberCode;
        messages.toMemberCode = guestMemberCode;
        messages.message = currentChat;
        messages.chatTime = nowTime;
        print("채팅 시간: " + messages.chatTime.ToString());
        total_messages.Add(JsonUtility.ToJson(messages, true).ToString());
        inputChat.text = "";
    }

    public void OnCLickSend()
    {
        insertMessageToList(inputChat.text, DateTime.Now.ToString());
    }

    public void SendChatToServer()
    {

        ChatInfo chatData = new ChatInfo();
        int fromMemberCode = HttpManager.instance.memberCode;
        int toMemberCode = guestMemberCode;
        if (fromMemberCode > toMemberCode)
        {
            chatData.memberCode1 = toMemberCode;
            chatData.memberCode2 = fromMemberCode;
        }
        else
        {
            chatData.memberCode2 = toMemberCode;
            chatData.memberCode1 = fromMemberCode;
        }
        // 그동안 하나씩 쌓아놓은 메세지 객체들을 보낼 객체에 할당해주고
        // 깨끗하게 비우기
        chatData.messages = total_messages;

        // 서버에 보내 저장한다.
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/chatting";
        requester.requestType = RequestType.POST;
        requester.postData = JsonUtility.ToJson(chatData, true);
        print(requester.postData);

        requester.onComplete = OnClickUpload;
        HttpManager.instance.SendRequest(requester);
    }

    private void OnClickUpload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        print("handler start");
        response_status = int.Parse(json["status"].ToString());
        total_messages.Clear();
    }



}
