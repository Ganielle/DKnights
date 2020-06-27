using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAssets : MonoBehaviour
{
    public static itemAssets Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    [Header("Item Type sprites")]
    public Sprite staffSprite;
    public Sprite wandSprite;
    public Sprite orbSprite;
    public Sprite bookSprite;
    public Sprite offenseSprite;
    public Sprite defenseSprite;
    public Sprite accessorySprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite moneySprite;

    [Header("Item Rarity sprites")]
    public Sprite normal;
    public Sprite common, uncommon, rare, legend;
}
