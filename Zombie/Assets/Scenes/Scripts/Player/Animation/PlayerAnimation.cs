using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimation : MonoBehaviour
{

    Animator ani;

    bool isJump;
    bool isFalling;
    bool isRunning;
    bool isClimbing;
    bool isGround;
    bool isCrouch;
    bool isAim;
    bool isFire;
    float moveSpeed;

    public bool _isJump { get { return isJump; } set { isJump = value; } }
    public bool _isFalling { get { return isFalling; } set { isFalling = value; } }
    public bool _isRunning { get { return isRunning; } set { isRunning = value; } }
    public bool _isClimbing { get { return isClimbing; } set { isClimbing = value; } }
    public bool _isGround { get { return isGround; } set { isGround = value; } }
    public bool _isCrouch { get { return isCrouch; } set { isCrouch = value; } }
    public bool _isAim { get { return isAim; } set { isAim = value; } }
    public bool _isFire { get { return isFire; } set { isFire = value; } }
    public float _moveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }


    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationState();
        Aim();
        Crouch();
    }
    void AnimationState()
    {
        ani.SetBool("isJump", isJump);
        ani.SetBool("isFalling", isFalling);
        ani.SetBool("isRunning", isRunning);
        ani.SetBool("isGround", isGround);
        ani.SetBool("isClimbing", isClimbing);
        ani.SetFloat("move", moveSpeed);
    }

    float setCrouchWeight;
    float setAimWeight;


    void Aim()
    {
        if (isAim)
        {
            setAimWeight = Mathf.Lerp(setAimWeight, 1, Time.deltaTime * 6.0f);
        }
        else
        {
            setAimWeight = Mathf.Lerp(setAimWeight, 0, Time.deltaTime * 6.0f);

        }
        ani.SetLayerWeight(2, setAimWeight);
    }

    void Crouch()
    {

       

        if (isCrouch)
        {
            setCrouchWeight = Mathf.Lerp(setCrouchWeight, 1, Time.deltaTime * 6.0f);
        }
        else
        {
            setCrouchWeight = Mathf.Lerp(setCrouchWeight, 0, Time.deltaTime * 6.0f);

        }
        ani.SetLayerWeight(1, setCrouchWeight);

    }





}


