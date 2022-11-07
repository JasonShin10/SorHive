using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInManager : MonoBehaviour
{
    public static RoomInManager instance;

    private void Awake()
    {
        instance = this;
    }
    public int GuestBookNum = 1;

    public Transform GuestBookListContent;

    public GameObject GuestBookUIFactory;
    // Start is called before the first frame update
    void Start()
    {
        CreateFeedUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("MainScenes");
    }

    public void CreateFeedUI()
    {
        GuestBookNum = Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/").Length/2;
        GuestBookNum++;
        //�ǵ��� ������ �ҷ�����
        //LoadFeedData();
        // FeedManager.FeedNum;
        // print(Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/").Length);
        for (int i = 1; i < GuestBookNum; i++)
        {
            string path = Application.dataPath + "/SJH/GuestBookData/guestBookData" + i + ".txt";
            //print(Directory.GetFiles(Application.dataPath + "/SJH/GuestBookData/")[i]);
            //print(GuestBookNum + "�ǵ尳��");

            string jsonData = File.ReadAllText(path);

            //�ǵ� �������� ������ش�.
            GameObject guestBook = Instantiate(GuestBookUIFactory, GuestBookListContent);

            GuestBookInfo info = JsonUtility.FromJson<GuestBookInfo>(jsonData);

            GuestBookItem guestBookItem = guestBook .GetComponent<GuestBookItem>();

            guestBookItem.myGuestBookNum = info.myGuestBookNum;
            guestBookItem.guestBookText.text = info.guestBookText;
            //feedItem.feedtexture.texture = info.feedtexture;
        }
    }
}
