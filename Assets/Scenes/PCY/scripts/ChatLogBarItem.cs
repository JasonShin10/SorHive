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
    public string memberCode; //==�����̸�(���̵�)

    // ���������� �� ��
    public Text chatLogBarText;

    //�ش� ä�ù��� ������ ��� �ð�
    public Text lastTime;

    public RawImage profileImage;



    private void Awake()
    {
        nickName.text = "default_nickName";
        chatLogBarText.text = "Ȯ���� �غ���";
    }
}