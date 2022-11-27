using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LifeingRoomImage : MonoBehaviour
{

  
    public GameObject[] RoomImages;




    int a;

    int b;


    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < RoomImages.Length; i++)
        {
            RoomImages[i].name.ToString();
            print(RoomImages[i].name);
        }*/
        //ScreenShotClick2();
        StartCoroutine(RoomImage());
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void ScreenShotClick2()
    {
        /*for(int i = 0; i < RoomImages.Length; i++)
        {
            RoomImages[i].SetActive(true);
                
            RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;

            Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

            *//*  // sprite = Sprite.Create(texture,)
              Texture2D roomSprite = Resources.Load<Texture2D>("Images/SampleImage");
              sprite = Sprite.Create(roomSprite, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));*//*

            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            string savePath = Application.persistentDataPath + "/Resources/RoomImages/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            File.WriteAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + RoomImages[i].name + ".png", texture.EncodeToPNG());

            print(RoomImages[i].name);
        }*/


    }

    IEnumerator RoomImage()
    {
        for (int i = 0; i < RoomImages.Length; i++)
        {
            RoomImages[i].SetActive(true);

            yield return new WaitForSecondsRealtime(0.5f);

            RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;

            Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

            /*  // sprite = Sprite.Create(texture,)
              Texture2D roomSprite = Resources.Load<Texture2D>("Images/SampleImage");
              sprite = Sprite.Create(roomSprite, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));*/

            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            
            string savePath = Application.persistentDataPath + "/Resources/RoomImages/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                File.WriteAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + RoomImages[i].name + ".png", texture.EncodeToPNG());
            }
            else
            {
                File.WriteAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + RoomImages[i].name + ".png", texture.EncodeToPNG());
            }

          

            print(RoomImages[i].name);
        }


    }
}
