using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGround : Map
{

    public GameObject cube;
    public Material[] floorMats;
    Button delete;
    public GameObject[] bedItems;
    //의자오브젝트
    public GameObject[] chairItems;
    //책상 오브젝트
    public GameObject[] DeskItem;




    void Start()
    {
        bedItems = Resources.LoadAll<GameObject>("bed");
        chairItems = Resources.LoadAll<GameObject>("armchair");
        DeskItem = Resources.LoadAll<GameObject>("office_desk");

        floorMats = Resources.LoadAll<Material>("floorMat");
        rb = GetComponent<MeshRenderer>();
        delete = AddManager.instance.deleteButton;
        delete.GetComponent<Button>().onClick.AddListener(OnRemoveJson);
    }

    void Update()
    {
        
    }
    public void OnClickButton(int index)
    {
        currButtonNum = index;
    }
    public override void ClickUIObj()
    {
        if (AddBed == true)
        {
            num = 0;
            Room(bedItems[currButtonNum]);
            AddBed = false;
        }
        if (AddChair == true)
        {
            num = 1;
            Room(chairItems[currButtonNum]);
            AddChair = false;
        }
        if (AddDesk == true)
        {
            num = 2;
            Room(DeskItem[currButtonNum]);
            AddDesk = false;
        }

    }

    public override void ClickUpObject()
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
                AddManager.instance.gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(-1.5f, selectObj.GetComponent<MeshRenderer>().bounds.size.y * 2f, -1.5f));
                AddManager.instance.gameObject.transform.GetChild(3).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(8, selectObj.GetComponent<MeshRenderer>().bounds.size.y * 2f, 8));
                selectObj = null;
            }
            else
            {
                selectObj.position = startPos;
                selectObj.rotation = startLocation;
                selectObj.GetComponent<BoxCollider>().center = new Vector3(selectObj.GetComponent<BoxCollider>().center.x, box, selectObj.GetComponent<BoxCollider>().center.z);
                SaveJson(selectObj.gameObject);
                selectObj = null;
            }
        }
    }

    public override void SelectObj()
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
    public override void ClickObject(RaycastHit hit)
    {
        base.ClickObject(hit);
        GameManager.instance.selected = selectObj.gameObject;
        selectObj.GetComponent<BoxCollider>().center = transform.InverseTransformPoint(new Vector3(0, 24.5f, 0));
    }
    
    protected override void SaveJson(GameObject obj)
    {
        base.SaveJson(obj);
        AddManager.instance.objectInfo.boxPosition = new Vector3(obj.GetComponent<BoxCollider>().center.x, box, obj.GetComponent<BoxCollider>().center.y);
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    }
    #region Json저장

    #endregion 


    protected override void Room(GameObject item)
    {
        base.Room(item);
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

    public void OnAddBed()
    {
        AddBed = true;
        AddChair = false;
        AddDesk = false;
    }

    public void OnAddChair()
    {
        AddChair = true;
        AddBed = false;
        AddDesk = false;
    }

    public void OnAddDesk()
    {
        AddDesk = true;
        AddBed = false;
        AddChair = false;
    }
    public void FloorMat()
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

        for (int i = AddManager.instance.objectInfoList.Count - 1; i >= 0; i--)
        {
            if (AddManager.instance.objectInfoList[i].obj == null && AddManager.instance.objectInfoList[i].wallNumber == 0 && AddManager.instance.objectInfoList[i].floorNumber == 0)
            {
                AddManager.instance.objectInfoList.RemoveAt(i);
            }
            else if (AddManager.instance.objectInfoList[i].obj)
            {
                if (AddManager.instance.objectInfoList[i].obj.name == AddManager.instance.deletetObj.gameObject.name)
                {
                    AddManager.instance.deletetObj.GetComponent<Furniture>().Delete();
                    AddManager.instance.objectInfoList.RemoveAt(i);
                    return;
                }
            }
            //전부삭제
            //AddManager.instance.objectInfoList.RemoveAt(i);
        }
        AddManager.instance.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        AddManager.instance.gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

}