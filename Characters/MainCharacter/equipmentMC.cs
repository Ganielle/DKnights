using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class equipmentMC
{

    public event EventHandler itemOnChange;

    public ItemData getWeaponItem() 
    {
        return GameDatabaseStatic.getWeaponItem();
    }

    public ItemData getArmorItem() 
    {
        return GameDatabaseStatic.getArmorItem();
    }

    public ItemData getAccessoryItem() 
    {
        return GameDatabaseStatic.getAccessoryItem();
    }

    public void setWeaponItem(ItemData item) 
    {
        GameDatabaseStatic.setWeaponItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setArmorItem(ItemData item) 
    {
        GameDatabaseStatic.setArmorItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }

    public void setAccessoryItem(ItemData item)
    {
        GameDatabaseStatic.setAccessoryItem(item);
        itemOnChange?.Invoke(this, EventArgs.Empty);
    }
}
