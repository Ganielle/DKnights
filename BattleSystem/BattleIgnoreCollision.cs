using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleIgnoreCollision : MonoBehaviour
{
    
    private void Start()
    {
        Physics.IgnoreLayerCollision(16,11);
    }
}
