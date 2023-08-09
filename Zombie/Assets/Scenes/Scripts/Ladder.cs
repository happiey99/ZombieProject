using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    PlayerController p = other.GetComponent<PlayerController>();
     
    //    if (p != null) 
    //    {
    //        PlayerAnimation ani = p.GetComponent<PlayerAnimation>();

    //        ani._isLadder = true;
    //        float vertical = Input.GetAxis("Vertical");
    //        Vector3 v = new Vector3(0, vertical, 0);

    //        p.PlayerMove(v, 1.0f);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    PlayerController p = other.GetComponent<PlayerController>();
       
    //    if (p != null)
    //    {
    //        PlayerAnimation ani = p.GetComponent<PlayerAnimation>();

         

    //        p.PlayerMove(Vector3.up, 5.0f);

    //        ani._isLadder = false ;

    //    }
    //}

}
