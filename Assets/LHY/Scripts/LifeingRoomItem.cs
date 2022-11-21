using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LifeingRoomItem : MonoBehaviour
{
    public RawImage avatarImage;
    public RawImage roomImage;
    public Text memberName;
    public RawImage ProfileImage;

    public string lifingYn;
    public int lifingNo;
    public int lifingCategoryNo;
    public string memberId;

    public string roomImg;
    public string avatarImg;


    public LifeingManager lifeingManager;

    public bool LifeingLoad = false;

    public bool roomY = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject lifeingmanager = GameObject.Find("LifeingPosManager");
        lifeingManager = lifeingmanager.GetComponent<LifeingManager>();
        StartCoroutine(GetTextureR(roomImage, avatarImage));
        ProfileImage.texture = roomImage.texture;




        //for()
    }

    // Update is called once per frame
    void Update()
    {
        if (lifingYn == "Y")
        {
            if (LifeingLoad == false)
            {
                roomY = true;
                print("라이핑 룸스토리 있음!!");
                //Texture2D tex = new Texture2D(0, 0);
                //Resources.Load("/ 02.Story / StoryRoom / " + lifingCategoryNo + "_" + lifingNo + ".png");
                var temp = File.ReadAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + lifingCategoryNo + "_" + lifingNo + ".png");

                print(lifingCategoryNo + ("카테고리 번호") + lifingNo + ("라이핑 이미지 번호"));
                //tex = Resources.Load("02.Story / StoryRoom /" + lifingCategoryNo + "_" + lifingNo + ".png", typeof(Texture2D)) as Texture2D;

                Texture2D tex = new Texture2D(0, 0);
                tex.LoadImage(temp);

                //tex.LoadImage(temp);

                roomImage.texture = tex;
                LifeingLoad = true;
            }

        }
        else
        {
            return;
        }

    }

    public void OnClickStoryViewScene()
    {
        SceneManager.LoadScene("StoryViewScene");
    }

    IEnumerator GetTextureR(RawImage roomImage, RawImage avatarImage)
    {
        
            //lifeingRoomItem.roomImage = friendList[i].roomImage
            var urlR = roomImg;
            var urlA = avatarImg;


            UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
            yield return wwwR.SendWebRequest();

            UnityWebRequest wwwA = UnityWebRequestTexture.GetTexture(urlA);
            yield return wwwA.SendWebRequest();

        if (roomY == false)
        {
            if (wwwR.result != UnityWebRequest.Result.Success)
                Debug.Log(wwwR.error);
            else
                roomImage.texture = ((DownloadHandlerTexture)wwwR.downloadHandler).texture;
        }
      

            if (wwwA.result != UnityWebRequest.Result.Success)
                Debug.Log(wwwA.error);
            else
                avatarImage.texture = ((DownloadHandlerTexture)wwwA.downloadHandler).texture;
            

            //yield return WaitForSeconds(0.1);
    }
}