using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    //플레이어 닉네임
    public Text UserNickname;

    public GameObject[] fadeType;

    public GameObject[] eyeBrowsType;

    public GameObject[] eyelashesType;

    public GameObject[] hairType;

    //플레이어의 얼굴 정보
    public Text CharacterFaceType;
    public Text CharacterEyebrowsType;
    public Text CharacterEyelashestype;

    public Text hairtype;


}
public class UserPhoto
{
    public Texture FacePhoto;
}
