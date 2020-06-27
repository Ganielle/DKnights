using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public string levelName;

    SceneChecker sceneChecker;


    public void setOOPScript(SceneChecker sceneChecker)
    {
        this.sceneChecker = sceneChecker;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene(levelName);
            sceneChecker.getsetSceneName = levelName;

            
        }
    }
}
