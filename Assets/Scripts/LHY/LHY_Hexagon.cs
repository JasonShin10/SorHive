using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_Hexagon : MonoBehaviour
{
    public GameObject hexFactory;

    public uint Radius;

    public float HexSideMultiplier = 1;

    private const float sq1 = 0.8660254037844386f;

    int x = 0, y = 0;

    void Start()
    {
        //Point of the next hexagon to be spawned
        Vector3 currentPoint = transform.position;
        if (hexFactory.transform.localScale.x != hexFactory.transform.localScale.y)
        {
            Debug.LogError("Hexagon has not uniform scale: cannot determine its side. Aborting");
            return;
        }

        Vector3[] mv =
        {
            new Vector3(-0.5f,sq1, 0),           //LU
            new Vector3(-1, 0, 0),               //LX
            new Vector3(-0.5f, -sq1, 0),         //LD
            new Vector3(1, 0, 0),                //RD
            new Vector3(0.5f, -sq1, 0),          //RX
            new Vector3(0.5f,sq1, 0),            //RU
        };

        int lmv = mv.Length;
        float HexSide = hexFactory.transform.localScale.x * HexSideMultiplier;
    }

    void Update()
    {
        
    }
}
