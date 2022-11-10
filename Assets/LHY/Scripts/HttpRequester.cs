using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class PostData
{
    public int userId;
    public int id;
    public string title;
    public string body;
}
[Serializable]
public class PostDataArray
{
    public List<PostData> data;
}

public enum RequestType
{
    POST,
    GET,
    PUT,
    DELETE
}


public class HttpRequester
{
    //url
    public string url;
    //��ûŸ��(GET,POST,PUT,DELETE)
    public RequestType requestType;

    //Post Data
    public string postData; //(body)

    //������ ���� �� ȣ���� �� �Լ�(Action)
    //Action : �Լ��� ���� �� �ִ� �ڷ���
    public Action<DownloadHandler> onComplete;
}
