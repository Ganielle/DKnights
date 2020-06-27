using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    //EVENT HANDLERS
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    public event EventHandler OnStatsChanged;

    //getter and setters
    //playerName
    public string getsetPlayerFName
    {
        get { return GameDatabaseStatic.getsetPlayerFName; }
        set { GameDatabaseStatic.getsetPlayerFName = value; }
    }
    public string getsetPlayerLName
    {
        get { return GameDatabaseStatic.getsetPlayerLName; }
        set { GameDatabaseStatic.getsetPlayerLName = value; }
    }

    //playerXp
    public int getsetPlayerXP
    {
        get { return GameDatabaseStatic.getsetPlayerXP; }
        set { GameDatabaseStatic.getsetPlayerXP = value; }
    }

    //level functions
    //playerXP Normalized
    public float getXPNormalized
    {
        get { return GameDatabaseStatic.getXPNormalized; }
    }

    //player level
    public int getsetPlayerLvl
    {
        get { return GameDatabaseStatic.getsetPlayerLvl; }
        set { GameDatabaseStatic.getsetPlayerLvl = value; }
    }

    //player XP to next level up
    public int getsetXPToNextLevelup
    {
        get { return GameDatabaseStatic.getsetXPToNextLevelup; }
        set { GameDatabaseStatic.getsetXPToNextLevelup = value; }
    }

    //player HP
    public int getsetPlayerHP
    {
        get { return GameDatabaseStatic.getsetPlayerHP; }
        set { GameDatabaseStatic.getsetPlayerHP = value; }
    }

    public int getsetPlayerMaxHP
    {
        get { return GameDatabaseStatic.getsetPlayerMaxHP; }
        set { GameDatabaseStatic.getsetPlayerMaxHP = value; }
    }

    public float getPlayerHPNormalized
    {
        get { return GameDatabaseStatic.getPlayerHPNormalized; }
    }

    //player MP
    public int getsetPlayerMP
    {
        get { return GameDatabaseStatic.getsetPlayerMP; }
        set { GameDatabaseStatic.getsetPlayerMP = value; }
    }
    public int getsetPlayerMaxMP
    {
        get { return GameDatabaseStatic.getsetPlayerMaxHP; }
        set { GameDatabaseStatic.getsetPlayerMaxHP = value; }
    }

    public float getPlayerMPNormalized
    {
        get { return GameDatabaseStatic.getPlayerMPNormalized; }
    }

    //status effects

    //Knowledge effect
    public int getsetKnowledgeEffect
    {
        get { return GameDatabaseStatic.getsetKnowledgeEffect; }
        set
        {
            GameDatabaseStatic.getsetKnowledgeEffect = value;
            OnStatsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    //Knowledge level
    public int getsetKnowledgeLevel
    {
        get { return GameDatabaseStatic.getsetKnowledgeLevel; }
        set
        {
            GameDatabaseStatic.getsetKnowledgeLevel = value;
            OnStatsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    //intelligence
    public int getsetIntelligence
    {
        get { return GameDatabaseStatic.getsetIntelligence; }
        set
        {
            GameDatabaseStatic.getsetIntelligence = value;
            OnStatsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}

