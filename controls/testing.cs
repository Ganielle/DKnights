using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    
private void OnCollisionEnter(Collision other) {
    
        if(other.gameObject.name == "Combined sixthEnvironmentBatch Mesh"){
            Debug.Log("hi");
        }
    }
}
