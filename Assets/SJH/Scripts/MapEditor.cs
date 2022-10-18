using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//누구를 커스텀 할것이냐?
[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    Map map;
    //Hierarchy 클릭이 되었을 때 호출 되는 함수
    private void OnEnable()
    {
        map = (Map)target;
    }

    //Inspector를 그리는 함수

    public override void OnInspectorGUI()
    {
       map.tileX = EditorGUILayout.IntField("타일 가로", map.tileX);
       map.tileZ = EditorGUILayout.IntField("타일 세로", map.tileZ);

        //최소최대값을 정하자
        map.tileX = Mathf.Clamp(map.tileX, 1, 500);
        map.tileZ = Mathf.Clamp(map.tileZ, 1, 500);

        //바닥 Prefab Field
       map.floor = (GameObject)EditorGUILayout.ObjectField("바닥", map.floor, typeof(GameObject), false);
    }

    //Scene 을 그리는 함수

    private void OnSceneGUI()
    {
        DrawGrid();
         
    }

    void DrawGrid()
    {
        Vector3 start;
        Vector3 end;
        //세로 줄 그리자
        Handles.color = Color.red;
        Handles.DrawLine(Vector3.zero, Vector3.one * 5);
        for (int i = 0; i <= map.tileX; i++)
        {
            start = new Vector3(i, 0, 0);
            end = new Vector3(i, 0, map.tileZ);
            Handles.DrawLine(start, end);
        }
        Handles.color = Color.blue;
        for (int i = 0; i <= map.tileZ; i++)
        {
            start = new Vector3(0, 0, i);
            end = new Vector3(map.tileX, 0, i);
            Handles.DrawLine(start, end);
        }
        if(floor == null)
        {
            CreateFloor();
        }
    }

    GameObject floor;
    void CreateFloor()
    {
       GameObject floor = (GameObject)PrefabUtility.InstantiatePrefab(map.floor);  

    }
}
