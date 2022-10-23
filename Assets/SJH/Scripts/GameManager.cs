using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public string name;
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
                    name = furniture.name;
                    //print(furniture.name);

                }
            }
        }
    }
}
