using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class Map : MonoBehaviour
{
   

    //protected 상속받은 자식들만 접근 가능




   
    protected int select = 0;
    protected int num;
    protected float ox;
    protected float oz;
    protected float oy; 
    protected float box;
    //protected string tag;
    public bool located = true;
    protected Vector3 startPos;
    protected Quaternion startLocation;
    public Ray ray;
    public RaycastHit hit;
    protected Vector3 firstPos;
    public Transform selectObj;
    protected MeshRenderer rb;
    protected int currButtonNum;
    protected GameObject currCube;
    GameObject floor;
    GameObject leftWall;
    GameObject rightWall;
    public bool AddBed = false;
    public bool AddChair = false;
    public bool AddDesk = false;
    public bool Ground = false;
    public bool Left = false;
    public bool Right = false;

    void Start()
    {
       
    }


    void Update()
    {
     
    }
   public virtual void ClickObject(RaycastHit hit)
    {
        selectObj = hit.transform;
        selectObj.gameObject.GetComponent<Furniture>().located = false;
        selectObj.gameObject.GetComponent<Furniture>().startPos = hit.transform.position;
        startPos = selectObj.gameObject.GetComponent<Furniture>().startPos;
        GameManager.instance.name = selectObj.name;
    }

    public virtual void ClickUpObject()
    {       

    }

    public virtual void SelectObj()
    {

    }
    public virtual void SpawnObject(RaycastHit hit)
    {

    }


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
    // 오브젝트를 클릭했을시
    //public virtual void SelectObject(string tag)
    //{

    //        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        int layer = 1 << LayerMask.NameToLayer("Obj");
    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
    //        {
    //            if (hit.transform.gameObject.CompareTag(tag))
    //            {
    //                selectObj = hit.transform;
    //                selectObj.gameObject.GetComponent<Furniture>().located = false;
    //                selectObj.gameObject.GetComponent<Furniture>().startPos = hit.transform.position;
    //                startPos = selectObj.gameObject.GetComponent<Furniture>().startPos;
    //                GameManager.instance.name = selectObj.name;
    //            }
    //        }

    //}


    protected virtual void Room(GameObject item)
    {
        currCube = Instantiate(item);
        currCube.name = item.name;
        currCube.layer = LayerMask.NameToLayer("Obj");
    }

    public virtual void ClickUIObj()
    {
    }
    
}