using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeInOutIndicator : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float fadeTime;

    [Header("GameObject")]
    [SerializeField] private GameObject IndicatorFoot,IndicatorHead;

    [Header("Lean Tween")]
    [SerializeField] private LeanTweenType easeType;

    LTDescr fadeInOne,fadeInTwo,fadeOutOne, fadeOutTwo;

    private void Start()
    {
        IndicatorFoot.gameObject.SetActive(false);
        IndicatorHead.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FadeIn(IndicatorFoot, IndicatorHead);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FadeOut(IndicatorFoot, IndicatorHead);
        }
    }

    private void FadeIn(GameObject image1, GameObject image2)
    {
        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(true);
        fadeInOne = LeanTween.alpha(image1.GetComponent<RectTransform>(), 1f, fadeTime).setEase(easeType);
        fadeInTwo = LeanTween.alpha(image2.GetComponent<RectTransform>(), 1f, fadeTime).setEase(easeType);
        fadeInOne.setOnComplete(stopFadeIn);
        fadeInTwo.setOnComplete(stopFadeIn);
    }

    private void stopFadeIn()
    {
        return;
    }

    private void FadeOut(GameObject image1, GameObject image2)
    {
        fadeOutOne = LeanTween.alpha(image1.GetComponent<RectTransform>(), 0f, fadeTime).setEase(easeType);
        fadeOutTwo = LeanTween.alpha(image2.GetComponent<RectTransform>(), 0f, fadeTime).setEase(easeType);
        fadeOutOne.setOnComplete(stopFadeOut);
        fadeOutTwo.setOnComplete(stopFadeOut);
    }

    private void stopFadeOut()
    {
        IndicatorFoot.gameObject.SetActive(false);
        IndicatorHead.gameObject.SetActive(false);
    }
}
