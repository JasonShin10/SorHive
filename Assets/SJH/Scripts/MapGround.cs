using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGround : Map
{
    int select = 0;
    int ox;
    int oz;
    float oy;
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
                        ObjectInfo objectInfo = new ObjectInfo();
                        objectInfo.position = currCube.transform.position;
                        objectInfo.scale = currCube.transform.localScale;
                        objectInfo.angle = currCube.transform.eulerAngles;
                        AddManager.instance.objectInfo = objectInfo;
                        objectInfoList.Add(objectInfo);  
                        AddManager.instance.AddBed = false;

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
                        AddManager.instance.AddChair = false;
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
        }
    }
}