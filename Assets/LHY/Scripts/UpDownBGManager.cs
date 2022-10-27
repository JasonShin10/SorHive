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

    public void OnChatingPage()
    {
        SceneManager.LoadScene("FeedUpLoadScene");
        DontDestroyOnLoad(DontDestroy);
    }

    public void OnBackMain()
    {
        PhotonNetwork.LoadLevel("MainScenes");
    }

    
}
