using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGround : Map
{
    int select = 0;
    int ox;
    int oz;
    float oy;
    int num;
    GameObject currCube;
    GameObject floor;
    Vector3 startPos;
    Quaternion startLocation;
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

        //for(int i = 0; i <= tileZ; i++)
        //{
        //    floor = Instantiate(quadFactory);
        //    Vector3 firstPos = floor.transform.position;
        //    firstPos.z += i;
        //    floor.transform.position = firstPos;
        //}
    }
    UnityEngine.Transform selectObj;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
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
                    selectObj.GetComponent<BoxCollider>().center = new Vector3(selectObj.GetComponent<BoxCollider>().center.x, selectObj.GetComponent<BoxCollider>().center.y - 1.5f, selectObj.GetComponent<BoxCollider>().center.z);
                    //RemoveJson(selectObj.gameObject);
                }
               
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (AddManager.instance.AddBed == true)
                    {
                        currCube = Instantiate(AddManager.instance.bedItems[AddManager.instance.currButtonNum]);
                        currCube.name = "d" + select;
                        select += 1;
                        currCube.layer = LayerMask.NameToLayer("Obj");
                        num = 0;
                        AddManager.instance.AddBed = false;
                        SaveJson(currCube.gameObject);
                        int x = (int)(hit.point.x);
                        int z = (int)(hit.point.z);
                        currCube.transform.position = new Vector3(x, hit.point.y, z);
                        if (currCube.GetComponent<Furniture>())
                        {
                            currCube.GetComponent<Furniture>().startPos = new Vector3(x, hit.point.y, z);
                            startPos = currCube.GetComponent<Furniture>().startPos;
                            currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                            startLocation = currCube.GetComponent<Furniture>().startRotation;
                        }
                    }
                    if (AddManager.instance.AddChair == true)
                    {
                        currCube = Instantiate(AddManager.instance.chairItems[AddManager.instance.currButtonNum]);
                        num = 1;
                        AddManager.instance.AddChair = false;
                        SaveJson(currCube.gameObject);
                        currCube.name = "d" + select;
                        select += 1;
                        currCube.layer = LayerMask.NameToLayer("Obj");
                        int x = (int)(hit.point.x);
                        int z = (int)(hit.point.z);
                        currCube.transform.position = new Vector3(x, hit.point.y, z);
                        if (currCube.GetComponent<Furniture>())
                        {
                            currCube.GetComponent<Furniture>().startPos = new Vector3(x, hit.point.y, z);
                            startPos = currCube.GetComponent<Furniture>().startPos;
                            currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                            startLocation = currCube.GetComponent<Furniture>().startRotation;
                        }
                    }
                    //startPos = currCube.transform.position;
                }
                if (AddManager.instance.AddDesk == true)
                {
                    currCube = Instantiate(AddManager.instance.DeskItem[AddManager.instance.currButtonNum]);
                    AddManager.instance.AddDesk = false;
                    currCube.name = "d" + select;
                    select += 1;
                    currCube.layer = LayerMask.NameToLayer("Obj");
                    int x = (int)(hit.point.x);
                    int z = (int)(hit.point.z);
                    currCube.transform.position = new Vector3(x, hit.point.y, z);
                    if (currCube.GetComponent<Furniture>())
                    {
                        currCube.GetComponent<Furniture>().startPos = new Vector3(x, hit.point.y, z);
                        startPos = currCube.GetComponent<Furniture>().startPos;
                        currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                        startLocation = currCube.GetComponent<Furniture>().startRotation;
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
                    SaveJson(selectObj.gameObject);
                    selectObj.gameObject.GetComponent<Furniture>().located = true;
                    selectObj.GetComponent<BoxCollider>().center = new Vector3(selectObj.GetComponent<BoxCollider>().center.x, selectObj.GetComponent<BoxCollider>().center.y+1.5f, selectObj.GetComponent<BoxCollider>().center.z);
                    selectObj = null;

                }
                else
                {
                    selectObj.position = startPos;
                    selectObj.rotation = startLocation;
                    SaveJson(selectObj.gameObject);
                    selectObj.GetComponent<Furniture>().canLocated = false;
                    selectObj = null;
                }

            }
        }
        if (selectObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
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
            if(Input.GetKeyDown("i"))
            {
                selectObj.GetComponent<Furniture>().Delete();
                RemoveJson(selectObj.gameObject);
                
            }
        }
    }

    void SaveJson(GameObject obj)
    {
        for(int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if(AddManager.instance.objectInfoList[i].obj == obj)
            {
                //정보수정
                AddManager.instance.objectInfoList[i].position = obj.transform.position;
                AddManager.instance.objectInfoList[i].scale = obj.transform.localScale;
                AddManager.instance.objectInfoList[i].angle = obj.transform.eulerAngles;
                return ;
            }
        }
        AddManager.instance.objectInfo = new ObjectInfo();
        AddManager.instance.obj = obj;
        
        AddManager.instance.pos = obj.transform.position;
        AddManager.instance.sca = obj.transform.localScale;
        AddManager.instance.ang = obj.transform.eulerAngles;
        AddManager.instance.objectInfo.objNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfo.folderNumber = num;
        AddManager.instance.objectInfo.obj = AddManager.instance.obj;
        AddManager.instance.objectInfo.position = AddManager.instance.pos;
        AddManager.instance.objectInfo.scale = AddManager.instance.sca;
        AddManager.instance.objectInfo.angle = AddManager.instance.ang;
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);       
    }

    void RemoveJson(GameObject obj)
    {
        ObjectInfo info;
        for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if (AddManager.instance.objectInfoList[i].obj == obj)
            {
                AddManager.instance.objectInfoList.RemoveAt(i);
                return;
            }
        }
      
    }


}