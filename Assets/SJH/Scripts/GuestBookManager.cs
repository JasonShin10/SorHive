using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;


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
    public GameObject chat;
    //public GameObject GBoxManager;

    //�ӽ� ������Ʈ ���� ���� ����
    GuestBookInfo guestBookInfo = new GuestBookInfo();
    public string upLoadText;

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
        upLoadText = mytext.text;
        
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
    #region GuestBookPost
    public void OnSaveGuestBook()
    {
        GuestBookJsonInfo info = new GuestBookJsonInfo();
        info.content = upLoadText;
        print(upLoadText);
        //info.offlineRoomImage = File.ReadAllBytes(Application.dataPath + "/Resources/ZRoomImage/my0.png");
        info.roomId = HttpManager.instance.roomId;
        //ArrayJson<ObjectInfo> arrayJson = new ArrayJson<ObjectInfo>();
        //arrayJson.furnitures = objectInfoList;
        //������ �Խù� ��ȸ ��û(/posts/1 , Get)
        HttpRequester requester = new HttpRequester();
        /// POST, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requester.url = "http://52.79.209.232:8080/api/v1/guestbook ";
        requester.requestType = RequestType.POST;
        //post data ����
        requester.postData = JsonUtility.ToJson(info, true);
        requester.onComplete = OnCompleteSaveGuestBook;
        //HttpManager���� ��û
        HttpManager.instance.SendRequest(requester);
    }
    #endregion
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
    public void OnCompleteSaveGuestBook(DownloadHandler handler)
    {
        print(handler);
        string s = "{\"furniture\":" + handler.text + "}";
        PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);

    }

    public void OnClickChat()
    {
        if(chat.activeSelf)
        {

        chat.SetActive(false);
        }
        else
        {
            chat.SetActive(true);
        }
    }

}
