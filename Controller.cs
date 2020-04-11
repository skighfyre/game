using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float verticalVelocity;
    public float speed = 6.0f;
    private float gravity = 7.0f;
    private float jumpForce = 7.0f;
    public float rotateSpeed = 10.0f;
    public float jumps;


    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        


    }
    void Update()
    {
        move_around();
        input_controls();
        bullshit();
    }
    void move_around()
    {
        Debug.Log("function called");
        float translation = Input.GetAxis("Vertical") * speed;

        if (controller.isGrounded)
        {
            
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButtonDown("Jump"))
            {
                
                moveDirection.y = jumpForce;
            }
            jumps = 0;
        }
        else
        {

            moveDirection = new Vector3(0, moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
            if (Input.GetButtonDown("Jump") && jumps < 1)
            {

                moveDirection.y = jumpForce;
                jumps++;
            }
        }
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void input_controls()
    {
        // animator.SetBool("Unarmed-Idle", true);
        if (Input.GetKey("w"))
        {
            animator.SetBool("Unarmed-Run-F", true);
            animator.SetBool("Unarmed-Idle", false);
        }
        if (Input.GetKeyUp("w"))
        {
            animator.SetBool("Unarmed-Run-F", false);
            animator.SetBool("Unarmed-Idle", true);
        }
        if (Input.GetKey("space"))
        {
            animator.SetTrigger("Unarmed-Jump");
        }

    }
    void bullshit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetInteger("Jumping", 1);
        }
    }
}
