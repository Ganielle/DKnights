using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friend1Stats
{
    
    //STATS VARIABLE
    

    //Event Handlers
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    #region NAME
    public string getCharFname
    {
        get{ return GameDatabaseStatic.getCharOneLName; }
    }

    public string getCharLName
    {
        get{ return GameDatabaseStatic.getCharOneLName; }
    }
    #endregion

    #region HEALTH
    //HEALTH POINTS

    public int getsetHp
    {
        get{ return GameDatabaseStatic.getsetOneHp; }
        set{ GameDatabaseStatic.getsetOneHp = value; }
    }

    public int getsetMaxHp
    {
        get{ return GameDatabaseStatic.getsetOneMaxHp; }
        set{ GameDatabaseStatic.getsetOneMaxHp = value; }
    }

    public float getHpNormalized
    {
        get{ return GameDatabaseStatic.getOneHpNormalized; }
    }
    #endregion

    #region MANA
    //MANA POINTS

    public int getsetMp
    {
        get{ return GameDatabaseStatic.getsetOneMaxMp; }
        set{ GameDatabaseStatic.getsetOneMaxMp = value; }
    }

    public int getsetMaxMp
    {
        get{ return GameDatabaseStatic.getsetOneMaxMp; }
        set{ GameDatabaseStatic.getsetOneMaxMp = value; }
    }

    public float getMpNormalized
    {
        get{ return GameDatabaseStatic.getOneMpNormalized; }
    }
    #endregion

    #region EXPERIENCE
    //EXPERIENCE
    public int getsetExp
    {
        get{ return GameDatabaseStatic.getsetOneExp; }
        set{ GameDatabaseStatic.getsetOneExp = value; }
    }

    public int getsetExpToNextLevel
    {
        get{ return GameDatabaseStatic.getsetOneExpToNextLevel; }
        set{ GameDatabaseStatic.getsetOneExpToNextLevel = value; }
    }

    public float getExpNormalized
    {
        get{ return GameDatabaseStatic.getOneExpNormalized; }
    }
    #endregion

    #region LEVEL
    //LEVEL 

    public int getsetLvl
    {
        get{ return GameDatabaseStatic.getsetOneLvl; }
        set{ GameDatabaseStatic.getsetOneLvl = value; }
    }
    #endregion

    #region STATUS
    //STATUS 

    public int getsetKnowledgeEffect
    {
        get{ return GameDatabaseStatic.getsetOneKnowledgeEffect; }
        set{ GameDatabaseStatic.getsetOneKnowledgeEffect = value; }
    }

    public int getsetKnowledgeLevel
    {
        get{ return GameDatabaseStatic.getsetOneKnowledgeLevel; }
        set{ GameDatabaseStatic.getsetOneKnowledgeLevel = value; }
    }

    public int getsetIntelligence
    {
        get{ return GameDatabaseStatic.getsetOneIntelligence; }
        set{ GameDatabaseStatic.getsetOneIntelligence = value; }
    }
    #endregion
}
