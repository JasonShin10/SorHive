using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedItem : MonoBehaviour
{
    //피드 번호
    public int myfeedNum = 0;

    //피드를 올린 사람(ID)
    public Text UserID; //==계정이름(아이디)

    //피드에 올린 사진
    public RawImage feedtexture;

    //피드에 쓴 글
    public Text feedText;

    //해당 피드의 좋아요 개수
    public int Like = 0;

    //해당 피드의 댓글 개수
    public int currcomment = 0;

    //피드 댓글 택스트
    public Text[] comments;
}
