using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public bool located = false;
    public bool canLocated = true;
    public List<GameObject> rays = new List<GameObject>();
    public Vector3 startPos;
    public Quaternion startRotation;
    // Start is called before the first frame update
    void Start()
    {
        canLocated = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Furniture"))
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
    }

}
