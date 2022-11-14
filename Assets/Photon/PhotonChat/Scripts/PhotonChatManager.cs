using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username;
    public Text chatText;
    
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
            GameObject chat = Instantiate(ChatUIFactory, ChatListContent);
            chat.transform.GetChild(0).GetComponent<Text>().text = currentChat;
            chatField.text = "";
            currentChat = "";        
            //chatText.text = currentChat;
        }
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