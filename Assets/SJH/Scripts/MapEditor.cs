using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//������ Ŀ���� �Ұ��̳�?
[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    Map map;
    //Hierarchy Ŭ���� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    private void OnEnable()
    {
        map = (Map)target;
    }

    //Inspector�� �׸��� �Լ�

    public override void OnInspectorGUI()
    {
       map.tileX = EditorGUILayout.IntField("Ÿ�� ����", map.tileX);
       map.tileZ = EditorGUILayout.IntField("Ÿ�� ����", map.tileZ);

        //�ּ��ִ밪�� ������
        map.tileX = Mathf.Clamp(map.tileX, 1, 500);
        map.tileZ = Mathf.Clamp(map.tileZ, 1, 500);

        //�ٴ� Prefab Field
       map.floor = (GameObject)EditorGUILayout.ObjectField("�ٴ�", map.floor, typeof(GameObject), false);
    }

    //Scene �� �׸��� �Լ�

    private void OnSceneGUI()
    {
        DrawGrid();
         
    }

    void DrawGrid()
    {
        Vector3 start;
        Vector3 end;
        //���� �� �׸���
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
