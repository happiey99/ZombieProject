using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStateManager
{
    public bool isJump;
    public bool isGround;
    public bool isAttack;
    public bool isMove;
    public bool isAim;
    public bool isReload;
    public bool isEmpty;
    public bool isSeat;

    public bool invenOpen;

   

    public void Init()
    {
        isJump = false;
        isEmpty = false;
        isGround = false;
        isAttack = false;
        isMove = false;
        isAim = false;
        isReload = false;
        isSeat = false;
        invenOpen = false;
    }
}
