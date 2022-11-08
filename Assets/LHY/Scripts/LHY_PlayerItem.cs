using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHY_PlayerItem : MonoBehaviour
{
    //�÷��̾� �г���
    public Text UserNickname;

    public GameObject[] faceType;

    public GameObject[] eyeBrowsType;

    public GameObject[] eyelashesType;

    public GameObject[] hairType;

    //�÷��̾��� �� ����
    public int CharacterFaceType;
    public int CharacterEyebrowsType;
    public int CharacterEyelashestype;

    public int hairtype;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < faceType.Length; i++)
        {
            faceType[i].SetActive(false);
        }
        faceType[CharacterFaceType].SetActive(true);

        for (int i = 0; i < eyeBrowsType.Length; i++)
        {
            eyeBrowsType[i].SetActive(false);
        }
        eyeBrowsType[CharacterEyebrowsType].SetActive(true);

        for (int i = 0; i < eyelashesType.Length; i++)
        {
            eyelashesType[i].SetActive(false);
        }
        eyelashesType[CharacterEyelashestype].SetActive(true);

        for (int i = 0; i < hairType.Length; i++)
        {
            hairType[i].SetActive(false);
        }
        hairType[hairtype].SetActive(true);
    }
}
