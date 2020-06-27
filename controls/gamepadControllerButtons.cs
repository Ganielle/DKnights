using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamepadControllerButtons : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField]private GameObject pauseMenu;

    [HideInInspector]public bool gameIsPaused;

    SoundManager sfx;

    PauseStateMenu pauseM;

    private void Awake()
    {
        this.pauseM = GameManager.instance.pauseState;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void showPauseMenu()
    {
        pauseM.getsetPauseMenuState = PauseStateMenu.PauseMenuState.PAUSEMENU;
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
        sfx.playOpenPauseMenu();
        Time.timeScale = 0f;
    }

}
