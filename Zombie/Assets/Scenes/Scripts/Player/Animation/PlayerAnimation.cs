using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimation : MonoBehaviour
{

    public Animator ani;

    bool isJump;

    bool isFalling;

    bool isRunning;

    bool isGround;

    bool isCrouch;

    bool isAim;
    bool isFire;

    bool isLadder;
    float moveSpeed;
    float ladderSpeed;

    public bool LadderU;
    public bool LadderD;
    public bool LadderS;

    public bool isGrab;
    public bool isHang;
    public float GrabValue;

    public bool isCombat;
    public int combatCount;

    public bool isAttack;

    public int attackCount;

    public bool _isJump { get { return isJump; } set { isJump = value; } }
    public bool _isFalling { get { return isFalling; } set { isFalling = value; } }
    public bool _isRunning { get { return isRunning; } set { isRunning = value; } }
    public bool _isGround { get { return isGround; } set { isGround = value; } }
    public bool _isCrouch { get { return isCrouch; } set { isCrouch = value; } }
    public bool _isAim { get { return isAim; } set { isAim = value; } }
    public bool _isFire { get { return isFire; } set { isFire = value; } }
    public bool _isLadder { get { return isLadder; } set { isLadder = value; } }
    public float _moveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float _ladderSpeed
    {
        get { return ladderSpeed; }
        set { ladderSpeed = value; }
    }

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
        ani.SetBool("isLadder", isLadder);
        ani.SetFloat("move", moveSpeed);
        ani.SetFloat("ladderSpeed", ladderSpeed);
        ani.SetFloat("GrabValue", GrabValue);
        ani.SetBool("LadderU", LadderU);
        ani.SetBool("LadderD", LadderD);
        ani.SetBool("isGrab", isGrab);
        ani.SetBool("LadderS", LadderS);
        ani.SetBool("isHang", isHang);

  
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

    public bool CurrentAnimationSet(bool current)
    {
        if (!current)
            current = true;
        else
            current = false;

        return current;
    }

}


