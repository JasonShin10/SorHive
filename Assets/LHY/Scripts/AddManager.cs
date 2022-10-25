using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AddManager : MonoBehaviour
{
    public static AddManager instance;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    //오브젝트들이 생성되는 장소
    public Transform SpawnPos ;

    //침대오브젝트
    public GameObject[] bedItems;
    //의자오브젝트
    public GameObject[] chairItems;
    //책상 오브젝트
    public GameObject[] DeskItem;
    //벽 걸이 오브젝트
    public GameObject[] WallHangItem;

    public bool AddBed = false;
    public bool AddChair = false;
    public bool AddDesk = false;
    public bool AddWallHang = false;

    public int currButtonNum = 0;

    
    public void Button0()
    {
        currButtonNum = 0;
    }

    public void Button1()
    {
        currButtonNum = 1;
    }
    public void Button2()
    {
        currButtonNum = 2;
    }
    public void Button3()
    {
        currButtonNum = 3;
    }
    public void Button4()
    {
        currButtonNum = 4;
    }
    public void Button5()
    {
        currButtonNum = 5;
    }

    public void OnAddBed()
    {
        AddBed = true;
       // GameObject bed = Instantiate(bedItems[currButtonNum]);
        //bed.transform.position = SpawnPos.transform.position;
    }

    public void OnAddChair()
    {

        GameObject chair = Instantiate(chairItems[currButtonNum]);
        chair.transform.position = SpawnPos.transform.position;
    }
    public void OnAddDesk()
    {
        GameObject desk = Instantiate(DeskItem[currButtonNum]);
        desk.transform.position = SpawnPos.transform.position;
    }
    public void OnAddWallHang()
    {
        AddWallHang = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
}
