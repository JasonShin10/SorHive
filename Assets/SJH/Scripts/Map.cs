using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject quadFactory;
    public int tileX = 5;
    public int tileZ = 5;

    GameObject currCube;
    GameObject floor;
    void Start()
    {
        for(int i = 0; i <= tileX; i++)
        {
            floor = Instantiate(quadFactory);
            Vector3 firstPos = floor.transform.position;
            firstPos.x += i;
            floor.transform.position = firstPos;
 
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                currCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                int x = (int)(hit.point.x);
                int z = (int)(hit.point.z);
                currCube.transform.position = new Vector3(x, hit.point.y, z);
                currCube.GetComponent<Collider>().enabled = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            currCube = null;
        }

        if (currCube != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int x = (int)(hit.point.x);
                int z = (int)(hit.point.z);
                currCube.transform.position = new Vector3(x, hit.point.y, z);
            }
        }
    }
}