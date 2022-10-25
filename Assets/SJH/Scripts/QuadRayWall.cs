using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadRayWall : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(transform.position, -transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            // hit에 담긴 가구 오브젝트의 태그가 가구이고 hit에 담긴 오브젝트가 배치전 상태일때 겹치는 부분이 있다면 가구의 배치가능을 flase로 바꾼다.
            // 만약 겹치는 부분이 없다면 가구의 배치가능 상태를 true로 바꾼다.
            //if (hit.transform.gameObject.CompareTag("Furniture") && hit.transform.gameObject.GetComponent<Furniture>().located == false)
            //{
            print(hit.transform.name);
            //}
            if (hit.transform.gameObject.CompareTag("Wall"))
            {
                // 가구가 배치 안되었을때
                if (hit.transform.gameObject.GetComponent<Furniture>().located == false)
                {
                    
                    if (hit.transform.gameObject.GetComponent<Furniture>().canLocated == false)
                    {
                        print(1111);
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(false);
                    }
                    //타일 상태 배치안됨
                    

                }
                // 가구가 배치되었을때
                else
                {
                    //타일 상태 배치됨
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);

                }
            }
            //print(hit.transform.name);
            
            
        }
        else
        {

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }


    }
}
