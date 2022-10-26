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

    //������Ʈ���� �����Ǵ� ���
    public Transform SpawnPos ;

    //ħ�������Ʈ
    public GameObject[] bedItems;
    //���ڿ�����Ʈ
    public GameObject[] chairItems;
    //å�� ������Ʈ
    public GameObject[] DeskItem;
    //�� ���� ������Ʈ
    public GameObject[] WallHangItem;
    //���� ������Ʈ
    public GameObject[] closetItems;
    //�ʰ��� ������Ʈ
    public GameObject[] clothesItems;
    //Ŀ�� ���̺� ������Ʈ
    public GameObject[] coffee_tableItems;
    //���� ��ǰ ������Ʈ
    public GameObject[] curtainsItems;
    //���� ��ǰ ������Ʈ
    public GameObject[] electrionicsItems;
    //�� ������Ʈ
    public GameObject[] flowerItems;
    //�ξ� ���� ������Ʈ
    public GameObject[] kitchenChairItems;
    //�ξ� ��ǰ ������Ʈ
    public GameObject[] kitchenTableItems;
    //���� ������Ʈ
    public GameObject[] lamp;
    //����� ���� ������Ʈ
    public GameObject[] loungeChairItems;
    //���� ������Ʈ
    public GameObject[] musical_instrumentItems;
    //���ǽ� ���� ������Ʈ
    public GameObject[] office_chair;
    //���ǽ� å�� ������Ʈ
    public GameObject[] office_desk;
    //���� ������Ʈ
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
