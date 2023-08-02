using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySys : MonoBehaviour
{
    [SerializeField] List<Slot> slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<Slot>();
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<Slot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Managers._charState.invenOpen)
        {
            for (int i = 0; i < Managers._inven.fullCount; i++)
            {
                slots[i].gameObject.SetActive(true);               
            }

            for (int i = 0; i < Managers._inven.InventorySlots.Count; i++)
            {
                if (Managers._inven.InventorySlots[i] != null)
                    slots[i].item = Managers._inven.InventorySlots[i];

            }
        }
    }
}
