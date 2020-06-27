using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAssets : MonoBehaviour
{
    public static characterAssets Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    [Header("Character Class Icon Sprite")]
    public Sprite mainCharacterEquipment;
    public Sprite friend1Equipment;
    public Sprite friend2Equipment;
    public Sprite friend3Equipment;

    [Header("Character battle gui sprite")]
    public Sprite mainCharacter;
    public Sprite IrahWitz;
    public Sprite YuukiBelle;
    public Sprite ReeveOwun;
}
