using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;



#region Json
[System.Serializable]
public class GuestBookJsonInfo
{
    public int roomId;
    //public byte[] onlineRoomImage;
    public string guestBookContent;
    public string guestBookWriterId;
    public string guestBookId;
}

[System.Serializable]
public class ObjectInfo
{
    public int wallNumber;
    public int floorNumber;
    public int furnitureCategoryNumber;
    public int furnitureNumber;
    public GameObject obj;
    public GameObject room;
    public string name;
    public Vector3 position;
    public Vector3 scale;
    public Vector3 angle;
    public Vector3 boxPosition;
    public int roomId;
    public string wallTag;
}
[System.Serializable]
public class FurnitureInfo
{
    public byte[] roomImage;
    //public byte[] onlineRoomImage;
    public List<ObjectInfo> furnitures;
}

[System.Serializable]
public class ArrayJson<T>
{
    public List<T> furnitures;
}

public class ArrayGuestJson<T>
{
    public List<T> guestBookDataList;
}

[System.Serializable]
public class LoginInfo2
{
    public string memberId;
    public string password;
}
[System.Serializable]
public class RoomStatus
{
    public int status;
    public string message;
    public RoomData data;
}
[System.Serializable]
public class RoomData
{
    public int id;
    public RoomCreator roomCreator;
}
[System.Serializable]
public class RoomCreator
{
    public RoomValue memberCode;
    public string name;
    public List<ObjectInfo> furnitures;
}
[System.Serializable]
public class RoomValue
{
    public int value;
}
[System.Serializable]
public class RoomImage
{
    public string memberRoomImage;
}
#endregion

public class AddManager : MonoBehaviour
{
    [SerializeField] string screenShotName;
    public static AddManager instance;
    public Transform deletetObj;
    public Button deleteButton;
    public ObjectInfo objectInfo;
    public List<ObjectInfo> objectInfoList = new List<ObjectInfo>();
    public GuestBookJsonInfo guestBookJsonInfo;
    public List<GuestBookJsonInfo> guestBookJsonInfoList = new List<GuestBookJsonInfo>();
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

    }

    //오브젝트들이 생성되는 장소
    public Transform SpawnPos;

    public Camera cam;
    //침대오브젝트
    public Canvas rotate;
    #region 오브젝트 배열
    public GameObject[] bedItems;
    //의자오브젝트
    public GameObject[] chairItems;
    //책상 오브젝트
    public GameObject[] DeskItem;
    //벽 걸이 오브젝트
    public GameObject[] WallHangItem;
    //옷장 오브젝트
    public GameObject[] closetItems;
    //옷걸이 오브젝트
    public GameObject[] clothesItems;
    //커피 테이블 오브젝트
    public GameObject[] coffee_tableItems;
    //전자 제품 오브젝트
    public GameObject[] entertainmentItems;
    //오락 제품 오브젝트
    public GameObject[] electrionicsItems;
    //꽃 오브젝트
    public GameObject[] flowerItems;
    //부엌 의자 오브젝트
    public GameObject[] kitchenChairItems;
    //부엌 용품 오브젝트
    public GameObject[] kitchenTableItems;
    //램프 오브젝트
    public GameObject[] lamp;
    //라운지 의자 오브젝트
    public GameObject[] loungeChairItems;
    //음악 오브젝트
    public GameObject[] musical_instrumentItems;
    //오피스 의자 오브젝트
    public GameObject[] office_chair;
    //선반 오브젝트
    public GameObject[] shelf;
    //벽지 머티리얼
    public Material[] mats;
    //바닥 머티리얼
    public Material[] floor;
    #endregion
    public List<GameObject> objActive = new List<GameObject>();
    MeshRenderer rb;

    public GameObject obj;
    public Vector3 pos;
    public Vector3 sca;
    public Vector3 ang;
    #region boolUI
    public bool AddBed = false;
    public bool AddChair = false;
    public bool AddDesk = false;
    public bool AddWallHang = false;
    public bool AddCloset = false;
    public bool AddClothes = false;
    public bool AddCoffeeTable = false;
    public bool AddEntertainment = false;
    public bool AddElectrionic = false;
    public bool AddFlower = false;
    public bool AddKitchenChair = false;
    public bool AddKitchenTable = false;
    public bool AddLamp = false;
    public bool AddLoungeChair = false;
    public bool AddInstrument = false;
    public bool AddOfficeChair = false;
    public bool AddShelf = false;
    public bool AddMaterial = false;
    public bool AddFloor = false;
    #endregion
    public int currButtonNum = 0;
    Scene scene;
    public int n;
    void Start()
    {
        #region Resources불러오기
        bedItems = Resources.LoadAll<GameObject>("bed");
        chairItems = Resources.LoadAll<GameObject>("armchair");
        DeskItem = Resources.LoadAll<GameObject>("office_desk");
        WallHangItem = Resources.LoadAll<GameObject>("window");
        closetItems = Resources.LoadAll<GameObject>("closet");
        clothesItems = Resources.LoadAll<GameObject>("clothes");
        coffee_tableItems = Resources.LoadAll<GameObject>("coffee_table");
        entertainmentItems = Resources.LoadAll<GameObject>("entertainment");
        electrionicsItems = Resources.LoadAll<GameObject>("electronics");
        flowerItems = Resources.LoadAll<GameObject>("flower");
        kitchenChairItems = Resources.LoadAll<GameObject>("kitchen_chair");
        kitchenTableItems = Resources.LoadAll<GameObject>("kitchen_table");
        lamp = Resources.LoadAll<GameObject>("lamp");
        loungeChairItems = Resources.LoadAll<GameObject>("lounge_chair");
        musical_instrumentItems = Resources.LoadAll<GameObject>("musical_instrument");
        office_chair = Resources.LoadAll<GameObject>("office_chair");
        shelf = Resources.LoadAll<GameObject>("shelf");
        mats = Resources.LoadAll<Material>("WallPaper");
        floor = Resources.LoadAll<Material>("floorMat");
        rb = GetComponent<MeshRenderer>();
        
        #endregion 
        //OnLoad2();
        //OnClickLogin();
        print(1);
        GetPostAll();
        //OnLoadJson();
        objActive.AddRange(GameObject.FindGameObjectsWithTag("Furniture"));
        scene = SceneManager.GetActiveScene();
        if (scene.name == "RoomInScene")
        {
            for (int i = 0; i < objActive.Count; i++)
            {
                objActive[i].GetComponent<Rigidbody>().isKinematic = false;
                objActive[i].GetComponent<BoxCollider>().isTrigger = false;
                objActive[i].GetComponent<BoxCollider>().center = new Vector3(objActive[i].GetComponent<BoxCollider>().center.x, objActive[i].GetComponent<BoxCollider>().center.y, 0);
            }
        }
        
        //C:\Users\sjaso\Documents\GitHub\SorHive\Assets\Resources\ZRoomImage
        JObject json = new JObject();
        json["byte"] = File.ReadAllBytes(Application.dataPath + "/Resources/ZRoomImage/my0.png");
        File.WriteAllText(Application.dataPath + "/test.txt", json.ToString());
    }
    #region 임시로그인
    public void OnClickLogin()
    {
        LoginInfo2 logdata = new LoginInfo2();
        //logdata.memberId = "john1230";
        HttpManager.instance.userId = "john1230";
        logdata.memberId = HttpManager.instance.id;
        print(HttpManager.instance.id);
        logdata.password = "qwer1234!";
        HttpRequester requester = new HttpRequester();
        requester.url = "http://52.79.209.232:8080/api/v1/auth/login";
        requester.requestType = RequestType.PUT;
        requester.putData = JsonUtility.ToJson(logdata);
        requester.onComplete = OnClickDownload;
        HttpManager.instance.SendRequest(requester);
    }

    private void OnClickDownload(DownloadHandler handler)
    {
        JObject json = JObject.Parse(handler.text);
        string token = json["data"]["accessToken"].ToString();
        print(token);
        PlayerPrefs.SetString("token", token);
        print("조회 완료");
    }
    #endregion 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //OnClickLogin();
            GetPostAll();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnClickLogin();
        }

        //print(n);

    }

    #region GetRoom
    public void GetPostAll()
    {
        HttpRequester requester = new HttpRequester();
        print(HttpManager.instance.memberCode);
        requester.url = "http://52.79.209.232:8080/api/v1/room/" + HttpManager.instance.memberCode;
        //requester.url = "http://52.79.209.232:8080/api/v1/room/" + 1;
        requester.requestName = "GetPostAll";
        requester.requestType = RequestType.GET;
        requester.onComplete = OnCompleteGetPostAll;


        HttpManager.instance.SendRequest(requester);
    }
    string sHandler;
    public void OnCompleteGetPostAll(DownloadHandler handler)
    {
        sHandler = handler.text;
        print(sHandler);
        JObject jsonData = JObject.Parse(sHandler);
        print(jsonData);
        //JArray jarry = jsonData["data"]["furnitures"].ToObject<JArray>();
        int jarry = jsonData["data"]["roomId"].ToObject<int>();
        HttpManager.instance.roomId = jarry;
        print(jarry);
        //for (int i =0; i< jarry.Count; i++)
        //{
        //int roomIdData = jarry[0]["roomId"].ToObject<int>();
        //print(roomIdData);

        //}
        //for(int i = 0; i < jarry.Count; i++)
        //{
        //    ObjectInfo info = new ObjectInfo();

        //    info.wallNumber = jarry[i]["wallNumber"].ToObject<int>();

        //    objectInfoList.Add(info);
        //}

        //int status = jsonData["status"].ToObject<int>();
        string furnituersData = "{\"furnitures\":" + jsonData["data"]["furnitures"].ToString() + "}";
        string guestBookData = "{\"guestBookDataList\":" + jsonData["data"]["guestBookDataList"].ToString() + "}";
        //string roomIdData = jsonData["data"]["roomId"].ToObject<int>();

        //string data = "{"+ jsonData["data"].ToString() + "}";
        print(furnituersData);
        //print(roomIdData);
        //HttpManager.instance.roomId = roomIdData;
        ArrayJson<ObjectInfo> objectInfo = JsonUtility.FromJson<ArrayJson<ObjectInfo>>(furnituersData);
        objectInfoList = objectInfo.furnitures;
        //HttpManager.instance.roomId = objectInfo.
        //string roomIdData = jsonData["data"]["roomId"].ToObject<int>();

        //string data = "{"+ jsonData["data"].ToString() + "}";
        print(guestBookData);
        //print(roomIdData);
        //HttpManager.instance.roomId = roomIdData;
        ArrayGuestJson<GuestBookJsonInfo> guestBookInfo = JsonUtility.FromJson<ArrayGuestJson<GuestBookJsonInfo>>(guestBookData);

        guestBookJsonInfoList = guestBookInfo.guestBookDataList;
        //HttpManager.instance.roomId = objectInfo.


        //n = objectInfoList.Count;
        if (scene.name == "RoomInScene")
        {
            for (int i = 0; i < guestBookJsonInfoList.Count; i++)
            {
                RoomInManager.instance.CreateObject(guestBookJsonInfoList[i]);
            }

        }
       
        n = objectInfoList.Count;

        for (int i = 0; i < objectInfoList.Count; i++)
        {
            CreateObject(objectInfoList[i]);
        }

        //PostDataArray array = JsonUtility.FromJson<PostDataArray>(sHandler);
        //for(int i=0; i<array.data.Count; i++)
        //{

        //}

        //print(roomStatue.message);
        //OnLoadJson(sHandler);
        //PostData postData = JsonUtility.FromJson<PostData>(handler.text);
        //string s = "{\"furniture\":" + handler.text + "}";
        print("조회 완료");
    }
    #endregion

    #region createObject

    public void CreateObject(ObjectInfo info)
    {
        if (info.furnitureCategoryNumber == 0)
        {
            info.obj = bedItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            createObj.GetComponent<BoxCollider>().center = info.boxPosition;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 1)
        {
            info.obj = chairItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            createObj.GetComponent<BoxCollider>().center = info.boxPosition;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 2)
        {
            info.obj = DeskItem[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            createObj.GetComponent<BoxCollider>().center = info.boxPosition;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 3)
        {
            info.obj = WallHangItem[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            if(info.wallTag == "WallLeft")
            {
                createObj.gameObject.tag = "WallLeft";
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 4)
        {
            info.obj = closetItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 5)
        {
            info.obj = coffee_tableItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 6)
        {
            info.obj = entertainmentItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 7)
        {
            info.obj = electrionicsItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 8)
        {
            info.obj = flowerItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 9)
        {
            info.obj = kitchenChairItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 10)
        {
            info.obj = kitchenTableItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 11)
        {
            info.obj = lamp[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 12)
        {
            info.obj = loungeChairItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 13)
        {
            info.obj = musical_instrumentItems[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 14)
        {
            info.obj = office_chair[info.furnitureNumber];
            GameObject createObj = Instantiate(info.obj);
            if (createObj.gameObject.name == info.name)
            {
                createObj.gameObject.name = info.name + 1;
            }
            else
            {
                createObj.gameObject.name = info.name;
            }
            createObj.transform.position = info.position;
            createObj.transform.localScale = info.scale;
            createObj.transform.eulerAngles = info.angle;
            //objectInfoList.Add(info);
            //info.obj.GetComponent<Furniture>().located = true;
        }
        if (info.furnitureCategoryNumber == 15)
        {
            info.room = GameObject.Find("Wall_B");
            info.room.GetComponent<MeshRenderer>().material = mats[info.wallNumber];
            //objectInfoList.Add(info);
        }
        if (info.furnitureCategoryNumber == 16)
        {
            info.room = GameObject.Find("Floor.007");
            info.room.GetComponent<MeshRenderer>().material = floor[info.floorNumber];
            //objectInfoList.Add(info);
        }

    }
    #endregion

    #region Post
    public void OnSaveSignIn()
    {
        FurnitureInfo info = new FurnitureInfo();
        info.roomImage = File.ReadAllBytes(Application.dataPath + "/Resources/ZRoomImage/my0.png");
        //info.offlineRoomImage = File.ReadAllBytes(Application.dataPath + "/Resources/ZRoomImage/my0.png");
        info.furnitures = objectInfoList;
        //ArrayJson<ObjectInfo> arrayJson = new ArrayJson<ObjectInfo>();
        //arrayJson.furnitures = objectInfoList;
        //서버에 게시물 조회 요청(/posts/1 , Get)
        HttpRequester requester = new HttpRequester();
        /// POST, 완료되었을 때 호출되는 함수
        requester.url = "http://52.79.209.232:8080/api/v1/room";
        requester.requestType = RequestType.POST;
        requester.requestName = "OnSaveSignIn";
        //post data 셋팅
        requester.postData = JsonUtility.ToJson(info, true);
        requester.onComplete = OnCompleteSignIn;
        //HttpManager에게 요청
        HttpManager.instance.SendRequest(requester);
    }

    //public void OnDefaultSaveSignIn()
    //{
    //    FurnitureInfo info = new FurnitureInfo();
    //    info.roomImage = File.ReadAllBytes(Application.dataPath + "/Resources/ZRoomImage/my0.png");
    //    //info.offlineRoomImage = File.ReadAllBytes(Application.dataPath + "/Resources/ZRoomImage/my0.png");
    //    info.furnitures = objectInfoList;
    //    //ArrayJson<ObjectInfo> arrayJson = new ArrayJson<ObjectInfo>();
    //    //arrayJson.furnitures = objectInfoList;
    //    //서버에 게시물 조회 요청(/posts/1 , Get)
    //    HttpRequester requester = new HttpRequester();
    //    /// POST, 완료되었을 때 호출되는 함수
    //    requester.url = "http://52.79.209.232:8080/api/v1/room";
    //    requester.requestType = RequestType.POST;
    //    requester.requestName = "OnSaveSignIn";
    //    //post data 셋팅
    //    requester.postData = JsonUtility.ToJson(info, true);
    //    requester.onComplete = OnCompleteSignIn;
    //    //HttpManager에게 요청
    //    HttpManager.instance.SendRequest(requester);
    //}

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        print(handler);
        string s = "{\"furniture\":" + handler.text + "}";
        PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);

    }

    #endregion
    #region Json로컬저장
    public void OnSave()
    {
        objectInfo = new ObjectInfo();
        objectInfo.obj = obj;
        objectInfo.position = pos;
        objectInfo.scale = sca;
        objectInfo.angle = ang;
        string jsonData = JsonUtility.ToJson(objectInfo, true);
        // 저장경로
        string path = Application.dataPath + "/furniture.txt";
        // 파일로 저장
        File.WriteAllText(path, jsonData);
        print(jsonData);
    }

    public void OnSave2()
    {
        ArrayJson<ObjectInfo> arrayJson = new ArrayJson<ObjectInfo>();
        arrayJson.furnitures = objectInfoList;
        //objectInfoList.Add(objectInfo);

        //ArrayJson -> json
        string jsonData = JsonUtility.ToJson(arrayJson, true);
        print(jsonData);
        // 저장경로
        string path = Application.dataPath + "/Data";

        //만약에 경로가 없다면
        if (Directory.Exists(path) == false)
        {
            //폴더를 만든다.
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/furniture.txt", jsonData);
        //RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
        //Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        //RenderTexture.active = renderTexture;

        //Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

        ///*  // sprite = Sprite.Create(texture,)
        //  Texture2D roomSprite = Resources.Load<Texture2D>("Images/SampleImage");
        //  sprite = Sprite.Create(roomSprite, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));*/

        //texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        //texture.Apply();

        //File.WriteAllBytes($"{Application.dataPath + "/Resources/ZRoomImage"} /{screenShotName}.png", texture.EncodeToPNG());
        //EditorApplication.ExecuteMenuItem("Assets/Refresh");
    }
    #endregion 
    #region Json로컬로드
    public void OnLoad()
    {
        //저장된 정보 불러오고
        string path = Application.dataPath + "/furniture.txt";
        string jsonData = File.ReadAllText(path);
        //json -> objectInfo 에 셋팅
        objectInfo = JsonUtility.FromJson<ObjectInfo>(jsonData);
        //물체생성
        CreateObject(objectInfo);
    }

    public void OnLoadJson(string s)
    {
        //string jsonData = File.ReadAllText(sHandler);
        print(sHandler);
        ArrayJson<ObjectInfo> arrayJson = JsonUtility.FromJson<ArrayJson<ObjectInfo>>(sHandler);
        //arrayJson를 참고해서 오브젝트 생성
        for (int i = 0; i < arrayJson.furnitures.Count; i++)
        {
            CreateObject(arrayJson.furnitures[i]);
        }
    }
    public void OnLoad2()
    {
        //파일로 불러오기
        string path = Application.dataPath + "/Data";
        string jsonData = File.ReadAllText(path + "/furniture.txt");

        //불러온 파일(jsonData) -> ArrayJson<ObjectInfo>
        ArrayJson<ObjectInfo> arrayJson = JsonUtility.FromJson<ArrayJson<ObjectInfo>>(jsonData);
        //arrayJson를 참고해서 오브젝트 생성
        for (int i = 0; i < arrayJson.furnitures.Count; i++)
        {
            CreateObject(arrayJson.furnitures[i]);
        }
    }
    #endregion

    public void CreateMat(ObjectInfo info)
    {


    }
    #region Button
    public void OnClickButton(int index)
    {
        currButtonNum = index;
    }

    public void Button0()
    {
        currButtonNum = 0;
    }

    public void Button1()
    {
        currButtonNum = 1;
    }
    public void Button2()
    {
        currButtonNum = 2;
    }
    public void Button3()
    {
        currButtonNum = 3;
    }
    public void Button4()
    {
        currButtonNum = 4;
    }
    public void Button5()
    {
        currButtonNum = 5;
    }

    public void Button6()
    {
        currButtonNum = 6;
    }

    public void Button7()
    {
        currButtonNum = 7;
    }

    public void Button8()
    {
        currButtonNum = 8;
    }

    public void Button9()
    {
        currButtonNum = 9;
    }

    public void Button10()
    {
        currButtonNum = 10;
    }

    public void Button11()
    {
        currButtonNum = 11;
    }
    public void Button12()
    {
        currButtonNum = 12;
    }
    public void Button13()
    {
        currButtonNum = 13;
    }

    public void Button14()
    {
        currButtonNum = 14;
    }

    public void Button15()
    {
        currButtonNum = 15;
    }

    public void Button16()
    {
        currButtonNum = 16;
    }

    public void Button17()
    {
        currButtonNum = 17;
    }

    public void Button18()
    {
        currButtonNum = 18;
    }

    public void Button19()
    {
        currButtonNum = 19;
    }

    public void Button20()
    {
        currButtonNum = 20;
    }

    public void Button21()
    {
        currButtonNum = 21;
    }

    public void Button22()
    {
        currButtonNum = 22;
    }

    public void Button23()
    {
        currButtonNum = 23;
    }

    public void Button24()
    {
        currButtonNum = 24;
    }

    public void Button25()
    {
        currButtonNum = 25;
    }

    public void Button26()
    {
        currButtonNum = 26;
    }

    public void Button27()
    {
        currButtonNum = 27;
    }

    public void Button28()
    {
        currButtonNum = 28;
    }

    public void Button29()
    {
        currButtonNum = 29;
    }

    public void Button30()
    {
        currButtonNum = 30;
    }

    public void Button31()
    {
        currButtonNum = 31;
    }

    public void Button32()
    {
        currButtonNum = 32;
    }

    public void Button33()
    {
        currButtonNum = 33;
    }

    public void Button34()
    {
        currButtonNum = 34;
    }

    public void Button35()
    {
        currButtonNum = 35;
    }

    public void Button36()
    {
        currButtonNum = 36;
    }

    public void Button37()
    {
        currButtonNum = 37;
    }

    public void Button38()
    {
        currButtonNum = 38;
    }

    public void Button39()
    {
        currButtonNum = 39;
    }

    public void Button40()
    {
        currButtonNum = 40;
    }

    public void Button41()
    {
        currButtonNum = 41;
    }

    public void Button42()
    {
        currButtonNum = 42;
    }

    public void Button43()
    {
        currButtonNum = 43;
    }

    public void Button44()
    {
        currButtonNum = 44;
    }

    #endregion 
    public void mainScene()
    {
        SceneManager.LoadScene("MainScenes");

    }

    #region OnFurniture
    public void OnAddBed()
    {
        AddBed = true;
        AddChair = false;
        AddDesk = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        // GameObject bed = Instantiate(bedItems[currButtonNum]);
        //bed.transform.position = SpawnPos.transform.position;
    }

    public void OnAddChair()
    {

        AddChair = true;
        AddBed = false;

        AddDesk = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject chair = Instantiate(chairItems[currButtonNum]);
        //chair.transform.position = SpawnPos.transform.position;
    }
    public void OnAddDesk()
    {
        AddDesk = true;
        AddBed = false;
        AddChair = false;

        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject desk = Instantiate(DeskItem[currButtonNum]);
        //desk.transform.position = SpawnPos.transform.position;
    }
    public void OnAddWallHang()
    {
        AddWallHang = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;     
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddCloset()
    {
        AddCloset = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddCoffeeTable()
    {
        AddCoffeeTable = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddEntertainment()
    {
        AddEntertainment = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;

        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddElectronics()
    {
        AddEntertainment = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddFlower()
    {
        AddFlower = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;
        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;

        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;

        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    //public void OnAddKitchenChair()
    //{
    //    AddKitchenChair = true;
    //    AddDesk = false;
    //    AddBed = false;
    //    AddChair = false;

    //    AddCloset = false;
    //    AddCoffeeTable = false;
    //    AddEntertainment = false;
    //    AddElectrionic = false;
    //    AddFlower = false;

    //    AddKitchenTable = false;
    //    AddLamp = false;
    //    AddLoungeChair = false;
    //    AddInstrument = false;
    //    AddOfficeChair = false;
    //    AddMaterial = false;
    //    AddShelf = false;
    //    AddWallHang = false;
    //    //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
    //    //wallhang.transform.position = SpawnPos.transform.position;
    //}
    //public void OnAddKitchenTable()
    //{
    //    AddKitchenTable = true;
    //    AddDesk = false;
    //    AddBed = false;
    //    AddChair = false;

    //    AddCloset = false;
    //    AddCoffeeTable = false;
    //    AddEntertainment = false;
    //    AddElectrionic = false;
    //    AddFlower = false;
    //    AddKitchenChair = false;

    //    AddLamp = false;
    //    AddLoungeChair = false;
    //    AddInstrument = false;
    //    AddOfficeChair = false;
    //    AddMaterial = false;
    //    AddShelf = false;
    //    AddWallHang = false;
    //    //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
    //    //wallhang.transform.position = SpawnPos.transform.position;
    //}
    public void OnAddLamp()
    {
        AddLamp = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;

        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;

        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }
    public void OnAddLoungeChair()
    {
        AddLoungeChair = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;

        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;

        AddInstrument = false;
        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }


    public void OnAddInstrument()
    {
        AddInstrument = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;

        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;

        AddOfficeChair = false;
        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    public void OnAddOfficeChair()
    {
        AddOfficeChair = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;

        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;

        AddMaterial = false;
        //AddShelf = false;
        AddWallHang = false;
        //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
        //wallhang.transform.position = SpawnPos.transform.position;
    }

    //public void OnShelf()
    //{
    //    AddShelf = true;
    //    AddDesk = false;
    //    AddBed = false;
    //    AddChair = false;

    //    AddCloset = false;
    //    AddCoffeeTable = false;
    //    AddEntertainment = false;
    //    AddElectrionic = false;
    //    AddFlower = false;
    //    AddKitchenChair = false;
    //    AddKitchenTable = false;
    //    AddLamp = false;
    //    AddLoungeChair = false;
    //    AddInstrument = false;
    //    AddOfficeChair = false;
    //    AddMaterial = false;

    //    AddWallHang = false;
    //    //GameObject wallhang = Instantiate(WallHangItem[currButtonNum]);
    //    //wallhang.transform.position = SpawnPos.transform.position;
    //}

    public void OnMaterial()
    {
        AddMaterial = true;
        AddDesk = false;
        AddBed = false;
        AddChair = false;

        AddCloset = false;
        AddCoffeeTable = false;
        AddEntertainment = false;
        AddElectrionic = false;
        AddFlower = false;
        //AddKitchenChair = false;
        //AddKitchenTable = false;
        AddLamp = false;
        AddLoungeChair = false;
        AddInstrument = false;
        AddOfficeChair = false;
        //AddShelf = false;
        AddWallHang = false;
    }

    public void OnFloor()
    {
        AddFloor = true;
    }
    #endregion
    public void OnRotate()
    {
        GameManager.instance.selected.transform.Rotate(0, -90, 0);
    }

    public void OnDestroyObject()
    {
        
    }
}
