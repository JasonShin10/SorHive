using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestBookItem : MonoBehaviour
{
    //���� ��ȣ
    public int myGuestBookNum = 0;

    //�ǵ带 �ø� ���(ID)
    public Text UserID; //==�����̸�(���̵�)

    //�ǵ忡 �� ��
    public Text guestBookText;

    //�ش� �ǵ��� ���ƿ� ����
    public int Like = 0;

    public Button delete;

    public Text guestBookId;
    //�ش� �ǵ��� ��� ����
    //public int currcomment = 0;

    //�ǵ� ��� �ý�Ʈ
    //public Text[] comments;
}
