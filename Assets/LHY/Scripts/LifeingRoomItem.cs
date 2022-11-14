using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeingRoomItem : MonoBehaviour
{
    public RawImage avatarImage;
    public RawImage roomImage;
    public Text memberName;
    public RawImage ProfileImage;



    public string roomImg;
    public string avatarImg;


    public LifeingManager lifeingManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject lifeingmanager = GameObject.Find("LifeingPosManager");
        lifeingManager = lifeingmanager.GetComponent<LifeingManager>();
        //StartCoroutine(GetTextureR(roomImage, avatarImage));
        GetTextureR(roomImage, avatarImage);
        ProfileImage.texture = roomImage.texture;
      

        //for()
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStoryViewScene()
    {
        SceneManager.LoadScene("StoryViewScene");
    }

    //IEnumerator GetTextureR(RawImage roomImage, RawImage avatarImage)
    //{
        
    //        //lifeingRoomItem.roomImage = friendList[i].roomImage
    //        var urlR = roomImg;
    //        var urlA = avatarImg;


    //        UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
    //        yield return wwwR.SendWebRequest();

    //        UnityWebRequest wwwA = UnityWebRequestTexture.GetTexture(urlA);
    //        yield return wwwA.SendWebRequest();

            //if (wwwR.result != UnityWebRequest.Result.Success)
            //    Debug.Log(wwwR.error);
            //else
            //    roomImage.texture = ((DownloadHandlerTexture) wwwR.downloadHandler).texture;

            //if (wwwA.result != UnityWebRequest.Result.Success)
            //    Debug.Log(wwwA.error);
            //else
            //    avatarImage.texture = ((DownloadHandlerTexture) wwwA.downloadHandler).texture;
            

    //        //yield return WaitForSeconds(0.1);
        


    //}

    public void GetTextureR(RawImage roomImage, RawImage avatarImage)
    {
        var urlR = roomImg;
        var urlA = avatarImg;
        UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
        UnityWebRequest wwwA = UnityWebRequestTexture.GetTexture(urlA);
        if (wwwR.result != UnityWebRequest.Result.Success)
            Debug.Log(wwwR.error);
        else
            roomImage.texture = ((DownloadHandlerTexture)wwwR.downloadHandler).texture;

        if (wwwA.result != UnityWebRequest.Result.Success)
            Debug.Log(wwwA.error);
        else
            avatarImage.texture = ((DownloadHandlerTexture)wwwA.downloadHandler).texture;

    }
}