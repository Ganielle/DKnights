using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class friend2Stats
{


    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    #region NAME
    public string getCharFname
    {
        get { return GameDatabaseStatic.getCharTwoLName; }
    }

    public string getCharLName
    {
        get { return GameDatabaseStatic.getCharTwoLName; }
    }
    #endregion

    #region HEALTH
    //HEALTH POINTS

    public int getsetHp
    {
        get { return GameDatabaseStatic.getsetTwoHp; }
        set { GameDatabaseStatic.getsetTwoHp = value; }
    }

    public int getsetMaxHp
    {
        get { return GameDatabaseStatic.getsetTwoMaxHp; }
        set { GameDatabaseStatic.getsetTwoMaxHp = value; }
    }

    public float getHpNormalized
    {
        get { return GameDatabaseStatic.getTwoHpNormalized; }
    }
    #endregion

    #region MANA
    //MANA POINTS

    public int getsetMp
    {
        get { return GameDatabaseStatic.getsetTwoMaxMp; }
        set { GameDatabaseStatic.getsetTwoMaxMp = value; }
    }

    public int getsetMaxMp
    {
        get { return GameDatabaseStatic.getsetTwoMaxMp; }
        set { GameDatabaseStatic.getsetTwoMaxMp = value; }
    }

    public float getMpNormalized
    {
        get { return GameDatabaseStatic.getTwoMpNormalized; }
    }
    #endregion

    #region EXPERIENCE
    //EXPERIENCE
    public int getsetExp
    {
        get { return GameDatabaseStatic.getsetTwoExp; }
        set { GameDatabaseStatic.getsetTwoExp = value; }
    }

    public int getsetExpToNextLevel
    {
        get { return GameDatabaseStatic.getsetTwoExpToNextLevel; }
        set { GameDatabaseStatic.getsetTwoExpToNextLevel = value; }
    }

    public float getExpNormalized
    {
        get { return GameDatabaseStatic.getTwoExpNormalized; }
    }
    #endregion

    #region LEVEL
    //LEVEL 

    public int getsetLvl
    {
        get { return GameDatabaseStatic.getsetTwoLvl; }
        set { GameDatabaseStatic.getsetTwoLvl = value; }
    }
    #endregion

    #region STATUS
    //STATUS 

    public int getsetKnowledgeEffect
    {
        get { return GameDatabaseStatic.getsetTwoKnowledgeEffect; }
        set { GameDatabaseStatic.getsetTwoKnowledgeEffect = value; }
    }

    public int getsetKnowledgeLevel
    {
        get { return GameDatabaseStatic.getsetTwoKnowledgeLevel; }
        set { GameDatabaseStatic.getsetTwoKnowledgeLevel = value; }
    }

    public int getsetIntelligence
    {
        get { return GameDatabaseStatic.getsetTwoIntelligence; }
        set { GameDatabaseStatic.getsetTwoIntelligence = value; }
    }
    #endregion
}
