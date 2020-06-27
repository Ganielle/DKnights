using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class StatusWindow : MonoBehaviour
{
    [Header("Text Mesh Pro")]
    [SerializeField]private TextMeshProUGUI nameText;
    [SerializeField]private TextMeshProUGUI currentexpText;
    [SerializeField]private TextMeshProUGUI xpToNextLvlup;
    [SerializeField]private TextMeshProUGUI lvlText, playerHP, playerMP;
    [SerializeField]private TextMeshProUGUI kEffextText, kLevelText, intelligenceText;


    [Header("Slider")]
    [SerializeField]private Slider expSlider;
    [SerializeField]private Slider hpSlider,mpSlider;


    //Instantiate player stats & level system class
    PlayerStats stats;
    private LevelSystemAnimator levelSystemAnimator;

    private void OnEnable() => stats.OnStatsChanged += statsOnChanged;
    private void OnDisable() => stats.OnStatsChanged -= statsOnChanged;

    private void Awake()
    {
        this.stats = GameManager.instance.playerStats;
    }

    private void Start()
    {
        setPlayerStatus();
    }

    private void statsOnChanged(object sender, EventArgs e)
    {
        setPlayerStatus();
    }

    private void setPlayerStatus()
    {
        setNameText(stats.getsetPlayerFName + " " + stats.getsetPlayerLName);
        setHPText(stats.getsetPlayerHP);
        SetHpBar(stats.getPlayerHPNormalized);
        setMPText(stats.getsetPlayerMP);
        SetMpBar(stats.getPlayerMPNormalized);
        SetExperienceBar(stats.getXPNormalized);
        SetXpToNextLvlUp(stats.getsetXPToNextLevelup);
        SetLevelText(stats.getsetPlayerLvl);
        setStatusEffects(stats.getsetKnowledgeEffect, stats.getsetKnowledgeLevel, stats.getsetIntelligence);
        setCurrentXPText(stats.getsetPlayerXP);
    }

    private void setNameText(string name) => nameText.text = name;
    private void SetHpBar(float hpNormalize) => hpSlider.value = hpNormalize;
    private void SetMpBar(float mpNormalize) => mpSlider.value = mpNormalize;
    private void SetExperienceBar(float expNormalize) => expSlider.value = expNormalize;
    private void setCurrentXPText(int xpnum) => currentexpText.text = xpnum.ToString();
    private void SetLevelText(int lvlnum) => lvlText.text = lvlnum.ToString();
    private void SetXpToNextLvlUp(int nextXp) => xpToNextLvlup.text = nextXp.ToString();
    private void setHPText(int currentHP) => playerHP.text = currentHP.ToString();
    private void setMPText(int currentMP) => playerMP.text = currentMP.ToString();
    private void setStatusEffects(int kEffect, int kLevel, int intelligence)
    {
        kEffextText.text = kEffect.ToString();
        kLevelText.text = kLevel.ToString();
        intelligenceText.text = intelligence.ToString();
    }








    //LEVELING SYSTEM
    // public void SetLevelSystemAnimator(LevelSystemAnimator levelSystemAnimator)
    // {
    //     this.levelSystemAnimator = levelSystemAnimator;

    //     SetExperienceBar(levelSystemAnimator.GetExperienceNormalized());
    //     setCurrentXPText(levelSystemAnimator.GetExperienceInt());

    //     levelSystemAnimator.OnExperienceChanged += statsPlayer_OnExperienceChanged;
    //     levelSystemAnimator.OnLevelChanged += statsPlayer_OnLevelChanged;
    // }
    // private void statsPlayer_OnExperienceChanged(object sender, System.EventArgs e)
    // {
    //     stats.getsetXPNormalized = (float) stats.getsetPlayerXP / stats.getsetXPToNextLevelup;
    //     SetExperienceBar(levelSystemAnimator.GetExperienceNormalized());
    //     setCurrentXPText(levelSystemAnimator.GetExperienceInt());
    // }
    // private void statsPlayer_OnLevelChanged(object sender, System.EventArgs e)
    // {
    //     if(stats.getsetPlayerLvl <= 10)
    //     {
    //         stats.getsetXPToNextLevelup += 50;
    //     }
    //     else if(stats.getsetPlayerLvl > 10 && stats.getsetPlayerLvl <= 20)
    //     {
    //         stats.getsetXPToNextLevelup += 100;
    //     }
    //     else if(stats.getsetPlayerLvl > 20 && stats.getsetPlayerLvl <= 30)
    //     {
    //         stats.getsetXPToNextLevelup += 150;
    //     }
    //     else if(stats.getsetPlayerLvl > 30 && stats.getsetPlayerLvl <= 40)
    //     {
    //         stats.getsetXPToNextLevelup += 200;
    //     }
    //     else if(stats.getsetPlayerLvl > 40 && stats.getsetPlayerLvl <= 50)
    //     {
    //         stats.getsetXPToNextLevelup += 250;
    //     }
    //     else if(stats.getsetPlayerLvl > 50 && stats.getsetPlayerLvl <= 60)
    //     {
    //         stats.getsetXPToNextLevelup += 300;
    //     }
    //     else if(stats.getsetPlayerLvl > 60 && stats.getsetPlayerLvl <= 70)
    //     {
    //         stats.getsetXPToNextLevelup += 350;
    //     }
    //     else if(stats.getsetPlayerLvl > 70 && stats.getsetPlayerLvl <= 80)
    //     {
    //         stats.getsetXPToNextLevelup += 400;
    //     }
    //     else if(stats.getsetPlayerLvl > 80 && stats.getsetPlayerLvl <= 90)
    //     {
    //         stats.getsetXPToNextLevelup += 450;
    //     }
    //     else if(stats.getsetPlayerLvl > 90 && stats.getsetPlayerLvl <= 99)
    //     {
    //         stats.getsetXPToNextLevelup += 500;
    //     }
    //     SetLevelText(levelSystemAnimator.GetLevelNumber());
    //     SetXpToNextLvlUp(stats.getsetXPToNextLevelup);
    //     Debug.Log(stats.getsetPlayerLvl);
    // }

    // public void add5Xp()
    // {
    //     stats.AddExperience(5);
    //     // Debug.Log("Experience LevelSystem: " + levelSystem.GetExperienceInt() + " Experience StatsSystem:" + stats.getsetPlayerXP);
    // }
    // public void add50Xp()
    // {
    //     stats.AddExperience(50);
    //     // Debug.Log("Experience LevelSystem: " + levelSystem.GetExperienceInt() + " Experience StatsSystem:" + stats.getsetPlayerXP);
    // }
    // public void add500Xp()
    // {
    //     stats.AddExperience(500);
    //     // Debug.Log("Experience LevelSystem: " + levelSystem.GetExperienceInt() + " Experience StatsSystem:" + stats.getsetPlayerXP);
    // }
}
