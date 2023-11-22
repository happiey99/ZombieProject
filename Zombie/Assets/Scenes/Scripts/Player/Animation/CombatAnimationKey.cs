using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationKey : MonoBehaviour
{
    Combat combat;
   
    private void Start()
    {
        combat = GetComponent<Combat>();
 
    }

    public void Next()
    {
        combat.isNext = true;
        combat.isAttack = false;
    }

    public void CloseNext()
    {
        combat.isAttack = true;
        combat.isNext = false;
    }
    public void Reset_Attack()
    {
        combat.attact_Count = 0;
        combat.isNext = false;
        combat.isAttack = false;
    }

    public void Attack()
    {
       
    }
}
