using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using System;

public class pauseMenuButtons : MonoBehaviour
{
    SoundManager sfx;


    PauseStateMenu pauseMenu;

    private void Awake()
    {
        this.pauseMenu = GameManager.instance.pauseState;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void statusButton()
    {
        pauseMenu.getsetPauseMenuState = PauseStateMenu.PauseMenuState.STATUS;
        sfx.playSelectPauseMenuType();
    }

    public void equipmentButton()
    {
        pauseMenu.getsetPauseMenuState = PauseStateMenu.PauseMenuState.EQUIPMENT;
        sfx.playSelectPauseMenuType();
    }

}
