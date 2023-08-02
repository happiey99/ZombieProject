using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager
{
    public Item Bag;
    public Item Gun;
    public Item Attack;
    public Item Grenade;


    public void SetItem(Item item)
    {

        switch (item.itemType)
        {
            case Item.ItemType.Bag:
                Bag = item;
                break;
            case Item.ItemType.Attack:
                Attack = item;
                break;
            case Item.ItemType.Gun:
                Gun = item;
                break;
            case Item.ItemType.Grenade:
                Grenade = item;
                break;
        }
    }
    public void UnEquipItem(Item item)
    {

        switch (item.itemType)
        {
            case Item.ItemType.Bag:
                Managers._inven.UnBag();
                Bag = null;
                break;
            case Item.ItemType.Attack:
                Attack = null;
                break;
            case Item.ItemType.Gun:
                Gun = null;
                break;
            case Item.ItemType.Grenade:
                Grenade = null;
                break;
        }
    }
}
