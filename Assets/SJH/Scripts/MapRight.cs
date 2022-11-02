using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRight : Map
{
   
    GameObject currCube;
    GameObject floor;
    float ox;
    int oz;
    int oy;
    int num;
    int mNum;
    float box;
    public Material[] mats;
    public Material[] floorMats;
    MeshRenderer rb;

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
                firstPos.y += j;
                firstPos.z += i;
                floor.transform.position = firstPos;
                floor.transform.rotation = transform.rotation;
            }
        }
        mats = Resources.LoadAll<Material>("WallPaper");
        rb = GetComponent<MeshRenderer>();
        floorMats = Resources.LoadAll<Material>("floorMat");
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
                if(hit.transform.gameObject.CompareTag("Wall"))
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
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight"))
                {
                    if (AddManager.instance.AddWallHang == true)
                    {
                        num = 3;
                        Room(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);
                        
                        //currCube = Instantiate(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddWallHang = false;
                        ////currCube = Instantiate(cube);
                        //currCube.layer = LayerMask.NameToLayer("Obj");
                        //int y = (int)(hit.point.y);
                        //int z = (int)(hit.point.z);
                        //currCube.transform.position = new Vector3(hit.point.x, y, z);
                        //if (currCube.GetComponent<Furniture>())
                        //{
                        //    currCube.GetComponent<Furniture>().startPos = new Vector3(hit.point.x, y, z);
                        //    startPos = currCube.GetComponent<Furniture>().startPos;
                        //    currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                        //    startLocation = currCube.GetComponent<Furniture>().startRotation;
                        //}
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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            int layer = 1 << LayerMask.NameToLayer("WallRight");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                //print(hit.transform.name);
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
                //정보수정
                AddManager.instance.objectInfoList[i].matNumber = AddManager.instance.currButtonNum;
                
                return;
            }
        }
        AddManager.instance.objectInfo = new ObjectInfo();
        AddManager.instance.objectInfo.room = objRoom;
        AddManager.instance.objectInfo.folderNumber = num;
        AddManager.instance.objectInfo.matNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    }
    public LineRenderer line;
}
