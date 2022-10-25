using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_LobbyCameMove : MonoBehaviour
{

    //모바일 버전 손가락 터치를 감지 해 줌인,아웃
    float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
    public float m_fStartScale = 1f;     // 룸메니져의 기본 스케일값을 1으로 정합니다.

    //PC버전 마우스 스크롤링 줌인,아웃
    //마우스 스크롤 스피드
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
        // 터치가 두개이고, 두 터치중 하나라도 이동한다면 카메라의 fieldOfView를 조정합니다.
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

            fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;

            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
            m_fStartScale -= fDis;

            // 최대는 60최소는 15으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
            m_fStartScale = Mathf.Clamp(m_fStartScale, 0.6f, 2.5f);

            // 확대 / 축소가 갑자기 되지않도록 보간합니다.
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, m_fStartScale, Time.deltaTime * 5);

            m_fOldToucDis = m_fToucDis;
        }

    }
}
