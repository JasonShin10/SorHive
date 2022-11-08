using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserInfo
{
    public string nickName;
    public string id;
    public string password;
}

public class LoginInfo
{
    public string id;
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



    public void OnClickSave()
    {
        UserInfo userdata = new UserInfo();
        userdata.nickName = inputnickName.text;
        userdata.id = inputid.text;
        userdata.password = inputpassword.text;

        HttpRequester requester = new HttpRequester();
        //url 경로
        requester.url = "http://13.125.174.193:8080/api/v1/auth/signup";
        requester.requestType = RequestType.POST;
        print("test");

        requester.postData = JsonUtility.ToJson(userdata, true);
        print(requester.postData);

        HttpManager.instance.SendRequest(requester);

        //requester.onComplete = On

        if(inputpasschack.text != userdata.password)
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
        logdata.id = logID.text;
        logdata.password = logPassword.text;

        HttpRequester requester = new HttpRequester();
        requester.url = "http://13.125.174.193:8080/api/v1/auth/login";
        requester.requestType = RequestType.PUT;

        requester.putData = JsonUtility.ToJson(logdata, true);
        print(requester.putData);

        requester.onComplete = OnClickDownload;



        HttpManager.instance.SendRequest(requester);
    }

    private void OnClickDownload(DownloadHandler handler)
    {
        PostTokenData postTokenData = JsonUtility.FromJson<PostTokenData>(handler.text);

        print(postTokenData.accessToken);

        PlayerPrefs.SetString("token", postTokenData.accessToken);
        print("조회 완료");
    }
}
