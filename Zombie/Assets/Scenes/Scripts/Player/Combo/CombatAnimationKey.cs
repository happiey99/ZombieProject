using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatAnimationKey : MonoBehaviour
{
    Combat combat;
    Transform HitBox;


    private void Start()
    {
        combat = GetComponent<Combat>();
        HitBox = transform.Find("HitBox");
        HitBox.gameObject.SetActive(false);
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
        HitBox.gameObject.SetActive(true);
        Invoke("OffObject",0.5f);
    }

    void OffObject()
    {
        HitBox.gameObject.SetActive(false);
    }
}
