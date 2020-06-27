using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class member1Equipment
{

    public event EventHandler itemOnChange;

    public ItemData getWeaponItem()
    {
        return GameDatabaseStatic.getOneWeaponItem();
    }

    public ItemData getArmorItem()
    {
        return GameDatabaseStatic.getOneArmorItem();
    }

    public ItemData getAccessoryItem()
    {
        return GameDatabaseStatic.getOneAccessoryItem();
    }

    public void setWeaponItem(ItemData item)
    {
        GameDatabaseStatic.setOneWeaponItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setArmorItem(ItemData item)
    {
        GameDatabaseStatic.setOneArmorItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setAccessoryItem(ItemData item)
    {
        GameDatabaseStatic.setOneAccessoryItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }
}
