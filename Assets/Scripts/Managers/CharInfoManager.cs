using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfoManager
{
    public int hp;

    public int AmmoAmount;

    public int curAmmo;
    public int curFullAmmo;

    public void Init()
    {
        curAmmo = 30;
        AmmoAmount = 30;
        curFullAmmo = 30;
    }
    public bool IsEmptyAmmo()
    {
        if (curFullAmmo <= 0)
            return true;
        return false;
    }
    public bool IsFullAmmo()
    {
        if (curAmmo == AmmoAmount)
            return true;
        return false;
    }
    public void Reloading()
    {
        int temp = AmmoAmount - curAmmo;

        curFullAmmo -= temp;
     
        if (curFullAmmo <= 0)
            curFullAmmo = 0;

        curAmmo += temp;

    }
}
