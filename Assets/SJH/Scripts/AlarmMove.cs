using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmMove : MonoBehaviour
{
    public GameObject alarm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alarm.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5, 0));
    }
}
