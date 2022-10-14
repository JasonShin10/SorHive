using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
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
            if(hitInfo.transform.tag == "Furniture")
            {
                GameObject furniture = hitInfo.transform.gameObject;
                print(furniture.name);
            }
        }
    }
}
