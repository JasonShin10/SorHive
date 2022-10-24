using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_LobbyCameMove : MonoBehaviour
{

    //����� ���� �հ��� ��ġ�� ���� �� ����,�ƿ�
    float m_fOldToucDis = 0f;       // ��ġ ���� �Ÿ��� �����մϴ�.
    float m_fFieldOfView = 38f;     // ī�޶��� FieldOfView�� �⺻���� 60���� ���մϴ�.

    //PC���� ���콺 ��ũ�Ѹ� ����,�ƿ�
    //���콺 ��ũ�� ���ǵ�
    public float scrollSpeed = 2000.0f;

    public Camera miniCame;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();

        if (miniCame.fieldOfView >= 15f && miniCame.fieldOfView <= 60f)
        {
            float s = Input.GetAxis("Mouse ScrollWheel");

            miniCame.fieldOfView += -s * scrollSpeed * Time.deltaTime;
        }
        else
        {
            if(miniCame.fieldOfView < 15)
            {
                miniCame.fieldOfView = 15;
            }
            if(miniCame.fieldOfView > 60)
            {
                miniCame.fieldOfView = 60;
            }
        }
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
            m_fFieldOfView -= fDis;

            // �ִ�� 60�ּҴ� 15���� ���̻� ���� Ȥ�� ���Ұ� ���� �ʵ��� �մϴ�.
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 15.0f, 60.0f);

            // Ȯ�� / ��Ұ� ���ڱ� �����ʵ��� �����մϴ�.
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, m_fFieldOfView, Time.deltaTime * 5);

            m_fOldToucDis = m_fToucDis;
        }

    }
}
