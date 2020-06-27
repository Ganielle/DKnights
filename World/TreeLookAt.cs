using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLookAt : MonoBehaviour
{
    private Transform cameraTarget;
    Quaternion originalRotation;

    private void Start()
    {
        cameraTarget = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update(){
        transform.LookAt(transform.position + cameraTarget.transform.rotation * Vector3.left);
    }
}
