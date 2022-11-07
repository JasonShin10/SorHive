using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//��������
public class GuestBookInfo
{
    //�ǵ� ��ȣ
    public int myGuestBookNum = 0;

    //�ǵ带 �ø� ���(ID)
    public string UserID; //==�����̸�(���̵�)

    //�ǵ忡 �� ��
    public string guestBookText;

    //�ǵ� ��� �ý�Ʈ
    public string[] comments; 
}


public class GuestBookManager : MonoBehaviour
{
    public Texture[] photos;

    //public GameObject GBoxManager;

    //�ӽ� ������Ʈ ���� ���� ����
    GuestBookInfo guestBookInfo = new GuestBookInfo();


    //���ù� ��ȣ
    public static int GuestBookNum = 0; //������ �ö� �� �� �ǵ尡 ���°�� �ö� �ǵ����� Ȯ���ϱ� ���� 


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
        guestBookInfo.guestBookText = upLoadText;             
    }

    public void OnClickSave()
    {
        RoomInManager.instance.GuestBookNum++;
        guestBookInfo.myGuestBookNum = 0;
        //�ǵ������� Json���� ��ȯ�Ѵ�.
        string jsonData = JsonUtility.ToJson(guestBookInfo, true);
        //print(jsonData);
        GuestBookNum = Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/").Length/2; 
        GuestBookNum++;

        string path = Application.dataPath + "/SJH/GuestBookData";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/guestBookData" + GuestBookNum + ".txt", jsonData);
        print(GuestBookNum);
        SceneManager.LoadScene("RoomInScene");
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
