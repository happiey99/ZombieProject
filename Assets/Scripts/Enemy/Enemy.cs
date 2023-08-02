using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Fov state;

    Animator ani;

    float IdleToWalk;

    private void Start()
    {
        IdleToWalk = 0;
        state = GetComponent<Fov>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Walk();
    }

    void Walk()
    {
        if (state.isMove)
        {

            if (state.isRun)
                IdleToWalk = Mathf.Lerp(IdleToWalk, 2, 5f * Time.deltaTime);
            else
                IdleToWalk = Mathf.Lerp(IdleToWalk, 1, 5f * Time.deltaTime);
        }
        else
        {
            IdleToWalk = Mathf.Lerp(IdleToWalk, 0, 5f * Time.deltaTime);
        }

        ani.SetFloat("Walk", IdleToWalk);

    }
}
