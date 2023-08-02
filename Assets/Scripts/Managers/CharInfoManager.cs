using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfoManager
{
    public int hp;

    public int AmmoAmount;

    public int curAmmo;

    public int curFullAmmo;

    public bool EquipGun = false;

    public int wholeAmount;

    public void Init()
    {
        curAmmo = 0;
        AmmoAmount = 30;
        curFullAmmo = 0;
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
    public void GetAmmo(int _curFullAmmo)
    {
        curFullAmmo += _curFullAmmo;
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
