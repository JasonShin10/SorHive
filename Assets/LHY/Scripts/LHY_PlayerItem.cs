using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo
{
    public int faceType;
    public int eyeBrowsType;
    public int eyeType;
    public int hairType;
    public byte[] memberAvatarImage;
}

public class LHY_PlayerItem : MonoBehaviour
{
    public static LHY_PlayerItem instance;

    //플레이어 닉네임
    public Text UserNickname;

    public GameObject[] faceType;

    public GameObject[] eyeBrowsType;

    public GameObject[] eyelashesType;

    public GameObject[] hairType;

    //플레이어의 얼굴 정보
    public int FaceType;
    public int EyebrowsType;
    public int Eyelashestype;
    public int Hairtype;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTypeChange()
    {
        for (int i = 0; i < faceType.Length; i++)
        {
            faceType[i].SetActive(false);
        }
        faceType[FaceType].SetActive(true);

        for (int i = 0; i < eyeBrowsType.Length; i++)
        {
            eyeBrowsType[i].SetActive(false);
        }
        eyeBrowsType[EyebrowsType].SetActive(true);

        for (int i = 0; i < eyelashesType.Length; i++)
        {
            eyelashesType[i].SetActive(false);
        }
        eyelashesType[Eyelashestype].SetActive(true);

        for (int i = 0; i < hairType.Length; i++)
        {
            hairType[i].SetActive(false);
        }
        hairType[Hairtype].SetActive(true);
    }

    public void OnClickSaveCustomData()
    {
        PlayerInfo playerdata = new PlayerInfo();
        playerdata.faceType = FaceType;
        playerdata.eyeBrowsType = EyebrowsType;
        playerdata.eyeType = Eyelashestype;
        playerdata.hairType = Hairtype;
        playerdata.memberAvatarImage = File.ReadAllBytes(Application.dataPath + "/Resources/AvatarImage/avatar0.png");

        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/avatar/create";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(playerdata, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);
      
    }
}
