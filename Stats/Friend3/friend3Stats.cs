using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class friend3Stats
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    #region NAME
    public string getCharFname
    {
        get { return GameDatabaseStatic.getCharThreeLName; }
    }

    public string getCharLName
    {
        get { return GameDatabaseStatic.getCharThreeLName; }
    }
    #endregion

    #region HEALTH
    //HEALTH POINTS

    public int getsetHp
    {
        get { return GameDatabaseStatic.getsetThreeHp; }
        set { GameDatabaseStatic.getsetThreeHp = value; }
    }

    public int getsetMaxHp
    {
        get { return GameDatabaseStatic.getsetThreeMaxHp; }
        set { GameDatabaseStatic.getsetThreeMaxHp = value; }
    }

    public float getHpNormalized
    {
        get { return GameDatabaseStatic.getThreeHpNormalized; }
    }
    #endregion

    #region MANA
    //MANA POINTS

    public int getsetMp
    {
        get { return GameDatabaseStatic.getsetThreeMaxMp; }
        set { GameDatabaseStatic.getsetThreeMaxMp = value; }
    }

    public int getsetMaxMp
    {
        get { return GameDatabaseStatic.getsetThreeMaxMp; }
        set { GameDatabaseStatic.getsetThreeMaxMp = value; }
    }

    public float getMpNormalized
    {
        get { return GameDatabaseStatic.getThreeMpNormalized; }
    }
    #endregion

    #region EXPERIENCE
    //EXPERIENCE
    public int getsetExp
    {
        get { return GameDatabaseStatic.getsetThreeExp; }
        set { GameDatabaseStatic.getsetThreeExp = value; }
    }

    public int getsetExpToNextLevel
    {
        get { return GameDatabaseStatic.getsetThreeExpToNextLevel; }
        set { GameDatabaseStatic.getsetThreeExpToNextLevel = value; }
    }

    public float getExpNormalized
    {
        get { return GameDatabaseStatic.getThreeExpNormalized; }
    }
    #endregion

    #region LEVEL
    //LEVEL 

    public int getsetLvl
    {
        get { return GameDatabaseStatic.getsetThreeLvl; }
        set { GameDatabaseStatic.getsetThreeLvl = value; }
    }
    #endregion

    #region STATUS
    //STATUS 

    public int getsetKnowledgeEffect
    {
        get { return GameDatabaseStatic.getsetThreeKnowledgeEffect; }
        set { GameDatabaseStatic.getsetThreeKnowledgeEffect = value; }
    }

    public int getsetKnowledgeLevel
    {
        get { return GameDatabaseStatic.getsetThreeKnowledgeLevel; }
        set { GameDatabaseStatic.getsetThreeKnowledgeLevel = value; }
    }

    public int getsetIntelligence
    {
        get { return GameDatabaseStatic.getsetThreeIntelligence; }
        set { GameDatabaseStatic.getsetThreeIntelligence = value; }
    }
    #endregion
}
