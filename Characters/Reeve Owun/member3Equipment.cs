using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class member3Equipment
{
    public event EventHandler itemOnChange;

    public ItemData getWeaponItem()
    {
        return GameDatabaseStatic.getThreeWeaponItem();
    }

    public ItemData getArmorItem()
    {
        return GameDatabaseStatic.getThreeArmorItem();
    }

    public ItemData getAccessoryItem()
    {
        return GameDatabaseStatic.getThreeAccessoryItem();
    }

    public void setWeaponItem(ItemData item)
    {
        GameDatabaseStatic.setThreeWeaponItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setArmorItem(ItemData item)
    {
        GameDatabaseStatic.setThreeArmorItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setAccessoryItem(ItemData item)
    {
        GameDatabaseStatic.setThreeAccessoryItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }
}
