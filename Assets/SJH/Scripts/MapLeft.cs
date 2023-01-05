using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLeft : Map
{

    public GameObject quadFactory;
    public GameObject cube;
    public Material[] floorMats;
    GameObject floor;
    
    void Start()
    {
        for (int i = 0; i <= tileX; i++)
        {
            for (int j = 0; j <= tileY; j++)
            {
                floor = Instantiate(quadFactory);
                Vector3 firstPos = transform.position;
                firstPos.x += j;
                firstPos.y += i;
                floor.transform.position = firstPos;
                floor.transform.rotation = transform.rotation;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer = 1 << LayerMask.NameToLayer("Obj");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.transform.CompareTag("WallLeft"))
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
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallLeft"))
                {
                    if (AddManager.instance.AddWallHang == true)
                    {
                        num = 3;
                        Room(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);
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
                    selectObj.gameObject.GetComponent<Furniture>().located = true;
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

            int layer = 1 << LayerMask.NameToLayer("WallLeft");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallLeft") && selectObj.CompareTag("WallLeft"))
                {
                    int y = (int)(hit.point.y);
                    oy = (int)(hit.point.y);
                    int x = (int)(hit.point.x);
                    ox = (int)(hit.point.x);
                    oz = hit.point.z;
                    selectObj.position = new Vector3(x, y, hit.point.z);
                }
            }
        }
    }

    protected override void Room(GameObject item)
    {
        select += 1;
        currCube.gameObject.tag = "WallLeft";
        currCube.transform.rotation = Quaternion.Euler(0, 0, 0);
        int x = (int)(hit.point.x);
        int y = (int)(hit.point.y);
        currCube.transform.position = new Vector3(x, y, hit.point.z);
        SaveJson(currCube);
    }
    protected override void SaveJson(GameObject obj)
    {
        base.SaveJson(obj);
        AddManager.instance.objectInfo.wallTag = "WallLeft";
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    }

}
