using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{

    Rigidbody2D rb;
    public float jumpMulpiter;//��Ծ����ϵ��
    public float fallGravity = 2f;//����ϵ��
    public Animator animator;

    public float checkRadius;
    public Vector3 checkOff;
    public LayerMask groundyer;

    public bool is_ground=true;
    float eps = 5e-3f;//���ڽ��и���Ƚϵ�С��

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&is_ground)//����ڵ��沢�Ұ��¿ո����Ծ
        {
            rb.AddForce(Vector3.up*jumpMulpiter,ForceMode2D.Impulse);//���һ���Ե���
        }

        Vector3 checkPos = (Vector3)transform.position + checkOff;//��λ����λ��
        is_ground =Physics2D.OverlapCircle(checkPos, checkRadius, groundyer);//�������ǲ��ǵ���

        if(rb.velocity.y-eps<0)//����ٶ�Ϊ��������ʾ���½�����������
        {
            animator.SetBool("is_jump_up", false);
            animator.SetBool("is_jump_fall", true);
            //ʩ������
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
    void OnDrawGizmos()//�������û���ڵ�����Ǹ���
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
