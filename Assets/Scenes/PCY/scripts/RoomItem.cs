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
        searchID = GameObject.Find("SearchManager").GetComponent<SearchID>();
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
            GameObject.Find("RoomWarp").GetComponent<WarpManager>().loadRoom(memberCode);
        }
        else
        {
            searchID.id = id.text;
            searchID.memberCode = memberCode;
            searchID.GetRoomImage();
            searchID.GetThree();
            HttpManager.instance.id = searchID.id;
            HttpManager.instance.fakeId = searchID.id;
            HttpManager.instance.memberCode = searchID.memberCode;
            searchID.myPageButton.onClick.Invoke();
        }

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