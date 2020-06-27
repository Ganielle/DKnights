using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class actionSelected
{
    private attackMagic method;
    private ItemData item;
    private GameObject selectedEnemy;
    public GameObject lastSelectedEnemy;

    public event EventHandler battleItemSelectedChange, selectedEnemyChange;

    public actionSelected()
    {
        setAttackMagic(null);
        setItem(null);
    }

    public attackMagic getMethod()
    {
        return method;
    }

    public void setAttackMagic(attackMagic method)
    {
        this.method = method;
        battleItemSelectedChange?.Invoke(this, EventArgs.Empty);
    }

    public ItemData getItem()
    {
        return item;
    }

    public void setItem(ItemData item)
    {
        this.item = item;
        battleItemSelectedChange?.Invoke(this, EventArgs.Empty);
    }

    public GameObject SelectedEnemy
    {
        get{ return selectedEnemy; }
        set 
        {
            lastSelectedEnemy = selectedEnemy;
            selectedEnemy = value;
            selectedEnemyChange?.Invoke(this, EventArgs.Empty);
        }
    }

}
