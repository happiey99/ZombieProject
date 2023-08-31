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

        //RayCast();
        RayCast1();
        if (ani._isLadder)
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


    void RayCast1()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.forward);

        LayerMask layerMask = LayerMask.GetMask("Ladder");

        RaycastHit ladder;


        bool hit = Physics.Raycast(ray, out ladder, 0.5f, layerMask);

        if (hit)
        {
            float vertical = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(StopMove(ladder));
            }
            
        }
    }


    
    IEnumerator StopMove(RaycastHit obj)
    {

        ani._isLadder = true;
        cc.enabled = false;
        transform.position = Vector3.Lerp(transform.position, obj.transform.position + obj.transform.forward * -1*0.5f, 2);
        yield return new WaitForSeconds(3f);
        cc.enabled = true;

        ani._isLadder = false;
    }

    float vel;
    #region LadderSystem
    void RayCast()
    {
        Ray Ladder = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.forward);
        Ray LadderUpRay = new Ray(transform.position + new Vector3(0, 1.5f, 0), transform.forward);
        Ray LadderDownRay = new Ray(transform.position, transform.forward);


        LayerMask layerMask = LayerMask.GetMask("Ladder");

        RaycastHit ladder;


        bool up = Physics.Raycast(LadderUpRay, 0.5f, layerMask);

        bool down = Physics.Raycast(LadderDownRay, out ladder, 1, layerMask);

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
            ani._isLadder = false;
            ani.LadderDown = false;

        }

    }

    #endregion


}






