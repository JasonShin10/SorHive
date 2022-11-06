using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LHY_PlayerMove : MonoBehaviour
{
    public float speed = 5;

    public Animator anim;

    public CharacterController cc;

    
    #region jump
    //�÷��̾ ���� ��ư�� �������� Ȯ���ϴ� ������ �����Ѵ�.
    public bool isjump = false;

    public int Jcount = 0;

    //�����ϴ� ���� ����Ǵ� ���� ������ �����Ѵ�.
    public float JumpPow = 5.0f;

    //ĳ���� ��Ʈ�ѷ��� ����Ǵ� �߷��� ũ�⸦ ������ �����Ѵ�.
    public float Gravity = 10.0f;
    //�����ϸ� ����� y��
    float yVelocity = 0;
    #endregion


    private void Awake()
    {
        //ĳ���� ��Ʈ�ѷ� ������ ���� ������Ʈ�� CharacterController������Ʈ�� �ҷ��� ��´�.
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
        //�ٴڿ� 10 cm ������ �ִٸ� ������ �Ǵ�����
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        int layer = 1 << gameObject.layer;
        if (Physics.Raycast(ray, out hitInfo, 1.2f, ~layer) == false)
        {
            //anim.SetBool("IsInAir", true);
        }

        if (cc == null) return;
        //ĳ���� �ݶ��̴��� �ٴۿ� ����ִٸ�
        if (cc.isGrounded)
        {
            //�����ϰ� ���� �ʴ�.
            isjump = false;
            //anim.SetBool("IsInAir", false);
            //yVelocity�� ���� 0���� �������ش�.
            yVelocity = 0;

            //�ٴڿ� ���������Ƿ� ���� ī��Ʈ�� 0���� ���� ���ش�.
            Jcount = 0;
        }
        //isjump = false;
        //�÷��̾ �����ϰ� ���� �ʰ�, ����ڰ� ����Ű�� �����ٸ�
        if (isjump == false && Input.GetButtonDown("Jump"))
        {
            //anim.Play("JStart");
            //��������ߴ��� ������ 1�� �߰����ش�.
            Jcount++;
            //���� ���� Ƚ���� 2�̻��̶��
            if (Jcount >= 2)
            {
                //�����ϰ� �ִ�.
                isjump = true;
                //���� Ƚ���� �������ش�.
                Jcount = 0;
            }
            //yVelocity�� �����Ŀ��� ��������ش�.
            yVelocity = JumpPow;
        }
        //�ð��� �帧�� ���� Gravity�� ���� yVelocity���� ���ش�. 
        yVelocity -= Gravity * Time.deltaTime;
    }

  
}
