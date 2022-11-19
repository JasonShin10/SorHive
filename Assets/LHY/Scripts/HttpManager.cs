using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    public static HttpManager instance;

    public GameObject LoadingCanvas;

    private void Awake()
    {
        //만약에 instance가 null이라면
        if (instance == null)
        {
            //instance에 나를 넣겠다.
            instance = this;
            //씬이 전환이 되어도 나를 파괴되지 않게 하겠다.
            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
        {
            //나를 파괴하겠다.
            Destroy(gameObject);
        }
    }
    [System.Serializable]
    public class GuestBookJsonInfo
    {
        public int roomId;
        //public byte[] onlineRoomImage;
        public string content;
    }
    public bool img = false;
    public string id;
    public int memberCode;
    public string userId;
    public int roomId;
    public string fakeId;
    public string accessToken;
    public bool firstId = false;
    public bool secondId = false;
    //서버에게 요청
    //url(posts/1), GET
    public void SendRequest(HttpRequester requester)
    {
        
        StartCoroutine(Send(requester));
    }
    IEnumerator Send(HttpRequester requester)
    {

        //WWWForm form = new WWWForm();
        //form.AddField("furnitures", requester.postData);

        UnityWebRequest webRequest = null;
        //UnityWebRequest webTexture = null;
        //requestType 에 따라서 호출해줘야한다.
        
        string accessToken = PlayerPrefs.GetString("token");
        switch (requester.requestType)
        {

            case RequestType.POST:
                print("post");

                webRequest = UnityWebRequest.Post(requester.url, requester.postData);
                byte[] data = Encoding.UTF8.GetBytes(requester.postData);
                webRequest.uploadHandler = new UploadHandlerRaw(data);
                webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);

                webRequest.SetRequestHeader("Content-Type", "application/json");

                LoadingCanvas.SetActive(true);
                break;
            case RequestType.GET:
               
                webRequest = UnityWebRequest.Get(requester.url);
                if (accessToken != null)
                {
                    webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);
                    
                    webRequest.SetRequestHeader("Content-Type", "application/json");
                }
                //}
                LoadingCanvas.SetActive(true);
                break;
            case RequestType.PUT:
                print("aaa");
                webRequest = UnityWebRequest.Put(requester.url.ToString(), requester.putData.ToString());
                print("1111");
                byte[] pdata = Encoding.UTF8.GetBytes(requester.putData);
                print("222");
                webRequest.uploadHandler = new UploadHandlerRaw(pdata);
                print("333");
                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.DELETE:
                webRequest = UnityWebRequest.Delete(requester.url);
                break;
        }
        //서버에 요청을 보내고 응답이 올때까지 기다린다.
        yield return webRequest.SendWebRequest();
        print("webRequest");
        //만약에 응답이 성공했다면
        
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            LoadingCanvas.SetActive(false);
            print(requester.requestName + ":" + webRequest.downloadHandler.text);

            //완료되었다고 requester.onComplete를 실행
            if (requester.onComplete != null)
            {
                requester.onComplete(webRequest.downloadHandler);
            }
        }
        else
        {
            LoadingCanvas.SetActive(false);
            //서버통신 실패....ㅠ
            print(requester.requestName +  " : 통신 실패" + webRequest.result + "\n" + webRequest.error);
        }
        //}
        //그렇지않다면
        yield return null;

        webRequest.Dispose();
    }

    public IEnumerator SendWarp(HttpRequester requester, int centerMemberCode)
    {
        print("send");

        UnityWebRequest webRequest = null;

        string accessToken = PlayerPrefs.GetString("token");
        print(accessToken);
        switch (requester.requestType)
        {
            case RequestType.POST:
                print("post");
                webRequest = UnityWebRequest.Post(requester.url, requester.postData);
                byte[] data = Encoding.UTF8.GetBytes(requester.postData);
                webRequest.uploadHandler = new UploadHandlerRaw(data);
                webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);

                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.GET:
                print("get");
                webRequest = UnityWebRequest.Get(requester.url);
                if (accessToken != null)
                {
                    webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);
                    webRequest.SetRequestHeader("Content-Type", "application/json");
                }
                break;
            case RequestType.PUT:
                print("put");
                webRequest = UnityWebRequest.Put(requester.url, requester.putData);
                byte[] pdata = Encoding.UTF8.GetBytes(requester.putData);
                webRequest.uploadHandler = new UploadHandlerRaw(pdata);
                webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);
                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.DELETE:
                webRequest = UnityWebRequest.Delete(requester.url);
                break;
        }

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print("네트워크 통신 성공");
            print(webRequest.downloadHandler.text);

            if (requester.onComplete != null)
            {
                print("onComplete실행");
                requester.onComplete(webRequest.downloadHandler);
            }
        }
        else
        {
            print("네트워크 통신 실패" + webRequest.result + "\n" + webRequest.error);
        }
        webRequest.Dispose();
        print("webRequest끝 reload시작");
        StartCoroutine(WarpManager.instance.DownloadImg());
        while ((WarpManager.instance.downLoadAvatarCount + WarpManager.instance.downLoadRoomCount) < 14) yield return null;
        WarpManager.instance.reloadRoom(centerMemberCode);
    }

    void Update()
    {
        //print(HttpManager.instance.memberCode);
       // print(HttpManager.instance.id);
        //print(HttpManager.instance.userId);
    }

  
}
