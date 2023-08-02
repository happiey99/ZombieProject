using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Bag,
        Gun,
        Attack,
        Grenade,
        Bullet,
        Heal,
    }

    public string ItemName;
    public ItemType itemType;
    public bool isEquip;

    public Item(string _ItemName, ItemType _itemType,bool isEquip =false)
    {
        ItemName = _ItemName;
        itemType = _itemType;
        this.isEquip = isEquip;
    }
}
public class Attack : Item
{
    public int power;

    public Attack(string _ItemName, ItemType _itemType, int power) : base(_ItemName, _itemType)
    {
       this.power = power;
    
    }
}
public class Use : Item
{
    public int healAmount;
    public int Count;
    public Use(string _ItemName, ItemType _itemType, int healAmount, int itemCount) : base(_ItemName, _itemType)
    {
        this.healAmount = healAmount;
        this.Count = itemCount;
    }
}
public class Bullet : Item
{
    public int AmmoAmount;

    public Bullet(string _ItemName, ItemType _itemType, int itemCount) : base(_ItemName, _itemType)
    {
        this.AmmoAmount = itemCount;
    }
}
public class Bag : Item
{
   public int SlotCount;
   
    public Bag(string _ItemName, ItemType _itemType,int SlotCount) : base(_ItemName, _itemType)
    {
       this.SlotCount = SlotCount;

    }
}