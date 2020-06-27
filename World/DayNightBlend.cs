using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightBlend : MonoBehaviour
{
    
    [SerializeField]private bool isDay;

    void Update()
    {
        if(isDay){
            RenderSettings.skybox.SetFloat("_Blend",1);
        }
        else{
            RenderSettings.skybox.SetFloat("_Blend",0);
        }
    }
}
