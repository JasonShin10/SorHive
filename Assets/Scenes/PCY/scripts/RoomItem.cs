using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RoomItem : MonoBehaviour
{
    public int memberCode;
    public RawImage avatarImage;
    public RawImage roomImage;
    public Text nickName;

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

    public void WarpButtonClick()
    {
        // 네트워크 단계
        print(memberCode);
        GameObject.Find("Hex").GetComponent<WarpManager>().loadRoom(memberCode);
        // 이미지, 코드 설정 완료 후 작용 단계
    }

    /*public void UpdateRoom(string roomPath)
    {
        StartCoroutine(DownloadRoomImg(roomPath));
    }

    private IEnumerator DownloadRoomImg(string roomPath)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(roomPath);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }*/

}