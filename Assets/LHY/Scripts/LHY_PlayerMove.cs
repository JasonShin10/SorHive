using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LHY_PlayerMove : MonoBehaviour
{
    public float speed = 5;

    public Animator anim;

    public CharacterController cc;

    
    #region jump
    //플레이어가 점프 버튼을 눌렀는지 확인하는 변수를 선언한다.
    public bool isjump = false;

    public int Jcount = 0;

    //점프하는 순간 적용되는 힘을 변수로 선언한다.
    public float JumpPow = 5.0f;

    //캐릭터 컨트롤러에 적용되는 중력의 크기를 변수로 선언한다.
    public float Gravity = 10.0f;
    //점프하면 적용될 y값
    float yVelocity = 0;
    #endregion


    private void Awake()
    {
        //캐릭터 컨트롤러 변수에 현재 오브젝트의 CharacterController컴포넌트를 불러와 담는다.
        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //MoveUpdate();
    }

    public void MoveUpdate(Vector2 inputDirection)
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        float v = inputDirection.y;
        float h = inputDirection.x;


        float w = -h + -v;
        float hi = -v + h;

        



        Vector3 dir = new Vector3(w, 0, hi);
        //Vector2 dir = inputDirection;
        dir.Normalize();

        P_jump();

        dir.y = yVelocity;
        cc.Move(dir * speed * Time.deltaTime);

    }

    private void P_jump()
    {
        //바닥에 10 cm 떨어져 있다면 점프로 판단하자
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        int layer = 1 << gameObject.layer;
        if (Physics.Raycast(ray, out hitInfo, 1.2f, ~layer) == false)
        {
            //anim.SetBool("IsInAir", true);
        }

        if (cc == null) return;
        //캐릭터 콜라이더가 바닦에 닿아있다면
        if (cc.isGrounded)
        {
            //점프하고 있지 않다.
            isjump = false;
            //anim.SetBool("IsInAir", false);
            //yVelocity의 값을 0으로 리셋해준다.
            yVelocity = 0;

            //바닥에 착지했으므로 점프 카운트를 0으로 리셋 해준다.
            Jcount = 0;
        }
        //isjump = false;
        //플레이어가 점프하고 있지 않고, 사용자가 점프키를 눌렀다면
        if (isjump == false && Input.GetButtonDown("Jump"))
        {
            //anim.Play("JStart");
            //몇번점프했는지 점수를 1점 추가해준다.
            Jcount++;
            //만약 점프 횟수가 2이상이라면
            if (Jcount >= 2)
            {
                //점프하고 있다.
                isjump = true;
                //점프 횟수를 리셋해준다.
                Jcount = 0;
            }
            //yVelocity에 점프파워를 적용시켜준다.
            yVelocity = JumpPow;
        }
        //시간이 흐름에 따라 Gravity의 값을 yVelocity에서 빼준다. 
        yVelocity -= Gravity * Time.deltaTime;
    }

  
}
