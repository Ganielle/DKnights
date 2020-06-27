using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDissolve : MonoBehaviour
{
    [SerializeField]private Material noDissolveMaterial;
    [SerializeField]private Material withDissolveMaterial;
    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("BattleField"))
        {
            GetComponent<Renderer>().material = withDissolveMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("BattleField"))
        {
            GetComponent<Renderer>().material = noDissolveMaterial;
        }
    }

}
