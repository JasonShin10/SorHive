using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//�ǵ�����
public class GuextBoxInfo
{
    //�ǵ� ��ȣ
    public int myfeedNum = 0;

    //�ǵ带 �ø� ���(ID)
    public string UserID; //==�����̸�(���̵�)

    //�ǵ忡 �� ��
    public string feedText;

    //�ǵ� ��� �ý�Ʈ
    public string[] comments; 
}


public class GuestBoxManager : MonoBehaviour
{
    public Texture[] photos;


    public GameObject GeustBoxManager;

    //�ӽ� ������Ʈ ���� ���� ����
    FeedInfo feedInfo = new FeedInfo();


    //���ù� ��ȣ
    public static int FeedNum = 0; //������ �ö� �� �� �ǵ尡 ���°�� �ö� �ǵ����� Ȯ���ϱ� ���� 


    public Text mytext;


    void Start()
    {
       
    }

    void Update()
    {
      
    }

    public void OnClickfill()
    {
        //upLoad�� �̹����� �ؽ��������������Ѵ�.(PC)
        //int upLoadImagePath = PhotoButton.instence.path;

        //upLoad�� �̹����� �ؽ��������������Ѵ�.(Mobile)
        //string upLoadImagePate1 = Application.persistentDataPath;

        //����ڰ� �Է��� Text�� ��Ʈ�������� ��ȯ�� �����Ѵ�.
        string upLoadText = mytext.text;
        
        //feedInfo �� �ǵ� �ο��̹��� �ؽ��ĸ� upLoadImage�� ����ȭ�Ѵ�.
        
        //feedInfo.feedtextureNum = upLoadImagePath;
        //feedInfo�� �ǵ� �ؽ�Ʈ�� upLoadText�� �Է°��� ����ȭ�Ѵ�.
        feedInfo.feedText = upLoadText;             
    }

    public void OnClickSave()
    {
        //LobbyManager.instence.FeedNum++;

        feedInfo.myfeedNum = 0;
        //�ǵ������� Json���� ��ȯ�Ѵ�.
        string jsonData = JsonUtility.ToJson(feedInfo, true);
        print(jsonData);
        FeedNum++;

        string path = Application.dataPath + "/SJH/GuestBoxData";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/GuestBoxData" + FeedNum + ".txt", jsonData);
    }

   /* public void OnClickLoad()
    {
        if (FeedNum != 0)
        {
            for(int i =0; i< FeedNum; i++)
            {
                //�� ������               
            }

        }
    }*/
}
