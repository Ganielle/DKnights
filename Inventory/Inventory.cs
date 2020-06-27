using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public enum ItemType
    {
        staff,
        wand,
        orb,
        book,
        defense,
        accessory,
        keyItems,
        questItems,
        healthPotion,
        manaPotion,
        money
    }

    private event EventHandler itemOnAdd;

    public event EventHandler onItemAdd
    {
        add
        {
            if (itemOnAdd == null || !itemOnAdd.GetInvocationList().Contains(value))
                itemOnAdd += value;
        }
        remove
        {
            itemOnAdd -= value;
        }
    }

    public void addItemStaff(ItemData item)
    {
        GameDatabaseStatic.addItemStaff(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addItemWand(ItemData item)
    {
        GameDatabaseStatic.addItemWand(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addItemBook(ItemData item)
    {
        GameDatabaseStatic.addItemBook(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addItemOrb(ItemData item)
    {
        GameDatabaseStatic.addItemOrb(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addItemDefense(ItemData item)
    {
        GameDatabaseStatic.addItemDefense(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addItemAccessory(ItemData item)
    {
        GameDatabaseStatic.addItemAccessory(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addItemConsumable(ItemData item)
    {
        GameDatabaseStatic.addItemConsumable(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addKeyItem(ItemData item)
    {
        GameDatabaseStatic.addKeyItem(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addQuestItem(ItemData item)
    {
        GameDatabaseStatic.addQuestItem(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public void addMoney(float item)
    {
        GameDatabaseStatic.addMoney(item);
        itemOnAdd?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemData> GetItemStaffList()
    {
        return GameDatabaseStatic.GetItemStaffList();
    }

    public List<ItemData> GetItemWandList()
    {
        return GameDatabaseStatic.GetItemWandList();
    }

    public List<ItemData> GetItemBookList()
    {
        return GameDatabaseStatic.GetItemBookList();
    }

    public List<ItemData> GetItemOrbList()
    {
        return GameDatabaseStatic.GetItemOrbList();
    }

    public List<ItemData> GetItemDefenseList()
    {
        return GameDatabaseStatic.GetItemDefenseList();
    }

    public List<ItemData> GetItemAccessoryList()
    {
        return GameDatabaseStatic.GetItemAccessoryList();
    }

    public List<ItemData> GetItemConsumableList()
    {
        return GameDatabaseStatic.GetItemConsumableList();
    }

    public List<ItemData> GetkeyItemsList()
    {
        return GameDatabaseStatic.GetKeyItemsList();
    }

    public List<ItemData> GetQuestItems()
    {
        return GameDatabaseStatic.GetQuestItems();
    }

    public float GetMoney()
    {
        return GameDatabaseStatic.GetMoney();
    }

    public List<ItemData> GetItems(ItemType itemType) 
    {
        switch (itemType)
        {
            default:
            case ItemType.staff: return GetItemStaffList();
            case ItemType.wand: return GetItemWandList();
            case ItemType.orb: return GetItemOrbList();
            case ItemType.book: return GetItemBookList();
            case ItemType.defense: return GetItemDefenseList();
            case ItemType.accessory: return GetItemAccessoryList();
            case ItemType.manaPotion: case ItemType.healthPotion: return GetItemConsumableList();
            case ItemType.questItems: return GetQuestItems();
        }
    }
}
