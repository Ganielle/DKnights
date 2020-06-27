using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenubgController : MonoBehaviour
{
    
    public List<GameObject> Buttons;
    public GameObject touch,touchScreen;
    public Animator animator;
    GameObject playButtons;
    public AudioClip touchMe;
    public AudioSource audioS;


    public void showButtons(){
        foreach (GameObject gObject in Buttons)
        {
            gObject.SetActive(true);
        }
    }

    public void touchTheScreen(){
        audioS.PlayOneShot(touchMe);
        animator.SetBool("isDisableTouchTheScreen", true);
    }

    public void disableTouchTheScreen(){
        touchScreen.SetActive(false);
    }

    public void enableTouchTheScreen(){
        touch.SetActive(true);
    }

}
