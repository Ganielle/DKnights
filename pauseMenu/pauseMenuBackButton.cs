using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenuBackButton : MonoBehaviour
{   
    [SerializeField]private GameObject gamepadController;
    
    SoundManager sfx;

    gamepadControllerButtons gamepad;

    RectTransform[] oldChildrenPauseMenuPos;
    RectTransform[] newChildrenPauseMenuPos;

    Vector3[] oldPauseMenuPos, oldPauseMenuScale;

    PauseStateMenu pauseMenu;

    private void Awake()
    {
        this.pauseMenu = GameManager.instance.pauseState;
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Start()
    {
        oldChildrenPauseMenuPos = GetComponentsInChildren<RectTransform>();
        newChildrenPauseMenuPos = GetComponentsInChildren<RectTransform>();
        gamepad = gamepadController.GetComponent<gamepadControllerButtons>();
        oldGUIPosPauseMenu();
    }

    //this is the cloase pause menu functions
    public void closePauseMenu()
    {
        if (pauseMenu.getsetPauseMenuState == PauseStateMenu.PauseMenuState.PAUSEMENU)
        {
            pauseMenu.getsetPauseMenuState = PauseStateMenu.PauseMenuState.NONE;
            newGUIPosPauseMenu();
            sfx.playClosePauseMenu();
            gamepadController.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void oldGUIPosPauseMenu()
    {
        for(int a = 0; a <= oldChildrenPauseMenuPos.Length; a++)
        {
            oldPauseMenuPos = new Vector3[a];
            oldPauseMenuScale = new Vector3[a];
        }

        for(int a = 0; a < oldChildrenPauseMenuPos.Length; a++)
        {
            oldPauseMenuPos[a] = new Vector3(oldChildrenPauseMenuPos[a].localPosition.x,oldChildrenPauseMenuPos[a].localPosition.y,oldChildrenPauseMenuPos[a].localPosition.z);
            oldPauseMenuScale[a] = new Vector3(oldChildrenPauseMenuPos[a].localScale.x,oldChildrenPauseMenuPos[a].localScale.y,oldChildrenPauseMenuPos[a].localScale.z);
        }
    }

    private void newGUIPosPauseMenu()
    {
        for(int a = 0; a < newChildrenPauseMenuPos.Length; a++)
        {
            newChildrenPauseMenuPos[a].localPosition = new Vector3(oldPauseMenuPos[a].x,oldPauseMenuPos[a].y,oldPauseMenuPos[a].z);
            newChildrenPauseMenuPos[a].localScale = new Vector3(oldPauseMenuScale[a].x,oldPauseMenuScale[a].y,oldPauseMenuScale[a].z);
        }
    }
}
