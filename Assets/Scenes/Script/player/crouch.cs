using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour
{
    
    public Animator animator;
    public bool is_crouching=false;
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))//�������s��
        {
            is_crouching=true;
            animator.SetBool("is_crouch", true);
        }
        else//����״̬
        {    
            is_crouching = false;
            animator.SetBool("is_crouch", false);
        }
    }
}
