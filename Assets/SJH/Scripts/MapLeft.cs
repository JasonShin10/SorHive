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
    Vector3 startPos;
    Quaternion startLocation;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Obj");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                selectObj = hit.transform;
                selectObj.gameObject.GetComponent<Furniture>().located = false;
                selectObj.gameObject.GetComponent<Furniture>().startPos = hit.transform.position;
                startPos = selectObj.gameObject.GetComponent<Furniture>().startPos;
                GameManager.instance.name = selectObj.name;
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
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallLeft"))
                {
                    if (AddManager.instance.AddWallHang == true)
                    {
                        currCube = Instantiate(AddManager.instance.WallHangItem[AddManager.instance.currButtonNum]);
                        AddManager.instance.AddWallHang = false;
                        //currCube = Instantiate(cube);
                        currCube.layer = LayerMask.NameToLayer("Obj");
                        int y = (int)(hit.point.y);
                        int x = (int)(hit.point.x);
                        currCube.transform.position = new Vector3(x, y, hit.point.z);
                        if (currCube.GetComponent<Furniture>())
                        {

                            currCube.GetComponent<Furniture>().startPos = new Vector3(x, y, hit.point.z);
                            startPos = currCube.GetComponent<Furniture>().startPos;
                            currCube.GetComponent<Furniture>().startRotation = currCube.transform.rotation;
                            startLocation = currCube.GetComponent<Furniture>().startRotation;
                        }
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
            int layer = 1 << LayerMask.NameToLayer("Wall");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                //print(hit.transform.name);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallLeft") && selectObj.CompareTag("Wall"))
                {
                    int y = (int)(hit.point.y + 0.5f);
                    oy = (int)(hit.point.y);
                    int x = (int)(hit.point.x);
                    ox = (int)(hit.point.z);
                    oz = hit.point.z;

                    selectObj.position = new Vector3(x, y, hit.point.z);
                }
                //line.SetPosition(0, Camera.main.transform.position);
                //line.SetPosition(1, hit.point);
            }
        }
    }
    }
