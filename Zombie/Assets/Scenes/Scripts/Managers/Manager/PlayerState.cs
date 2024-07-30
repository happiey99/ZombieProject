using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerState
{
    idle,
    move,
    fall,
    jump,
    crouch,
    aim,
    ladder,
}

public class PlayerState
{
    playerState ps = playerState.idle;
    
    public void Init()
    {
        ps = playerState.idle;
    }


    public void SetPlayerState(playerState _ps)
    {
        ps = _ps;
    }
    public playerState GetPlayerState()
    {
        return ps;
    }
}
