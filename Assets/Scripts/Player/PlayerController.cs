using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{

    private CharacterController CC;
    float moveAniSpeed = 3.0f;
    float turnSpeed = 6.0f;
    [SerializeField] PlayerGun playerGun;
    [SerializeField] ParticleSystem GunShells;
    [SerializeField] ParticleSystem GunShotEF;
    [SerializeField] Transform bullet;
    [SerializeField] Rig rig;
    [SerializeField] TwoBoneIKConstraint leftArmRig;
    [SerializeField] MultiAimConstraint BodyW;
    [SerializeField] MultiAimConstraint AimW;
    [SerializeField] MultiAimConstraint HeadW;

    [SerializeField] GameObject Back;
    [SerializeField] GameObject Gun;




    float rigWeight;

    Vector3 Velocity;

    private float gravity = 18.6f;
    [SerializeField] private float jumpPower = 12.0f;

    private Transform GroundCheck;

    Animator PlayerAnimator;

    void Awake()
    {
        CC = GetComponent<CharacterController>();
        GroundCheck = transform.Find("GroundCheck");
        PlayerAnimator = transform.GetComponent<Animator>();
        GunShells.Pause();
        GunShotEF.Pause();
        rig.weight = 1;

    }

    void Update()
    {
        if (Managers._charInfo.curAmmo <= 0)
        {
            Managers._charState.isEmpty = true;
        }
        else
        {
            Managers._charState.isEmpty = false;
        }
        if (Managers._charState.isReload)
        {

            leftArmRig.weight = 0;
        }
        else
        {
            leftArmRig.weight = Mathf.Lerp(leftArmRig.weight, 1, Time.deltaTime * 20f);
        }

        AnimationWeight();
        Gravity();
        PlayerInput();
        PlayerJump();
        Aimming();
        Shot();
        HeadAim();
        Reload();
        Seat();

        OpenInven();
        BodyW.weight = Mathf.Lerp(BodyW.weight, rigWeight, Time.deltaTime * 20f);
        AimW.weight = Mathf.Lerp(AimW.weight, rigWeight, Time.deltaTime * 20f);

    }

    float Aiminput = 0;
    float seatWeight = 0;

    void OpenInven()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(Managers._charState.invenOpen)
            {
                Managers._charState.invenOpen = false;
                return;
            }
            Managers._charState.invenOpen = true;
        }
    }
        
    void Aimming()
    {

        if (Input.GetMouseButton(1) || (Managers._charState.isReload))
        {
            Aiminput = Mathf.Lerp(Aiminput, 1, Time.deltaTime * moveAniSpeed);
            rigWeight = 1;
            seatWeight = 0;
            if (!Managers._charState.isReload)
                leftArmRig.weight = 1;//////

            Managers._charState.isAim = true;
            playerGun.LookPoint();
            Back.gameObject.SetActive(false);
            Gun.gameObject.SetActive(true);
        }
        else
        {
            Aiminput = Mathf.Lerp(Aiminput, 0, Time.deltaTime * moveAniSpeed);
            leftArmRig.weight = 0;
            rigWeight = 0;
            Managers._charState.isAim = false;
            Back.gameObject.SetActive(true);
            Gun.gameObject.SetActive(false);
        }


        PlayerAnimator.SetBool("IsAim", Managers._charState.isAim);

    }
    
    void AnimationWeight()
    {
        if (Managers._charState.isSeat)
        {
            seatWeight = Mathf.Lerp(seatWeight, 1, Time.deltaTime * moveAniSpeed);
        }
        else
        {
            seatWeight = Mathf.Lerp(seatWeight, 0, Time.deltaTime * moveAniSpeed);
            //Aiminput = 1;
        }

        PlayerAnimator.SetLayerWeight(2, seatWeight);
        PlayerAnimator.SetLayerWeight(1, Aiminput);
    }
    void Reload()
    {
        if (Managers._charState.isAttack|| Managers._charInfo.IsEmptyAmmo()|| Managers._charInfo.IsFullAmmo())
            return;
        if ((Input.GetKeyDown(KeyCode.R) || Managers._charInfo.curAmmo <= 0) && Managers._charState.isAim)
        {
            Managers._charState.isReload = true;

            PlayerAnimator.SetBool("isReload", true);

            Managers._charInfo.Reloading();
        }

    }

    public float ShotTime = 0;
    float callTime = 0.3f;
    void Shot()
    {
        if (Managers._charState.isReload)
            return;

        if (Input.GetMouseButton(0))
        {
            if (ShotTime <= 0.1f)
            {
                if (Managers._charState.isAim && Managers._charInfo.curAmmo > 0)
                {
                    ShotTime = callTime;
                    Managers._charInfo.curAmmo -= 1;
                    Transform b = Instantiate(bullet, playerGun.ShootPoint.transform.position, Quaternion.identity);
                    b.transform.forward = playerGun.ShootPoint.transform.forward;
                    GunShells.Play();
                    GunShotEF.Play();
                    Managers._charState.isAttack = true;
                }

            }
        }
        else
        {
            Managers._charState.isAttack = false;
        }
        PlayerAnimator.SetBool("isReload", Managers._charState.isReload);
        PlayerAnimator.SetBool("IsAttack", Managers._charState.isAttack);

        if (ShotTime <= 0) return;

        ShotTime -= Time.deltaTime;
    }


    void Gravity()
    {
        Managers._charState.isGround = Physics.CheckSphere(GroundCheck.position, 0.5f, LayerMask.GetMask("Default"));

        if (Managers._charState.isGround && Velocity.y < 0)
        {
            Velocity.y = -2f;
            PlayerAnimator.SetBool("Jump", false);
        }
        Velocity.y -= gravity * Time.deltaTime;
        PlayerAnimator.SetBool("IsGround", Managers._charState.isGround);
        CC.Move(Velocity * Time.deltaTime);
    }

    void PlayerJump()
    {
        if (Managers._charState.isAim || Managers._charState.isReload)
            return;
        if (Input.GetKeyDown(KeyCode.Space) && Managers._charState.isGround)
        {
            PlayerAnimator.SetBool("Jump", true);
        }
    }


    public float move;


    void PlayerInput()
    {
        float Horizontal = (Input.GetAxis("Horizontal"));
        float Vertical = (Input.GetAxis("Vertical"));

        Vector3 moveInput = new Vector3(Horizontal, 0, Vertical);

        if (moveInput == Vector3.zero)
        {
            Managers._charState.isMove = false;
            move = Mathf.Lerp(move, 0, Time.deltaTime * moveAniSpeed);
        }
        else
        {

            Vector3 lookForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;
            Vector3 moveDir = ((lookForward * moveInput.z) + (lookRight * moveInput.x));

            Managers._charState.isMove = true;

            if (!Managers._charState.isAim)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * turnSpeed);
            }
            else
            {
                CC.Move(moveDir * 1 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = Mathf.Lerp(move, 6, Time.deltaTime * moveAniSpeed);
            }
            else
            {
                move = Mathf.Lerp(move, 1, Time.deltaTime * moveAniSpeed);
            }

        }
        PlayerAnimator.SetBool("isMove", Managers._charState.isMove);

        PlayerAnimator.SetFloat("MoveSpeed", move);
    }
    float MaxAngle = 110;
    float headWeight;
    void HeadAim()
    {
        Vector3 delta = playerGun.target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, delta);

        if (angle > MaxAngle)
        {
            headWeight = 0;
        }
        else
        {
            headWeight = 1;
        }

        HeadW.weight = Mathf.Lerp(HeadW.weight, headWeight, Time.deltaTime * 10f);
    }


    void Seat()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Managers._charState.isSeat)
                Managers._charState.isSeat = false;
            else
                Managers._charState.isSeat = true;
        }

    }
    //float Increase(float curValue, float _num)
    //{
    //    float value = curValue;

    //    if (value < _num)
    //    {
    //        value += Time.deltaTime * moveAniSpeed;

    //        if (value > _num - 0.05f)
    //        {
    //            value = _num;
    //        }
    //    }
    //    else
    //    {
    //        value -= Time.deltaTime * moveAniSpeed;

    //        if (value < _num + 0.05f)
    //        {
    //            value = _num;
    //        }
    //    }

    //    return value;
    //}



    //void PlayerMove()
    //{
    //    Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    //    if (moveInput == Vector3.zero)
    //    {
    //        moveAniSpeed = Mathf.Lerp(moveAniSpeed, 0, 1f * Time.deltaTime * moveSpeed);
    //    }
    //    else
    //    {
    //        moveAniSpeed = Mathf.Lerp(moveAniSpeed, 1, 1f * Time.deltaTime * moveSpeed);

    //        Vector3 lookForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
    //        Vector3 lookRight = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;
    //        Vector3 moveDir = ((lookForward * moveInput.z) + (lookRight * moveInput.x));

    //        transform.forward = moveDir;

    //        //      CC.Move(moveDir * Time.deltaTime * Speed); // 케릭터 움직이는거
    //    }

    //    PlayerAnimator.SetFloat("MoveSpeed", moveAniSpeed);
    //}



}
