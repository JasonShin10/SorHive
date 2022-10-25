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

            // hit�� ��� ���� ������Ʈ�� �±װ� �����̰� hit�� ��� ������Ʈ�� ��ġ�� �����϶� ��ġ�� �κ��� �ִٸ� ������ ��ġ������ flase�� �ٲ۴�.
            // ���� ��ġ�� �κ��� ���ٸ� ������ ��ġ���� ���¸� true�� �ٲ۴�.
            //if (hit.transform.gameObject.CompareTag("Furniture") && hit.transform.gameObject.GetComponent<Furniture>().located == false)
            //{
            print(hit.transform.name);
            //}
            if (hit.transform.gameObject.CompareTag("Wall"))
            {
                // ������ ��ġ �ȵǾ�����
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
                    //Ÿ�� ���� ��ġ�ȵ�
                    

                }
                // ������ ��ġ�Ǿ�����
                else
                {
                    //Ÿ�� ���� ��ġ��
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
