using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public item data;

    public int itemAmount;
    public bool isEquip;

    [Header("put value if you choose money in Item Script")]
    public int moneyValue;

    public string itemEquipMemberType;

    public Sprite GetSprite()
    {
        switch (data.itemType)
        {
            default:
            case item.ItemType.staff: return itemAssets.Instance.staffSprite;
            case item.ItemType.wand: return itemAssets.Instance.wandSprite;
            case item.ItemType.orb: return itemAssets.Instance.orbSprite;
            case item.ItemType.book: return itemAssets.Instance.bookSprite;
            case item.ItemType.defense: return itemAssets.Instance.defenseSprite;
            case item.ItemType.accessory: return itemAssets.Instance.accessorySprite;
            case item.ItemType.healthPotion: return itemAssets.Instance.healthPotionSprite;
            case item.ItemType.manaPotion: return itemAssets.Instance.manaPotionSprite;
            case item.ItemType.money: return itemAssets.Instance.moneySprite;
        }
    }

    public Sprite getRaritySprite()
    {
        switch (data.itemRarity)
        {
            default:
            case item.ItemRarity.normal: return itemAssets.Instance.normal;
            case item.ItemRarity.common: return itemAssets.Instance.common;
            case item.ItemRarity.uncommon: return itemAssets.Instance.uncommon;
            case item.ItemRarity.rare: return itemAssets.Instance.rare;
            case item.ItemRarity.legend: return itemAssets.Instance.legend;
        }
    }
}
