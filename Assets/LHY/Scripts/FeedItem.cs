using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedItem : MonoBehaviour
{
    //�ǵ� ��ȣ
    public int myfeedNum = 0;

    //�ǵ带 �ø� ���(ID)
    public Text UserID; //==�����̸�(���̵�)

    //�ǵ忡 �ø� ����
    public RawImage feedtexture;

    //�ǵ忡 �� ��
    public Text feedText;

    //�ش� �ǵ��� ���ƿ� ����
    public int Like = 0;

    //�ش� �ǵ��� ��� ����
    public int currcomment = 0;

    //�ǵ� ��� �ý�Ʈ
    public Text[] comments;
}
