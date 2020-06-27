using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IgnoreFog : MonoBehaviour
{
    [SerializeField] private bool AllowFog = false;
    private bool FogOn;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += onPreRender;
        RenderPipelineManager.endCameraRendering += onPostRender;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= onPreRender;
        RenderPipelineManager.endCameraRendering -= onPostRender;
    }

    private void onPreRender(ScriptableRenderContext arg1, Camera arg2)
    {
        FogOn = RenderSettings.fog;
        RenderSettings.fog = AllowFog;
    }

    private void onPostRender(ScriptableRenderContext arg1, Camera arg2)
    {
        RenderSettings.fog = FogOn;
    }

}
