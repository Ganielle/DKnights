using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private GameObject boy;
    [SerializeField] private GameObject girl;

    [Header("Animator")]
    [SerializeField] private Animator boyAnimator;
    [SerializeField] private Animator girlAnimator;


    playerMovement PlayerMovement;
    checkGender isBoy;

    private void Start()
    {
        this.isBoy = GameManager.instance.gender;
        this.isBoy.genderOnChange += onGenderChange;
        PlayerMovement = GetComponent<playerMovement>();
        genderChecker();
    }

    private void onGenderChange(object sender, EventArgs e)
    {
        genderChecker();
    }

    private void genderChecker()
    {
        if (isBoy.getsetBoyGirlChecker)
        {
            boy.SetActive(true);
            girl.SetActive(false);

            //set animator
            PlayerMovement.anim = boyAnimator;
        }
        else
        {
            boy.SetActive(false);
            girl.SetActive(true);

            //set animator
            PlayerMovement.anim = girlAnimator;
        }
    }
}
