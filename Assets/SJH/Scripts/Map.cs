using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ObjectInfo
    {
        public int type;
        public Vector3 position;
        public Vector3 scale;
        public Vector3 angle;
    }

public class ArrayJson<T>
{
    public List<T> data;
}
public class Map : MonoBehaviour
{
    //저장데이터 : type, 위치, 크기, 회전


    //여러개 오브젝트 담을 변수
    public List<ObjectInfo> objectInfoList = new List<ObjectInfo>();


    public GameObject quadFactory;
    public int tileX = 16;
    public int tileZ = 16;
    public int tileY = 16;
    public GameObject cube;
    public bool located = true;
    public int select = 0;

    GameObject currCube;
    GameObject floor;
    Vector3 startPos;
    void Start()
    {
       
    }

   
    void Update()
    {
       
       
    }

    
}