using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveControl : MonoBehaviour
{
    public GameObject plane; //ground
    public Animator animator; 
    float speed = 2;
    float verticalSpeedStart = 10;
    float verticalSpeed = 0;
    float g = 10;
    bool isWalk=false;
    public bool isGround = false;
    AnimatorStateInfo animStateInfo;
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
       
    }
    

    void movement()
    {
        isWalk = false;
        //rotate
        float x = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, x * Time.deltaTime * 500);
        //walk
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            isWalk = true;
        }
        //jump
        jump();
        animator.SetBool("isWalk", isWalk);

    }
    void jump()
    {
        if (!isGround)
        {
            verticalSpeed -= Time.deltaTime * g;
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * verticalSpeed);           
        }
        else
        {
            //别问为什么分开 getkey中放jumpup，时间太长，会出现触发器触发2次
            //GetKeyDown放translate会出现不灵敏（点了不跳） 预计是放down里时间太短了，根本没跳起来
            if (Input.GetKey(KeyCode.Space))
            {
                verticalSpeed = verticalSpeedStart;
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * verticalSpeed);
              
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jumpUp");
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == plane)
        {
            isGround = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == plane)
        {
            isGround = false;
        }
    }


}
