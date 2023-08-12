using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{


    CharacterController cc;
    PlayerAnimation ani;

    Vector3 velocity;
    float gravity = -9.8f;
    LayerMask groundMask;


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

        groundMask = LayerMask.GetMask("Ground");

        //target = transform.GetChild(3);
    }

    // Update is called once per frame
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

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 6.0f);
            if (Input.GetKey(KeyCode.LeftShift) && !ani._isCrouch)
                moveSpeed = Mathf.Lerp(moveSpeed, 4, aniSpeed * Time.deltaTime);
            else
                moveSpeed = Mathf.Lerp(moveSpeed, 2, aniSpeed * Time.deltaTime);
            ani._isRunning = true;
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
   
    public bool triggerLadder;

    float vel;

    void RayCast()
    {
       Ladder = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.forward);

       LadderUpLay = new Ray(transform.position + new Vector3(0, 1.5f, 0), transform.forward);
       
       LadderDownLay = new Ray(transform.position, transform.forward);

        LayerMask layerMask = LayerMask.GetMask("Ladder");

        RaycastHit hit;

        bool up = Physics.Raycast(LadderUpLay, out hit, 0.5f, layerMask);

        bool down = Physics.Raycast(LadderDownLay, out hit, 0.5f, layerMask);

        bool inLadder = Physics.Raycast(Ladder, out hit, 0.5f, layerMask);

        if (inLadder && triggerLadder)
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

            if (v > 0)
            {
                ani._isLadder = true;      
            }
            
            if (ani._isLadder)
            {
                ani._ladderSpeed = vel;
                      
                if (v < 0 && ani._isGround)
                {
                    ani.LadderD = true;

                    ani._isLadder = false;
                }
                else
                {
                    ani.LadderD = false;
                }

                if (!up && v > 0) 
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
        else
        {
            ani._isLadder = false;
            triggerLadder = false;
        }

        if (!up && !down && !inLadder)
        {
            ani._isLadder = false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawRay(Ladder);
    //    Gizmos.DrawRay(LadderUpLay);
    //    Gizmos.DrawRay(LadderDownLay);


    //}


    #region 사다리 첫번#
    //int ladderLayer;

    //Ray ladderRay;

    //Ray outRay;

    //void CharacterRay()
    //{
    //    ladderLayer = LayerMask.GetMask("Ladder");

    //    ladderRay = new Ray(transform.position, transform.forward- new Vector3(0,0,0.5f));

    //    outRay = new Ray(transform.position + new Vector3(0,1.5f,0), transform.forward - new Vector3(0, 0, 0.5f));

    //    RaycastHit ladder;

    //    bool cast = Physics.Raycast(ladderRay, out ladder, 1, ladderLayer);


    //    if (cast)
    //    {
    //        if (Input.GetKey(KeyCode.W))
    //        {
    //            ani.LadderU = false;
    //            ani.LadderD = false;
    //            ani._isLadder = true;
    //            ani._moveSpeed = 0;
    //        }


    //        if (!Physics.Raycast(outRay, 1, ladderLayer))
    //        {
    //            StartCoroutine(LadderUp());
    //        }
    //        else
    //        {
    //            if (ani._isLadder)
    //            {
    //                ani._ladderSpeed = Input.GetAxis("Vertical");

    //                Vector3 v = new Vector3(0, ani._ladderSpeed, 0);

    //                if (ani._ladderSpeed < 0 && ani._isGround)
    //                {
    //                    ani.LadderD = true;

    //                    ani._isLadder = false;
    //                }

    //                cc.Move(v * Time.deltaTime);
    //            }
    //        }
    //    }



    //}


    //IEnumerator LadderUp()
    //{
    //    ani.LadderU = true;

    //    yield return new WaitForSeconds(1);

    //    ani.LadderU = false;
    //    ani._isLadder = false;
    //}



    #endregion

}



