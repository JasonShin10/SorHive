using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatLogBarItem : MonoBehaviour
{
    //ä��bar index
    public int chatLogBarItemNum = 0;

    //ä�� �ø� ���(NickName)
    public Text nickName; //==�����̸�(���̵�)

    //ä�� �ø� ���(NickName)
    public int memberCode; //==�����̸�(���̵�)

    // ���������� �� ��
    public Text chatLogBarText;

    //�ش� ä�ù��� ������ ��� �ð�
    public Text lastTime;

    public RawImage guestProfileImage;

    public RawImage myProfileImage;



    private void Awake()
    {
        nickName.text = "default_nickName";
        chatLogBarText.text = "Ȯ���� �غ���";
    }

    public void OpenChatPage()
    {
        print("����ä�ù� �̵�");
        ChatPageManager.instance.OpenChatPage(nickName.text, ChatManager.instance.myId.text, memberCode, guestProfileImage, myProfileImage);
    }
}