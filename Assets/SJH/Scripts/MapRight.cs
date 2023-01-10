using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRight : Map
{
    

    public GameObject cube;
    public Material[] floorMats;
    public Material[] mats;
    //Transform selectObj;
    GameObject floor;
    
    void Start()
    {
        //tag = "wall";
        //for (int i = 0; i <= tileX; i++)
        //{
        //    for (int j = 0; j <= tileX; j++)
        //    {
        //        floor = Instantiate(quadFactory);
        //        Vector3 firstPos = transform.position;
        //        firstPos.y += j;
        //        firstPos.z += i;
        //        floor.transform.position = firstPos;
        //        floor.transform.rotation = transform.rotation;
        //    }
        //}


        //mats = Resources.LoadAll<Material>("WallPaper");
        //rb = GetComponent<MeshRenderer>();
        //floorMats = Resources.LoadAll<Material>("floorMat");
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //SelectObject(tag);
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    int layer = 1 << LayerMask.NameToLayer("Obj");
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        //    {
        //        if (hit.transform.gameObject.CompareTag("Wall"))
        //        {
        //            selectObj = hit.transform;
        //            selectObj.gameObject.GetComponent<Furniture>().located = false;
        //            selectObj.gameObject.GetComponent<Furniture>().startPos = hit.transform.position;
        //            startPos = selectObj.gameObject.GetComponent<Furniture>().startPos;
        //            GameManager.instance.name = selectObj.name;
        //        }
        //    }
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight"))
        //        {
        //            if (AddManager.instance.AddWallHang == true)
        //            {
        //                num = 3;
        //                Room(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);          
        //                AddManager.instance.AddWallHang = false;
        //            }
        //            if (AddManager.instance.AddMaterial == true)
        //            {          
        //                num = 15;
        //                Mat();
        //                SaveMat(GameObject.Find("Wall_B"));
        //                AddManager.instance.AddWallHang = false;
        //            }
        //        }
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (selectObj)
        //    {
        //        if (selectObj.GetComponent<Furniture>().canLocated == true)
        //        {
        //            selectObj.position = new Vector3(ox, oy, oz);
        //            selectObj.gameObject.GetComponent<Furniture>().located = true;
        //            SaveJson(selectObj.gameObject);
        //            AddManager.instance.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        //            AddManager.instance.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        //            AddManager.instance.gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(-1.5f, selectObj.GetComponent<MeshRenderer>().bounds.size.y, -1.5f));
        //            AddManager.instance.gameObject.transform.GetChild(3).gameObject.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(AddManager.instance.cam, selectObj.position + new Vector3(8, selectObj.GetComponent<MeshRenderer>().bounds.size.y, 8));
        //            selectObj = null;
        //        }
        //        else
        //        {
        //            selectObj.position = startPos;
        //            selectObj.rotation = startLocation;
        //            selectObj.GetComponent<Furniture>().canLocated = false;
        //            SaveJson(selectObj.gameObject);
        //            selectObj = null;
        //        }
        //    }
        //}

        //if (selectObj != null)
        //{
        //    AddManager.instance.deletetObj = selectObj;
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);      
        //    int layer = 1 << LayerMask.NameToLayer("WallRight");
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        //    {
        //        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight") && selectObj.CompareTag("Wall"))
        //        {
        //            int y = (int)(hit.point.y + 0.5f);
        //            oy = (int)(hit.point.y);
        //            int z = (int)(hit.point.z);
        //            oz = (int)(hit.point.z);
        //            ox = hit.point.x;
                    
        //            selectObj.position = new Vector3(hit.point.x, y, z);
        //        }
        //    }
        //}
    }

   
    //void Room(GameObject item)
    // {
    //     currCube = Instantiate(item);
    //     currCube.name = item.name;
    //     currCube.layer = LayerMask.NameToLayer("Obj");
    //     select += 1;
    //     int y = (int)(hit.point.y);
    //     int z = (int)(hit.point.z);
    //     currCube.transform.position = new Vector3(hit.point.x, y, z);
    //     SaveJson(currCube);
    // }

    // void Mat()
    // {
    //     GameObject.Find("Wall_B").GetComponent<MeshRenderer>().material = mats[AddManager.instance.currButtonNum];
    // }
    // protected override void SaveJson(GameObject obj)
    // {
    //     base.SaveJson(obj);
    //     AddManager.instance.objectInfo.wallTag = "WallRight";
    //     AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    // }


    // void SaveMat(GameObject objRoom)
    // {
    //     for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
    //     {
    //         if (AddManager.instance.objectInfoList[i].room == objRoom)
    //         {
    //             AddManager.instance.objectInfoList[i].wallNumber = AddManager.instance.currButtonNum;          
    //             return;
    //         }
    //     }
    //     AddManager.instance.objectInfo = new ObjectInfo();
    //     AddManager.instance.objectInfo.room = objRoom;
    //     AddManager.instance.objectInfo.furnitureCategoryNumber = num;
    //     AddManager.instance.objectInfo.wallNumber = AddManager.instance.currButtonNum;
    //     AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    // }


    //public override void SelectObject(string tag)
    //{
    //    base.SelectObject(tag);
    //}
}
