using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestBookItem : MonoBehaviour
{
    //���� ��ȣ
    public int myGuestBoxNum = 0;

    //�ǵ带 �ø� ���(ID)
    public Text UserID; //==�����̸�(���̵�)

    //�ǵ忡 �� ��
    public Text guestBoxText;

    //�ش� �ǵ��� ���ƿ� ����
    public int Like = 0;

    //�ش� �ǵ��� ��� ����
    //public int currcomment = 0;

    //�ǵ� ��� �ý�Ʈ
    //public Text[] comments;
}
