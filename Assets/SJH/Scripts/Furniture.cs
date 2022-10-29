using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public bool located = false;
    public bool canLocated = true;
   
    public Vector3 startPos;
    public Quaternion startRotation;
    // Start is called before the first frame update
    void Start()
    {
        canLocated = true;
        //located = true;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Furniture"))
        {
            canLocated = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            canLocated = false;
        }
        if (other.gameObject.CompareTag("WallLeft"))
        {
            canLocated = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Furniture"))
        {
            canLocated = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            canLocated = true;
        }
        if (other.gameObject.CompareTag("WallLeft"))
        {
            canLocated = true;
        }
    }

    public void Delete()
    {
        
        Destroy(gameObject);
    }


}
