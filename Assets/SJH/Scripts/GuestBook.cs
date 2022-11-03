using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestBook : MonoBehaviour
{
    public GameObject player;
    public ScrollRect guestBoxUI;
    public GameObject guestBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float box = 3;
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(guestBox.transform.position , player.transform.position); 
        if (distance > 3)
        {
            guestBoxUI.transform.GetChild(0).gameObject.SetActive(true);
            guestBoxUI.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            guestBoxUI.transform.GetChild(0).gameObject.SetActive(false);
            guestBoxUI.transform.GetChild(1).gameObject.SetActive(true);
        }
        print(distance);
    }
}
