using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    
    //main character
    private int mainCharLevel;
    private int mainCharExperience;
    private int mainCharExperienceToNextLevel;

    //friend 1 
    private int friend1Level;
    private int friend1Experience;
    private int friend1ExperienceToNextLevel;

    //friend 2 
    private int friend2Level;
    private int friend2Experience;
    private int friend2ExperienceToNextLevel;

    //friend 3 
    private int friend3Level;
    private int friend3Experience;
    private int friend3ExperienceToNextLevel;

    PlayerStats mainCharStats;
    friend1Stats friend1CharStats;
    friend2Stats friend2CharStats;
    friend3Stats friend3CharStats;


    public void setCharacterStats(PlayerStats mainCharStats, friend1Stats friend1CharStats, friend2Stats friend2CharStats, friend3Stats friend3CharStats)
    {
        this.mainCharStats = mainCharStats;
        this.friend1CharStats = friend1CharStats;
        this.friend2CharStats = friend2CharStats;
        this.friend3CharStats = friend3CharStats;

        //mainChar
        mainCharLevel = mainCharStats.getsetPlayerLvl;
        mainCharExperience = mainCharStats.getsetPlayerXP;
        mainCharExperienceToNextLevel = mainCharStats.getsetXPToNextLevelup;

        //friend 1
        friend1Level = friend1CharStats.getsetLvl;
        friend1Experience = friend1CharStats.getsetExp;
        friend1ExperienceToNextLevel = friend1CharStats.getsetExpToNextLevel;

        //friend 2
        friend2Level = friend2CharStats.getsetLvl;
        friend2Experience = friend2CharStats.getsetExp;
        friend2ExperienceToNextLevel = friend2CharStats.getsetExpToNextLevel;

        //friend 3
        friend3Level = friend3CharStats.getsetLvl;
        friend3Experience = friend3CharStats.getsetExp;
        friend3ExperienceToNextLevel = friend3CharStats.getsetExpToNextLevel;
    }

    //LEVELING SYSTEM

    public void mainCharAddExperience(int amount)
    {
        mainCharExperience += amount;
        if(mainCharExperience >= mainCharExperienceToNextLevel)
        {
            mainCharLevel++;
            mainCharExperience -= mainCharExperienceToNextLevel;
        }
        setMainCharStats();
    }

    public void friend1AddExperience(int amount)
    {
        friend1Experience += amount;
        if(friend1Experience >= friend1ExperienceToNextLevel)
        {
            friend1Level++;
            friend1Experience -= friend1ExperienceToNextLevel;
        }
        setfriend1CharStats();
    }

    public void friend2AddExperience(int amount)
    {
        friend2Experience += amount;
        if(friend2Experience >= friend2ExperienceToNextLevel)
        {
            friend2Level++;
            friend2Experience -= friend2ExperienceToNextLevel;
        }
        setfriend2CharStats();
    }

    public void friend3AddExperience(int amount)
    {
        friend3Experience += amount;
        if(friend3Experience >= friend3ExperienceToNextLevel)
        {
            friend3Level++;
            friend3Experience -= friend3ExperienceToNextLevel;
        }
        setfriend3CharStats();
    }


    //SAVE TO CHARACTERS STATS SCRIPT

    private void setMainCharStats()
    {
        mainCharStats.getsetPlayerXP = mainCharExperience;
        mainCharStats.getsetPlayerLvl = mainCharLevel;
    }

    private void setfriend1CharStats()
    {
        friend1CharStats.getsetExp = friend1Experience;
        friend1CharStats.getsetLvl = friend1Level;
    }

    private void setfriend2CharStats()
    {
        friend2CharStats.getsetExp = friend2Experience;
        friend2CharStats.getsetLvl = friend2Level;
    }

    private void setfriend3CharStats()
    {
        friend3CharStats.getsetExp = friend3Experience;
        friend3CharStats.getsetLvl = friend3Level;
    }
}
