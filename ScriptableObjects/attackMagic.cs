using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "D-Knights/Magic Attack")]
public class attackMagic : ScriptableObject
{

    public enum AttackType
    {
        NORMAL,
        MAGIC,
        GIVEHEALTH,
        GIVEMANA
    }

    public enum MagicType
    {
        NORMAL,
        FIRE,
        EARTH,
        WATER,
        AIR,
        MANAHEAL,
        HEALTHHEAL
    }

    public AttackType attackType;
    public MagicType magicType;
    public string attackName;

    [TextArea]
    public string description;

    [Header("Only put if you choose normal or magic")]
    public int damage;
    public int manaCost;

    [Header("Only put if you choose health or mana and Heal on magicType")]
    public int healthMana;


    public Sprite getMagicSprite()
    {
        switch (magicType)
        {
            default:
            case MagicType.NORMAL: return magicIconAsset.Instance.normalAttack;
            case MagicType.FIRE: return magicIconAsset.Instance.fireAttack;
            case MagicType.EARTH: return magicIconAsset.Instance.earthAttack;
            case MagicType.WATER: return magicIconAsset.Instance.waterAttack;
            case MagicType.AIR: return magicIconAsset.Instance.airAttack;
            case MagicType.MANAHEAL: return magicIconAsset.Instance.manaHeal;
            case MagicType.HEALTHHEAL: return magicIconAsset.Instance.healthHeal;
        }
    }
}
