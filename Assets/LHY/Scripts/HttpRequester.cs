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
    //요청타입(GET,POST,PUT,DELETE)
    public RequestType requestType;

    //Post Data
    public string postData; //(body)

    //응답이 왔을 때 호출해 줄 함수(Action)
    //Action : 함수를 넣을 수 있는 자료형
    public Action<DownloadHandler> onComplete;
}
