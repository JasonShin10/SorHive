using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomInfo
{
    public RawImage roomImage;
    public RawImage character;
    public Text nickNameText;
    public RawImage ProfileImage;
}

public class RoomItem : MonoBehaviour
{
    public RawImage roomImage;
    public RawImage character;
    public Text nickNameText;
    public RawImage ProfileImage;

    public Text roomInfo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, byte maxPlayer)
    {
        //roomInfo.text = roomName +  s
    }
}
