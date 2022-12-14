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
    float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        print(HttpManager.instance.userId);
        print(HttpManager.instance.id);
    }
    float box = 3;
    public bool cancel = false;
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            distance = Vector3.Distance(guestBox.transform.position, player.transform.position);
            // 거리가 3보다 크면 cancle을 false로 바꿔준다.

            if (distance > 3)
            {
                cancel = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (distance < 3 && cancel == false)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                if (HttpManager.instance.userId == HttpManager.instance.id)
                {
                    gameObject.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    gameObject.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                }

            }
        }
        if(GameObject.Find("Player(Clone)"))
        {
            player = GameObject.Find("Player(Clone)");
        }
        //print(HttpManager.instance.userId);
        //print(HttpManager.instance.id);
        //print(distance);

    }

    public void OnGuestBookWrite()
    {
        SceneManager.LoadScene("GuestBookLoadScene");
        
    }

    public void OnCancle()
    {
        cancel = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}
