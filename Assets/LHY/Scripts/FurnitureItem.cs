using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FurnitureItem")]
public class FurnitureItem : ScriptableObject
{
    //���� ������ �̸�
    public string itemName;
    // ���� ������ �̹���
    public Sprite itemImage;
    // ���� ������ ��(������)
    public GameObject itemPrefab;
    //���� ����
    public FunitureType funitureType;

    public enum FunitureType
    {
        OnFloor,
        OnWall,
        OnDesk,
        ETC
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
