using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        PlayerAnimation ani = other.GetComponent<PlayerAnimation>();
        if (ani&&ani._isLadder)
        {

        }
    }
}
