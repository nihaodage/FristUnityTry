using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class move : MonoBehaviour
{
    public float forceMultiplier;//ʩ������ϵ��
    public float max_speed;//����ٶȣ�-max_speedΪ��С�ٶ�
    Rigidbody2D rb2d;
    public Animator animator;
    public int faceDire = 1;//1��ʾ�����棬0��ʾ������
    public bool is_crouch=false;//��ǰ�Ƿ��ڶ���
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
        float horizontalInput = Input.GetAxis("Horizontal");//��ȡˮƽ����ֵ

        if(Input.GetButton("Horizontal")||!GetComponent<jump>().is_ground)//���ƶ�����Ծ��������С������ʱ�������ϴ��С����
        {
            rb2d.drag = 3;
        }
        else
        {
            rb2d.drag = 10;
        }

        if (horizontalInput != 0f) //������������ʱ
        {
            is_crouch = GetComponent<crouch>().is_crouching;
            if (!is_crouch)
                moving(horizontalInput);//������������ݾͿ�ʼ�ƶ�
        }
        else//��û��moveʱ���ͱ��û���ƶ��Ķ���
        {
            animator.SetBool("is_move", false);
        }
    }

    void moving(float horizontalInput)
    {
        animator.SetBool("is_move", true);
        if (horizontalInput < 0f)//���С�����ʾ�����ƶ�
        {
            boxcollider2d.offset = new (-collider, boxcollider2d.offset.y);
            GetComponent<SpriteRenderer>().flipX = true;//��x��ת����Ϊ����
            faceDire = 0;
        }
        else//���������ƶ�
        {
            boxcollider2d.offset = new(collider, boxcollider2d.offset.y);
            GetComponent<SpriteRenderer>().flipX = false;//��x����ת��Ĭ������
            faceDire = 1;
        }
        

        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0).normalized;//ת��Ϊ����
        rb2d.AddForce(moveDirection * max_speed, ForceMode2D.Impulse);

        rb2d.AddForce(moveDirection * forceMultiplier, ForceMode2D.Force);//����ʩ����

        Vector3 speed = rb2d.velocity;//ʹ���ٶ�����С�ٶ�������ٶ�֮��
        speed.x = Mathf.Clamp(speed.x, -max_speed, max_speed);
        rb2d.velocity = speed;
    }
}
