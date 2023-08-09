using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        Debug.Log("111");
        if (p != null) 
        {
            PlayerAnimation ani = p.GetComponent<PlayerAnimation>();

            ani._isLadder = true;
            float vertical = Input.GetAxis("Vertical");
            Vector3 v = new Vector3(0, vertical, 0);

            p.PlayerMove(v, 1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
