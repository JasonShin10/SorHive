using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
    Vector3 mOffset;
    float mZCoord;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Select();
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
                    print(furniture.name);
                   
                }
            }
        }
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
       transform.position = GetMouseWorldPos() + mOffset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


}
