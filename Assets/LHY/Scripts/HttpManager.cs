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

    public string id;

    public string userId;
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
                webRequest = UnityWebRequest.Get(requester.url);
                if (accessToken != null)
                {
                    webRequest.SetRequestHeader("Authorization", "Bearer " + accessToken);
                   
                    webRequest.SetRequestHeader("Content-Type", "application/json");
                }
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
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);
           
            //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
            if (requester.onComplete != null)
            {
                requester.onComplete(webRequest.downloadHandler);
            }
        }
        //�׷����ʴٸ�
        else
        {
            //������� ����....��
            print("��� ����" + webRequest.result + "\n" + webRequest.error);
        }
        yield return null;

        webRequest.Dispose();
    }
}
