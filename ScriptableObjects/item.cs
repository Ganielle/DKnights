using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "D-Knights/Item")]
public class item : ScriptableObject
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

    public enum ItemRarity
    {
        normal,
        common,
        uncommon,
        rare,
        legend
    }

    public ItemType itemType;
    public ItemRarity itemRarity;
    public int itemID;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public int attack, defense, intelligence, healthRegen, manaRegen, price, sell, howMany;
    public bool isStackable;
}
