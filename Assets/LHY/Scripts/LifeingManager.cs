using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeingItemInfo
{
    public string lifingId;
    public string lifingConetent;
    public string lifingNo;
    public string lifingImagePath;
    //public string memberCode;
    public string name;
    public string uploadTimet;
    public string deleteYn;
    public int memberCode;
    public int pagenNo;
}

public class LifeingManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GetPostAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPostAll()
    {
        HttpRequester requester = new HttpRequester();
        requester.url = "http://13.125.174.193:8080/api/v1/lifing";
        requester.requestType = RequestType.GET;
        //requester.onComplete = OnCompleteGetPostAll;


        HttpManager.instance.SendRequest(requester);
    }
}
