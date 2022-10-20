using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 mOffset;
    float mZCoord;
    float mYCoord;
    float speed;
    float mspeed;

    // Start is called before the first frame update
    void Update()
    {

       
            if (GameManager.instance.name == gameObject.transform.name)
            {
                Vector2 wheel = Input.mouseScrollDelta;
                //if (wheel.x != 0 || wheel.y != 0)
                //{
                //    print(wheel.y);
                //}
                if (wheel.y > 0)
                {
                    gameObject.transform.Rotate(0, speed, 0);
                    speed = 10;
                }
                else if (wheel.y < 0)
                {
                    gameObject.transform.Rotate(0, mspeed, 0);
                    mspeed = -10;
                }

            }

        
    }
    public void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (Input.GetButton("Fire1"))
            {


                if (hitInfo.transform.tag == "Furniture")
                {
                    GameObject furniture = hitInfo.transform.gameObject;
                    name = furniture.name;
                    print(furniture.name);

                }
            }
        }
    }

    //void OnMouseDown()
    //{
    //    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    //    mYCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
    //    mOffset = gameObject.transform.position - GetMouseWorldPos();
    //    mOffset.y = 0;
    //}

    //void OnMouseDrag()
    //{
    //    Vector3 drag = GetMouseWorldPos() + mOffset;
    //    drag.y = 0;
    //    transform.position = drag;

    //}



    private void Rotate(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        //mousePoint.y = mYCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
