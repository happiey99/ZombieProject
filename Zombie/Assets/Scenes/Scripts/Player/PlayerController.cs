using JetBrains.Annotations;
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

        RayCast();

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

    Ray lagRay;
    Ray ray;
    Ray headRay;

    public float ladderSpeed;
    void RayCast()
    {
        /*Ray*/
        lagRay = new Ray(transform.position, transform.forward);
        /*Ray*/
        ray = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.forward);
        /*Ray*/
        headRay = new Ray(transform.position + new Vector3(0, 1.5f, 0), transform.forward);

        LayerMask layerMask = LayerMask.GetMask("Ladder");

        RaycastHit ladder;


        bool hit = Physics.Raycast(ray, out ladder, 0.5f, layerMask);
        bool headHit = Physics.Raycast(headRay, out ladder, 1f, layerMask);
        bool lagHit = Physics.Raycast(lagRay, out ladder, 1f, layerMask);


        if (hit)
        {
            float vertical = Input.GetAxis("Vertical");

            if (vertical > 0 && !ani._isLadder)
            {
                StartCoroutine(LadderSetPos(ladder));
            }

            if (ani._isLadder)
            {
                if (vertical > 0)
                {
                    ladderSpeed = Mathf.Lerp(ladderSpeed, 1, Time.deltaTime * 7);

                    if (!headHit)
                    {
                        StartCoroutine(LadderUpSetPos());
                    }

                    //if (lagHit && !headHit && !headHit) 
                    //{
                    //    ani.LadderD = true;

                    //    StartCoroutine(LadderSetPos(ladder));
                    //}
                }
                else if (vertical < 0)
                {
                    

                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        ladderSpeed = Mathf.Lerp(ladderSpeed, -2, Time.deltaTime * 7);
                    }
                    else
                    {
                        ladderSpeed = Mathf.Lerp(ladderSpeed, -1, Time.deltaTime * 7);
                    }

                    if (ani._isGround)
                    {
                        ani._isLadder = false;
                    }
                }
                else
                {
                    ladderSpeed = Mathf.Lerp(ladderSpeed, 0, Time.deltaTime * 7);
                }

                Vector3 vector = new Vector3(0, ladderSpeed, 0);

                if (cc.enabled)
                {
                    ani._ladderSpeed = ladderSpeed;
                    cc.Move(vector * Time.deltaTime);
                }
            }
        }
    }

    float time = 0.1f;
    IEnumerator LadderSetPos(RaycastHit HitObj)
    {
        cc.enabled = false;

        Vector3 vector = HitObj.transform.position + HitObj.transform.forward * -0.35f;

        float elaps = 0.0f;

        while (elaps < time)
        {
            elaps += Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, HitObj.transform.forward, elaps / time);
            transform.position = Vector3.Lerp(transform.position, vector, elaps / time);

            yield return null;
        }
        ani.LadderD = false;
        cc.enabled = true;
        ani._isLadder = true;
        yield return null;
    }

    IEnumerator LadderUpSetPos()
    {
        if (!animator)
            animator = ani.ani;

        cc.enabled = false;

        ani.LadderU = true;
        yield return new WaitForSeconds(0.01f);
        ani.LadderU = false;
        float curAnimationTime = animator.GetCurrentAnimatorClipInfo(0).Length;

        yield return new WaitForSeconds(curAnimationTime);
        Vector3 vector = transform.position + transform.forward / 10;

        float elaps = 0.0f;
        while (elaps < time)
        {
            elaps += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, vector, elaps / time);
            yield return null;
        }

        ani._isLadder = false;

        cc.enabled = true;
    }


}






