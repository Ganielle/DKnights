﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{
    private Transform camTransform;

    Quaternion originalRotation;

    void Start()
    {
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        originalRotation.y = transform.rotation.y;
    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
