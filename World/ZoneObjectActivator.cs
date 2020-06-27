using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneObjectActivator : MonoBehaviour
{
    [SerializeField]private GameObject zoneObject;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            zoneObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            zoneObject.SetActive(false);
        }
    }
}
