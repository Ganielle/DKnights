using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class SSControllerFinal : MonoBehaviour
{
    
    public GameObject ss, mainMenu;
    public List<GameObject> ssPlay;
    public AudioSource audiosource;
    VideoPlayer anim;

    public void playVideo(){
        foreach(GameObject go in ssPlay){
            anim = go.GetComponent<VideoPlayer>();
            anim.Play();
        }
    }
    public void playAudioSS(){
        audiosource.Play();
    }

    public void stopVideo(){
        foreach(GameObject go in ssPlay){
            anim = go.GetComponent<VideoPlayer>();
            anim.Stop();
        }
    }

    public void showMainMenu(){
        mainMenu.SetActive(true);
        ss.SetActive(false);
    }
}
