using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

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

    public void ReceiverOnValueChange(string valueIn)
    {
        inputField = valueIn;
        print(inputField);
    }

    public void OnCreateChat()
    {
        print("��ư�� ����");
        GameObject chatLogBar = Instantiate(chatItemUIFactory, chatItemListContent);
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

    public void backToChatListPage()
    {
        GameObject.Find("ChatPageCanvas").gameObject.SetActive(false);
    }

}
