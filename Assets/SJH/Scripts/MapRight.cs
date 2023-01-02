using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRight : Map
{
    public Material[] mats;

    void Start()
    {
        Tile(firstPos.y, firstPos.z);
        mats = Resources.LoadAll<Material>("WallPaper");
        rb = GetComponent<MeshRenderer>();
        floorMats = Resources.LoadAll<Material>("floorMat");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);     
            int layer = 1 << LayerMask.NameToLayer("Obj");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if(hit.transform.gameObject.CompareTag("Wall"))
                {
                selectObj = hit.transform;
                selectObj.gameObject.GetComponent<Furniture>().located = false;
                selectObj.gameObject.GetComponent<Furniture>().startPos = hit.transform.position;
                startPos = selectObj.gameObject.GetComponent<Furniture>().startPos;
                GameManager.instance.name = selectObj.name;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight"))
                {
                    if (AddManager.instance.AddWallHang == true)
                    {
                        num = 3;
                        Room(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);          
                        AddManager.instance.AddWallHang = false;
                    }
                    if (AddManager.instance.AddMaterial == true)
                    {          
                        num = 15;
                        Mat();
                        SaveMat(GameObject.Find("Wall_B"));
                        AddManager.instance.AddWallHang = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectObj)
            {
                if (selectObj.GetComponent<Furniture>().canLocated == true)
                {
                    selectObj.position = new Vector3(ox, oy, oz);
                    selectObj.gameObject.GetComponent<Furniture>().located = true;
                    SaveJson(selectObj.gameObject);
                    AddManager.instance.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    AddManager.instance.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    AddManager.instance.gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(-1.5f, selectObj.GetComponent<MeshRenderer>().bounds.size.y, -1.5f));
                    AddManager.instance.gameObject.transform.GetChild(3).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(8, selectObj.GetComponent<MeshRenderer>().bounds.size.y, 8));
                    selectObj = null;
                }
                else
                {
                    selectObj.position = startPos;
                    selectObj.rotation = startLocation;
                    selectObj.GetComponent<Furniture>().canLocated = false;
                    SaveJson(selectObj.gameObject);
                    selectObj = null;
                }

            }
        }

        if (selectObj != null)
        {
            AddManager.instance.deletetObj = selectObj;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);      
            int layer = 1 << LayerMask.NameToLayer("WallRight");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight") && selectObj.CompareTag("Wall"))
                {
                    int y = (int)(hit.point.y + 0.5f);
                    oy = (int)(hit.point.y);
                    int z = (int)(hit.point.z);
                    oz = (int)(hit.point.z);
                    ox = hit.point.x;
                    
                    selectObj.position = new Vector3(hit.point.x, y, z);
                }
                //line.SetPosition(0, Camera.main.transform.position);
                //line.SetPosition(1, hit.point);
            }
        }
    }
    

    void Room(GameObject item)
    {
        currCube = Instantiate(item);
        //SaveJson(currCube.gameObject);
        //currCube.name = "d" + select;
        currCube.name = item.name;
        select += 1;
        currCube.layer = LayerMask.NameToLayer("Obj");
        int y = (int)(hit.point.y);
        int z = (int)(hit.point.z);
        currCube.transform.position = new Vector3(hit.point.x, y, z);
        SaveJson(currCube);
        //if (currCube.GetComponent<Furniture>())
        //{
        //    currCube.GetComponent<Furniture>().startPos = new Vector3(hit.point.x, y, z);
        //    startPos = currCube.GetComponent<Furniture>().startPos;
        //    currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
        //    startLocation = currCube.GetComponent<Furniture>().startRotation;
         
        //}

    }

    void Mat()
    {
        GameObject.Find("Wall_B").GetComponent<MeshRenderer>().material = mats[AddManager.instance.currButtonNum];
    }

  

    void SaveMat(GameObject objRoom)
    {
        for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if (AddManager.instance.objectInfoList[i].room == objRoom)
            {
                //��������
                AddManager.instance.objectInfoList[i].wallNumber = AddManager.instance.currButtonNum;
                
                return;
            }
        }
        AddManager.instance.objectInfo = new ObjectInfo();
        AddManager.instance.objectInfo.room = objRoom;
        AddManager.instance.objectInfo.furnitureCategoryNumber = num;
        AddManager.instance.objectInfo.wallNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
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
            }
        }
        AddManager.instance.deletetObj.GetComponent<Furniture>().Delete();

    }
    public LineRenderer line;
}
