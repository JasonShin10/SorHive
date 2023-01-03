using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class Map : MonoBehaviour
{
    public GameObject quadFactory;
    public GameObject cube;
    public GameObject currCube;
    public Material[] floorMats;
    public int tileX = 16;
    public int tileZ = 16;
    public int tileY = 16;
    public int select = 0;
    public int num;
    public float ox;
    public float oz;
    public float oy;
    
    public bool located = true;
    public Vector3 firstPos;
    float box;
    public Transform selectObj;
    public MeshRenderer rb;

    GameObject floor;
    void Start()
    {
       
    }

   
    void Update()
    {
       
       
    }

    //public void Tile(float a, float b)
    //{
    //    for (int i = 0; i <= tileX; i++)
    //    {
    //        for (int j = 0; j <= tileX; j++)
    //        {
    //            firstPos = transform.position;
    //            floor = Instantiate(quadFactory);
    //            a += j;
    //            b += i;
    //            floor.transform.position = firstPos;
    //            floor.transform.rotation = transform.rotation;
    //        }
    //    }
    //}

    public void SaveJson(GameObject obj)
    {
        for (int i = 0; i < AddManager.instance.objectInfoList.Count; i++)
        {
            if (AddManager.instance.objectInfoList[i].obj)
            {   
                    AddManager.instance.objectInfoList[i].position = obj.transform.position;
                    AddManager.instance.objectInfoList[i].scale = obj.transform.localScale;
                    AddManager.instance.objectInfoList[i].angle = obj.transform.eulerAngles;
                    return;   
            }
        }
        AddManager.instance.objectInfo = new ObjectInfo();
        AddManager.instance.objectInfo.furnitureNumber = AddManager.instance.currButtonNum;
        AddManager.instance.objectInfo.furnitureCategoryNumber = num;
        AddManager.instance.objectInfo.obj = obj;
        AddManager.instance.objectInfo.position = obj.transform.position;
        AddManager.instance.objectInfo.scale = obj.transform.localScale;
        AddManager.instance.objectInfo.angle = obj.transform.eulerAngles;
        AddManager.instance.objectInfo.name = obj.gameObject.name;
        if (gameObject.GetComponent<MapLeft>())
        {
            AddManager.instance.objectInfo.wallTag = "WallLeft";
        }
        else if (gameObject.GetComponent<MapGround>())
        {
            AddManager.instance.objectInfo.boxPosition = new Vector3(obj.GetComponent<BoxCollider>().center.x, box, obj.GetComponent<BoxCollider>().center.y);
        }
        else if (gameObject.GetComponent<MapRight>())
        {
            AddManager.instance.objectInfo.wallTag = "WallRight";
        }
        AddManager.instance.objectInfoList.Add(AddManager.instance.objectInfo);
    }

   
}