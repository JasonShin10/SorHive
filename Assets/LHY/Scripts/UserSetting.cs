using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

}
