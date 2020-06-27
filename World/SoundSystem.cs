using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField]private AudioClip openPause;
    [SerializeField]private AudioClip closePause;
    [SerializeField]private AudioClip chooseButtons;
    [SerializeField]private AudioClip chooseBackButtons;
    [SerializeField] private AudioClip equipButton, unequipButton, equipmentSelectButton;
    [SerializeField] private AudioClip pickUpItem;

    [Header("Audio Sources")]
    [SerializeField]private AudioSource ButtonSounds;
    [SerializeField] private AudioSource sfx;

    public void chooseButtonsSfx()
    {
        ButtonSounds.PlayOneShot(chooseButtons);
    }

    public void chooseBackButtonsSfx()
    {
        ButtonSounds.PlayOneShot(chooseBackButtons);
    }

    public void openMenuSfx()
    {
        ButtonSounds.PlayOneShot(openPause);
    }
    public void closeMenuSfx()
    {
        ButtonSounds.PlayOneShot(closePause);
    }

    public void equipButtonSfx() 
    {
        ButtonSounds.PlayOneShot(equipButton);
    }

    public void unequipButtonSfx() 
    {
        ButtonSounds.PlayOneShot(unequipButton);
    }

    public void equipmentButtonSfx() 
    {
        ButtonSounds.PlayOneShot(equipmentSelectButton);
    }

    public void pickupItemSfx()
    {
        sfx.PlayOneShot(pickUpItem);
    }

}
