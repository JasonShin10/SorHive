using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRight : MonoBehaviour
{
    public GameObject quadFactory;
    public int tileX = 5;
    public int tileY = 5;
    public GameObject cube;
    GameObject currCube;
    GameObject floor;
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
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight"))
                {
                    currCube = Instantiate(cube);
                    currCube.layer = LayerMask.NameToLayer("Obj");
                    int x = (int)(hit.point.x);
                    int z = (int)(hit.point.z);
                    currCube.transform.position = new Vector3(x, hit.point.y, z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectObj = null;
        }

        if (selectObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("WallRight");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                print(hit.transform.name);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallRight") && selectObj.CompareTag("Wall"))
                {
                    int x = (int)(hit.point.x);
                    int z = (int)(hit.point.z + 0.5f);
                    int y = (int)(hit.point.y + 0.5f);
                    
                    selectObj.position = new Vector3(x, y, z);
                }
                line.SetPosition(0, Camera.main.transform.position);
                line.SetPosition(1, hit.point);
            }
        }
    }

    public LineRenderer line;
}
