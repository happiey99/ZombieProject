using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region components
    CharacterController cc;
    PlayerAnimation PA;
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

        PA = GetComponent<PlayerAnimation>();
        if (PA == null)
        {
            PA = gameObject.AddComponent<PlayerAnimation>();
        }
        #endregion

        _camera = Camera.main;

        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {

        PA._isGround = Physics.CheckSphere(transform.position, 0.2f, groundMask);
        if (PA._isGround)
        {
            PA._isFalling = false;
            PA._isJump = false;
        }
        else
        {
            PA._isFalling = true;
        }

        Gravity();
        Jump();
        Move();
    }


    void Move()
    {

        float vertical = Input.GetAxis("Vertical");
        float horizental = Input.GetAxis("Horizontal");
      
        Vector3 move = new Vector3(horizental, 0, vertical);

        if (move == Vector3.zero)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, aniSpeed * Time.deltaTime);
            PA._isRunning = false;
        }
        else
        {
            Vector3 lookForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            Vector3 lookRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);

            moveDir = (lookForward * move.z) + (lookRight * move.x);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 6.0f);
            if (Input.GetKey(KeyCode.LeftShift))
                moveSpeed = Mathf.Lerp(moveSpeed, 4, aniSpeed * Time.deltaTime);
            else
                moveSpeed = Mathf.Lerp(moveSpeed, 2, aniSpeed * Time.deltaTime);
            PA._isRunning = true;
        }

        PA._moveSpeed = moveSpeed;
    
        cc.Move(moveDir * Time.deltaTime * moveSpeed);


    }


    void Gravity()
    {
        if (PA._isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }


    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && PA._isGround)
        {
            PA._isJump = true;

            // Jump 공식 = sqrt(JumpHight * -2f * gravity)
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);

        }
    }
}



