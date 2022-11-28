using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public Transform setPlayer;

    public Quaternion quaternion;

    Scene scene;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public string name;
    public GameObject selected;
   
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SerializationRate = 60;

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.Instantiate("Player", setPlayer.transform.position, quaternion);
        if (scene.name == "RoomInScene")
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Select();
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print(PhotonNetwork.PlayerList[0].NickName + "이 방에 참여했따!!!");
    }

    //public void Select()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(ray, out hitInfo))
    //    {
    //        if (Input.GetButton("Fire1"))
    //        {


    //            if (hitInfo.transform.tag == "Furniture")
    //            {
    //                GameObject furniture = hitInfo.transform.gameObject;
    //                name = furniture.name;
    //                print(furniture.name);

    //            }
    //        }
    //    }
    //}
}
