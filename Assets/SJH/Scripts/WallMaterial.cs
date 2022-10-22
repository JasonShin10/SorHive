using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterial : MonoBehaviour
{
    public Material[] mats;
    // Start is called before the first frame update
    void Start()
    {
        mats = Resources.LoadAll<Material>("WallPaper");
        Renderer rd = GetComponent<MeshRenderer>();
        Material mat = rd.material;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
