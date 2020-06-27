using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTime
{
    public enum DayPhases
    {
        Dawn,
        Day,
        Dusk,
        Night
    }

    public DayPhases getsetDayPhases
    {
        get { return (DayPhases) Enum.Parse(typeof(DayPhases), GameDatabaseStatic.getsetDayPhases, true); }
        set { GameDatabaseStatic.getsetDayPhases = value.ToString(); }
    }

    public float getsetCounter
    {
        get { return GameDatabaseStatic.getsetCounter; }
        set { GameDatabaseStatic.getsetCounter = value; }
    }

    public int getsetDays
    {
        get { return GameDatabaseStatic.getsetDays; }
        set { GameDatabaseStatic.getsetDays = value; }
    }

    public int getsetHours
    {
        get { return GameDatabaseStatic.getsetHours; }
        set { GameDatabaseStatic.getsetHours = value; }
    }

    public int getsetSeconds
    {
        get { return GameDatabaseStatic.getsetSeconds; }
        set { GameDatabaseStatic.getsetSeconds = value; }
    }

    public int getsetMinutes
    {
        get { return GameDatabaseStatic.getsetMinutes; }
        set { GameDatabaseStatic.getsetMinutes = value; }
    }
}
