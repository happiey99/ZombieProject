using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnimation : MonoBehaviour
{
    Animator player;
    int comboStep = 0;
    [SerializeField] Transform center;
    [SerializeField] Vector3 size;
    bool isAttack = false;
    [SerializeField] PlayerGun PG;
    [SerializeField] PlayerController pc;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        PG = GetComponent<PlayerGun>();
        comboStep = Mathf.Clamp(comboStep, 0, 2);
        player = GetComponent<Animator>();
    }
    public void Update()
    {
        if (Managers._charState.isAim || Managers._charState.invenOpen)
            return;
        if (isAttack)
        {
            PG.LookPoint();
            pc.Back_Attack.SetActive(false);
            pc.Hand_Attack.SetActive(true);
        }
      


        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;

            if (!Managers._charState.E_Attack)
            {
                if (comboStep == 0)
                    player.Play("Combat_Hook");
                else if (comboStep == 1)
                    player.Play("Combat_Uppercut");
                else if (comboStep == 2)
                    player.Play("Combat_High_KicK");


            }
            else
            {
                if (comboStep == 0)
                    player.Play("SS_Slash01");
                else if (comboStep == 1)
                    player.Play("SS_Slash02");
                else if (comboStep == 2)
                    player.Play("SS_Slash03");


            }

        }

    }
    public void Combo()
    {
        comboStep++;
    }

    public void ComboReset()
    {
        pc.Back_Attack.SetActive(true);
        pc.Hand_Attack.SetActive(false);
        comboStep = 0;
        isAttack = false;
    }
    public void Hit()
    {
        RaycastHit[] hit = Physics.BoxCastAll(center.position, size, center.position);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.GetComponent<Enemy>() != null)
            {
                Debug.Log("hit");
            }

        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(center.position, size);
    }
}
