using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHY_HexManager : MonoBehaviour
{
    public int count = 0; 



    // Start is called before the first frame update
    void Start()
    {
        //GameObject Room = Instantiate(roomItemFactory, hexPos[count]);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRoomPlus()
    {
        count++;
       
    }
}
