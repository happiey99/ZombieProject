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
    bool isLadder;
    float moveSpeed;

    public bool _isJump { get { return isJump; } set { isJump = value; } }
    public bool _isFalling { get { return isFalling; } set { isFalling = value; } }
    public bool _isRunning { get { return isRunning; } set { isRunning = value; } }
    public bool _isClimbing { get { return isClimbing; } set { isClimbing = value; } }
    public bool _isGround { get { return isGround; } set { isGround = value; } }
    public bool _isCrouch { get { return isCrouch; } set { isCrouch = value; } }
    public bool _isAim { get { return isAim; } set { isAim = value; } }
    public bool _isFire { get { return isFire; } set { isFire = value; } }
    public bool _isLadder { get { return isLadder; } set { isLadder = value; } }
    public float _moveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }


    float setCrouchWeight;
    float setAimWeight;

    void Start()
    {
        ani = GetComponent<Animator>();

        Init();
    }

    void Init()
    {
        setCrouchWeight = 0;
        setAimWeight = 0;
    }


    // Update is called once per frame
    void Update()
    {
        AnimationState();

        setCrouchWeight = SetAniLayer(1, isCrouch, setCrouchWeight);
        setAimWeight = SetAniLayer(2, isAim, setAimWeight);
    }

    void AnimationState()
    {
        ani.SetBool("isJump", isJump);
        ani.SetBool("isFalling", isFalling);
        ani.SetBool("isRunning", isRunning);
        ani.SetBool("isGround", isGround);
        ani.SetBool("isClimbing", isClimbing);
        ani.SetBool("isLadder", isLadder);
        ani.SetFloat("move", moveSpeed);
    }


    float SetAniLayer(int index, bool condition, float curWeight)
    {
        float value = curWeight;

        if (condition)
            value = Mathf.Lerp(value, 1, Time.deltaTime * 6.0f);
        else
            value = Mathf.Lerp(value, 0, Time.deltaTime * 6.0f);

        ani.SetLayerWeight(index, value);

        return value;
    }

}

