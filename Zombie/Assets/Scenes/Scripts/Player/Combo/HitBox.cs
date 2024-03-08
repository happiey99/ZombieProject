using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = transform.parent.GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        
        if (e != null)
        {
            e.Hit(20,player);
        }
    }

    
}
