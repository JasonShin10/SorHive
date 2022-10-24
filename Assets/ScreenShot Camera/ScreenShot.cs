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

    
        public void ScreenShotClick()
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

            File.WriteAllBytes($"{Application.dataPath}/{screenShotName}.png", texture.EncodeToPNG());
        }

        private void Start()
        {
            
        }

        //이미지를 스프라이트로 저장하자

    }
}