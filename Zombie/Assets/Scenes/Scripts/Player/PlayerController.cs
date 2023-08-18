using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.XR;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{


    CharacterController cc;
    PlayerAnimation ani;

    Vector3 velocity;
    float gravity = -9.8f * 2;
    LayerMask groundMask;

    Transform aimTarget;

    float jumpHight = 3f;

    float moveSpeed = 2f;

    float aniSpeed = 10;

    //Camera _camera;


    //Transform target;


    void Awake()
    {
        #region GetComponent
        cc = GetComponent<CharacterController>();

        ani = Extention.GetAddComponent<PlayerAnimation>(this.gameObject);
        #endregion

        //_camera = Camera.main;
        aimTarget = Camera.main.transform.parent.GetChild(0);
        groundMask = LayerMask.GetMask("Ground");

        //target = transform.GetChild(3);
    }

    // Update is called once per frame
    playerState p;
    void Update()
    {

        Ground();

        RayCast();

        if (ani._isLadder)
            return;

        Move();

        Gravity();

        Jump();

        Crouch();
        mouseInput();

        // switchMotions();

    }

    //void switchMotions()
    //{
    //    switch (Managers._playerState.GetPlayerState())
    //    {
    //        case playerState.idle:
    //            Debug.Log(1);
    //            break;
    //        case playerState.move:
    //            Debug.Log(1);
    //            break;
    //        case playerState.fall:
    //            Debug.Log(1);
    //            break;
    //        case playerState.jump:
    //            Debug.Log(1);
    //            break;
    //        case playerState.crouch:
    //            Debug.Log(1);
    //            break;
    //        case playerState.aim:
    //            Debug.Log(1);
    //            break;
    //        case playerState.ladder:
    //            Debug.Log(1);
    //            break;

    //    }
    //}

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

    Ray Ladder;

    Ray LadderUpLay;

    Ray LadderDownLay;

    Ray Obstcale;

    void RayCast()
    {
        Ladder = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.forward);

        LadderUpLay = new Ray(transform.position + new Vector3(0, 1.5f, 0), transform.forward);

        LadderDownLay = new Ray(transform.position, transform.forward);

        LadderSystem();
    }

    public bool triggerLadder = false;

    float vel;


    void LadderSystem()
    {
        LayerMask layerMask = LayerMask.GetMask("Ladder");

        RaycastHit ladder;

        bool up = Physics.Raycast(LadderUpLay, 0.5f, layerMask);

        bool down = Physics.Raycast(LadderDownLay, out ladder, 0.5f, layerMask);

        bool inLadder = Physics.Raycast(Ladder, 0.5f, layerMask);

        if (!inLadder && down && Input.GetKey(KeyCode.E))
        {
            ani.LadderDown = true;
        }

        if (inLadder)
        {

            float v = Input.GetAxis("Vertical");

            if (v < 0)
            {
                vel = Mathf.Lerp(vel, -1, Time.deltaTime * 7);
            }
            else if (v > 0)
            {
                vel = Mathf.Lerp(vel, 1, Time.deltaTime * 7);
            }
            else
            {
                vel = Mathf.Lerp(vel, 0, Time.deltaTime * 7);
            }

            if (v > 0 || down || ani.LadderDown)
            {
                ani._isLadder = true;
             
            }

            if (ani._isLadder)
            {
                ani._ladderSpeed = vel;

                if (v < 0)
                {
                    if (ani._isGround && !ani.LadderDown)
                    {
                        ani.LadderD = true;
                        ani._isLadder = false;
                    }
                }
                else
                {
                    ani.LadderD = false;
                }

                if (!up && v > 0 && !ani.LadderDown)
                {
                    ani.LadderU = true;
                }
                else
                {
                    ani.LadderU = false;
                }

                Vector3 vector = new Vector3(0, vel, 0);

                cc.Move(((vector) * Time.deltaTime));
            }

        }


        if (!up && !down && !inLadder)
        {
            triggerLadder = false;
            ani._isLadder = false;
            ani.LadderDown = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Ladder);
        Gizmos.DrawRay(LadderUpLay);
        Gizmos.DrawRay(LadderDownLay);
        Gizmos.DrawRay(Obstcale);
    }




}



