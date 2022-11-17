using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class LHY_PlayerMove : MonoBehaviourPun, IPunObservable
{
    public float speed = 5;

    public Animator anim;

    public CharacterController cc;

    public bool buttonClicked = false;

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

    public PhotonView pv;

    //���� ��ġ
    Vector3 receivePos;
    //ȸ���Ǿ� �ϴ� ��
    Quaternion receiveRot;

    public float lerpSpeed = 100;



    private void Awake()
    {
        //ĳ���� ��Ʈ�ѷ� ������ ���� ������Ʈ�� CharacterController������Ʈ�� �ҷ��� ��´�.
        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == true)
        {
            MoveJump();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }
     

      
        //P_jump();

        //MoveUpdate();




    }

    public void MoveJump()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //float v = inputDirection.y;
        //float h = inputDirection.x;

        float w = -h + -v;
        float hi = -v + h;

        Vector3 dir = new Vector3(w, 0, hi);
        //Vector2 dir = inputDirection;
        dir.Normalize();

        P_jump();

         dir.y = yVelocity;
        cc.Move(dir * speed * Time.deltaTime);

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

    public void P_jump()
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
        //if (isjump == false && Input.GetButtonDown("Jump"))
            if (isjump == false && buttonClicked == true)
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
        buttonClicked = false;
    }

    public void OnClickJump()
    {
        buttonClicked = true;
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //������ ������
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }

        //������ �ޱ�
        else if(stream.IsReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
