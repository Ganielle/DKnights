using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuButtonAnim : MonoBehaviour
{
    public List<Animator> animButton;
    public List<GameObject> buttons;

    public void playAnim(){
        foreach(Animator animB in animButton){
            animB.enabled = true;
        }
    }
    public void stopAnim(){
        foreach(Animator animB in animButton){
            animB.enabled = false;
        }
    }
    public void activateButtons(){
        foreach(GameObject go in buttons){
            go.SetActive(true);
        }
    }
    public void deactivateButtons(){
        foreach(GameObject go in buttons){
            go.SetActive(false);
        }
    }
}
