using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FurnitureItem")]
public class FurnitureItem : ScriptableObject
{
    //가구 아이템 이름
    public string itemName;
    // 가구 아이템 이미지
    public Sprite itemImage;
    // 가구 아이템 모델(프리팹)
    public GameObject itemPrefab;
    //가구 유형
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
