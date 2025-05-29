using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{

    Rigidbody2D rb;
    public float jumpMulpiter;//跳跃力度系数
    public float fallGravity = 2f;//重力系数
    public Animator animator;

    public float checkRadius;
    public Vector3 checkOff;
    public LayerMask groundyer;

    public bool is_ground=true;
    float eps = 5e-3f;//用于进行浮点比较的小数

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&is_ground)//如果在地面并且按下空格就跳跃
        {
            rb.AddForce(Vector3.up*jumpMulpiter,ForceMode2D.Impulse);//添加一次性的力
        }

        Vector3 checkPos = (Vector3)transform.position + checkOff;//定位检测的位置
        is_ground =Physics2D.OverlapCircle(checkPos, checkRadius, groundyer);//检测脚下是不是地面

        if(rb.velocity.y-eps<0)//如果速度为负数，表示在下降，增加重力
        {
            animator.SetBool("is_jump_up", false);
            animator.SetBool("is_jump_fall", true);
            //施加重力
            rb.velocity += Vector2.up * Physics.gravity.y * (fallGravity - 1) * Time.deltaTime;
        }else if(rb.velocity.y-eps>0)
        {
            animator.SetBool("is_jump_fall", false);
            animator.SetBool("is_jump_up", true);
        }

        if(is_ground)
        {
            animator.SetBool("is_jump_fall", false);
            animator.SetBool("is_jump_up", false);
        }



    }
    void OnDrawGizmos()//画检测有没有在地面的那个点
    {
        if (is_ground)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Vector3 checkPos = transform.position + (Vector3)checkOff;
        Gizmos.DrawWireSphere(checkPos, checkRadius);
    }
}
