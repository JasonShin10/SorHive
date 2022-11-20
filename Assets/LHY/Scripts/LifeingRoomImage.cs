using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LifeingRoomImage : MonoBehaviour
{

    public RawImage lifeingRoomImage;

    public RawImage lifeingScreenShot;

    public LifeingItem lifeingItem;


    int a;

    int b;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a = lifeingItem.lifingCategoryNo;
        b = lifeingItem.lifingNo;
    }

    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture2D;
    }

    public void OnClickSaveRoomImg()
    {
        //Texture2D texture = (Texture2D)lifeingRoomImage.texture;


        //Texture2D texture = lifeingRoomImage.texture as Texture2D;
        //Texture2D texture = TextureToTexture2D(lifeingRoomImage);

        /*        Texture2D texture = Resources.Load("02.Story / StoryRoom /" + a + "_" + b + ".png", typeof(Texture2D)) as Texture2D;

                string savePath = Application.persistentDataPath + "/Resources/RoomImages/";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                File.WriteAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + a +"_"+ b + ".png", texture.EncodeToPNG());*/


        lifeingScreenShot.texture = lifeingRoomImage.texture;

        //EditorApplication.ExecuteMenuItem("Assets/Refresh");
    }
}
