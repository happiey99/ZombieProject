using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList
{
    public Dictionary<string, Item> itemList = new Dictionary<string, Item>();

    public void Init()
    {
        AttackList();
        UseList();
        BagList();
        BulletList();
    }

    public void AttackList()
    {
        itemList.Add("AK", new Attack("AK", Item.ItemType.Gun, 5));
        itemList.Add("Bet", new Attack("Bet", Item.ItemType.Attack, 20));
    }
    public void UseList()
    {

    }
    public void BulletList()
    {
        itemList.Add("AK_Ammo", new Bullet("AK_Ammo", Item.ItemType.Bullet, 30));
    }
    public void BagList()
    {
        itemList.Add("Bag", new Bag("Bag", Item.ItemType.Bag, 4));
    }
    public Item GetItem(string _name)
    {
        Item item = null;
        if (!itemList.TryGetValue(_name, out item))
        {
            return null;
        }
        return item;
    }
}
