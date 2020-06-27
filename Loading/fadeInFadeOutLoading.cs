using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeInFadeOutLoading : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private RectTransform gameController;

    [Header("Animator")]
    [SerializeField] private Animator blackScreen;

    SceneChecker sceneChecker;

    private void Start()
    {
        this.sceneChecker = GameManager.instance.sceneChecker;
        sceneChecker.sceneNameChange += onSceneNameChange;
    }


    private void onSceneNameChange(object sender, EventArgs e)
    {
        fadeInfadeOut();
    }

    private void fadeInfadeOut()
    {
        gameController.gameObject.SetActive(false);
        blackScreen.SetTrigger("fadeIn");
    }

    public void showLoading()
    {
        GameManager.instance.LoadGame();
    }

    public void slowTime()
    {

        Time.timeScale = 0.5f;
    }

    public void returnTime()
    {
        Time.timeScale = 1f;
    }

    public void showGameController()
    {
        gameController.gameObject.SetActive(true);
    }
}
