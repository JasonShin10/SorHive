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
            //���ο� ��������� �����Ǹ� �� ��ġ�� �����ص� hexPos�� ��ġ�� �Ѵ�.
            room[i].gameObject.transform.position = hexPos[i].transform.position;
        }
    }
}
