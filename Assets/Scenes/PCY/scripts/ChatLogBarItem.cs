using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatLogBarItem : MonoBehaviour
{
    //채팅bar index
    public int chatLogBarItemNum = 0;

    //채팅 올린 사람(NickName)
    public Text nickName; //==계정이름(아이디)

    //채팅 올린 사람(NickName)
    public int memberCode; //==계정이름(아이디)

    // 마지막으로 쓴 글
    public Text chatLogBarText;

    //해당 채팅방의 마지막 기록 시간
    public Text lastTime;

    public RawImage guestProfileImage;

    public RawImage myProfileImage;



    private void Awake()
    {
        nickName.text = "default_nickName";
        chatLogBarText.text = "확인좀 해보자";
    }

    public void OpenChatPage()
    {
        print("개인채팅방 이동");
        ChatPageManager.instance.OpenChatPage(nickName.text, ChatManager.instance.myId.text, memberCode, guestProfileImage, myProfileImage);
    }
}