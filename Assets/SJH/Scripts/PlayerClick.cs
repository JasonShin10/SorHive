using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    // Start is called before the first frame update
    Ray ray;
    RaycastHit hit;
    Transform selectObj;
    public Canvas Inventory;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //int layer = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Inventory.gameObject.SetActive(true);
                }
                else
                {
                    Inventory.gameObject.SetActive(false);
                }
            }

        }
    }
}
