using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButtonController : MonoBehaviour
{
    public GameObject touch;
    public Animator animator;
    public AudioSource audioS;
    public AudioClip clip;
    public List<Animator> otherButtons;

    public void showTouch(){

        touch.SetActive(true);
    }

    public void touchSelected(){

        audioS.PlayOneShot(clip);
        animator.SetBool("isSelected",true);
        foreach(Animator anim in otherButtons){

            anim.SetBool("elseSelected",true);
        }
    }

    

}
