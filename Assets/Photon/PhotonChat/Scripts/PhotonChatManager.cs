using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class ChatMessageInfo
{
    public int fromMemberCode;
    public int toMemberCode;
    public string message;
    public string chatTime;
}


[System.Serializable]
public class ChatInfo
{
    public int memberCode1;
    public int memberCode2;
    public List<string> messages;
}

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    public static PhotonChatManager instance;
    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected = false;
    [SerializeField] string username;
    int response_status = 0;
    public List<string> total_messages = new List<string>();

    private void Awake()
    {
        //만약에 instance가 null이라면
        if (instance == null)
        {
            //instance에 나를 넣겠다.
            instance = this;
        }
    }

    public void UsernameOnValueChange(string valueIn)
    {
        username = valueIn;
    }

    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        print("Connecting");
    }

    [SerializeField] GameObject chatPanel;
    string privateReceiver = "";
    string currentChat;
    [SerializeField] InputField chatField;
    [SerializeField] Text chatDisplay;
    public Transform ChatListContent;
    public GameObject ChatUIFactory;

    // Start is called before the first frame update
    void Start()
    {
        chatField.onValueChanged.AddListener(TypeChatOnValueChange);
        ChatConnectOnClick();
    }

    // Update is called once per frame 
    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if(chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }
       
    }
    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }
    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
       
    }


 

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisplay.text += "\n" + msgs;
            //print(msgs);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";

        msgs = string.Format("(Private) {0}: {1}", sender, message);

        chatDisplay.text += "\n" + msgs;

        Debug.Log(msgs);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
       
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
      
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnConnected()
    {
        print("Connected");
        joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel" });
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }
    public void OnUserUnsubscribed(string channel, string user)
    {
       
    }

    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == "")
        {

            chatClient.PublishMessage("RegionChannel", currentChat);
            print(currentChat);

            string nowTime = DateTime.Now.ToString();
            // 서버에 채팅 기록 송신 코드
            insertMessageToList(currentChat, nowTime);
            // 챗박스 프리펩을 생성하여 채팅을 보여준다.

            GameObject chat = Instantiate(ChatUIFactory, ChatListContent);
            chat.transform.GetChild(0).GetComponent<Text>().text = currentChat;
            chatField.text = "";
            currentChat = "";        
            //chatText.text = currentChat;
        }
    }

    private void insertMessageToList(string currentChat, string nowTime)
    {
        ChatMessageInfo messages = new ChatMessageInfo();
        messages.fromMemberCode = HttpManager.instance.memberCode;
        // 포톤에서 플레이어 리스트로 닉네임 받기.
        // PhotonNetwork.PlayerList[i].NickName
        messages.toMemberCode = messages.fromMemberCode + 1;
        messages.message = currentChat;
        messages.chatTime = nowTime;
        print("채팅 시간: " + messages.chatTime.ToString());
        total_messages.Add(JsonUtility.ToJson(messages));
    }

    public void SendChatToServer()
    {
        ChatInfo chatData = new ChatInfo();
        int fromMemberCode = HttpManager.instance.memberCode;
        // int toMemberCode = PhotonNetwork.PlayerList[0].NickName;
        int toMemberCode = fromMemberCode + 1;
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
        print("이게 가는건데 ");
        print(PlayerPrefs.GetString("token"));
        print(requester.postData);

        // 닉네임 따오는 함수
        // PhotonNetwork.PlayerList[i].NickName

        requester.onComplete = OnClickSet;
        HttpManager.instance.SendRequest(requester);
    }

    private void OnClickSet(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        print("handler start");
        response_status = int.Parse(json["status"].ToString());
        total_messages.Clear();
    }

    public void SubmitPrivateChatOnClick()
    {      
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
            GameObject chat = Instantiate(ChatUIFactory, ChatListContent);
            chat.transform.GetChild(0).GetComponent<Text>().text = currentChat;
            //chatText.text = chatDisplay.text;
        }
    }

}
