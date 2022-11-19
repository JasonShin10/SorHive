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
        //���࿡ instance�� null�̶��
        if (instance == null)
        {
            //instance�� ���� �ְڴ�.
            instance = this;
            //���� ��ȯ�� �Ǿ ���� �ı����� �ʰ� �ϰڴ�.
            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
        {
            //���� �ı��ϰڴ�.
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
    //�������� ��û
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
        //requestType �� ���� ȣ��������Ѵ�.
        
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
        //������ ��û�� ������ ������ �ö����� ��ٸ���.
        yield return webRequest.SendWebRequest();
        print("webRequest");
        //���࿡ ������ �����ߴٸ�
        
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            LoadingCanvas.SetActive(false);
            print(requester.requestName + ":" + webRequest.downloadHandler.text);

            //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
            if (requester.onComplete != null)
            {
                requester.onComplete(webRequest.downloadHandler);
            }
        }
        else
        {
            LoadingCanvas.SetActive(false);
            //������� ����....��
            print(requester.requestName +  " : ��� ����" + webRequest.result + "\n" + webRequest.error);
        }
        //}
        //�׷����ʴٸ�
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
            print("��Ʈ��ũ ��� ����");
            print(webRequest.downloadHandler.text);

            if (requester.onComplete != null)
            {
                print("onComplete����");
                requester.onComplete(webRequest.downloadHandler);
            }
        }
        else
        {
            print("��Ʈ��ũ ��� ����" + webRequest.result + "\n" + webRequest.error);
        }
        webRequest.Dispose();
        print("webRequest�� reload����");
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
