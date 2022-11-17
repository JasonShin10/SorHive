using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

using UnityEngine;

public class NickNameItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNickName(string nickName){
        GetComponent<Text>().text = nickName;
    }
}
