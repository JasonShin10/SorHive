using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    //public static ItemManager instance;
    //private void Awake()
    //{
    //    if (!instance)
    //    {
    //        instance = this;
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool AddItem = false;
    public bool AddItem1 = false;
    public bool AddItem2 = false;
    public bool AddItem3 = false;
    public bool AddItem4 = false;
    
    public int currButtonNum = 0;
    // Update is called once per frame
    void Update()
    {
        
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


    public void OnAddItem()
    {
        AddItem = true;
    }

    public void OnAddItem1()
    {
        AddItem1 = true;
    }
    public void OnAddItem2()
    {
        AddItem2 = true;
    }
    public void OnAddItem3()
    {
        AddItem3 = true;
    }
    public void OnAddItem4()
    {
        AddItem4 = true;
    }
}
