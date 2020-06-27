using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class checkGender
{
    public event EventHandler genderOnChange;

    public checkGender()
    {
        getsetBoyGirlChecker = true;
    }

    public bool getsetBoyGirlChecker
    {
        get { return GameDatabaseStatic.getsetBoyGirlChecker; }
        set 
        {
            GameDatabaseStatic.getsetBoyGirlChecker = value;
            genderOnChange?.Invoke(this, EventArgs.Empty);
        }
    } 
}
