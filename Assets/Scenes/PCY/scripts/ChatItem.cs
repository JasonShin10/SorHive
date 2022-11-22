using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 채팅 하나의 블럭을 나타냄
public class ChatItem : MonoBehaviour
{
    //채팅bar index
    public int chatItemNum = 0;

    // 자신이 보냈는지 여부
    public bool isMe;

    public Text nickName;

    public RawImage profileImage;

    // 채팅
    public Text chatText;

    // 채팅 쓴 시간
    public Text writeTime;

    private void Awake()
    {
        chatText.text = "확인좀 해보자";
    }
}