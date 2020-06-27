using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFarAway : MonoBehaviour
{
    
    [SerializeField] private GameObject itemActivatorObj = null;
    private ItemActivator activationScript = null;

    void Start()
    {

        itemActivatorObj = GameObject.FindGameObjectWithTag("Player");
        activationScript = itemActivatorObj.GetComponent<ItemActivator>();
        StartCoroutine("addToList");
    }

    IEnumerator addToList()
    {
        yield return new WaitForSeconds(0.1f);

        activationScript.activatorItems.Add(new ActivatorItem{ item = this.gameObject, itemPos = transform.position});
    }
}
