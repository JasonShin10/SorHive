using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_LobbyCameMove : MonoBehaviour
{

    //����� ���� �հ��� ��ġ�� ���� �� ����,�ƿ�
    float m_fOldToucDis = 0f;       // ��ġ ���� �Ÿ��� �����մϴ�.
    public float m_fStartScale = 1f;     // ��޴����� �⺻ �����ϰ��� 1���� ���մϴ�.

    //PC���� ���콺 ��ũ�Ѹ� ����,�ƿ�
    //���콺 ��ũ�� ���ǵ�
    public float scrollSpeed = 3000f;

    //public Camera miniCame;

    public GameObject RoomManager;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CheckTouch();

        if (m_fStartScale >= 0.6f && m_fStartScale <= 2.5f)
        {
            float s = Input.GetAxis("Mouse ScrollWheel");

            m_fStartScale += s * scrollSpeed * Time.deltaTime;
        }
        else
        {
            if(m_fStartScale < 0.6f)
            {
                m_fStartScale = 0.6f;
            }
            if(m_fStartScale > 2.5f)
            {
                m_fStartScale = 2.5f;
            }
        }
        RoomManager.gameObject.transform.localScale = new Vector3(m_fStartScale, m_fStartScale, m_fStartScale);
    }

    private void CheckTouch()
    {
        int nTouch = Input.touchCount;
        float m_fToucDis = 0f;
        float fDis = 0f;
        // ��ġ�� �ΰ��̰�, �� ��ġ�� �ϳ��� �̵��Ѵٸ� ī�޶��� fieldOfView�� �����մϴ�.
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;

            // ���� �� ��ġ�� �Ÿ��� ���� �� ��ġ�� �Ÿ��� ���̸� FleldOfView�� �����մϴ�.
            m_fStartScale -= fDis;

            // �ִ�� 60�ּҴ� 15���� ���̻� ���� Ȥ�� ���Ұ� ���� �ʵ��� �մϴ�.
            m_fStartScale = Mathf.Clamp(m_fStartScale, 0.6f, 2.5f);

            // Ȯ�� / ��Ұ� ���ڱ� �����ʵ��� �����մϴ�.
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, m_fStartScale, Time.deltaTime * 5);

            m_fOldToucDis = m_fToucDis;
        }

    }
}
