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
    public string memberCode; //==계정이름(아이디)

    // 마지막으로 쓴 글
    public Text chatLogBarText;

    //해당 채팅방의 마지막 기록 시간
    public Text lastTime;

    public RawImage profileImage;



    private void Awake()
    {
        nickName.text = "default_nickName";
        chatLogBarText.text = "확인좀 해보자";
    }
}