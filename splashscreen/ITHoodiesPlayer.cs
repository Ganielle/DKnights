using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ITHoodiesPlayer : MonoBehaviour
{
    public float waitforsec = 1.0f;
    public RawImage imagePlayer;
    public VideoPlayer player;
    public AudioSource audiosource;
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(PlayVideo());
        
    }

    private IEnumerator PlayVideo()
    {
        WaitForSeconds seconds = new WaitForSeconds(waitforsec);
        player.Prepare();
        while (!player.isPrepared)
        {
            yield return seconds;
            break;
        }

        imagePlayer.texture = player.texture;
        player.Play();
        audiosource.Play();

        yield return null;
    }
}
