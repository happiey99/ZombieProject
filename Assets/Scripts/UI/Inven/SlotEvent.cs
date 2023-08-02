using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotEvent : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        RaycastResult r = eventData.pointerCurrentRaycast;

        Slot slot = r.gameObject.GetComponent<Slot>();

        Item s = null;

        if (slot != null)
            s = slot.item;

        if (s != null)
        {
            if (s.itemType == Item.ItemType.Bag ||
                s.itemType == Item.ItemType.Attack ||
                s.itemType == Item.ItemType.Gun ||
                s.itemType == Item.ItemType.Grenade)
            {
                if (!s.isEquip)
                {
                    Managers._equip.SetItem(s);
                    s.isEquip = true;
                    if (slot.E != null)
                        slot.E.alpha = 1;
                }
                else
                {
                    Managers._equip.UnEquipItem(s);
                    s.isEquip = false;
                    if (slot.E != null)
                        slot.E.alpha = 0;
                }
            }
        }


    }
}
