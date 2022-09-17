using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //不知道[SerializeField]这个东西有啥用，以后可能会用到，可能跟private的变量数据存储有关
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
    /// 水平移动
    /// </summary>
    private void Movement()
    {
        float xmoveDirection;//移动方向
        float faceDirection;//面朝方向

        xmoveDirection = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");

        //水平面朝方向
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }

        //水平移动方向
        if (xmoveDirection != 0)
        {
            rb.velocity = new Vector2(xmoveDirection * xspeed * Time.fixedDeltaTime, rb.velocity.y);
            //播放移动动画
            anim.SetFloat("running", Mathf.Abs(faceDirection));
        }
        
    }


    /// <summary>
    /// 跳跃
    /// </summary>
    private void Jumpment()
    {
        if (Input.GetButtonDown("Jump") && isAbleJump())
        {
            jumpTimes--;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.fixedDeltaTime);//此处必须为fixed否则每帧间隔不一样，跳跃高度就不同
            anim.SetBool("jumping",true);
            anim.SetBool("falling", false);
        }

    }

    /// <summary>
    /// 跳跃剩余次数是否足够
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


    void SwitchAnim()//跳跃动画过渡函数
    {
        anim.SetBool("idling", false);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)//播放坠落动画
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(coll.IsTouchingLayers(ground))//接触地面后回到idle动画状态
        {   //如果你的player有两个碰撞体的话，一定要看清楚添加正确的碰撞体！
            anim.SetBool("falling", false);
            anim.SetBool("idling", true);
            jumpTimes = 2;//接触地面后能跳跃次数回到2
        }
    }
}