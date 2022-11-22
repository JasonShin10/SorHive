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
    int response_status;

    string inputField;

    public GameObject chatItemUIFactory;
    public Transform chatItemListContent;

    // Start is called before the first frame update
    void Start()
    {
        // Ȱ��ȭ!
        LoadChat();
    }

    public void LoadChat()
    {
        print("loadChat");
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/chatting";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnClickSet;
        StartCoroutine(HttpManager.instance.DownLoadChatList(requester));
    }

    public void SendChat()
    {

    }

    

    public void OnCreateChat()
    {
        print("��ư�� ����");
        GameObject chatLogBar = Instantiate(chatItemUIFactory, chatItemListContent);
    }

    [SerializeField] GameObject chatPage;
    public void OpenChatPage(string guestNickName, string myNickName, int guestMemberCode, RawImage guestProfileImage, RawImage myProfileImage)
    {
        chatPage.transform.GetChild(0).gameObject.SetActive(true);
        ChatPageManager.instance.guestNickName.text = guestNickName;
        ChatPageManager.instance.myNickName.text = myNickName;
        ChatPageManager.instance.guestMemberCode = guestMemberCode;
        ChatPageManager.instance.guestProfileImage.texture = guestProfileImage.texture;
        ChatPageManager.instance.myProfileImage.texture = myProfileImage.texture;
    }


    private void OnClickSet(DownloadHandler handler)
    {
        JsonString jsonString = JsonUtility.FromJson<JsonString>(handler.text);
        if (jsonString.status == 200)
        {
            for (int i = 0; i < jsonString.data.messages.Count; i++)
            {
                GameObject chatItemPrefeb = Instantiate(chatItemUIFactory, chatItemListContent);
                ChatItem chatItem = chatItemPrefeb.GetComponent<ChatItem>();
                if (jsonString.data.messages[i].fromMemberCode == guestMemberCode)
                {   // ���� ����� ������ ��
                    chatItem.isMe = false;
                    chatItem.nickName.text = guestNickName.text;
                    chatItem.transform.GetChild(0).GetComponent<RawImage>().texture = guestProfileImage.texture;
                }
                else
                {
                    chatItem.isMe = true;
                    chatItem.nickName.text = myNickName.text;
                    chatItem.transform.GetChild(0).GetComponent<RawImage>().texture = myProfileImage.texture;
                }
                chatItem.writeTime.text = jsonString.data.messages[i].chatTime.ToString();
                chatItem.chatText.text = jsonString.data.messages[i].message.ToString();

                // ���� ���������� �и� ���� isMe�� �̿��ؼ�
            }
        }
        else
        {
            print("response_data_error");
        }
    }

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
        public List<JsonMessages> messages;
        public string uploadTime;
    }
    public class JsonString
    {
        public int status;
        public string message;
        public JsonData data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���ư���

    public void backToChatListPage()
    {
        GameObject.Find("GuestBox").gameObject.SetActive(false);
        SendChatToServer();
    }


    // ä�� ������
    public List<ChatMessageInfo> total_messages = new List<ChatMessageInfo>();
    public void ReceiverOnValueChange(string valueIn)
    {
        print(valueIn);
        if (total_messages.Count != 0 && Input.GetKey(KeyCode.Return))
        {
            insertMessageToList(valueIn, DateTime.Now.ToString());
        }
    }

    private void insertMessageToList(string currentChat, string nowTime)
    {
        ChatMessageInfo messages = new ChatMessageInfo();
        messages.fromMemberCode = HttpManager.instance.memberCode;
        messages.toMemberCode = messages.fromMemberCode + 1;
        messages.message = currentChat;
        messages.chatTime = nowTime;
        print("ä�� �ð�: " + messages.chatTime.ToString());
        total_messages.Add(messages);
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
        // �׵��� �ϳ��� �׾Ƴ��� �޼��� ��ü���� ���� ��ü�� �Ҵ����ְ�
        // �����ϰ� ����
        chatData.messages = total_messages;

        // ������ ���� �����Ѵ�.
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/chatting";
        requester.requestType = RequestType.POST;
        requester.postData = JsonUtility.ToJson(chatData, true);
        print("�̰� ���°ǵ� ");
        print(requester.postData);

        // �г��� ������ �Լ�
        // PhotonNetwork.PlayerList[i].NickName

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
