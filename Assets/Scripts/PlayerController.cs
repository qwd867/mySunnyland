using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //��֪��[SerializeField]���������ɶ�ã��Ժ���ܻ��õ������ܸ�private�ı������ݴ洢�й�
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D coll;
    public LayerMask ground;

    public float xspeed;
    public float jumpSpeed;

    public int jumpTimes = 2;

    public bool isJumping = false;
    public bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        xspeed = 400;
    }

    // Update is called once per frame
    void Update()
    {
        Jumpment();
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        //isOnGround = Physics2D.OverlapCircle(isOnGround);
        Movement();
    }


    /// <summary>
    /// ˮƽ�ƶ�
    /// </summary>
    private void Movement()
    {
        float xmoveDirection;//�ƶ�����
        float faceDirection;//�泯����

        xmoveDirection = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");

        //ˮƽ�泯����
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }

        //ˮƽ�ƶ�����
        if (xmoveDirection != 0)
        {
            rb.velocity = new Vector2(xmoveDirection * xspeed * Time.fixedDeltaTime, rb.velocity.y);
            //�����ƶ�����
            anim.SetFloat("running", Mathf.Abs(faceDirection));
        }
        
    }


    /// <summary>
    /// ��Ծ
    /// </summary>
    private void Jumpment()
    {
        if (Input.GetButtonDown("Jump") && isAbleJump())
        {
            jumpTimes--;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.fixedDeltaTime);//�˴�����Ϊfixed����ÿ֡�����һ������Ծ�߶ȾͲ�ͬ
            anim.SetBool("jumping",true);
            anim.SetBool("falling", false);
        }

    }

    /// <summary>
    /// ��Ծʣ������Ƿ��㹻
    /// </summary>
    /// <returns></returns>
    bool isAbleJump()
    {
        if (jumpTimes > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void SwitchAnim()//��Ծ�������ɺ���
    {
        anim.SetBool("idling", false);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)//����׹�䶯��
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(coll.IsTouchingLayers(ground))//�Ӵ������ص�idle����״̬
        {   //������player��������ײ��Ļ���һ��Ҫ����������ȷ����ײ�壡
            anim.SetBool("falling", false);
            anim.SetBool("idling", true);
            jumpTimes = 2;//�Ӵ����������Ծ�����ص�2
        }
    }
}