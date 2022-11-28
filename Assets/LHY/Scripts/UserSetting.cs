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
    public string email;
}

[System.Serializable]
public class MemberEmail
{
    public string email;
}

[System.Serializable]
public class MemberId
{
    public string memberId;
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
    public List<T> followSummary;
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

    public Text loginStatusText;
    public void Awake()
    {
        //PlayerPrefs.DeleteAll();
        loginStatusText.text = "";
    }


    bool isVerifyId = false;
    bool isVerifyEmail = false;
    bool isVerifyPassword = false;
    bool isPasswordLength = false;
    bool isVerifyNickName = false;
    bool isVerifyIdLength = false;
    public void Update()
    {
        if (isVerifyId && isVerifyEmail && isVerifyPassword && isPasswordLength && isVerifyIdLength && isVerifyNickName)
        {
            signUpBtnConnect.interactable = true;
        }
    }

    public Button signUpBtnConnect;
    public Text nickNameVerifyText;
    void Start()
    {
        signUpBtnConnect.interactable = false;
        if (inputpasschack != null)
        {
            inputpassword.onValueChanged.AddListener(OnPassword);
            inputpasschack.onValueChanged.AddListener(OnPasswordCheck);
            inputid.onValueChanged.AddListener(OnIdCheck);
            inputEmail.onValueChanged.AddListener(OnEmailCheck);
            inputnickName.onValueChanged.AddListener(OnNickNameCheck);
        }
    }

    // �г��� ���� üũ(��ȿ�� �˻�)
    public void OnNickNameCheck(string s)
    {
        if (inputnickName.text.Length <= 10)
        {
            print("��� ������ �̸�.");
            isVerifyNickName = true;
            nickNameVerifyText.text = "��� ������ �̸��Դϴ�.";
        }
        else
        {
            isVerifyNickName = false;
            nickNameVerifyText.text = "10�����Ͽ��� �մϴ�.";
        }
    }


    public Text passwordLengthText;
    // ��й�ȣ ���� üũ
    public void OnPassword(string s)
    {
        if (inputpassword.text.Length > 7)
        {
            print("��й�ȣ 8�� �̻�");
            isPasswordLength = true;
            passwordLengthText.text = "��� ������ ��й�ȣ�Դϴ�.";
        }
        else
        {
            isPasswordLength = false;
            passwordLengthText.text = "��й�ȣ�� 8�� �̻� �Է����ּ���.";
        }
    }

    public Text passwordCheck;
    // 0. ��ȿ�� �˻�
    public void OnPasswordCheck(string s)
    {
        if(inputpassword.text == inputpasschack.text)
        {
            print("��й�ȣ ��ġ");
            isVerifyPassword = true;
            passwordCheck.text = "��й�ȣ�� ��ġ�մϴ�.";
        }
        else
        {
            isVerifyPassword = false;
            passwordCheck.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
        }
    }

    // id ���� üũ(��ȿ�� �˻�)
    public void OnIdCheck(string s)
    {
        if (inputid.text.Length >= 3 && inputid.text.Length <= 10)
        {
            print("��� ������ Id.");
            isVerifyIdLength = true;
            idCheckText.text = "��� ������ ID�Դϴ�.";
        }
        else
        {
            isVerifyIdLength = false;
            idCheckText.text = "ID�� 3�����̻� 10�����Ͽ��� �մϴ�.";
        }
    }

    public void OnEmailCheck(string s)
    {
        isVerifyEmail = false;
    }


    // 1. ȸ������ ������
    public void OnClickSave()
    {
        UserInfo userdata = new UserInfo();
        userdata.memberName = inputnickName.text;
        userdata.memberId = inputid.text;
        userdata.password = inputpassword.text;
        userdata.email = inputEmail.text;

        HttpRequester requester = new HttpRequester();
        //url ���
        requester.url = "http://52.79.209.232:8080/api/v1/auth/signup";
        requester.requestType = RequestType.POST;

        print("ȸ������ �õ�");

        requester.postData = JsonUtility.ToJson(userdata, true);
        print(requester.postData);
        requester.onComplete = OnSave;
        print("�� �ȵ�1");
        requester.requestName = "save";
        StartCoroutine(HttpManager.instance.SendSignUp(requester));
    }

    private void OnSave(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 201)
        {
            print("ȸ������ �Ϸ�");
            SigninPage.SetActive(false);
            LoginPage.SetActive(true);
            loginStatusText.text = "ȸ�� ���� �Ϸ�. �α������ּ���.";
        }
        else
        {
            print("ȸ������ ����");
            verifyEmail.text = "ȸ�� ���� ����. �ٽ� �õ����ּ���.";
        }
    }


    // 2. �α��� �ϱ�
    public void OnClickLogin()
    {
        
        LoginInfo logdata = new LoginInfo();
        logdata.memberId = logID.text;
        logdata.password = logPassword.text;

        HttpRequester requester = new HttpRequester();
        requester.url = "ssl.sorhive.shop/api/v1/auth/login";
        requester.requestType = RequestType.PUT;

        requester.putData = JsonUtility.ToJson(logdata, true);
        print(requester.putData);

        requester.onComplete = downloadLoginData;
        requester.requestName = "login";

        StartCoroutine(HttpManager.instance.SendSignUp(requester));
        HttpManager.instance.userId = logID.text;
        HttpManager.instance.id = logID.text;
    }

    private void downloadLoginData(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 204)
        {
            string token = json["data"]["accessToken"].ToString();
            int memberCode = json["data"]["memberCode"].ToObject<int>();
            HttpManager.instance.avatarYn = json["data"]["avatarYn"].ToString();

            HttpManager.instance.memberCode = memberCode;
            HttpManager.instance.userMemberCode = memberCode;
            print("postTokenData" + token);
            print(memberCode);
            PlayerPrefs.SetString("token", token);
            Photon.Pun.PhotonNetwork.JoinLobby();
            print("��ȸ �Ϸ�");
            ConnectionManager.instance.OnClickJoinLobby();
        }
        else if (status == 400){
            loginStatusText.text = "���̵� Ȥ�� ��й�ȣ�� Ʋ���ϴ�.";
        }
        else
        {   // ����α��� �ƴ� ��
            print("�������� �α����� �ƴմϴ�.");
            loginStatusText.text = "���� �α��� �ƴ�";
        }
    }

    // 3. id �ߺ��˻�
    public Text idCheckText;
    public void OnClickVerifyId()
    {
        MemberId newId = new MemberId();
        newId.memberId = inputid.text;

        HttpRequester requester = new HttpRequester();
        print(inputid.text);
        requester.url = "http://52.79.209.232:8080/api/v1/auth/id";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(newId, true);
        print(requester.postData);

        requester.onComplete = RecieveCheckId;
        requester.requestName = "���̵� �ߺ� �˻�";
        StartCoroutine(HttpManager.instance.SendSignUp(requester));
    }

    private void RecieveCheckId(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 200)
        {
            print("���̵� �ߺ� �˻� ����");
            idCheckText.text = json["data"].ToString();
            isVerifyId = true;
        }
        else if (status == 400)
        {
            idCheckText.text = "�߸��� �����Դϴ�.";
        }
        else 
        {   // ����α��� �ƴ� ��
            // idCheckText.text = json["data"].ToString();
            idCheckText.text = json["data"].ToString();
        }
    }


    // 4. �̸��� ���� ������
    private string verifyNumber;
    public Text verifyEmail;
    public InputField inputEmail;
    public void OnClickVerifyEmailSend()
    {
        MemberEmail newEmail = new MemberEmail();
        newEmail.email = inputEmail.text;

        HttpRequester requester = new HttpRequester();
        print(newEmail.email);
        requester.url = "http://52.79.209.232:8080/api/v1/auth/email";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(newEmail, true);
        print(requester.postData);

        requester.onComplete = RecieveVerifyEmailSend;
        requester.requestName = "�̸��� ���� ��û ������";
        StartCoroutine(HttpManager.instance.SendSignUp(requester));
    }

    private void RecieveVerifyEmailSend(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 200)
        {
            print("�̸��� ���� ������");
            verifyEmail.text = "�̸��Ϸ� ������ȣ�� ���½��ϴ�.";
            verifyNumber = json["data"].ToString();
        }
        else if (status == 400)
        {
            verifyEmail.text = "�߸��� �����Դϴ�.";
        }
        else
        {   // �̸��� �ߺ� ��
            verifyNumber = "-1";
            verifyEmail.text = json["data"].ToString();
        }
    }

    // 5. �̸��Ϸ� ���� ������ȣ�� ���������ϱ�.
    public InputField inputVerifyNumber;
    public Text verifyNumberText;

    public void OnClickVerifyNumber()
    {
        if (verifyNumber == inputVerifyNumber.text)
        {
            print("�̸��� ���� ����");
            verifyNumberText.text = "�̸��� ���� ����";
            isVerifyEmail = true;
        }
        else
        {
            verifyNumberText.text = "�̸��� ���� ����. �ٽ� �����ּ���.";
        }
    }


    /*    private void OnClickDownload(DownloadHandler handler)
        {
            JObject json = JObject.Parse(handler.text);
            string token = json["data"]["accessToken"].ToString();
            print(token);

            PlayerPrefs.SetString("token", token);
            print("��ȸ �Ϸ�");
        }*/


}
