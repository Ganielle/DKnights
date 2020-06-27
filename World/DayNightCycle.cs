using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Light directionalLight;
    [SerializeField] float timeSpeed = 1.0f;

    [Header("Time")]
    [SerializeField] int days;
    [SerializeField] int seconds;
    [SerializeField] int minutes;
    [SerializeField] int hours;

    [Header("Day States Start Time")]
    [SerializeField] int dawnStartTime = 6;
    [SerializeField] int dayStartTime = 8;
    [SerializeField] int duskStartTime = 18;
    [SerializeField] int nightStartTime = 20;

    [Header("Sun Intensity Settings")]

    [SerializeField] float sunDimTime = .01f;
    [SerializeField] float dawnSunIntensity = .5f;
    [SerializeField] float daySunIntensity = 1f;
    [SerializeField] float duskSunIntensity = .25f;
    [SerializeField] float nightSunIntensity = 0f;

    [Header("Ambient Settings")]
    [SerializeField] float ambientDimTime = 0.0001f;
    [SerializeField] float ambientDawnTimeIntensity = 0.5f;
    [SerializeField] float ambientDayTimeIntensity = 1f;
    [SerializeField] float ambientDuskDimTime = 0.25f;
    [SerializeField] float ambientNightDimTime = 0f;

    [Header("Skybox Blend Settings")]
    [SerializeField] float dawnSkyboxBlendFactor = 0.5f;
    [SerializeField] float daySkyboxBlendFactor = 1f;
    [SerializeField] float duskSkyboxBlendFactor = 0.25f;
    [SerializeField] float nightSkyboxBlendFactor = 0f;
    [SerializeField] float skyboxBlendFactor;
    [SerializeField] float skyboxBlendSpeed = 0.01f;

    DayTime dayTime;

    private void Awake()
    {
        this.dayTime = GameManager.instance.dayTime;

        dayTime.getsetDayPhases = DayTime.DayPhases.Night;

        directionalLight.intensity = nightSunIntensity;
        RenderSettings.ambientIntensity = ambientNightDimTime;
    }

    void Start(){
        days = GameManager.instance.dayTime.getsetDays;
        seconds = GameManager.instance.dayTime.getsetSeconds;
        minutes = GameManager.instance.dayTime.getsetMinutes;
        hours = GameManager.instance.dayTime.getsetHours;

        RenderSettings.skybox.SetFloat("_Blend", skyboxBlendFactor);

        StartCoroutine("TimeOfDayFiniteStateMachine");
    }

    void Update(){
        secondsCounter();
        UpdateSkybox();
    }

    IEnumerator TimeOfDayFiniteStateMachine(){
        while(true){
            switch(dayTime.getsetDayPhases)
            {
                case DayTime.DayPhases.Dawn:
                    Dawn();
                    break;
                case DayTime.DayPhases.Day:
                    Day();
                    break;
                case DayTime.DayPhases.Dusk:
                    Dusk();
                    break;
                case DayTime.DayPhases.Night:
                    Night();
                    break;
            }
            yield return null;
        }
    }

    void secondsCounter()
    {
        
        if(dayTime.getsetCounter == 60)
            dayTime.getsetCounter = 0;

        dayTime.getsetCounter += timeSpeed * Time.deltaTime;
        seconds = (int)dayTime.getsetCounter;
        dayTime.getsetSeconds = seconds;

        if (dayTime.getsetCounter < 60)
            return;
        
        if(dayTime.getsetCounter > 60)
            dayTime.getsetCounter = 60;

        if(dayTime.getsetCounter == 60)
            minutesCounter();
    }
    
    void minutesCounter()
    {

        minutes++;
        dayTime.getsetMinutes = minutes;

        if (minutes == 60)
        {
            hoursCounter();
            minutes = 0;
            dayTime.getsetMinutes = minutes;
        }
    }

    void hoursCounter()
    {

        hours++;
        dayTime.getsetHours = hours;

        if (dayTime.getsetHours == 24){
            dayCounter();
            hours = 0;
            dayTime.getsetHours = hours;
        }
    }

    void dayCounter(){
        days++;
        dayTime.getsetDays = days;
    }

    void Dawn(){
        if (directionalLight.intensity < dawnSunIntensity)
            directionalLight.intensity += sunDimTime * Time.unscaledDeltaTime;

        if (directionalLight.intensity > dawnSunIntensity)
            directionalLight.intensity = dawnSunIntensity;

        if (RenderSettings.ambientIntensity < ambientDawnTimeIntensity)
            RenderSettings.ambientIntensity += ambientDimTime * Time.unscaledDeltaTime;

        if (RenderSettings.ambientIntensity > ambientDawnTimeIntensity)
            RenderSettings.ambientIntensity = ambientDawnTimeIntensity;

        if (dayTime.getsetHours == dayStartTime && dayTime.getsetHours < duskStartTime){
            dayTime.getsetDayPhases = DayTime.DayPhases.Day;
        }
    }

    void Day(){
        if (directionalLight.intensity < daySunIntensity)
            directionalLight.intensity += sunDimTime * Time.unscaledDeltaTime;

        if (directionalLight.intensity > daySunIntensity)
            directionalLight.intensity = daySunIntensity;

        if (RenderSettings.ambientIntensity < ambientDayTimeIntensity)
            RenderSettings.ambientIntensity += ambientDimTime * Time.unscaledDeltaTime;

        if (RenderSettings.ambientIntensity > ambientDayTimeIntensity)
            RenderSettings.ambientIntensity = ambientDayTimeIntensity;

        if (dayTime.getsetHours == duskStartTime && dayTime.getsetHours < nightStartTime){
            dayTime.getsetDayPhases = DayTime.DayPhases.Dusk;
        }
    }

    void Dusk(){
        if (directionalLight.intensity < duskSunIntensity)
            directionalLight.intensity = duskSunIntensity;

        if (directionalLight.intensity > duskSunIntensity)
            directionalLight.intensity -= sunDimTime * Time.unscaledDeltaTime;

        if (RenderSettings.ambientIntensity > ambientDuskDimTime)
            RenderSettings.ambientIntensity -= ambientDimTime * Time.unscaledDeltaTime;

        if (RenderSettings.ambientIntensity < ambientDuskDimTime)
            RenderSettings.ambientIntensity = ambientDuskDimTime;

        if (dayTime.getsetHours == nightStartTime){
            dayTime.getsetDayPhases = DayTime.DayPhases.Night;
        }
    }

    void Night(){
        if (directionalLight.intensity < nightSunIntensity)
            directionalLight.intensity = nightSunIntensity;

        if (directionalLight.intensity > nightSunIntensity)
            directionalLight.intensity -= sunDimTime* Time.unscaledDeltaTime; 

        if (RenderSettings.ambientIntensity > ambientNightDimTime)
            RenderSettings.ambientIntensity -= ambientDimTime * Time.unscaledDeltaTime;

        if (RenderSettings.ambientIntensity < ambientNightDimTime)
            RenderSettings.ambientIntensity = ambientNightDimTime;

        if (dayTime.getsetHours == dawnStartTime && dayTime.getsetHours < dayStartTime){
            dayTime.getsetDayPhases = DayTime.DayPhases.Dawn;
        }
    }

    private void UpdateSkybox()
    {
        if(dayTime.getsetDayPhases == DayTime.DayPhases.Dawn)
        {
            if (skyboxBlendFactor == dawnSkyboxBlendFactor)
                return;

            skyboxBlendFactor += skyboxBlendSpeed * Time.unscaledDeltaTime;

            if (skyboxBlendFactor > dawnSkyboxBlendFactor)
                skyboxBlendFactor = dawnSkyboxBlendFactor;
        }
        else if (dayTime.getsetDayPhases == DayTime.DayPhases.Day)
        {
            if (skyboxBlendFactor == daySkyboxBlendFactor)
                return;

            skyboxBlendFactor += skyboxBlendSpeed * Time.unscaledDeltaTime;

            if (skyboxBlendFactor > daySkyboxBlendFactor)
                skyboxBlendFactor = daySkyboxBlendFactor;
        }
        else if (dayTime.getsetDayPhases == DayTime.DayPhases.Dusk)
        {
            if (skyboxBlendFactor == duskSkyboxBlendFactor)
                return;

            skyboxBlendFactor -= skyboxBlendSpeed * Time.unscaledDeltaTime;

            if (skyboxBlendFactor < duskSkyboxBlendFactor)
                skyboxBlendFactor = duskSkyboxBlendFactor;
        }
        else if (dayTime.getsetDayPhases == DayTime.DayPhases.Night)
        {
            if (skyboxBlendFactor == nightSkyboxBlendFactor)
                return;

            skyboxBlendFactor -= skyboxBlendSpeed * Time.unscaledDeltaTime;

            if (skyboxBlendFactor < nightSkyboxBlendFactor)
                skyboxBlendFactor = nightSkyboxBlendFactor;
        }

        RenderSettings.skybox.SetFloat("_Blend", skyboxBlendFactor);
    }
}
