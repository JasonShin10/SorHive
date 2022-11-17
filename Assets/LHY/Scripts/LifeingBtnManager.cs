using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeingBtnManager : MonoBehaviour
{
    public GameObject lifeing;

    public LifeingItem lifeingItem;

    public GameObject[] lifingCategoryfage;

    // Start is called before the first frame update
    void Start()
    {
        lifeingItem = lifeing.GetComponent<LifeingItem>();
    }

    void Update()
    {
        for (int i = 0; i < lifingCategoryfage.Length; i++)
        {
            lifingCategoryfage[i].SetActive(false);
        }
        lifingCategoryfage[lifeingItem.lifingCategoryNo].SetActive(true);
    }

    //카테고리 번호
    public void OnClickCategoryNo()
    {
        lifeingItem.lifingCategoryNo = -1;
    }

    public void OnClickCategoryNo0()
    {
        lifeingItem.lifingCategoryNo = 0;
    }
    public void OnClickCategoryNo1()
    {
        lifeingItem.lifingCategoryNo = 1;
    }
    public void OnClickCategoryNo2()
    {
        lifeingItem.lifingCategoryNo = 2;
    }
    public void OnClickCategoryNo3()
    {
        lifeingItem.lifingCategoryNo = 3;
    }
    public void OnClickCategoryNo4()
    {
        lifeingItem.lifingCategoryNo = 4;
    }
    public void OnClickCategoryNo5()
    {
        lifeingItem.lifingCategoryNo = 5;
    }
    public void OnClickCategoryNo6()
    {
        lifeingItem.lifingCategoryNo = 6;
    }
    public void OnClickCategoryNo7()
    {
        lifeingItem.lifingCategoryNo = 7;
    }

    //라이핑 이미지 번호
    public void OnClickLifeingImageNo0()
    {
        lifeingItem.lifingNo = 0;
    }
    public void OnClickLifeingImageNo1()
    {
        lifeingItem.lifingNo = 1;
    }
    public void OnClickLifeingImageNo2()
    {
        lifeingItem.lifingNo = 2;
    }
    public void OnClickLifeingImageNo3()
    {
        lifeingItem.lifingNo = 3;
    }
    public void OnClickLifeingImageNo4()
    {
        lifeingItem.lifingNo = 4;
    }
    public void OnClickLifeingImageNo5()
    {
        lifeingItem.lifingNo = 5;
    }


    // Update is called once per frame

}
