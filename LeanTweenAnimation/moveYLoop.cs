using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveYLoop : MonoBehaviour
{
    [Header("Lean Tween")]
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private LeanTweenType loopType;

    [Header("Rect Transform")]
    [SerializeField] private RectTransform selectIndicator;

    [Header("Value")]
    [SerializeField] private float speed;
    [SerializeField] private float moveY;

    Vector3 oldPos;
    int ltIndicator;
    BattleStates battleStates;

    private void Start()
    {
        oldPos = new Vector3(selectIndicator.localPosition.x,selectIndicator.localPosition.y,selectIndicator.localPosition.z);
    }

    public void playSelectIndicator()
    {
        selectIndicator.gameObject.SetActive(true);
        selectIndicator.localPosition = oldPos;
        ltIndicator = LeanTween.moveY(selectIndicator, moveY, speed).setEase(easeType).setLoopType(loopType).id;
    }

    public void stopLeanTweenIndicator()
    {
        LeanTween.cancel(ltIndicator);
        selectIndicator.gameObject.SetActive(false);
    }
}
