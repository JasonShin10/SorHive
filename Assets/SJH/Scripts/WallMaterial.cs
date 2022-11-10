using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterial : MonoBehaviour
{
    public Material[] mats;
    MeshRenderer rb;

    // Start is called before the first frame update
    void Start()
    {
        mats = Resources.LoadAll<Material>("WallPaper");
        rb = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space"))
        {
            rb.material = mats[0];
        }
    }

    void SaveJson()
    {
        AddManager.instance.objectInfo = new ObjectInfo();
        //AddManager.instance.objectInfo.wallNumber = 

    }

}
