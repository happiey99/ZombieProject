using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Vector3 PlayerCurFront;
    
    private CharacterController CC;
    [SerializeField] private float Speed = 6.0f;


    Vector3 Velocity;
    
    
    private float gravity = 9.8f;
    private bool isGround;
    private Transform GroundCheck;


    void Awake()
    {
        CC = GetComponent<CharacterController>();
        GroundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        Gravity();
        PlayerMove();
    }


    void Gravity()
    {
        isGround = Physics.CheckSphere(GroundCheck.position, 0.3f, LayerMask.GetMask("ground"));

        if (!isGround && Velocity.y < 0) 
        {
            Velocity.y = -2f;
        }
        Velocity.y -= gravity * Time.deltaTime;
        
        CC.Move(Velocity * Time.deltaTime);
    }
    
    void PlayerMove()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveInput == Vector3.zero) 
            return;
        
        //PlayerCurFront = (transform.position - Camera.main.transform.position).normalized;

        Vector3 lookForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 lookRight = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;
        Vector3 moveDir = ((lookForward * moveInput.z) + (lookRight * moveInput.x));
        
        transform.forward = moveDir;
    
        CC.Move(moveDir * Time.deltaTime * Speed);
    }


    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(Center.position,radius);
    // }
}
