using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public Transform setPlayer;

    public Quaternion quaternion;


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

        PhotonNetwork.Instantiate("PLayer", setPlayer.transform.position, quaternion);
    }

    // Update is called once per frame
    void Update()
    {
        //Select();
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print(PhotonNetwork.PlayerList[1].NickName + "�� �濡 �����ߵ�!!!");
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
