using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSave : MonoBehaviour
{
    RawImage img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>();
        if(Resources.Load<Texture>("ZRoomImage/my0"))
        img.texture = Resources.Load<Texture>("ZRoomImage/my0");
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSave()
    {
        
    }
}
