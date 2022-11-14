using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    public static HttpManager instance;

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
                break;
            case RequestType.GET:
                //if (img == true)
                //{
                //    webTexture = UnityWebRequestTexture.GetTexture(requester.url);
                //    if (accessToken != null)
                //    {
                //        webTexture.SetRequestHeader("Authorization", "Bearer " + accessToken);

                //        webTexture.SetRequestHeader("Content-Type", "application/json");
                //    }
                //}
                //else
                //{
                webRequest = UnityWebRequest.Get(requester.url);
                if (accessToken != null)
                {
                    webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);
                    
                    webRequest.SetRequestHeader("Content-Type", "application/json");
                }
                //}
                break;
            case RequestType.PUT:
                print("aaa");
                webRequest = UnityWebRequest.Put(requester.url, requester.putData);
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
        //if (img == true)
        //{
        //    if (webTexture.result == UnityWebRequest.Result.Success)
        //    {
        //        print(webTexture.downloadHandler.text);

        //        //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
        //        if (requester.onImgComplete != null)
        //        {
        //            requester.onImgComplete((DownloadHandlerTexture)webTexture.downloadHandler);
        //        }
        //    }
        //    else
        //    {
        //        //������� ����....��
        //        print("��� ����" + webRequest.result + "\n" + webRequest.error);
        //    }
        //}
        //else
        //{
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);

            //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
            if (requester.onComplete != null)
            {
                requester.onComplete(webRequest.downloadHandler);
            }
        }
        else
        {
            //������� ����....��
            print("��� ����" + webRequest.result + "\n" + webRequest.error);
        }
        //}
        //�׷����ʴٸ�
        yield return null;

        webRequest.Dispose();
    }

    void Update()
    {
        print(HttpManager.instance.memberCode);
       // print(HttpManager.instance.id);
        //print(HttpManager.instance.userId);
    }

  
}
