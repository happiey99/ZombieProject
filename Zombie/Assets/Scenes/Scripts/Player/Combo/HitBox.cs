using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Transform player;

    private Transform target;

    private float attack_Dis = 1.0f;

    private float outOfRange = 2.0f;

    private LayerMask _layerMask;

    public List<Enemy> priority = new List<Enemy>();
    
    
    private void Start()
    {
        player = transform.parent.GetComponent<Transform>();
        _layerMask =LayerMask.GetMask("Zombie");
    }

    private void Update()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, 2f, transform.forward, 0,_layerMask);

        if (hit.Length <= 0) 
            return;

        for (int i = 0; i < hit.Length; i++)
        {
            Enemy en = hit[i].transform.GetComponent<Enemy>();

            if (hit.Length > priority.Count && !priority.Contains(en)) 
                priority.Add(en);
            
            en.playerDistance(player);

            if (en.distance > outOfRange)
            {
                priority.Remove(en);
            }
            
            priority = SortPriority(priority);
        }

    }

    List<Enemy> SortPriority(List<Enemy> list)
    {
        List<Enemy> temp = new List<Enemy>();

        Enemy temp_enemy = null;
        
        for (int i = 0; i < list.Count; i++)
            temp.Add(list[i]);

        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (temp[i].distance < temp[j].distance)
                {
                    temp_enemy = temp[j];
                    temp[j] = temp[i];
                    temp[i] = temp_enemy;
                }
            }
        }
        return temp;
    }

    public Enemy AttackDisCheck()
    {
        Enemy temp = null;
        
        if (priority.Count > 0)
        {
            if (priority[0].distance <= attack_Dis)
            {
                temp = priority[0];
                
                // player look enemy;
            }
        }
        
        return temp;
    }
}
