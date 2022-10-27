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
    //���嵥���� : type, ��ġ, ũ��, ȸ��


    //������ ������Ʈ ���� ����
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