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

    // 닉네임 길이 체크(유효성 검사)
    public void OnNickNameCheck(string s)
    {
        if (inputnickName.text.Length <= 10)
        {
            print("사용 가능한 이름.");
            isVerifyNickName = true;
            nickNameVerifyText.text = "사용 가능한 이름입니다.";
        }
        else
        {
            isVerifyNickName = false;
            nickNameVerifyText.text = "10자이하여야 합니다.";
        }
    }


    public Text passwordLengthText;
    // 비밀번호 길이 체크
    public void OnPassword(string s)
    {
        if (inputpassword.text.Length > 7)
        {
            print("비밀번호 8자 이상");
            isPasswordLength = true;
            passwordLengthText.text = "사용 가능한 비밀번호입니다.";
        }
        else
        {
            isPasswordLength = false;
            passwordLengthText.text = "비밀번호를 8자 이상 입력해주세요.";
        }
    }

    public Text passwordCheck;
    // 0. 유효성 검사
    public void OnPasswordCheck(string s)
    {
        if(inputpassword.text == inputpasschack.text)
        {
            print("비밀번호 일치");
            isVerifyPassword = true;
            passwordCheck.text = "비밀번호가 일치합니다.";
        }
        else
        {
            isVerifyPassword = false;
            passwordCheck.text = "비밀번호가 일치하지 않습니다.";
        }
    }

    // id 길이 체크(유효성 검사)
    public void OnIdCheck(string s)
    {
        if (inputid.text.Length >= 3 && inputid.text.Length <= 10)
        {
            print("사용 가능한 Id.");
            isVerifyIdLength = true;
            idCheckText.text = "사용 가능한 ID입니다.";
        }
        else
        {
            isVerifyIdLength = false;
            idCheckText.text = "ID는 3글자이상 10자이하여야 합니다.";
        }
    }

    public void OnEmailCheck(string s)
    {
        isVerifyEmail = false;
    }


    // 1. 회원가입 보내기
    public void OnClickSave()
    {
        UserInfo userdata = new UserInfo();
        userdata.memberName = inputnickName.text;
        userdata.memberId = inputid.text;
        userdata.password = inputpassword.text;
        userdata.email = inputEmail.text;

        HttpRequester requester = new HttpRequester();
        //url 경로
        requester.url = "http://52.79.209.232:8080/api/v1/auth/signup";
        requester.requestType = RequestType.POST;

        print("회원가입 시도");

        requester.postData = JsonUtility.ToJson(userdata, true);
        print(requester.postData);
        requester.onComplete = OnSave;
        print("왜 안돼1");
        requester.requestName = "save";
        StartCoroutine(HttpManager.instance.SendSignUp(requester));
    }

    private void OnSave(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 201)
        {
            print("회원가입 완료");
            SigninPage.SetActive(false);
            LoginPage.SetActive(true);
            loginStatusText.text = "회원 가입 완료. 로그인해주세요.";
        }
        else
        {
            print("회원가입 실패");
            verifyEmail.text = "회원 가입 실패. 다시 시도해주세요.";
        }
    }


    // 2. 로그인 하기
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
            print("조회 완료");
            ConnectionManager.instance.OnClickJoinLobby();
        }
        else if (status == 400){
            loginStatusText.text = "아이디 혹은 비밀번호가 틀립니다.";
        }
        else
        {   // 정상로그인 아닐 시
            print("정상적인 로그인이 아닙니다.");
            loginStatusText.text = "정상 로그인 아님";
        }
    }

    // 3. id 중복검사
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
        requester.requestName = "아이디 중복 검사";
        StartCoroutine(HttpManager.instance.SendSignUp(requester));
    }

    private void RecieveCheckId(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 200)
        {
            print("아이디 중복 검사 성공");
            idCheckText.text = json["data"].ToString();
            isVerifyId = true;
        }
        else if (status == 400)
        {
            idCheckText.text = "잘못된 형식입니다.";
        }
        else 
        {   // 정상로그인 아닐 시
            // idCheckText.text = json["data"].ToString();
            idCheckText.text = json["data"].ToString();
        }
    }


    // 4. 이메일 인증 보내기
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
        requester.requestName = "이메일 인증 요청 보내기";
        StartCoroutine(HttpManager.instance.SendSignUp(requester));
    }

    private void RecieveVerifyEmailSend(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        int status = json["status"].ToObject<int>();
        if (status == 200)
        {
            print("이메일 인증 보내기");
            verifyEmail.text = "이메일로 인증번호를 보냈습니다.";
            verifyNumber = json["data"].ToString();
        }
        else if (status == 400)
        {
            verifyEmail.text = "잘못된 형식입니다.";
        }
        else
        {   // 이메일 중복 시
            verifyNumber = "-1";
            verifyEmail.text = json["data"].ToString();
        }
    }

    // 5. 이메일로 받은 인증번호로 본인인증하기.
    public InputField inputVerifyNumber;
    public Text verifyNumberText;

    public void OnClickVerifyNumber()
    {
        if (verifyNumber == inputVerifyNumber.text)
        {
            print("이메일 인증 성공");
            verifyNumberText.text = "이메일 인증 성공";
            isVerifyEmail = true;
        }
        else
        {
            verifyNumberText.text = "이메일 인증 실패. 다시 보내주세요.";
        }
    }


    /*    private void OnClickDownload(DownloadHandler handler)
        {
            JObject json = JObject.Parse(handler.text);
            string token = json["data"]["accessToken"].ToString();
            print(token);

            PlayerPrefs.SetString("token", token);
            print("조회 완료");
        }*/


}
