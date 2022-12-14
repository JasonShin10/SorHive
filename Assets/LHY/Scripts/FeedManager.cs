using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//피드정보
public class FeedInfo
{
    //피드 번호
    public int myfeedNum = 0;

    //피드를 올린 사람(ID)
    public string UserID; //==계정이름(아이디)

    //피드에 올린 사진
    public int feedtextureNum;

    //피드에 쓴 글
    public string feedText;

    //해당 피드의 좋아요 개수
    public int Like = 0;

    //해당 피드의 댓글 개수
    public int currcomment = 0;

    //피드 댓글 택스트
    public string[] comments; 
}


public class FeedManager : MonoBehaviour
{
    public Texture[] photos;



    public GameObject feedManager;

    //임시 오브젝트 정보 담을 변수
    FeedInfo feedInfo = new FeedInfo();


    //개시물 번호
    public static int FeedNum = 0; //서버에 올라갈 때 이 피드가 몇번째로 올라간 피드인지 확인하기 위함 


    public Text mytext;


    void Start()
    {
       
    }

    void Update()
    {
      
    }

    public void OnClickfill()
    {

       

        //upLoad된 이미지의 텍스쳐정보를저장한다.(PC)
        int upLoadImagePath = PhotoButton.instence.path;

        //upLoad된 이미지의 텍스쳐정보를저장한다.(Mobile)
        //string upLoadImagePate1 = Application.persistentDataPath;

        //사용자가 입력한 Text를 스트링값으로 변환해 저장한다.
        string upLoadText = mytext.text;
        
        //feedInfo 에 피드 로우이미지 텍스쳐를 upLoadImage와 동기화한다.
        
        feedInfo.feedtextureNum = upLoadImagePath;
        //feedInfo에 피드 텍스트를 upLoadText의 입력값과 동기화한다.
        feedInfo.feedText = upLoadText;             
    }

    public void OnClickSave()
    {
        LobbyManager.instence.FeedNum++;

        feedInfo.myfeedNum = 0;
        //피드정보를 Json으로 변환한다.
        string jsonData = JsonUtility.ToJson(feedInfo, true);
        print(jsonData);
        FeedNum++;

        print("사진번호"+feedInfo.feedtextureNum);

        string path = Application.persistentDataPath + "/LHY/FeedData";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        File.WriteAllText(path + "/feedData" + FeedNum + ".txt", jsonData);
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
