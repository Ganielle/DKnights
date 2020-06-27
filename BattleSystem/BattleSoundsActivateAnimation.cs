using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSoundsActivateAnimation : MonoBehaviour
{
    SoundManager sfx;

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void playCOKSound()
    {
        sfx.playCOKBannerSound();
    }

    public void playClickSound()
    {
        sfx.playClick();
    }

    public void playSelectButton()
    {
        sfx.playSelectEquipmentButton();
    }
}
