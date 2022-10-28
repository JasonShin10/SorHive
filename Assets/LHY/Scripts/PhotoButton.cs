using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoButton : MonoBehaviour
{
    public static PhotoButton instence = new PhotoButton();

    public int path;

    private void Awake()
    {
        instence = this;
    }

    public void OnClick01()
    {
        path = 1;
    }
    public void OnClick02()
    {
        path = 2;
    }
    public void OnClick03()
    {
        path = 3;
    }
    public void OnClick04()
    {
        path = 4;
    }
    public void OnClick05()
    {
        path = 5;
    }
    public void OnClick06()
    {
        path = 6;
    }
}
