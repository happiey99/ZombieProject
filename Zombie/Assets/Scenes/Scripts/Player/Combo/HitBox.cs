using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        
        if (e != null)
        {
            e.Hit(20);
        }
    }

    
}
