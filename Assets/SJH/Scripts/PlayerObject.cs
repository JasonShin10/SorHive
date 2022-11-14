using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObject : MonoBehaviour
{
    public GameObject player;
    GameObject image;
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "RoomInScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            image = GameObject.Find("GuestBoxCanvas");

        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (scene.name == "RoomInScene")
        //{
        //    float distance = Vector3.Distance(transform.position, player.transform.position);
        //    // �Ÿ��� 3���� ũ�� cancle�� false�� �ٲ��ش�.

        //    if (distance > 3)
        //    {
        //        image.transform.GetChild(7).gameObject.SetActive(false);
        //    }
        //    else if (distance < 3)
        //    {
        //        image.transform.GetChild(7).gameObject.SetActive(true);
        //    }
        //}
    }
}
