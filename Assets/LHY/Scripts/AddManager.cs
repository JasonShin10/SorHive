using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObjectInfo
{
    public int folderNumber; 
    public int objNumber;
    public GameObject obj;
    public Vector3 position;
    public Vector3 scale;
    public Vector3 angle;
}

public class ArrayJson<T>
{
    public List<T> data;
}


public class AddManager : MonoBehaviour
{
    public static AddManager instance;

    public ObjectInfo objectInfo;
    public List<ObjectInfo> objectInfoList = new List<ObjectInfo>();
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    //������Ʈ���� �����Ǵ� ���
    public Transform SpawnPos;

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
    public GameObject[] entertainmentItems;
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
    //���� ������Ʈ
    public GameObject[] shelf;

    public GameObject obj;
    public Vector3 pos;
    public Vector3 sca;
    public Vector3 ang;
    public bool AddBed = false;
    public bool AddChair = false;
    public bool AddDesk = false;
    public bool AddWallHang = false;
    public bool AddCloset = false;
    public bool AddClothes = false;
    public bool AddCoffeeTable = false;
    public bool AddEntertainment = false;
    public bool AddElectrionic = false;
    public bool AddFlower = false;
    public bool AddKitchenChair = false;
    public bool AddKitchenTable = false;
    public bool AddLamp = false;
    public bool AddLoungeChair = false;
    public bool AddInstrument = false;
    public bool AddOfficeChair = false;
    public bool AddShelf = false;


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
        OnLoad2();
        //for (int i = 0; i < closetItems.Length; i++)
        //{
        //    closetItems[i].AddComponent<Furniture>();
        //    closetItems[i].AddComponent<DragDrop>();
        //    closetItems[i].AddComponent<Rigidbody>();
        //    closetItems[i].tag = "Furniture";
        //}
     
        //for (int i = 0; i < WallHangItem.Length; i++)
        //{
        //    WallHangItem[i].AddComponent<Furniture>();
        //    WallHangItem[i].AddComponent<DragDrop>();
        //    WallHangItem[i].AddComponent<Rigidbody>();
        //    WallHangItem[i].tag = "Wall";
        //}
    }
    
    public void OnSave()
    {
        objectInfo = new ObjectInfo();
        objectInfo.obj = obj;
        objectInfo.position = pos;
        objectInfo.scale = sca;
        objectInfo.angle = ang;
        string jsonData = JsonUtility.ToJson(objectInfo, true);
        // ������
        string path = Application.dataPath + "/data.txt";
        // ���Ϸ� ����
        File.WriteAllText(path, jsonData);
        print(jsonData);
    }

    public void OnSave2()
    {
        
        ArrayJson<ObjectInfo> arrayJson = new ArrayJson<ObjectInfo>();
        
        arrayJson.data = objectInfoList;
        //objectInfoList.Add(objectInfo);
        
        //ArrayJson -> json
        string jsonData = JsonUtility.ToJson(arrayJson);
        print(jsonData);
        // ������
        string path = Application.dataPath + "/Data";
        
        //���࿡ ��ΰ� ���ٸ�
        if(Directory.Exists(path)== false)
        {
            //������ �����.
            Directory.CreateDirectory(path);
        }

        // ���Ϸ� ����
        File.WriteAllText(path + "/data.txt", jsonData);

    }

    public void OnLoad()
    {
        //����� ���� �ҷ�����
        string path = Application.dataPath + "/data.txt";
        string jsonData = File.ReadAllText(path);
        //json -> objectInfo �� ����
        objectInfo = JsonUtility.FromJson<ObjectInfo>(jsonData);
        //��ü����
        CreateObject(objectInfo);
    }

    public void OnLoad2()
    {
        //���Ϸ� �ҷ�����
        string path = Application.dataPath + "/Data";
        string jsonData = File.ReadAllText(path + "/data.txt");
        //�ҷ��� ����(jsonData) -> ArrayJson<ObjectInfo>
        ArrayJson<ObjectInfo> arrayJson = JsonUtility.FromJson<ArrayJson<ObjectInfo>>(jsonData);
        //arrayJson�� �����ؼ� ������Ʈ ����
        for(int i =0; i < arrayJson.data.Count; i++)
        {
            CreateObject(arrayJson.data[i]);
        }
    }

    public void CreateObject(ObjectInfo info)
    {
       if (info.folderNumber == 0)
        {
            info.obj = bedItems[info.objNumber];
        GameObject createObj = Instantiate(info.obj);
            
        createObj.transform.position = info.position;
        createObj.transform.localScale = info.scale;
        createObj.transform.eulerAngles = info.angle;
            objectInfoList.Add(info);
        }
       if (info.folderNumber == 1)
        {
            info.obj = chairItems[info.objNumber];
            GameObject createObj = Instantiate(info.obj);
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            objectInfoList.Add(info);
        }
        


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

    public void Button6()
    {
        currButtonNum = 6;
    }

    public void Button7()
    {
        currButtonNum = 7;
    }

    public void Button8()
    {
        currButtonNum = 8;
    }

    public void Button9()
    {
        currButtonNum = 9;
    }

    public void Button10()
    {
        currButtonNum = 10;
    }

    public void Button11()
    {
        currButtonNum = 11;
    }
    public void Button12()
    {
        currButtonNum = 12;
    }
    public void Button13()
    {
        currButtonNum = 13;
    }

    public void Button14()
    {
        currButtonNum = 14;
    }

    public void Button15()
    {
        currButtonNum = 15;
    }

    public void Button16()
    {
        currButtonNum = 16;
    }

    public void Button17()
    {
        currButtonNum = 17;
    }

    public void Button18()
    {
        currButtonNum = 18;
    }

    public void Button19()
    {
        currButtonNum = 19;
    }

    public void Button20()
    {
        currButtonNum = 20;
    }

    public void Button21()
    {
        currButtonNum = 21;
    }

    public void Button22()
    {
        currButtonNum = 22;
    }

    public void Button23()
    {
        currButtonNum = 23;
    }

    public void Button24()
    {
        currButtonNum = 24;
    }

    public void Button25()
    {
        currButtonNum = 25;
    }

    public void Button26()
    {
        currButtonNum = 26;
    }

    public void Button27()
    {
        currButtonNum = 27;
    }

    public void Button28()
    {
        currButtonNum = 28;
    }

    public void Button29()
    {
        currButtonNum = 29;
    }

    public void Button30()
    {
        currButtonNum = 30;
    }

    public void Button31()
    {
        currButtonNum = 31;
    }

    public void Button32()
    {
        currButtonNum = 32;
    }

    public void Button33()
    {
        currButtonNum = 33;
    }

    public void Button34()
    {
        currButtonNum = 34;
    }

    public void Button35()
    {
        currButtonNum = 35;
    }

    public void Button36()
    {
        currButtonNum = 36;
    }

    public void Button37()
    {
        currButtonNum = 37;
    }

    public void Button38()
    {
        currButtonNum = 38;
    }

    public void Button39()
    {
        currButtonNum = 39;
    }

    public void Button40()
    {
        currButtonNum = 40;
    }

    public void Button41()
    {
        currButtonNum = 41;
    }

    public void Button42()
    {
        currButtonNum = 42;
    }

    public void Button43()
    {
        currButtonNum = 43;
    }

    public void Button44()
    {
        currButtonNum = 44;
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
    public void OnAddCloset()
    {
        AddCloset = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddCoffeeTable()
    {
        AddCoffeeTable = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddEntertainment()
    {
        AddEntertainment = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddElectronics()
    {
        AddEntertainment = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddFlower()
    {
        AddFlower = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddKitchenChair()
    {
        AddKitchenChair = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddKitchenTable()
    {
        AddKitchenTable = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddLamp()
    {
        AddLamp = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddLoungeChair()
    {
        AddLoungeChair = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

   
    public void OnAddInstrument()
    {
        AddInstrument = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddOfficeChair()
    {
        AddOfficeChair = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnShelf()
    {
        AddShelf = true;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }



}
