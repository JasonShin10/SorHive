using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//방명록정보
public class GuestBookInfo
{
    //피드 번호
    public int myGuestBookNum = 0;

    //피드를 올린 사람(ID)
    public string UserID; //==계정이름(아이디)

    //피드에 쓴 글
    public string guestBookText;

    //피드 댓글 택스트
    public string[] comments; 
}


public class GuestBookManager : MonoBehaviour
{
    public Texture[] photos;

    //public GameObject GBoxManager;

    //임시 오브젝트 정보 담을 변수
    GuestBookInfo guestBookInfo = new GuestBookInfo();


    //개시물 번호
    public static int GuestBookNum = 0; //서버에 올라갈 때 이 피드가 몇번째로 올라간 피드인지 확인하기 위함 


    public Text mytext;


    void Start()
    {
       
    }

    void Update()
    {
      
    }

    public void OnClickfill()
    {
        //사용자가 입력한 Text를 스트링값으로 변환해 저장한다.
        string upLoadText = mytext.text;
        
        //GuestInfo에 피드 텍스트를 upLoadText의 입력값과 동기화한다.
        guestBookInfo.guestBookText = upLoadText;             
    }

    public void OnClickSave()
    {
        RoomInManager.instance.GuestBookNum++;
        guestBookInfo.myGuestBookNum = 0;
        //피드정보를 Json으로 변환한다.
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
                //불 러오기               
            }

        }
    }*/
}
