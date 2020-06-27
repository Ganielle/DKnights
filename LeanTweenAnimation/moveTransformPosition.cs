using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTransformPosition : MonoBehaviour
{
    [Header("Lean Tween ease type")]
    [SerializeField]private LeanTweenType easeType;

    [Header("Position X")]
    [HideInInspector]public float posX;
    
    [Header("Lean Tween Time")]
    [SerializeField]private float delayTime, showTime;

    LTDescr gameObjectPosition;

    Transform gameObjectTransform;

    private void Start()
    {
        gameObjectTransform = GetComponent<Transform>();
    }

    public void movePosition()
    {
        gameObjectPosition = LeanTween.moveX(this.gameObject, posX, showTime).setDelay(delayTime).setEase(easeType);
        gameObjectPosition.setOnComplete(onComplete);
    }

    private void onComplete()
    {
        gameObjectTransform.position = new Vector3(posX, gameObjectTransform.position.y, gameObjectTransform.position.z);
    }
}
