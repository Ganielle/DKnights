    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicIconAsset : MonoBehaviour
{
    public static magicIconAsset Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [Header("Magic type icon sprites")]
    public Sprite normalAttack;
    public Sprite airAttack;
    public Sprite waterAttack;
    public Sprite earthAttack;
    public Sprite fireAttack;
    public Sprite manaHeal;
    public Sprite healthHeal;
}
