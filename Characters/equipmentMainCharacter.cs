using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipmentMainCharacter : MonoBehaviour
{

    [Header("Game Objects")]
    [SerializeField] private GameObject mcBoy;
    [SerializeField] private GameObject mcGirl;

    checkGender isBoy;


    private void Start()
    {
        this.isBoy = GameManager.instance.gender;

        this.isBoy.genderOnChange += onGenderChange;

        enableMC();
    }

    private void onGenderChange(object sender, EventArgs e)
    {
        enableMC();
    }

    private void enableMC()
    {
        if (isBoy.getsetBoyGirlChecker)
        {
            mcBoy.SetActive(true);
            mcGirl.SetActive(false);
        }
        else
        {
            mcGirl.SetActive(true);
            mcBoy.SetActive(false);
        }
    }
}
