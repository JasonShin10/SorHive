using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UpDownBGManager : MonoBehaviour
{
    public GameObject DontDestroy;


    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnFeedUpLoad()
    {
        SceneManager.LoadScene("FeedUpLoadScene");
    }

    public void OnUpLoadImage()
    {
       
        SceneManager.LoadScene("FeedUpLoadScene");
        DontDestroyOnLoad(DontDestroy);
    }

    public void OnChatScene()
    {
        SceneManager.LoadScene("ChatScene");
    }

    public void OnChatingPage()
    {
        SceneManager.LoadScene("ChatScene");
    }

    public void OnBackMain()
    {
        
        PhotonNetwork.LoadLevel("MainScenes");
    }

    public void ExitLifeingUpLoad()
    {
        SceneManager.LoadScene("MainScenes");
        GameObject.Find("LifeingUpLoadCanvas").gameObject.SetActive(false) ;
    }

}
