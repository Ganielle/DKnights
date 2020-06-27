using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Teleporter : MonoBehaviour
{
    [Header("LOAD SCENE LEVEL")]
    [SerializeField] private string nextScene;

    [Header("Rect Transform")]
    [SerializeField] private RectTransform blackScreen;

    SceneChecker sceneChecker;

    private void Awake()
    {
        this.sceneChecker = GameManager.instance.sceneChecker;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            sceneChecker.getsetSceneName = nextScene;
            setActiveBlackScreen();
        }
    }


    private void setActiveBlackScreen()
    {
        blackScreen.gameObject.SetActive(true);
    }
}
