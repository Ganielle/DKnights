using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneChecker
{
    public event EventHandler sceneNameChange;
    public event EventHandler loadingBoolChange;

    public string getsetSceneName
    {
        get { return GameDatabaseStatic.getsetNextScene; }
        set
        {
            GameDatabaseStatic.getsetNextScene = value;
            sceneNameChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public string getsetPreviousSceneName
    {
        get { return GameDatabaseStatic.getsetPreviousScene; }
        set { GameDatabaseStatic.getsetPreviousScene = value; }
    }

    public bool getsetIsFinishedLoading
    {
        get { return GameDatabaseStatic.getsetIsLoadingFinished; }
        set 
        {
            GameDatabaseStatic.getsetIsLoadingFinished = value;
            loadingBoolChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
