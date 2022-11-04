using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHY_HexManager : MonoBehaviour
{
    public int count = 0; 

    public Transform[] hexPos;

    public GameObject roomItemFactory;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Room = Instantiate(roomItemFactory, hexPos[count]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRoomPlus()
    {
        count++;
        GameObject Room = Instantiate(roomItemFactory, hexPos[count]);
    }
}
