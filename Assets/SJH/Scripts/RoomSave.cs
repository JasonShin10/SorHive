using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSave : MonoBehaviour
{
    ObjectInfo objectInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSave()
    {
        string jsonData = JsonUtility.ToJson(objectInfo);
    }
}
