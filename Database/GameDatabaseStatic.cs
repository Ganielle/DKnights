using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDatabaseStatic
{
    #region GENDER CHECKER
    private static bool isBoy;

    public static bool getsetBoyGirlChecker
    {
        get { return isBoy; }
        set { isBoy = value; }
    }
    #endregion

    #region DAYTIME

    private static int days, hours, seconds, minutes;
    private static float counter;
    private static string dayPhases;

    public static string getsetDayPhases
    {
        get { return dayPhases; }
        set { dayPhases = value; }
    }

    public static float getsetCounter
    {
        get { return counter; }
        set { counter = value; }
    }

    public static int getsetDays
    {
        get { return days; }
        set { days = value; }
    }

    public static int getsetHours
    {
        get { return hours; }
        set { hours = value; }
    }

    public static int getsetSeconds
    {
        get { return seconds; }
        set { seconds = value; }
    }

    public static int getsetMinutes
    {
        get { return minutes; }
        set { minutes = value; }
    }

    #endregion

    #region SCENE MANAGER
    private static string nextSceneName, previousSceneName;
    private static bool isLoadingFinished;

    public static string getsetNextScene
    {
        get { return nextSceneName; }
        set { nextSceneName = value; }
    }

    public static bool getsetIsLoadingFinished
    {
        get { return isLoadingFinished; }
        set { isLoadingFinished = value; }
    }

    public static string getsetPreviousScene
    {
        get { return previousSceneName; }
        set { previousSceneName = value; }
    }
    #endregion

    #region PARTY MEMBERS

    private static List<partyMembers> partyList = new List<partyMembers>();

    public static void addParty(partyMembers partyMem)
    {
        partyList.Add(partyMem);
    }
    
    public static List<partyMembers> getPary()
    {
        return partyList;
    }

    #endregion

    #region MAIN PLAYER STATS

    private static string playerFName = "Ganielle", playerLName = "Kazuto";
    private static int playerXP = 0, xpToNextLevel = 100, playerLvl = 1, playerHP = 50, playerMP = 50;
    private static int knowledgeEffect = 10, knowledgeLevel = 15, intelligence = 15, air, water, earth, fire;
    private static int playerMaxHP = 50, playerMaxMP = 50;

    //normalized variables
    private static float playerXPNormalized = (float)playerXP / xpToNextLevel;
    private static float playerHPNormalized = (float)playerHP / playerMaxHP;
    private static float playerMPNormalized = (float)playerMP / playerMaxMP;

    public static string getsetPlayerFName
    {
        get { return playerFName; }
        set { playerFName = value; }
    }
    public static string getsetPlayerLName
    {
        get { return playerLName; }
        set { playerLName = value; }
    }

    //playerXp
    public static int getsetPlayerXP
    {
        get { return playerXP; }
        set { playerXP = value; }
    }

    //level functions
    //playerXP Normalized
    public static float getXPNormalized
    {
        get { return playerXPNormalized; }
    }

    //player level
    public static int getsetPlayerLvl
    {
        get { return playerLvl; }
        set { playerLvl = value; }
    }

    //player XP to next level up
    public static int getsetXPToNextLevelup
    {
        get { return xpToNextLevel; }
        set { xpToNextLevel = value; }
    }

    //player HP
    public static int getsetPlayerHP
    {
        get { return playerHP; }
        set { playerHP = value; }
    }

    public static int getsetPlayerMaxHP
    {
        get { return playerMaxHP; }
        set { playerMaxHP = value; }
    }

    public static float getPlayerHPNormalized
    {
        get { return playerHPNormalized; }
    }
    //player MP
    public static int getsetPlayerMP
    {
        get { return playerMP; }
        set { playerMP = value; }
    }
    public static int getsetPlayerMaxMP
    {
        get { return playerMaxMP; }
        set { playerMaxMP = value; }
    }

    public static float getPlayerMPNormalized
    {
        get { return playerMPNormalized; }
    }

    //status effects

    //Knowledge effect
    public static int getsetKnowledgeEffect
    {
        get { return knowledgeEffect; }
        set
        {
            knowledgeEffect = value;
        }
    }

    //Knowledge level
    public static int getsetKnowledgeLevel
    {
        get { return knowledgeLevel; }
        set
        {
            knowledgeLevel = value;
        }
    }

    //intelligence
    public static int getsetIntelligence
    {
        get { return intelligence; }
        set
        {
            intelligence = value;
        }
    }

    #endregion

    #region Irah Witz STATS

    private static string charFOneName = "Irah", charOneLName = "Witz";

    private static int charOneHp, charOneMaxHp, charOneMp, charOneMaxMp, charOneExp, charOneExpNextToLvl, charOneLvl;
    private static int charOneKnowledgeEffect, charOneKnowledgeLevel, charOneIntelligence;

    //Normalized Variable
    private static float charOneHpNormalized = (float) charOneHp / charOneMaxHp, charOneExpNormalized = (float) charOneExp / charOneExpNextToLvl,
        charOneMpNormalized = (float) charOneMp / charOneMaxMp;

    public static string getOneCharFname
    {
        get { return charFOneName; }
    }

    public static string getCharOneLName
    {
        get { return charOneLName; }
    }

    //HEALTH POINTS

    public static int getsetOneHp
    {
        get { return charOneHp; }
        set { charOneHp = value; }
    }

    public static int getsetOneMaxHp
    {
        get { return charOneMaxHp; }
        set { charOneMaxHp = value; }
    }

    public static float getOneHpNormalized
    {
        get { return charOneHpNormalized; }
    }

    //MANA POINTS

    public static int getsetOneMp
    {
        get { return charOneMp; }
        set { charOneMp = value; }
    }

    public static int getsetOneMaxMp
    {
        get { return charOneMaxMp; }
        set { charOneMaxMp = value; }
    }

    public static float getOneMpNormalized
    {
        get { return charOneMpNormalized; }
    }

    //EXPERIENCE
    public static int getsetOneExp
    {
        get { return charOneExp; }
        set { charOneExp = value; }
    }

    public static int getsetOneExpToNextLevel
    {
        get { return charOneExpNextToLvl; }
        set { charOneExpNextToLvl = value; }
    }

    public static float getOneExpNormalized
    {
        get { return charOneExpNormalized; }
    }

    //LEVEL 

    public static int getsetOneLvl
    {
        get { return charOneLvl; }
        set { charOneLvl = value; }
    }


    //STATUS 

    public static int getsetOneKnowledgeEffect
    {
        get { return charOneKnowledgeEffect; }
        set { charOneKnowledgeEffect = value; }
    }

    public static int getsetOneKnowledgeLevel
    {
        get { return charOneKnowledgeLevel; }
        set { charOneKnowledgeLevel = value; }
    }

    public static int getsetOneIntelligence
    {
        get { return charOneIntelligence; }
        set { charOneIntelligence = value; }
    }
    #endregion

    #region YUUKI BELLE STATS
    //STATS VARIABLE
    private static string charTwoFName = "Yuuki", charTwoLName = "Belle";

    private static int charTwoHp, charTwoMaxHp, charTwoMp, charTwoMaxMp, charTwoExp, charTwoExpNextToLvl, charTwoLvl;
    private static int charTwoKnowledgeEffect, charTwoKnowledgeLevel, charTwoIntelligence;

    //Normalized Variable
    private static float charTwoHpNormalized = (float) charTwoHp / charTwoMaxHp, 
        charTwoExpNormalized = (float) charTwoExp / charTwoExpNextToLvl, charTwoMpNormalized = (float) charTwoMp / charTwoMaxMp;

    public static string getCharTwoFname
    {
        get { return charTwoFName; }
    }

    public static string getCharTwoLName
    {
        get { return charTwoLName; }
    }

    //HEALTH POINTS

    public static int getsetTwoHp
    {
        get { return charTwoHp; }
        set { charTwoHp = value; }
    }

    public static int getsetTwoMaxHp
    {
        get { return charTwoMaxHp; }
        set { charTwoMaxHp = value; }
    }

    public static float getTwoHpNormalized
    {
        get { return charTwoHpNormalized; }
    }

    //MANA POINTS

    public static int getsetTwoMp
    {
        get { return charTwoMp; }
        set { charTwoMp = value; }
    }

    public static int getsetTwoMaxMp
    {
        get { return charTwoMaxMp; }
        set { charTwoMaxMp = value; }
    }

    public static float getTwoMpNormalized
    {
        get { return charTwoMpNormalized; }
    }

    //EXPERIENCE
    public static int getsetTwoExp
    {
        get { return charTwoExp; }
        set { charTwoExp = value; }
    }

    public static int getsetTwoExpToNextLevel
    {
        get { return charTwoExpNextToLvl; }
        set { charTwoExpNextToLvl = value; }
    }

    public static float getTwoExpNormalized
    {
        get { return charTwoExpNormalized; }
    }

    //LEVEL 

    public static int getsetTwoLvl
    {
        get { return charTwoLvl; }
        set { charTwoLvl = value; }
    }


    //STATUS 

    public static int getsetTwoKnowledgeEffect
    {
        get { return charTwoKnowledgeEffect; }
        set { charTwoKnowledgeEffect = value; }
    }

    public static int getsetTwoKnowledgeLevel
    {
        get { return charTwoKnowledgeLevel; }
        set { charTwoKnowledgeLevel = value; }
    }

    public static int getsetTwoIntelligence
    {
        get { return charTwoIntelligence; }
        set { charTwoIntelligence = value; }
    }
    #endregion

    #region REEVE OWUN
    private static string charThreeFName = "Reeve", charThreeLName = "OWUN";

    private static int charThreeHp, charThreeMaxHp, charThreeMp, charThreeMaxMp, charThreeExp, charThreeExpNextToLvl, charThreeLvl;
    private static int charThreeKnowledgeEffect, charThreeKnowledgeLevel, charThreeIntelligence;

    //Normalized Variable
    private static float charThreeHpNormalized = (float)charThreeHp / charThreeMaxHp,
        charThreeExpNormalized = (float)charThreeExp / charThreeExpNextToLvl, charThreeMpNormalized = (float)charThreeMp / charThreeMaxMp;

    public static string getCharThreeFname
    {
        get { return charThreeFName; }
    }

    public static string getCharThreeLName
    {
        get { return charThreeLName; }
    }

    //HEALTH POINTS

    public static int getsetThreeHp
    {
        get { return charThreeHp; }
        set { charThreeHp = value; }
    }

    public static int getsetThreeMaxHp
    {
        get { return charThreeMaxHp; }
        set { charThreeMaxHp = value; }
    }

    public static float getThreeHpNormalized
    {
        get { return charThreeHpNormalized; }
    }

    //MANA POINTS

    public static int getsetThreeMp
    {
        get { return charThreeMp; }
        set { charThreeMp = value; }
    }

    public static int getsetThreeMaxMp
    {
        get { return charThreeMaxMp; }
        set { charThreeMaxMp = value; }
    }

    public static float getThreeMpNormalized
    {
        get { return charThreeMpNormalized; }
    }

    //EXPERIENCE
    public static int getsetThreeExp
    {
        get { return charThreeExp; }
        set { charThreeExp = value; }
    }

    public static int getsetThreeExpToNextLevel
    {
        get { return charThreeExpNextToLvl; }
        set { charThreeExpNextToLvl = value; }
    }

    public static float getThreeExpNormalized
    {
        get { return charThreeExpNormalized; }
    }

    //LEVEL 

    public static int getsetThreeLvl
    {
        get { return charThreeLvl; }
        set { charThreeLvl = value; }
    }


    //STATUS 

    public static int getsetThreeKnowledgeEffect
    {
        get { return charThreeKnowledgeEffect; }
        set { charThreeKnowledgeEffect = value; }
    }

    public static int getsetThreeKnowledgeLevel
    {
        get { return charThreeKnowledgeLevel; }
        set { charThreeKnowledgeLevel = value; }
    }

    public static int getsetThreeIntelligence
    {
        get { return charThreeIntelligence; }
        set { charThreeIntelligence = value; }
    }
    #endregion

    #region INVENTORY
    private static List<ItemData> itemStaffList = new List<ItemData>(), 
        itemWandList = new List<ItemData>(),
        itemBookList = new List<ItemData>(), 
        itemOrbList = new List<ItemData>(),
        itemDefenseList = new List<ItemData>(),
        itemAccessoryList = new List<ItemData>(),
        itemConsumableList = new List<ItemData>(),
        keyItemsList = new List<ItemData>();
    private static List<ItemData> questItemList = new List<ItemData>();
    private static float money;



    public static void addItemStaff(ItemData item)
    {
        itemStaffList.Add(item);
    }

    public static void addItemWand(ItemData item)
    {
        itemWandList.Add(item);
    }

    public static void addItemBook(ItemData item)
    {
        itemBookList.Add(item);
    }

    public static void addItemOrb(ItemData item)
    {
        itemOrbList.Add(item);
    }

    public static void addItemDefense(ItemData item)
    {
        itemDefenseList.Add(item);
    }

    public static void addItemAccessory(ItemData item)
    {
        itemAccessoryList.Add(item);
    }

    public static void addItemConsumable(ItemData item)
    {
        itemConsumableList.Add(item);
    }

    public static void addKeyItem(ItemData item)
    {
        keyItemsList.Add(item);
    }

    public static void addQuestItem(ItemData item)
    {
        questItemList.Add(item);
    }

    public static void addMoney(float item)
    {
        money = item;
    }

    public static List<ItemData> GetItemStaffList()
    {
        return itemStaffList;
    }

    public static List<ItemData> GetItemWandList()
    {
        return itemWandList;
    }

    public static List<ItemData> GetItemBookList()
    {
        return itemBookList;
    }

    public static List<ItemData> GetItemOrbList()
    {
        return itemOrbList;
    }

    public static List<ItemData> GetItemDefenseList()
    {
        return itemDefenseList;
    }

    public static List<ItemData> GetItemAccessoryList()
    {
        return itemAccessoryList;
    }

    public static List<ItemData> GetItemConsumableList()
    {
        return itemConsumableList;
    }

    public static List<ItemData> GetKeyItemsList()
    {
        return keyItemsList;
    }

    public static List<ItemData> GetQuestItems()
    {
        return questItemList;
    }

    public static float GetMoney()
    {
        return money;
    }
    #endregion

    #region MAIN PLAYER EQUIPMENT

    private static ItemData weaponItem;
    private static ItemData armorItem;
    private static ItemData accessoryItem;

    public static ItemData getWeaponItem()
    {
        return weaponItem;
    }

    public static ItemData getArmorItem()
    {
        return armorItem;
    }

    public static ItemData getAccessoryItem()
    {
        return accessoryItem;
    }

    public static void setWeaponItem(ItemData item)
    {
        weaponItem = item;
    }

    public static void setArmorItem(ItemData item)
    {
        armorItem = item;
    }

    public static void setAccessoryItem(ItemData item)
    {
        accessoryItem = item;
    }
    #endregion

    #region IRAH WITZ EQUIPMENT

    private static ItemData OneweaponItem;
    private static ItemData OnearmorItem;
    private static ItemData OneaccessoryItem;

    public static ItemData getOneWeaponItem()
    {
        return OneweaponItem;
    }

    public static ItemData getOneArmorItem()
    {
        return OnearmorItem;
    }

    public static ItemData getOneAccessoryItem()
    {
        return OneaccessoryItem;
    }

    public static void setOneWeaponItem(ItemData item)
    {
        OneweaponItem = item;
    }

    public static void setOneArmorItem(ItemData item)
    {
        OnearmorItem = item;
    }

    public static void setOneAccessoryItem(ItemData item)
    {
        OneaccessoryItem = item;
    }
    #endregion

    #region YUUKI BELLE EQUIPMENT
    private static ItemData TwoweaponItem;
    private static ItemData TwoarmorItem;
    private static ItemData TwoaccessoryItem;

    public static ItemData getTwoWeaponItem()
    {
        return TwoweaponItem;
    }

    public static ItemData getTwoArmorItem()
    {
        return TwoarmorItem;
    }

    public static ItemData getTwoAccessoryItem()
    {
        return TwoaccessoryItem;
    }

    public static void setTwoWeaponItem(ItemData item)
    {
        TwoweaponItem = item;
    }

    public static void setTwoArmorItem(ItemData item)
    {
        TwoarmorItem = item;
    }

    public static void setTwoAccessoryItem(ItemData item)
    {
        TwoaccessoryItem = item;
    }
    #endregion

    #region REEVE OWUN EQUIPMENT
    private static ItemData ThreeweaponItem;
    private static ItemData ThreearmorItem;
    private static ItemData ThreeaccessoryItem;

    public static ItemData getThreeWeaponItem()
    {
        return ThreeweaponItem;
    }

    public static ItemData getThreeArmorItem()
    {
        return ThreearmorItem;
    }

    public static ItemData getThreeAccessoryItem()
    {
        return ThreeaccessoryItem;
    }

    public static void setThreeWeaponItem(ItemData item)
    {
        ThreeweaponItem = item;
    }

    public static void setThreeArmorItem(ItemData item)
    {
        ThreearmorItem = item;
    }

    public static void setThreeAccessoryItem(ItemData item)
    {
        ThreeaccessoryItem = item;
    }
    #endregion
}
