using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public CharacterController cc;
    public PlayerAnimation ani;
    public Animator animator;

    Vector3 velocity;
    float gravity = -9.8f * 2;
    LayerMask groundMask;

    Transform aimTarget;

    float jumpHight = 3f;

    float moveSpeed = 2f;

    float aniSpeed = 10;


    void Awake()
    {
        #region GetComponent
        cc = GetComponent<CharacterController>();

        ani = Extention.GetAddComponent<PlayerAnimation>(this.gameObject);
        #endregion

        aimTarget = Camera.main.transform.parent.GetChild(0);
        groundMask = LayerMask.GetMask("Ground");

    }

    // Update is called once per frame
    playerState p;
    void Update()
    {
        Ground();

        if (ani._isLadder || !cc.enabled)
            return;

        Move();

        Gravity();

        Jump();

        Crouch();
        mouseInput();

    }

    void Move()
    {
        Vector3 moveDir = Vector3.zero;

        float vertical = Input.GetAxis("Vertical");
        float horizental = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(horizental, 0, vertical).normalized;

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

            if (!ani._isAim)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 6.0f);

            if (Input.GetKey(KeyCode.LeftShift) && !ani._isCrouch)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, 4, aniSpeed * Time.deltaTime);
                ani._isRunning = true;
            }
            else
            {
                moveSpeed = Mathf.Lerp(moveSpeed, 2, aniSpeed * Time.deltaTime);
                ani._isRunning = false;
            }


        }

        ani._moveSpeed = moveSpeed;

        PlayerMove(moveDir, moveSpeed);
    }

    public void PlayerMove(Vector3 move, float speed)
    {
        cc.Move(move * Time.deltaTime * speed);
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

  
}

