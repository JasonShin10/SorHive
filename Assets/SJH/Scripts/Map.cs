using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class Map : MonoBehaviour
{
    //protected 상속받은 자식들만 접근 가능
    
    protected GameObject currCube;
    protected int tileX = 16;
    protected int tileZ = 16;
    protected int tileY = 16;
    protected int select = 0;
    protected int num;
    protected float ox;
    protected float oz;
    protected float oy; 
    public bool located = true;
    protected Vector3 startPos;
    protected Quaternion startLocation;
    protected Ray ray;
    protected RaycastHit hit;
    protected Vector3 firstPos;
    protected float box;
    protected Transform selectObj;
    protected MeshRenderer rb;

    GameObject floor;
    void Start()
    {
       
    }

   
    void Update()
    {
       
       
    }

    //protected void Tile(float a, float b)
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

    protected virtual void SaveJson(GameObject obj)
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
    }

    protected virtual void Room(GameObject item)
    {
        currCube = Instantiate(item);
        currCube.name = item.name;
        currCube.layer = LayerMask.NameToLayer("Obj");       
    }


}