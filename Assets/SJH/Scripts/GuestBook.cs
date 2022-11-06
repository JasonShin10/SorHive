using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuestBook : MonoBehaviour
{
    public GameObject player;
    public ScrollRect guestBoxUI;
    public GameObject guestBox;
    public GameObject guestBoxWrite;
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
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        //print(distance);
    }

    public void OnGuestBookWrite()
    {
        SceneManager.LoadScene("GuestBookLoadScene");
        //guestBoxWrite.SetActive(true);
    }
}
