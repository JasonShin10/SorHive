using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Map map;
    public GameObject floorQuadFactory;
    public GameObject rightQuadFactory;
    public GameObject leftQuadFactory;
    int tile = 16;
    GameObject floor;
    GameObject leftWall;
    GameObject rightWall;
    //Ray ray;
    //RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        SetTile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            map.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer = 1 << LayerMask.NameToLayer("Obj");
            if (Physics.Raycast(map.ray, out map.hit, Mathf.Infinity, layer))
            {
                if (map.hit.transform.CompareTag("Furniture"))
                {
                    // selectobj 선택된 가구
                    map = GetComponent<MapGround>();
                    map.ClickObject(map.hit);
                    map.Ground = true;

                }
                else if (map.hit.transform.CompareTag("WallLeft"))
                {
                    map = GetComponent<MapLeft>();
                    map.ClickObject(map.hit);
                    map.Left = true;
                }
                else if (map.hit.transform.CompareTag("Wall"))
                {
                    map = GetComponent<MapRight>();
                    map.ClickObject(map.hit);
                    map.Right = true;
                }

            }
            if (Physics.Raycast(map.ray, out map.hit))
            {
                if (map.hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    map = GetComponent<MapGround>();
                    map.ClickUIObj();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            map.ClickUpObject();
        }

        if (map.selectObj)
        {
            map.SelectObj();
        }

    }
    public void SetTile()
    {
        for (int i = 0; i <= tile; i++)
        {
            for (int j = 0; j <= tile; j++)
            {
                floor = Instantiate(floorQuadFactory);
                Vector3 firstPos = transform.position;
                firstPos.x += j;
                firstPos.z += i;
                floor.transform.position = firstPos;
                floor.transform.rotation = transform.rotation;

                rightWall = Instantiate(rightQuadFactory);
                firstPos = transform.position;
                firstPos.y += j;
                firstPos.z += i;
                rightWall.transform.position = firstPos;
                rightWall.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

                leftWall = Instantiate(leftQuadFactory);
                firstPos = transform.position;
                firstPos.x += j;
                firstPos.y += i;
                leftWall.transform.position = firstPos;
                leftWall.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            }
        }
    }
    public void OnClickFurniture()
    {
        map = GetComponent<MapGround>();
    }

    public void OnClickWallHang()
    {
        map = GetComponent<MapRight>();
    }

}
