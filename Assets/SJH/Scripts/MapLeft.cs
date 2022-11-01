using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLeft : Map
{

    GameObject currCube;
    GameObject floor;
    int ox;
    float oz;
    int oy;
    int num;
    int mNum;
    float box;
    Vector3 startPos;
    Quaternion startLocation;
    Ray ray;
    RaycastHit hit;
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
                //currCube = Instantiate(cube);
                //currCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //int x = (int)(hit.point.x);
                //int z = (int)(hit.point.z);
                //currCube.transform.position = new Vector3(x, hit.point.y, z);
                //currCube.GetComponent<Collider>().enabled = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallLeft"))
                {
                    if (AddManager.instance.AddWallHang == true)
                    {
                       
                       
                        //currCube = Instantiate(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);
                        num = 3;
                        Room(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddWallHang = false;
                        //currCube.gameObject.tag = "WallLeft";
                        //currCube.transform.rotation = Quaternion.Euler(0, 0, 0);
                        ////currCube = Instantiate(cube);
                        //currCube.layer = LayerMask.NameToLayer("Obj");
                        //int y = (int)(hit.point.y);
                        //int x = (int)(hit.point.x);
                        //currCube.transform.position = new Vector3(x, y, hit.point.z);

                        //if (currCube.GetComponent<Furniture>())
                        //{

                        //    currCube.GetComponent<Furniture>().startPos = new Vector3(x, y, hit.point.z);
                        //    startPos = currCube.GetComponent<Furniture>().startPos;
                        //    currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                        //    startLocation = currCube.GetComponent<Furniture>().startRotation;
                        //}
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
                    //print(1);
                    selectObj.position = new Vector3(ox, oy, oz);
                    //print(selectObj.position);
                    //print(ox);
                    selectObj.gameObject.GetComponent<Furniture>().located = true;
                    selectObj = null;
                }
                else
                {
                    selectObj.position = startPos;
                    selectObj.rotation = startLocation;
                    selectObj.GetComponent<Furniture>().canLocated = false;
                    selectObj = null;
                }

            }
        }
        if (selectObj != null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layer = 1 << LayerMask.NameToLayer("WallLeft");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                //print(hit.transform.name);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallLeft") && selectObj.CompareTag("WallLeft"))
                {
                    //print(oy);
                    int y = (int)(hit.point.y);
                    oy = (int)(hit.point.y);
                    int x = (int)(hit.point.x);
                    ox = (int)(hit.point.x);
                    oz = hit.point.z;


                    selectObj.position = new Vector3(x, y, hit.point.z);
                }
                //line.SetPosition(0, Camera.main.transform.position);
                //line.SetPosition(1, hit.point);
            }
        }
    }
    void SaveJson(GameObject obj)
    {
        for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if (AddManager.instance.objectInfoList[i].obj == obj)
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
        AddManager.instance.objectInfo.objNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfo.folderNumber = num;
        AddManager.instance.objectInfo.obj = AddManager.instance.obj;
        AddManager.instance.objectInfo.position = AddManager.instance.pos;
        AddManager.instance.objectInfo.scale = AddManager.instance.sca;
        AddManager.instance.objectInfo.angle = AddManager.instance.ang;

        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);

    }

    void Room(GameObject item)
    {
        currCube = Instantiate(item);

        //SaveJson(currCube.gameObject);
        currCube.name = "d" + select;
        select += 1;
        currCube.gameObject.tag = "WallLeft";
        currCube.transform.rotation = Quaternion.Euler(0, 0, 0);
        currCube.layer = LayerMask.NameToLayer("Obj");
        int x = (int)(hit.point.x);
        int y = (int)(hit.point.y);
        currCube.transform.position = new Vector3(x, y, hit.point.z);
        SaveJson(currCube);
        //if (currCube.GetComponent<Furniture>())
        //{
        //    currCube.GetComponent<Furniture>().startPos = new Vector3(hit.point.x, y, z);
        //    startPos = currCube.GetComponent<Furniture>().startPos;
        //    currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
        //    startLocation = currCube.GetComponent<Furniture>().startRotation;

        //}

    }
}
