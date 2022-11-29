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
    public Text id;
    public SearchID searchID;
    void Start()
    {
        // searchID = GameObject.Find("SearchManager").GetComponent<SearchID>();
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
        print("���ذ� �Ȱ���");
        
        if (this.transform.parent.name != "RoomPos0")
        {
            HttpManager.instance.LoadingCanvas.SetActive(true);
            // ��Ʈ��ũ �ܰ�
            WarpManager.instance.loadRoom(memberCode);
        }
        /*else
        {
            searchID.id = id.text;
            searchID.memberCode = memberCode;
            searchID.GetRoomImage();
            searchID.GetThree();
            HttpManager.instance.id = searchID.id;
            HttpManager.instance.fakeId = searchID.id;
            HttpManager.instance.memberCode = searchID.memberCode;
            searchID.myPageButton.onClick.Invoke();
        }*/

        // �̹���, �ڵ� ���� �Ϸ� �� �ۿ� �ܰ�
    }

    public IEnumerator DownloadRoomImg(string roomImagePath)
    {
        UnityWebRequest wwwRoom = UnityWebRequestTexture.GetTexture(roomImagePath);

        yield return wwwRoom.SendWebRequest();
        if (wwwRoom.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwRoom.error);
        }
        else
        {
            roomImage.texture = ((DownloadHandlerTexture)wwwRoom.downloadHandler).texture;
        }
        wwwRoom.Dispose();
        WarpManager.instance.finishDownload++;
    }

    public IEnumerator DownloadAvatarImg(string avatarImagePath)
    {
        UnityWebRequest wwwAvatar = UnityWebRequestTexture.GetTexture(avatarImagePath);

        yield return wwwAvatar.SendWebRequest();
        if (wwwAvatar.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(wwwAvatar.error);
        }
        else
        {
            avatarImage.texture = ((DownloadHandlerTexture)wwwAvatar.downloadHandler).texture;
        }
        wwwAvatar.Dispose();
        WarpManager.instance.finishDownload++;
    }
}