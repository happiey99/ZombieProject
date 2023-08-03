using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region components
    CharacterController cc;
    Animator ani;
    #endregion

    #region Gravity
    Vector3 velocity;

    float gravity = -9.8f;

    bool isGround;

    LayerMask groundMask;
    #endregion

    float jumpHight = 3f;

    float moveSpeed = 2f;

    Camera _camera;


    bool isJump;
    bool isFalling;
    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        #region GetComponent
        cc = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        #endregion

        _camera = Camera.main;

        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(transform.position, 0.2f, groundMask);
        if (isGround)
        {
            isFalling = false;
            isJump = false;
        }
        else
        {
            isFalling = true;
        }

        Gravity();

        Jump();
        Move();


        AnimationState();
    }


    Vector3 lookForward;
    Vector3 lookRight;
    Vector3 moveDir;


    float aniSpeed = 10;
    void Move()
    {

        float vertical = Input.GetAxis("Vertical");
        float horizental = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(horizental, 0, vertical);

        if (move == Vector3.zero)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, aniSpeed * Time.deltaTime);
            isRunning = false;
        }
        else
        {
            lookForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            lookRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

            moveDir = (lookForward * move.z) + (lookRight * move.x);

            //transform.rotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 6.0f);
            if (Input.GetKey(KeyCode.LeftShift))
                moveSpeed = Mathf.Lerp(moveSpeed, 4, aniSpeed * Time.deltaTime);
            else
                moveSpeed = Mathf.Lerp(moveSpeed, 2, aniSpeed * Time.deltaTime);
            isRunning = true;
        }


        ani.SetFloat("move", moveSpeed);

        cc.Move(moveDir * Time.deltaTime * moveSpeed);


    }


    void Gravity()
    {
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }


    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;

            // Jump 공식 = sqrt(JumpHight * -2f * gravity)
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);

        }
    }


    void AnimationState()
    {
        ani.SetBool("isJump", isJump);
        ani.SetBool("isFalling", isFalling);
        ani.SetBool("isRunning", isRunning);
        ani.SetBool("isGround", isGround);
    }
}
