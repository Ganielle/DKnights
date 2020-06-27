using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Video;

public class SSController : MonoBehaviour
{
    
    public GameObject ss,mainm,blacks,world;   
    public PlayableDirector titleCamera; 
    public List<GameObject> splashScreen;
    public AudioSource audiosource;
    VideoPlayer anim;
    
    public void titleCam(){
        titleCamera.Play();
    }

    public void playVideo(){
        foreach(GameObject go in splashScreen){
            anim = go.GetComponent<VideoPlayer>();
            anim.Play();
        }
    }

    public void playAudioSS(){
        audiosource.Play();
    }

    public void stopVideo(){
        foreach(GameObject go in splashScreen){
            anim = go.GetComponent<VideoPlayer>();
            anim.Stop();
        }
    }

    public void mainmenu(){
        ss.SetActive(false);
        blacks.SetActive(false);
    }

    public void activeWorld(){
        mainm.SetActive(true);
        world.SetActive(true);
    }

    
}
