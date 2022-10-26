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
    //옷장 오브젝트
    public GameObject[] closetItems;
    //옷걸이 오브젝트
    public GameObject[] clothesItems;
    //커피 테이블 오브젝트
    public GameObject[] coffee_tableItems;
    //전자 제품 오브젝트
    public GameObject[] entertainmentItems;
    //오락 제품 오브젝트
    public GameObject[] electrionicsItems;
    //꽃 오브젝트
    public GameObject[] flowerItems;
    //부엌 의자 오브젝트
    public GameObject[] kitchenChairItems;
    //부엌 용품 오브젝트
    public GameObject[] kitchenTableItems;
    //램프 오브젝트
    public GameObject[] lamp;
    //라운지 의자 오브젝트
    public GameObject[] loungeChairItems;
    //음악 오브젝트
    public GameObject[] musical_instrumentItems;
    //오피스 의자 오브젝트
    public GameObject[] office_chair;
    //선반 오브젝트
    public GameObject[] shelf;
    

    public bool AddBed = false;
    public bool AddChair = false;
    public bool AddDesk = false;
    public bool AddWallHang = false;

    public int currButtonNum = 0;
    void Start()
    {
        bedItems = Resources.LoadAll<GameObject>("bed");
        chairItems = Resources.LoadAll<GameObject>("armchair");
        DeskItem = Resources.LoadAll<GameObject>("office_desk");
        WallHangItem = Resources.LoadAll<GameObject>("window");
        closetItems = Resources.LoadAll<GameObject>("closet");
        clothesItems = Resources.LoadAll<GameObject>("clothes");
        coffee_tableItems = Resources.LoadAll<GameObject>("coffee_table");
        entertainmentItems = Resources.LoadAll<GameObject>("entertainment");
        electrionicsItems = Resources.LoadAll<GameObject>("electronics");
        flowerItems = Resources.LoadAll<GameObject>("flower");
        kitchenChairItems = Resources.LoadAll<GameObject>("kitchen_chair");
        kitchenTableItems = Resources.LoadAll<GameObject>("kitchen_table");
        lamp = Resources.LoadAll<GameObject>("lamp");
        loungeChairItems = Resources.LoadAll<GameObject>("lounge_chair");
        musical_instrumentItems = Resources.LoadAll<GameObject>("musical_instrument");
        office_chair = Resources.LoadAll<GameObject>("office_chair");
        shelf = Resources.LoadAll<GameObject>("shelf");
        //for (int i = 0; i < bedItems.Length; i++)
        //{
        //    bedItems[i].AddComponent<Furniture>();
        //    bedItems[i].AddComponent<DragDrop>();
        //    bedItems[i].AddComponent<Rigidbody>();
        //    bedItems[i].tag = "Furniture";
        //}
        //for (int i = 0; i < chairItems.Length; i++)
        //{
        //    chairItems[i].AddComponent<Furniture>();
        //    chairItems[i].AddComponent<DragDrop>();
        //    chairItems[i].AddComponent<Rigidbody>();
        //    chairItems[i].tag = "Furniture";

        //}
        //for (int i = 0; i < DeskItem.Length; i++)
        //{
        //    DeskItem[i].AddComponent<Furniture>();
        //    DeskItem[i].AddComponent<DragDrop>();
        //    DeskItem[i].AddComponent<Rigidbody>();
        //    DeskItem[i].tag = "Furniture";
        //}
        //for (int i = 0; i < WallHangItem.Length; i++)
        //{
        //    WallHangItem[i].AddComponent<Furniture>();
        //    WallHangItem[i].AddComponent<DragDrop>();
        //    WallHangItem[i].AddComponent<Rigidbody>();
        //    WallHangItem[i].tag = "Wall";
        //}
    }


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
        AddChair = true;
        //GameObject chair = Instantiate(chairItems[currButtonNum]);
        //chair.transform.position = SpawnPos.transform.position;
    }
    public void OnAddDesk()
    {
        AddDesk = true;
        //GameObject desk = Instantiate(DeskItem[currButtonNum]);
        //desk.transform.position = SpawnPos.transform.position;
    }
    public void OnAddWallHang()
    {
        AddWallHang = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    
}
