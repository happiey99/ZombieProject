using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator ani;

    bool isJump;
    bool isFalling;
    bool isRunning;
    bool isClimbing;
    bool isGround;

    float moveSpeed;

    public bool _isJump { get { return isJump; } set { isJump = value; } }
    public bool _isFalling { get { return isFalling; } set { isFalling = value; } }
    public bool _isRunning { get { return isRunning; } set { isRunning = value; } }
    public bool _isClimbing { get { return isClimbing; } set { isClimbing = value; } }
    public bool _isGround { get { return isGround; } set { isGround = value; } }

    public float _moveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationState();
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
}

