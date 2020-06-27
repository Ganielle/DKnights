using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class setPlayerCameraGender : MonoBehaviour
{

    [Header("Game Object")]
    [SerializeField] private GameObject mainCharacter;

    checkGender isBoy;
    CinemachineFreeLook playerCamera;

    private void Awake()
    {
        
    }

    private void Start()
    {
        this.isBoy = GameManager.instance.gender;
        isBoy.genderOnChange += onGenderChange;

        playerCamera = GetComponent<CinemachineFreeLook>();
        setupCamera();
    }

    private void onGenderChange(object sender, EventArgs e)
    {
        setupCamera();
    }

    private void setupCamera()
    {
        if (isBoy.getsetBoyGirlChecker)
        {
            playerCamera.Follow = mainCharacter.transform.GetChild(1);
            playerCamera.LookAt = mainCharacter.transform.GetChild(1);
        }
        else
        {
            playerCamera.Follow = mainCharacter.transform.GetChild(2);
            playerCamera.LookAt = mainCharacter.transform.GetChild(2);
        }
    }
}
