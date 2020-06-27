using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LevelSystemAnimator
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    private PlayerStats stats;
    private bool isAnimating;

    private int level;
    private int experience;
    private int experienceToNextLevel;

    private void Update()
    {
        if (isAnimating)
        {
            if(level < stats.getsetPlayerLvl)
            {
                AddExperience();
            }
            else
            {
                if(experience < stats.getsetPlayerXP)
                {
                    AddExperience();
                }
                else
                {
                    isAnimating = false;
                }
            }
        }
    }

    public LevelSystemAnimator(PlayerStats stats)
    {
        SetPlayerStats(stats);
        FunctionUpdater.Create(() => Update());
    }

    public void SetPlayerStats(PlayerStats stats)
    {
        this.stats = stats;

        level = stats.getsetPlayerLvl;
        experience = stats.getsetPlayerXP;
        experienceToNextLevel = stats.getsetXPToNextLevelup;

        stats.OnExperienceChanged += PlayerStats_OnExperienceChanged;
        stats.OnLevelChanged += PlayerStats_OnLevelChanged;
    }

    private void PlayerStats_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }
    private void PlayerStats_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void AddExperience()
    {
        experience++;
        if(experience >= stats.getsetXPToNextLevelup)
        {
            level ++;
            experience = 0;
            if(OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if(OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetExperienceInt()
        {
            return experience;
        }

        public int GetExpToLevelUp()
        {
            return experienceToNextLevel;
        }

        public float GetExperienceNormalized()
        {
            return (float)experience / stats.getsetXPToNextLevelup;
        }

        public int GetLevelNumber()
        {
            return level;
        }
}
