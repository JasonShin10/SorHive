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

    //�޾ƾ��ϴ� ������ ����

    //ȸ������
    public InputField inputnickName;
    public InputField inputid;
    public InputField inputpassword;
    public InputField inputpasschack;

    public GameObject nonPasstext;

    //�α���
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
            print("��й�ȣ�� ���� �ʽ��ϴ�. �ٽ� �Է��� �ּ���");
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
