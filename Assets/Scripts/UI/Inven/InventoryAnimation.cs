using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    Animator inven;

    void Start()
    {
        inven = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inven.SetBool("IsOpen", Managers._charState.invenOpen);
    }
}
