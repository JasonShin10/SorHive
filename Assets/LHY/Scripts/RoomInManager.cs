using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInManager : MonoBehaviour
{
    public int GuestBoxNum = 1;

    public Transform GuestBoxListContent;

    public GameObject GuestBoxUIFactory;
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
        GuestBoxNum = Directory.GetFiles(Application.dataPath + "/SJH/GuestBoxData/").Length;
        //�ǵ��� ������ �ҷ�����
        //LoadFeedData();
        // FeedManager.FeedNum;
        for (int i = 1; i <= GuestBoxNum; i++)
        {
            string path = Application.dataPath + "/SJH/GuestBoxData/GuestBoxData" + i + ".txt";

            print(GuestBoxNum + "�ǵ尳��");

            string jsonData = File.ReadAllText(path);

            //�ǵ� �������� ������ش�.
            GameObject feed = Instantiate(GuestBoxUIFactory, GuestBoxListContent);

            FeedInfo info = JsonUtility.FromJson<FeedInfo>(jsonData);

            FeedItem feedItem = feed.GetComponent<FeedItem>();

            feedItem.myfeedNum = info.myfeedNum;
            feedItem.feedText.text = info.feedText;
            //feedItem.feedtexture.texture = info.feedtexture;
        }
    }
}
