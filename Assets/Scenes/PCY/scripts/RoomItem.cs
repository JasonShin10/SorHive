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
        // ��Ʈ��ũ �ܰ�
        print(memberCode);
        GameObject.Find("Hex").GetComponent<WarpManager>().loadRoom(memberCode);
        // �̹���, �ڵ� ���� �Ϸ� �� �ۿ� �ܰ�
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