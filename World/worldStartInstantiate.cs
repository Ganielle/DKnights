using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class worldStartInstantiate : MonoBehaviour
{

    //[SerializeField]private playerMovement movementScript;
    //[SerializeField]public bool isBoy;
    //[SerializeField]private GameObject bPlayer,gPlayer,staff;
    //[SerializeField]private CinemachineFreeLook cineCam;
    //[SerializeField]private Animator bAnimator,gAnimator;

    //[SerializeField] private TextMeshProUGUI fpsCounter;

    //const float fpsMeasurePeriod = 0.5f;
    //private int m_FpsAccumulator = 0;
    //private float m_FpsNextPeriod = 0;
    //private int m_CurrentFps;
    //const string display = "{0} FPS";

    //private void Awake() {


    //    staff.SetActive(false);

    //    if(isBoy){
    //        cineCam.Follow = bPlayer.transform;
    //        cineCam.LookAt = bPlayer.transform;
    //        movementScript.anim = bAnimator;
    //        bPlayer.SetActive(true);
    //    }
    //    else{
    //        gPlayer.SetActive(false);
    //    }
    //}

    //private void Start()
    //{
    //    //set the framerate
    //    Application.targetFrameRate = 60;

    //    m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    //}

    //private void Update()
    //{
    //    m_FpsAccumulator++;
    //    if (Time.realtimeSinceStartup > m_FpsNextPeriod)
    //    {
    //        m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
    //        m_FpsAccumulator = 0;
    //        m_FpsNextPeriod += fpsMeasurePeriod;
    //        fpsCounter.text = string.Format(display, m_CurrentFps);
    //    }
    //}
}
