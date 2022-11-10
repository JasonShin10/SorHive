using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButtenManager : MonoBehaviour
{
    public GameObject Player;

    public LHY_PlayerItem playerItem;

    void Start()
    {
        playerItem = Player.GetComponent<LHY_PlayerItem>();   
    }
    
    //얼굴타입 변경
    public void OnClickFace0()
    {
        playerItem.FaceType = 0;
    }
    public void OnClickFace1()
    {
        playerItem.FaceType = 1;
    }
    public void OnClickFace2()
    {
        playerItem.FaceType = 2;
    }
    public void OnClickFace3()
    {
        playerItem.FaceType = 3;
    }
    public void OnClickFace4()
    {
        playerItem.FaceType = 4;
    }

    //눈썹 타입 변경
    public void OnClickEyebrows0()
    {
        playerItem.EyebrowsType = 0;
    }
    public void OnClickEyebrows1()
    {
        playerItem.EyebrowsType = 1;
    }
    public void OnClickEyebrows2()
    {
        playerItem.EyebrowsType = 2;
    }
    public void OnClickEyebrows3()
    {
        playerItem.EyebrowsType = 3;
    }
    public void OnClickEyebrows4()
    {
        playerItem.EyebrowsType = 4;
    }
    public void OnClickEyebrows5()
    {
        playerItem.EyebrowsType = 5;
    }

    //눈 타입 변경
    public void OnClickEyelashes0()
    {
        playerItem.Eyelashestype = 0;
    }
    public void OnClickEyelashes1()
    {
        playerItem.Eyelashestype = 1;
    }
    public void OnClickEyelashes2()
    {
        playerItem.Eyelashestype = 2;
    }
    public void OnClickEyelashes3()
    {
        playerItem.Eyelashestype = 3;
    }
    public void OnClickEyelashes4()
    {
        playerItem.Eyelashestype = 4;
    }

    //머리카락 타입 변경
    public void OnClickHair0()
    {
        playerItem.Hairtype = 0;
    }
    public void OnClickHair1()
    {
        playerItem.Hairtype = 1;
    }
    public void OnClickHair2()    
    {
        playerItem.Hairtype = 2;
    }


}
