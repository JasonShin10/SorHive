using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHY_HexManager : MonoBehaviour
{

    public Transform[] hexPos;

    public GameObject[] room;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i > hexPos.Length; i++ )
        {
            //새로운 룸아이템이 생성되면 그 위치를 지정해둔 hexPos의 위치로 한다.
            room[i].gameObject.transform.position = hexPos[i].transform.position;
        }
    }
}
