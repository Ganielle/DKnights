using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSelected
{
    private ItemData itemSelect;

    //EVENT HANDLERS
    public event EventHandler selectedItem;

    public ItemData getItemSelected() 
    {
        return itemSelect;
    }

    public void setItemSelected(ItemData item) 
    {
        this.itemSelect = item;
        selectedItem?.Invoke(this, EventArgs.Empty);
    }
}
