using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] Slot B_Slot;
    [SerializeField] Slot A_Slot;
    [SerializeField] Slot G_Slot;
    [SerializeField] Slot E_Slot;


    void Update()
    {
        if (B_Slot.item == null)
        {
            if (Managers._equip.Bag != null)
            {
                Managers._charState.E_Bag = true;
                Managers._inven.WearingBag();
                B_Slot.item = Managers._equip.Bag;
            }
            else
            {
                Managers._charState.E_Bag = false;
                B_Slot.item = null;
            }
        }
        if (A_Slot.item == null)
        {
            if (Managers._equip.Attack != null)
            {
                Managers._charState.E_Attack = true;
                A_Slot.item = Managers._equip.Attack;

            }
            else
            {
                Managers._charState.E_Attack = false;
                A_Slot.item = null;

            }
        }
        if (G_Slot.item == null)
        {
            if (Managers._equip.Gun != null)
            {
                Managers._charState.E_Gun = true;
                G_Slot.item = Managers._equip.Gun;
            }
            else
            {
                Managers._charState.E_Gun = false;
                G_Slot.item = null;
            }
        }
        if (E_Slot.item == null)
        {
            if (Managers._equip.Grenade != null)
                Managers._charState.E_Grenade = true;
            else
                Managers._charState.E_Grenade = false;
        }
    }

}


