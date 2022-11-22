using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class UserInfo
{
    public string memberName;
    public string memberId;
    public string password;
}

[System.Serializable]
public class UserGetInfo
{
    
    public string id;
    public string memberId;
    public string memberName;
    public int followId;
    public int memberCode;
    public int followingCount;
    public int followerCount;
    public int feedCount;
    
}

[System.Serializable]
public class ArrayJsonID<T>
{
    public List<T> data;
    public List<T> followerData;
    public List<T> followingData;
}

public class LoginInfo
{
    public string memberId;
    public string password;
}

[serializable]
public class PostTokenData
{
    public string accessToken;
}

public class UserSetting : MonoBehaviour
{

    //받아야하는 유저의 정보

    //회원가입
    public InputField inputnickName;
    public InputField inputid;
    public InputField inputpassword;
    public InputField inputpasschack;

    public GameObject nonPasstext;

    //로그인
    public InputField logID;
    public InputField logPassword;

    public GameObject LoginPage;
    public GameObject SigninPage;
    public void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    void Start()
{
    //PlayerPrefs.DeleteAll();
}



    public void OnClickSave()
    {
        UserInfo userdata = new UserInfo();
        userdata.memberName = inputnickName.text;
        userdata.memberId = inputid.text;
        userdata.password = inputpassword.text;

        HttpRequester requester = new HttpRequester();
        //url 경로
        requester.url = "http://52.79.209.232:8080/api/v1/auth/signup";
        requester.requestType = RequestType.POST;

        print("test");

        requester.postData = JsonUtility.ToJson(userdata, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);
        requester.requestName = "save";
        //requester.onComplete = On

        if (inputpasschack.text != userdata.password)
        {
            nonPasstext.SetActive(true);
            print("비밀번호가 맞지 않습니다. 다시 입력해 주세요");
        }
        else
        { 
            SigninPage.SetActive(false);
            LoginPage.SetActive(true);
        }
    }


    public void OnClickLogin()
    {
        
        LoginInfo logdata = new LoginInfo();
        logdata.memberId = logID.text;
        logdata.password = logPassword.text;

        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/auth/login";
        requester.requestType = RequestType.PUT;

        requester.putData = JsonUtility.ToJson(logdata, true);
        print(requester.putData);

        requester.onComplete = OnClickDownload;
        requester.requestName = "login";

        HttpManager.instance.SendRequest(requester);
        HttpManager.instance.userId = logID.text;
        HttpManager.instance.id = logID.text;
        
    }

/*    private void OnClickDownload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        string token = json["data"]["accessToken"].ToString();
        print(token);

        PlayerPrefs.SetString("token", token);
        print("조회 완료");
    }*/

    private void OnClickDownload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        string token = json["data"]["accessToken"].ToString();
        int memberCode = json["data"]["memberCode"].ToObject<int>();
        HttpManager.instance.memberCode = memberCode;
        HttpManager.instance.userMemberCode = memberCode;
        print("postTokenData"+ token);
        print(memberCode);
        PlayerPrefs.SetString("token", token);

        //PlayerPrefs.SetString("memberId",)

        Photon.Pun.PhotonNetwork.JoinLobby();
        print("조회 완료");
    }
}
