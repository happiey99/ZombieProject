using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CombatAnimationKey))]
public class Combat : MonoBehaviour
{

    public int attact_Count;


    public bool isNext ;
    public bool isAttack ;

    PlayerAnimation p_ani;

    Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        p_ani = GetComponent<PlayerAnimation>();
        isNext = false;
        isAttack = false;
        attact_Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!p_ani._isGround)
            return;

        if (Input.GetMouseButtonDown(0)&&!isAttack)
        {
           
            isAttack = true;

            if (attact_Count == 0 && !isNext)
            {
                attact_Count = 1;
                ani.SetTrigger("Combat_Hook");
                //Debug.Log("attack 1");
            }
            else if (isNext)
            {
                isNext = false;
                Attack_Animation();
            }

        }
    }


    void Attack_Animation()
    {
        
        if (attact_Count == 1)
        {
            ani.SetTrigger("Punch_R");
            attact_Count = 2;
            //Debug.Log("attack 2");
        }
        else if (attact_Count == 2)
        {
            ani.SetTrigger("Punch_L");
            attact_Count = 3;
            //Debug.Log("attack 3");
        }
        else if(attact_Count == 3)
        {
            ani.SetTrigger("Combat_High_KicK");
            attact_Count = 4;
            //Debug.Log("attack 4");
        }
        else if (attact_Count == 4)
        {
            ani.SetTrigger("Combat_Spinning_Kick");
            attact_Count = 0;
            //Debug.Log("attack 5");
        }

       
    }
}
