using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip forestBGMusic;
    [SerializeField] private AudioClip grasslandBattleBGMusic;

    [Header("Ambiance")]
    [SerializeField] private AudioClip forestAmbiance;

    [Header("Pause Menu")]
    [SerializeField] private AudioClip openMenu;
    [SerializeField] private AudioClip closeMenu;
    [SerializeField] private AudioClip pauseTypeSelect;
    [SerializeField] private AudioClip equipButton, unequipButton, selectEquipmentButton;
    [SerializeField] private AudioClip backButton;

    [Header("Battle System")]
    [SerializeField] private AudioClip click;

    [Header("Effects")]
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip COKBanner;

    [Header("BG MUSIC VALUES")]
    [SerializeField] private float forestBGMusicLoopTime;
    [SerializeField] private float forestBGMaxTime;
    [SerializeField] private float battleBGMusicLoopTime, battleBGMusicMaxTime;

    [Header("Values")]
    [SerializeField] private float changeMusicWaitTime;
    [SerializeField] private float changeBattleMusicWaitTime;
    [SerializeField] private float fadeSpeed;

    [Header("Audio Source")]
    [SerializeField] private AudioSource BGMusicSource;
    [SerializeField] private AudioSource ambianceSource;
    [SerializeField] private AudioSource buttonSource;
    [SerializeField] private AudioSource pickUpSource;
    [SerializeField] private AudioSource battleSystemSFXSource;

    BattleStates battleStates;
    PauseStateMenu pauseState;
    SceneChecker sceneChecker;

    private void Start()
    {
        this.battleStates = GameManager.instance.battleStates;
        this.pauseState = GameManager.instance.pauseState;
        this.sceneChecker = GameManager.instance.sceneChecker;

        sceneChecker.sceneNameChange += changeBGMusic;
        pauseState.pauseStateChange += onPauseStateChange;
        battleStates.battleStateChange += onBattleStateChange;

        ambianceSource.clip = forestAmbiance;
        BGMusicSource.clip = forestBGMusic;
        BGMusicSource.Play();
        ambianceSource.Play();
    }

    private void Update()
    {
        //Debug.Log(BGMusicSource.time);
    }

    private void LateUpdate()
    {
        BGMusicLooper();
    }

    #region EVENT

    private void onBattleStateChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
            StartCoroutine(musicChangeOnBattleStateChange());
    }
    private void onPauseStateChange(object sender, EventArgs e)
    {
        reduceVolumeOnPause();
    }
    private void changeBGMusic(object sender, EventArgs e)
    {
        //StartCoroutine(musicChangeOnSceneChange());
    }

    #endregion

    #region SOUND SETTINGS

    private void BGMusicLooper()
    {
        if ((sceneChecker.getsetSceneName == "SummoningForest" || sceneChecker.getsetSceneName == "Prototyping") &&
            battleStates.getsetBattleState == BattleStates.BattleState.NONE)
        {
            BGMusicTime(forestBGMaxTime, forestBGMusicLoopTime);
        }

        if(battleStates.getsetBattleState != BattleStates.BattleState.NONE)
        {
            BGMusicTime(battleBGMusicMaxTime, battleBGMusicLoopTime);
        }
    }
    private void BGMusicTime(float maxTime, float loopTime)
    {
        if (BGMusicSource.time >= maxTime)
        {
            BGMusicSource.time = Convert.ToInt32(loopTime);
            BGMusicSource.Play();
        }
    }
    private void reduceVolumeOnPause()
    {
        if (pauseState.getsetPauseMenuState == PauseStateMenu.PauseMenuState.NONE)
        {
            BGMusicSource.volume = 1.0f;
            return;
        }

        BGMusicSource.volume = 0.3f;
    }

    #endregion

    #region IENUMERATOR FUNCTIONS

    IEnumerator musicChangeOnSceneChange()
    {
        if (battleStates.getsetBattleState != BattleStates.BattleState.NONE)
            yield return null;

        yield return new WaitForSeconds(changeMusicWaitTime);
        if (sceneChecker.getsetSceneName == "Prototyping")
            BGMusicSource.clip = forestBGMusic;

        BGMusicSource.time = 0f;
        BGMusicSource.Play();
        StartCoroutine(StartFade(BGMusicSource, fadeSpeed, 1.0f));
        yield break;
    }

    IEnumerator musicChangeOnBattleStateChange()
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            StartCoroutine(musicChangeOnSceneChange());

        BGMusicSource.Stop();

        yield return new WaitForSeconds(changeBattleMusicWaitTime);
        if (sceneChecker.getsetSceneName == "Prototyping")
            BGMusicSource.clip = grasslandBattleBGMusic;

        BGMusicSource.time = 0f;
        BGMusicSource.Play();
        yield break;
    }
    IEnumerator StartFade(AudioSource source, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = source.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    #endregion

    #region PAUSE MENU

    public void playOpenPauseMenu()
    {
        buttonSource.PlayOneShot(openMenu);
    }
    public void playClosePauseMenu()
    {
        buttonSource.PlayOneShot(closeMenu);
    }
    public void playSelectPauseMenuType()
    {
        buttonSource.PlayOneShot(pauseTypeSelect);
    }
    public void playEquipButton()
    {
        buttonSource.PlayOneShot(equipButton);
    }
    public void playUnequipButton()
    {
        buttonSource.PlayOneShot(unequipButton);
    }
    public void playSelectEquipmentButton()
    {
        buttonSource.PlayOneShot(selectEquipmentButton);
    }
    public void playPauseTypeBackButton()
    {
        buttonSource.PlayOneShot(backButton);
    }

    #endregion

    #region BATTLE SYSTEM

    public void playCOKBannerSound()
    {
        battleSystemSFXSource.PlayOneShot(COKBanner);
    }

    public void playClick()
    {
        battleSystemSFXSource.PlayOneShot(click);
    }

    #endregion

    #region SOUND EFFECTS

    public void playPickUpItemSound()
    {
        pickUpSource.PlayOneShot(pickUpSound);
    }

    #endregion
}
