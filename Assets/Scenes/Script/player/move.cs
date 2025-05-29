using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class move : MonoBehaviour
{
    public float forceMultiplier;//施加力的系数
    public float max_speed;//最大速度，-max_speed为最小速度
    Rigidbody2D rb2d;
    public Animator animator;
    public int faceDire = 1;//1表示朝右面，0表示朝左面
    public bool is_crouch=false;//当前是否在蹲下
    BoxCollider2D boxcollider2d;
    float collider;
    void Start()
    {
        rb2d= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxcollider2d = GetComponent<BoxCollider2D>();
        collider = boxcollider2d.offset.x;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");//获取水平的增值

        if(Input.GetButton("Horizontal")||!GetComponent<jump>().is_ground)//在移动或跳跃中阻力较小，其他时候阻力较大减小惯性
        {
            rb2d.drag = 3;
        }
        else
        {
            rb2d.drag = 10;
        }

        if (horizontalInput != 0f) //当有输入内容时
        {
            is_crouch = GetComponent<crouch>().is_crouching;
            if (!is_crouch)
                moving(horizontalInput);//如果有输入内容就开始移动
        }
        else//当没有move时，就变回没有移动的动画
        {
            animator.SetBool("is_move", false);
        }
    }

    void moving(float horizontalInput)
    {
        animator.SetBool("is_move", true);
        if (horizontalInput < 0f)//如果小于零表示向左移动
        {
            boxcollider2d.offset = new (-collider, boxcollider2d.offset.y);
            GetComponent<SpriteRenderer>().flipX = true;//令x翻转，变为左面
            faceDire = 0;
        }
        else//否则向右移动
        {
            boxcollider2d.offset = new(collider, boxcollider2d.offset.y);
            GetComponent<SpriteRenderer>().flipX = false;//令x不翻转，默认右面
            faceDire = 1;
        }
        

        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0).normalized;//转换为向量
        rb2d.AddForce(moveDirection * max_speed, ForceMode2D.Impulse);

        rb2d.AddForce(moveDirection * forceMultiplier, ForceMode2D.Force);//持续施加力

        Vector3 speed = rb2d.velocity;//使得速度在最小速度与最大速度之间
        speed.x = Mathf.Clamp(speed.x, -max_speed, max_speed);
        rb2d.velocity = speed;
    }
}
