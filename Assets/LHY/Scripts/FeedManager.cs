using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ǵ�����
public class FeedInfo
{
    //�ǵ� ��ȣ
    public int myfeedNum = 0;

    //�ǵ带 �ø� ���(ID)
    public string UserID; //==�����̸�(���̵�)

    //�ǵ忡 �ø� ����
    public string feedtexture;

    //�ǵ忡 �� ��
    public string feedText;

    //�ش� �ǵ��� ���ƿ� ����
    public int Like = 0;

    //�ش� �ǵ��� ��� ����
    public int currcomment = 0;

    //�ǵ� ��� �ý�Ʈ
    public string[] comments; 
}


public class FeedManager : MonoBehaviour
{

    public GameObject feedManager;

    //�ӽ� ������Ʈ ���� ���� ����
    FeedInfo feedInfo = new FeedInfo();


    //���ù� ��ȣ
    public static int FeedNum = 0; //������ �ö� �� �� �ǵ尡 ���°�� �ö� �ǵ����� Ȯ���ϱ� ���� 

    //�����ø� ������ �ؽ��� ������ ���� ����
    public Texture mytextur;

    public Text mytext;


    void Start()
    {
       
    }

    void Update()
    {
      
    }

    public void OnClickfill()
    {
        
        //upLoad�� �̹����� �ؽ��������������Ѵ�.
       // string upLoadImage = my;

        //����ڰ� �Է��� Text�� ��Ʈ�������� ��ȯ�� �����Ѵ�.
        string upLoadText = mytext.text;
        
        //feedInfo �� �ǵ� �ο��̹��� �ؽ��ĸ� upLoadImage�� ����ȭ�Ѵ�.
        //feedInfo.feedtexture = upLoadImage;
        //feedInfo�� �ǵ� �ؽ�Ʈ�� upLoadText�� �Է°��� ����ȭ�Ѵ�.
        feedInfo.feedText = upLoadText;

        feedInfo.myfeedNum++;        
    }

    public void OnClickSave()
    {
        //�ǵ������� Json���� ��ȯ�Ѵ�.
        string jsonData = JsonUtility.ToJson(feedInfo, true);
        print(jsonData);
        FeedNum++;
    }

    public void OnClickLoad()
    {
        if (FeedNum != 0)
        {
            //�ҷ�����
        }
    }
}
