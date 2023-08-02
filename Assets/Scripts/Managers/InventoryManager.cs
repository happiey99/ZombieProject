using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public List<Item> InventorySlots;


    public int fullCount;
    public void Init()
    {
        fullCount = 2;

        InventorySlots = new List<Item>();
    }
    public void WearingBag()
    {
        fullCount = 6;
    }
    public void UnBag()
    {
        fullCount = 2;
    }

    public bool BagIsFull()
    {
        if (InventorySlots.Count == fullCount)
            return true;
        return false;
    }

    public bool GetItem(string _itemName)
    {
        Item item = null;
        item = Managers._item.GetItem(_itemName);

        if (item == null)
            return false;

        if (BagIsFull())
        {
            return false;
        }
        else
        {
            if (item.itemType == Item.ItemType.Bullet)
            {
                if (!SameItem(item))
                {
                    InventorySlots.Add(item);
                }
                Managers._charInfo.GetAmmo(30);
            }
            else
            {
                InventorySlots.Add(item);
            }
        }

        return true;
    }

    bool SameItem(Item item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i] == item)
                return true;

        }
        return false;

    }
}
