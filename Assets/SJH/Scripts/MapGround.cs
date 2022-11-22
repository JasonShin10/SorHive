using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGround : Map
{
    int select = 0;
    int ox;
    int oz;
    float oy;
    int num;
    float box = 0;
    float e = 0;
    public Material[] floorMats;
    GameObject currCube;
    GameObject floor;
    Button delete;
    Transform removeSelectObj;
    Vector3 startPos;
    Quaternion startLocation;
    MeshRenderer rb;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        for (int i = 0; i <= tileX; i++)
        {
            for (int j = 0; j <= tileX; j++)
            {
                floor = Instantiate(quadFactory);
                Vector3 firstPos = transform.position;
                firstPos.x += j;
                firstPos.z += i;
                floor.transform.position = firstPos;
                floor.transform.rotation = transform.rotation;
            }
        }
        floorMats = Resources.LoadAll<Material>("floorMat");
        rb = GetComponent<MeshRenderer>();
        delete = AddManager.instance.deleteButton;
        delete.GetComponent<Button>().onClick.AddListener(OnRemoveJson);
        
    }
    UnityEngine.Transform selectObj;
    void Update()
    {
      
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layer = 1 << LayerMask.NameToLayer("Obj");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.transform.CompareTag("Furniture"))
                {
                    selectObj = hit.transform;
                    selectObj.gameObject.GetComponent<Furniture>().located = false;
                    selectObj.gameObject.GetComponent<Furniture>().startPos = hit.transform.position;
                    startPos = selectObj.gameObject.GetComponent<Furniture>().startPos;
                    GameManager.instance.name = selectObj.name;
                    GameManager.instance.selected = selectObj.gameObject;
                    //selectObj.GetComponent<BoxCollider>().center = new Vector3(selectObj.GetComponent<BoxCollider>().center.x, 0, selectObj.GetComponent<BoxCollider>().center.z);
                    selectObj.GetComponent<BoxCollider>().center = transform.InverseTransformPoint(new Vector3(0, 27.5f, 0));
                }
            }
            //AddManager.instance.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    #region 방생성
                    if (AddManager.instance.AddBed == true)
                    {
       
                        num = 0;
                        Room(AddManager.instance.bedItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddBed = false;
                    }
                   if (AddManager.instance.AddChair == true)
                    {
                     
                        num = 1;
                        Room(AddManager.instance.chairItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddChair = false;
                        //currCube = Instantiate(AddManager.instance.chairItems[AddManager.instance.currButtonNum]);
                        //num = 1;
                        //AddManager.instance.AddChair = false;
                        //SaveJson(currCube.gameObject);
                        //currCube.name = "d" + select;
                        //select += 1;
                        //currCube.layer = LayerMask.NameToLayer("Obj");
                        //int x = (int)(hit.point.x);
                        //int z = (int)(hit.point.z);
                        //currCube.transform.position = new Vector3(x, hit.point.y, z);
                        //if (currCube.GetComponent<Furniture>())
                        //{
                        //    currCube.GetComponent<Furniture>().startPos = new Vector3(x, hit.point.y, z);
                        //    startPos = currCube.GetComponent<Furniture>().startPos;
                        //    currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                        //    startLocation = currCube.GetComponent<Furniture>().startRotation;
                        //}
                    }
                    //startPos = currCube.transform.position;
                    if (AddManager.instance.AddDesk == true)
                    {
                 
                        num = 2;
                        Room(AddManager.instance.DeskItem[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddDesk = false;
                    }
                    if (AddManager.instance.AddCloset == true)
                    {
                   
                        num = 4;
                        Room(AddManager.instance.closetItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddCloset = false;
                    }
                    if (AddManager.instance.AddCoffeeTable == true)
                    {
          
                        num = 5;
                        Room(AddManager.instance.coffee_tableItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddCoffeeTable = false;
                    }
                    if (AddManager.instance.AddEntertainment == true)
                    {                  
                        num = 6;
                        Room(AddManager.instance.entertainmentItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddEntertainment = false;
                    }

                    if (AddManager.instance.AddElectrionic == true)
                    {                
                        num = 7;
                        Room(AddManager.instance.electrionicsItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddElectrionic = false;
                    }

                    if (AddManager.instance.AddFlower == true)
                    {             
                        num = 8;
                        Room(AddManager.instance.flowerItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddFlower = false;
                    }
                    if (AddManager.instance.AddKitchenChair == true)
                    {
                        num = 9;
                        Room(AddManager.instance.kitchenChairItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddKitchenChair = false;
                    }
                    if (AddManager.instance.AddKitchenTable == true)
                    {
                        
                        num = 10;
                        Room(AddManager.instance.kitchenTableItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddKitchenTable = false;
                    }
                    if (AddManager.instance.AddLamp == true)
                    {
                     
                        num = 11;
                        Room(AddManager.instance.lamp[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddLamp = false;
                    }
                    if (AddManager.instance.AddLoungeChair == true)
                    {
                  
                        num = 12;
                        Room(AddManager.instance.loungeChairItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddLoungeChair = false;
                    }
                    if (AddManager.instance.AddInstrument == true)
                    {
        
                        num = 13;
                        Room(AddManager.instance.musical_instrumentItems[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddInstrument = false;
                    }
                    if (AddManager.instance.AddOfficeChair == true)
                    {                   
                        num = 14;
                        Room(AddManager.instance.office_chair[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddOfficeChair = false;
                    }
                    if (AddManager.instance.AddShelf == true)
                    {                 
                        num = 15;
                        Room(AddManager.instance.shelf[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddShelf = false;
                    }
                    if (AddManager.instance.AddFloor == true)
                    {
                        num = 16;
                        FloorMat();
                        SaveMat(GameObject.Find("Floor.007"));
                        AddManager.instance.AddFloor = false;
                    }
                    #endregion
                }

            }
        }
        #region 마우스 놓았을때
        if (Input.GetMouseButtonUp(0))
        {
            if (selectObj)
            {
                if (selectObj.GetComponent<Furniture>().canLocated == true)
                {
                    selectObj.position = new Vector3(ox, oy, oz);
                    selectObj.gameObject.GetComponent<Furniture>().located = true;

                    selectObj.GetComponent<BoxCollider>().center = new Vector3(selectObj.GetComponent<BoxCollider>().center.x, box, selectObj.GetComponent<BoxCollider>().center.z);
                    SaveJson(selectObj.gameObject);

                    AddManager.instance.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    AddManager.instance.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    AddManager.instance.gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3( -1.5f,selectObj.GetComponent<MeshRenderer>().bounds.size.y *2f,-1.5f));
                    AddManager.instance.gameObject.transform.GetChild(3).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(8, selectObj.GetComponent<MeshRenderer>().bounds.size.y * 2f, 8));
                    selectObj = null;

                }
                else
                {
                    selectObj.position = startPos;
                    selectObj.rotation = startLocation;
                    selectObj.GetComponent<BoxCollider>().center = new Vector3(selectObj.GetComponent<BoxCollider>().center.x, box, selectObj.GetComponent<BoxCollider>().center.z);
                    SaveJson(selectObj.gameObject);
                    //selectObj.GetComponent<Furniture>().canLocated = false;
                    selectObj = null;
                }

            }
        }
        if (selectObj != null)
        {
            AddManager.instance.deletetObj = selectObj;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layer = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    int x = (int)(hit.point.x);
                    ox = (int)(hit.point.x);
                    int z = (int)(hit.point.z);
                    oz = (int)(hit.point.z);
                    oy = hit.point.y;
                    selectObj.position = new Vector3(x, hit.point.y + 5, z);
                }
            }
            if (Input.GetKeyDown("i"))
            {
                RemoveJson(selectObj.gameObject);
                selectObj.GetComponent<Furniture>().Delete();

            }
        }
        #endregion
    }
    void FloorMat()
    {
        GameObject.Find("Floor.007").GetComponent<MeshRenderer>().material = floorMats[AddManager.instance.currButtonNum];
    }

    void SaveMat(GameObject objRoom)
    {
        for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if (AddManager.instance.objectInfoList[i].room == objRoom)
            {
                //정보수정
                AddManager.instance.objectInfoList[i].floorNumber = AddManager.instance.currButtonNum;

                return;
            }
        }

        AddManager.instance.objectInfo = new ObjectInfo();
        AddManager.instance.objectInfo.room = objRoom;
        AddManager.instance.objectInfo.furnitureCategoryNumber = num;
        AddManager.instance.objectInfo.floorNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    }
    #region Json저장
    void SaveJson(GameObject obj)
    {
        for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if (AddManager.instance.objectInfoList[i].obj.name == obj.name)
            {
                //정보수정
                AddManager.instance.objectInfoList[i].position = obj.transform.position;
                AddManager.instance.objectInfoList[i].scale = obj.transform.localScale;
                AddManager.instance.objectInfoList[i].angle = obj.transform.eulerAngles;
                return;
            }
        }
        AddManager.instance.objectInfo = new ObjectInfo();
        AddManager.instance.obj = obj;
        AddManager.instance.pos = obj.transform.position;
        AddManager.instance.sca = obj.transform.localScale;
        AddManager.instance.ang = obj.transform.eulerAngles;
        AddManager.instance.objectInfo.furnitureNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfo.furnitureCategoryNumber = num;
        AddManager.instance.objectInfo.obj = AddManager.instance.obj;
        AddManager.instance.objectInfo.position = AddManager.instance.pos;
        AddManager.instance.objectInfo.scale = AddManager.instance.sca;
        AddManager.instance.objectInfo.angle = AddManager.instance.ang;
        AddManager.instance.objectInfo.name = AddManager.instance.obj.name;
        AddManager.instance.objectInfo.boxPosition = new Vector3(obj.GetComponent<BoxCollider>().center.x, box, obj.GetComponent<BoxCollider>().center.y);
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);   
    }
    #endregion 
    void RemoveJson(GameObject obj)
    {

        for (int i = AddManager.instance.objectInfoList.Count - 1; i >= 0; i--)
        {
            if (AddManager.instance.objectInfoList[i].obj == null && AddManager.instance.objectInfoList[i].wallNumber == 0 && AddManager.instance.objectInfoList[i].floorNumber == 0)
            {
                AddManager.instance.objectInfoList.RemoveAt(i);
            }
            else if (AddManager.instance.objectInfoList[i].obj.name == AddManager.instance.deletetObj.gameObject.name)
            {
                AddManager.instance.objectInfoList.RemoveAt(i);
            }
        }
        AddManager.instance.deletetObj.GetComponent<Furniture>().Delete();

    }

    public void OnRemoveJson()
    {
        print("OnRemoveJson");
        int count = AddManager.instance.objectInfoList.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    //AddManager.instance.objectInfoList.RemoveAt(i);
        //if (AddManager.instance.objectInfoList[i].obj == null && AddManager.instance.objectInfoList[i].wallNumber == 0 && AddManager.instance.objectInfoList[i].floorNumber == 0)
        //{

        //    AddManager.instance.objectInfoList.RemoveAt(i);
        //}
        //else if (AddManager.instance.objectInfoList[i].obj.name == removeSelectObj.gameObject.name)
        //{
        //}
        //}
        for (int i = AddManager.instance.objectInfoList.Count - 1; i >= 0; i--)
        {
            if (AddManager.instance.objectInfoList[i].obj == null && AddManager.instance.objectInfoList[i].wallNumber == 0 && AddManager.instance.objectInfoList[i].floorNumber == 0)
            {
                AddManager.instance.objectInfoList.RemoveAt(i);
            }
            else if (AddManager.instance.objectInfoList[i].obj.name == AddManager.instance.deletetObj.gameObject.name)
            {
                AddManager.instance.objectInfoList.RemoveAt(i);
        AddManager.instance.deletetObj.GetComponent<Furniture>().Delete();
                return;
            }
            //전부삭제
            //AddManager.instance.objectInfoList.RemoveAt(i);
        }
    }
    
    void Room(GameObject item)
    {
        currCube = Instantiate(item);
        //SaveJson(currCube.gameObject);
        currCube.name = item.name;
        //currCube.name = "d" + select;
        //select += 1;
        currCube.layer = LayerMask.NameToLayer("Obj");
        int x = (int)(hit.point.x);
        int z = (int)(hit.point.z);
        currCube.transform.position = new Vector3(x, hit.point.y, z);
        currCube.GetComponent<Furniture>().canLocated = true;
        if (currCube.GetComponent<Furniture>())
        {
            currCube.GetComponent<Furniture>().startPos = new Vector3(x, hit.point.y, z);
            startPos = currCube.GetComponent<Furniture>().startPos;
            currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
            startLocation = currCube.GetComponent<Furniture>().startRotation;
            box = currCube.GetComponent<BoxCollider>().center.y - 0.01f;
            currCube.GetComponent<BoxCollider>().center = new Vector3(currCube.GetComponent<BoxCollider>().center.x, box, currCube.GetComponent<BoxCollider>().center.z);
            SaveJson(currCube);
        }
    }


}