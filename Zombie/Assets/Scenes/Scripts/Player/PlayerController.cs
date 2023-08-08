using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    #region components
    CharacterController cc;
    PlayerAnimation ani;
    #endregion
    #region Gravity
    Vector3 velocity;

    float gravity = -9.8f;

    //bool isGround;

    LayerMask groundMask;
    #endregion

    float jumpHight = 3f;

    float moveSpeed = 2f;

    float aniSpeed = 10;

    Camera _camera;

    Vector3 moveDir;



    void Awake()
    {
        #region GetComponent
        cc = GetComponent<CharacterController>();

        //ani = GetComponent<PlayerAnimation>();
        //if (ani == null)
        //{
        //    ani = gameObject.AddComponent<PlayerAnimation>();
        //}

        ani = Extention.GetAddComponent<PlayerAnimation>(this.gameObject);
        #endregion

        _camera = Camera.main;

        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Ground();

        //ray();
        if (ani._isLadder)
            return;

        //Gravity();
        //Jump();
        Move();
        Crouch();
        mouseInput();
    }


    void Move()
    {

        float vertical = Input.GetAxis("Vertical");
        float horizental = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(horizental, 0, vertical);
        
        if (move == Vector3.zero)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, aniSpeed * Time.deltaTime);
            ani._isRunning = false;
        }
        else
        {
            Vector3 lookForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            Vector3 lookRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

            moveDir = (lookForward * move.z) + (lookRight * move.x);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 6.0f);
            if (Input.GetKey(KeyCode.LeftShift) && !ani._isCrouch)
                moveSpeed = Mathf.Lerp(moveSpeed, 4, aniSpeed * Time.deltaTime);
            else
                moveSpeed = Mathf.Lerp(moveSpeed, 2, aniSpeed * Time.deltaTime);
            ani._isRunning = true;
        }

        ani._moveSpeed = moveSpeed;

        cc.Move(moveDir * Time.deltaTime * moveSpeed);


    }


    void Gravity()
    {
        if (ani._isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }


    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && ani._isGround)
        {
            ani._isJump = true;

            // Jump 공식 = sqrt(JumpHight * -2f * gravity)
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);

        }
    }
    void mouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ani._isAim = ani.CurrentAnimationSet(ani._isAim);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ani._isCrouch = ani.CurrentAnimationSet(ani._isCrouch);
        }
    }

    

    void Ground()
    {
        ani._isGround = Physics.CheckSphere(transform.position, 0.2f, groundMask);

        if (ani._isGround)
        {
            ani._isFalling = false;
            ani._isJump = false;
        }
        else
        {
            ani._isFalling = true;
            ani._isCrouch = false;
        }

    }
    Ray Aay;
    void ray()
    {
        Aay = new Ray(transform.position, transform.forward);


        if (Physics.Raycast(Aay, out RaycastHit hit, 1))
        {

            ani._isLadder = true;

            float vertical = Input.GetAxis("Vertical");
            Debug.Log(vertical);
            Vector3 v = new Vector3(0, vertical, 0);

            cc.Move(v * Time.deltaTime * 10);

        }
        else
        {
            ani._isLadder = false;
        }
    }




}



