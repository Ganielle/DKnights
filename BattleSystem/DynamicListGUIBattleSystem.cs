using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicListGUIBattleSystem : MonoBehaviour
{
    [SerializeField]private GameObject itemPrefab;

    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject newItems = Instantiate<GameObject>(itemPrefab, transform);
        }
    }
}
