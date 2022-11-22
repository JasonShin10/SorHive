using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ä�� �ϳ��� ���� ��Ÿ��
public class ChatItem : MonoBehaviour
{
    //ä��bar index
    public int chatItemNum = 0;

    // �ڽ��� ���´��� ����
    public bool isMe;

    public Text nickName;

    public RawImage profileImage;

    // ä��
    public Text chatText;

    // ä�� �� �ð�
    public Text writeTime;

    private void Awake()
    {
        chatText.text = "Ȯ���� �غ���";
    }
}