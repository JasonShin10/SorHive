using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//��������
public class GuestBoxInfo
{
    //�ǵ� ��ȣ
    public int myGuestBoxNum = 0;

    //�ǵ带 �ø� ���(ID)
    public string UserID; //==�����̸�(���̵�)

    //�ǵ忡 �� ��
    public string guestBoxText;

    //�ǵ� ��� �ý�Ʈ
    public string[] comments; 
}


public class GuestBoxManager : MonoBehaviour
{
    public Texture[] photos;

    //public GameObject GBoxManager;

    //�ӽ� ������Ʈ ���� ���� ����
    GuestBoxInfo guestBoxInfo = new GuestBoxInfo();


    //���ù� ��ȣ
    public static int GuestBoxNum = 0; //������ �ö� �� �� �ǵ尡 ���°�� �ö� �ǵ����� Ȯ���ϱ� ���� 


    public Text mytext;


    void Start()
    {
       
    }

    void Update()
    {
      
    }

    public void OnClickfill()
    {
        //����ڰ� �Է��� Text�� ��Ʈ�������� ��ȯ�� �����Ѵ�.
        string upLoadText = mytext.text;
        
        //GuestInfo�� �ǵ� �ؽ�Ʈ�� upLoadText�� �Է°��� ����ȭ�Ѵ�.
        guestBoxInfo.guestBoxText = upLoadText;             
    }

    public void OnClickSave()
    {
        
        guestBoxInfo.myGuestBoxNum = 0;
        //�ǵ������� Json���� ��ȯ�Ѵ�.
        string jsonData = JsonUtility.ToJson(guestBoxInfo, true);
        print(jsonData);
        GuestBoxNum++;
        string path = Application.dataPath + "/SJH/GuestBoxData";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/GuestBoxData" + GuestBoxNum + ".txt", jsonData);
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
