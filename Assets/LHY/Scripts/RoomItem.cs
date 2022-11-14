using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RoomItem : MonoBehaviour
{
    public RawImage roomImage;
    public RawImage avatarImage;
    public Text nickNameText;
    public RawImage ProfileImage;
    public int memberCode;
    public string roomImgPath;
    public string avatarImgPath;

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

    public void WarpButtonClick()
    {
        // 네트워크 단계
        // StartCoroutine(WarpGetTextureR(roomImage, avatarImage));
        WarpManager.instance.loadRoom(memberCode);
        // 이미지, 코드 설정 완료 후 작용 단계
    }

    // public IEnumerator WarpGetTextureR(RawImage roomImage, RawImage avatarImage)
    // {
    //     //lifeingRoomItem.roomImage = friendList[i].roomImage
    //     var urlR = roomImgPath;
    //     var urlA = avatarImgPath;
    //     UnityWebRequest wwwR = UnityWebRequestTexture.GetTexture(urlR);
    //     yield return wwwR.SendWebRequest();

    //     UnityWebRequest wwwA = UnityWebRequestTexture.GetTexture(urlA);
    //     yield return wwwA.SendWebRequest();

    //     if (wwwR.result != UnityWebRequest.Result.Success)
    //         Debug.Log(wwwR.error);
    //     else
    //         roomImage.texture = ((DownloadHandlerTexture)wwwR.downloadHandler).texture;

    //     if (wwwA.result != UnityWebRequest.Result.Success)
    //         Debug.Log(wwwA.error);
    //     else
    //         avatarImage.texture = ((DownloadHandlerTexture)wwwA.downloadHandler).texture;
    // }

}