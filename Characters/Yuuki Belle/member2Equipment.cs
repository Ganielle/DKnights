using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class member2Equipment
{
    public event EventHandler itemOnChange;

    public ItemData getWeaponItem()
    {
        return GameDatabaseStatic.getTwoWeaponItem();
    }

    public ItemData getArmorItem()
    {
        return GameDatabaseStatic.getTwoArmorItem();
    }

    public ItemData getAccessoryItem()
    {
        return GameDatabaseStatic.getTwoAccessoryItem();
    }

    public void setWeaponItem(ItemData item)
    {
        GameDatabaseStatic.setTwoWeaponItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setArmorItem(ItemData item)
    {
        GameDatabaseStatic.setTwoArmorItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setAccessoryItem(ItemData item)
    {
        GameDatabaseStatic.setTwoAccessoryItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }
}
