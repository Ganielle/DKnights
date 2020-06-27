using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenAnimation : MonoBehaviour
{
    [Header("Lean Tween")]
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private LeanTweenType loopType;

    [Header("Rect Transform")]
    [SerializeField] private RectTransform dKnightsLogo;

    [Header("Value")]
    [SerializeField] private float alphaSpeed;

    LTDescr dknightsIcon;

    private void Start()
    {
        DKnightsIconAnimate();
    }


    private void DKnightsIconAnimate()
    {
        dknightsIcon = LeanTween.alpha(dKnightsLogo, 0.2f, alphaSpeed).setEase(easeType).setLoopType(loopType);
    }
}
