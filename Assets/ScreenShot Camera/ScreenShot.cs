using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CustomUtils.ScreenShot))]
public class ScreenShotEditor : Editor 
{
    CustomUtils.ScreenShot screenShot;
	void OnEnable() => screenShot = target as CustomUtils.ScreenShot;

	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();
        if (GUILayout.Button("ScreenShot"))
        {
            screenShot.ScreenShotClick();
            EditorApplication.ExecuteMenuItem("Assets/Refresh");
        } 
	}
}
#endif

namespace CustomUtils
{
    public class ScreenShot : MonoBehaviour
    {
        [SerializeField] string screenShotName;

        //Sprite sprite;

        public Camera sCame;

        public LifeingItem lifeingItem;

   
        private void Start()
        {
           
        }

        private void Update()
        {
           
        }

        public void ScreenShotClick()
        {
            RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;

            Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

            /*  // sprite = Sprite.Create(texture,)
              Texture2D roomSprite = Resources.Load<Texture2D>("Images/SampleImage");
              sprite = Sprite.Create(roomSprite, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));*/
            string savePath = Application.dataPath + "/Resources/ZRoomImage";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            File.WriteAllBytes($"{savePath} /{screenShotName}.png", texture.EncodeToPNG());

        }

        public void ScreenShotClick1()
        {
            RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;

            Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

            /*  // sprite = Sprite.Create(texture,)
              Texture2D roomSprite = Resources.Load<Texture2D>("Images/SampleImage");
              sprite = Sprite.Create(roomSprite, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));*/

            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            string savePath = Application.persistentDataPath + "/Resources/Avatarimg";
            if(!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                print(savePath);
            }

            File.WriteAllBytes($"{savePath}/{screenShotName}.png", texture.EncodeToPNG());
            //EditorApplication.ExecuteMenuItem("Assets/Refresh");
        }

        //public void ScreenShotClick2()
        //{
        //    RenderTexture renderTexture = GetComponent<Camera>().targetTexture;
        //    Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        //    RenderTexture.active = renderTexture;

        //    Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));

        //    /*  // sprite = Sprite.Create(texture,)
        //      Texture2D roomSprite = Resources.Load<Texture2D>("Images/SampleImage");
        //      sprite = Sprite.Create(roomSprite, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));*/

        //    texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        //    texture.Apply();

        //    string savePath = Application.persistentDataPath + "/Resources/RoomImages/";
        //    if (!Directory.Exists(savePath))
        //    {
        //        Directory.CreateDirectory(savePath);
        //    }

        //    File.WriteAllBytes(Application.persistentDataPath + "/Resources/RoomImages/" + lifeingItem.lifingCategoryNo + "_" + lifeingItem.lifingNo + ".png", texture.EncodeToPNG());

        //    print(lifeingItem.lifingCategoryNo + "/" + lifeingItem.lifingNo);
        //    //EditorApplication.ExecuteMenuItem("Assets/Refresh");
        //}


        //이미지를 스프라이트로 저장하자

    }
}